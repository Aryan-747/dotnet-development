using System;
using System.Collections.Generic;
using System.Linq;

#region Models

public class Transaction
{
    public string TransactionId { get; set; }
    public string CardNumber { get; set; }
    public decimal Amount { get; set; }
    public string City { get; set; }
    public DateTime Timestamp { get; set; }
}

public class FraudAlert
{
    public string CardNumber { get; set; }
    public string RuleTriggered { get; set; }
    public List<string> TransactionIds { get; set; }
}

#endregion

public class FraudDetector
{
    private static readonly TimeSpan HighValueWindow = TimeSpan.FromMinutes(2);
    private static readonly TimeSpan CityWindow = TimeSpan.FromMinutes(10);
    private const decimal HighValueAmount = 50000;

    public List<FraudAlert> DetectFraud(List<Transaction> txns)
    {
        var alerts = new List<FraudAlert>();

        // Group by card for window-based analysis
        var txnsByCard = txns
            .OrderBy(t => t.Timestamp) // deterministic
            .GroupBy(t => t.CardNumber);

        foreach (var cardGroup in txnsByCard)
        {
            var cardTxns = cardGroup.ToList();

            DetectHighValueBurst(cardGroup.Key, cardTxns, alerts);
            DetectMultiCityUsage(cardGroup.Key, cardTxns, alerts);
        }

        return alerts;
    }

    // ---------------- Rule 1 ----------------
    private void DetectHighValueBurst(
        string cardNumber,
        List<Transaction> txns,
        List<FraudAlert> alerts)
    {
        for (int i = 0; i < txns.Count; i++)
        {
            var window = txns
                .Where(t =>
                    t.Amount > HighValueAmount &&
                    t.Timestamp >= txns[i].Timestamp &&
                    t.Timestamp <= txns[i].Timestamp + HighValueWindow)
                .ToList();

            if (window.Count >= 3)
            {
                alerts.Add(new FraudAlert
                {
                    CardNumber = cardNumber,
                    RuleTriggered = "3+ high-value transactions within 2 minutes",
                    TransactionIds = window.Select(t => t.TransactionId).ToList()
                });

                break; // avoid duplicate alerts per card
            }
        }
    }

    // ---------------- Rule 2 ----------------
    private void DetectMultiCityUsage(
        string cardNumber,
        List<Transaction> txns,
        List<FraudAlert> alerts)
    {
        for (int i = 0; i < txns.Count; i++)
        {
            for (int j = i + 1; j < txns.Count; j++)
            {
                if (txns[j].Timestamp - txns[i].Timestamp > CityWindow)
                    break;

                if (!txns[i].City.Equals(txns[j].City, StringComparison.OrdinalIgnoreCase))
                {
                    alerts.Add(new FraudAlert
                    {
                        CardNumber = cardNumber,
                        RuleTriggered = "Same card used in different cities within 10 minutes",
                        TransactionIds = new List<string>
                        {
                            txns[i].TransactionId,
                            txns[j].TransactionId
                        }
                    });

                    return; // one alert per card
                }
            }
        }
    }
}

#region Demo

class Program
{
    static void Main()
    {
        var transactions = new List<Transaction>
        {
            new Transaction { TransactionId="T1", CardNumber="C1", Amount=60000, City="Delhi", Timestamp=DateTime.Now },
            new Transaction { TransactionId="T2", CardNumber="C1", Amount=70000, City="Delhi", Timestamp=DateTime.Now.AddSeconds(30) },
            new Transaction { TransactionId="T3", CardNumber="C1", Amount=80000, City="Delhi", Timestamp=DateTime.Now.AddSeconds(60) },

            new Transaction { TransactionId="T4", CardNumber="C2", Amount=1000, City="Mumbai", Timestamp=DateTime.Now },
            new Transaction { TransactionId="T5", CardNumber="C2", Amount=1200, City="Bangalore", Timestamp=DateTime.Now.AddMinutes(5) }
        };

        var detector = new FraudDetector();
        var alerts = detector.DetectFraud(transactions);

        foreach (var alert in alerts)
        {
            Console.WriteLine($"Fraud Alert for Card: {alert.CardNumber}");
            Console.WriteLine($"Rule: {alert.RuleTriggered}");
            Console.WriteLine($"Transactions: {string.Join(", ", alert.TransactionIds)}");
            Console.WriteLine();
        }
    }
}

#endregion
