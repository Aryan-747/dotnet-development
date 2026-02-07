using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#region Models
public class PaymentRequest
{
    public string PaymentId { get; set; }
    public decimal Amount { get; set; }
}

public class PaymentResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
}

#endregion

#region Exceptions

// Represents transient failures like timeouts
public class TransientPaymentException : Exception
{
    public TransientPaymentException(string message) : base(message) { }
}

#endregion

#region Payment Gateway

public class ResilientPaymentGateway
{
    private const int MaxRetries = 3;
    private static readonly TimeSpan FailureWindow = TimeSpan.FromMinutes(1);
    private static readonly TimeSpan CircuitOpenDuration = TimeSpan.FromSeconds(30);

    private readonly List<DateTime> failureTimestamps = new List<DateTime>();
    private DateTime? circuitOpenedAt = null;
    private readonly object lockObj = new object();

    public async Task<PaymentResult> ProcessPaymentAsync(
        PaymentRequest request,
        CancellationToken cancellationToken)
    {
        // ----- Circuit Breaker: Fail Fast -----
        lock (lockObj)
        {
            if (IsCircuitOpen())
            {
                return new PaymentResult
                {
                    Success = false,
                    Message = "Circuit is open. Please try later."
                };
            }
        }

        int attempt = 0;

        while (attempt < MaxRetries)
        {
            cancellationToken.ThrowIfCancellationRequested();
            attempt++;

            try
            {
                // Simulated external gateway call
                await CallExternalGatewayAsync(request, cancellationToken);

                return new PaymentResult
                {
                    Success = true,
                    Message = "Payment processed successfully"
                };
            }
            catch (TransientPaymentException)
            {
                RegisterFailure();

                if (attempt == MaxRetries)
                {
                    return new PaymentResult
                    {
                        Success = false,
                        Message = "Payment failed after retries"
                    };
                }

                // Small backoff before retry
                await Task.Delay(200, cancellationToken);
            }
        }

        return new PaymentResult
        {
            Success = false,
            Message = "Unexpected failure"
        };
    }

    // ----------------- Circuit Breaker Logic -----------------

    private bool IsCircuitOpen()
    {
        if (circuitOpenedAt == null)
            return false;

        if (DateTime.UtcNow - circuitOpenedAt < CircuitOpenDuration)
            return true;

        // Half-open: reset circuit
        circuitOpenedAt = null;
        failureTimestamps.Clear();
        return false;
    }

    private void RegisterFailure()
    {
        lock (lockObj)
        {
            DateTime now = DateTime.UtcNow;

            failureTimestamps.Add(now);

            // Remove old failures
            failureTimestamps.RemoveAll(
                t => now - t > FailureWindow
            );

            if (failureTimestamps.Count >= 5)
            {
                circuitOpenedAt = now;
            }
        }
    }

    // ----------------- Simulated Gateway -----------------

    private async Task CallExternalGatewayAsync(
        PaymentRequest request,
        CancellationToken cancellationToken)
    {
        await Task.Delay(300, cancellationToken);

        // Simulate timeout failure
        throw new TransientPaymentException("Gateway timeout");
    }
}

#endregion

#region Demo

class Program
{
    static async Task Main()
    {
        var gateway = new ResilientPaymentGateway();
        var request = new PaymentRequest { PaymentId = "P123", Amount = 500 };

        using var cts = new CancellationTokenSource();

        for (int i = 1; i <= 7; i++)
        {
            var result = await gateway.ProcessPaymentAsync(request, cts.Token);
            Console.WriteLine($"Attempt {i}: {result.Message}");
        }
    }
}

#endregion
