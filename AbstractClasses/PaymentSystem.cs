using System;

// We cannot create instances of abstract classes!!!

abstract class Payment
{
    public decimal Amount { get; }
    protected Payment(decimal amount) => Amount = amount;

    // function to print receipt amount
    public void PrintReceipt()
    {
        Console.WriteLine($"Receipt: Amount={Amount}");
    }

    public abstract void Pay(); // must be implemented in all child classes
}

class UpiPayment : Payment
{
    public string UpiId { get; }
    public UpiPayment(decimal amount, string upiId) : base(amount) => UpiId = upiId;

    // overriding the abstract class method.
    public override void Pay()
    {
        Console.WriteLine($"Paid {Amount} via UPI ({UpiId}).");
    }
}

class CashPayment : Payment
{
    // defining constructor for cash payments
    public string CashDenomition { get; }
    public CashPayment(decimal amount, string denomition) : base(amount)
    {
        CashDenomition = denomition;
    }

    // overriding the abstract class method
    public override void Pay()
    {
        Console.WriteLine($"Amount: {Amount} paid using cash denomition of {CashDenomition}.");
    }
}

class PaymentSystem
{
    public static void Main(string[] args)
    {
        Payment p1 = new UpiPayment(599.99m,"aryan@ptsbi");
        Payment p2 = new CashPayment(900,"2 Notes of 500");

        p1.PrintReceipt();
        p1.Pay();

        p2.PrintReceipt();
        p2.Pay();
    }
}