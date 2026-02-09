class BankAccount
{
    // Data Variables
    private double Balance;

    // Parameterized Constructor
    public BankAccount(double balance)
    {
        Balance = balance;
    }

    // Methods

    public void Deposit(double amount)
    {
        if(amount<=0)
        {
            Console.WriteLine("Amount Should Be Greater Than 0.");
        }
        else
        {
            Balance += amount;
        }
    }

    public void Withdraw(double amount)
    {
        if(amount>0 && amount<=Balance)
        {
            Balance -= amount;
        }
        else
        {
            Console.WriteLine("Amount Should Be Greater Than 0 & Amount<=Balance");
        }
    }

    public void DisplayBalance()
    {
        Console.WriteLine($"Balance: {Balance}");
    }
}

class Program
{
    public static void Main(string[] args)
    {
        BankAccount b1 = new BankAccount(555.50);
        b1.DisplayBalance();
        b1.Deposit(0);
        b1.DisplayBalance();
        b1.Deposit(450);
        b1.DisplayBalance();
        b1.Withdraw(10000);
        b1.DisplayBalance();
        b1.Withdraw(300);
        b1.DisplayBalance();
    }

}