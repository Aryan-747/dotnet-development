using System.Security.Cryptography.X509Certificates;

// Base EcommerceShop Class
class EcommerceShop
{
    public string UserName {get; set;}
    public double WalletBalance {get; set;}
    public double TotalPurchaseAmount {get; set;}
}

// Custom Exception Class
public class InsufficientWalletBalanceException : Exception
{   
    public InsufficientWalletBalanceException(string message) : base(message) {}
}

class EcommerceProgram
{
    public EcommerceShop MakePayment(string name, double balance, double amount)
    {
        if(balance<amount)
        {
            throw new InsufficientWalletBalanceException("Insufficient balance in your digital wallet");
        }

        EcommerceShop e = new EcommerceShop
        {
            UserName = name,
            WalletBalance = balance-amount, // Remaining Balance
            TotalPurchaseAmount = amount 
        };

        return e; 
    }
    public static void Main(string[] args)
    {
        EcommerceProgram p1 = new EcommerceProgram();

        Console.Write("Enter user name: ");
        string username = Console.ReadLine();

        Console.Write("Enter Wallet Balance: ");
        double balance = double.Parse(Console.ReadLine());

        Console.Write("Enter Purchase Amount: ");
        double amount = double.Parse(Console.ReadLine());

        try
        {
            EcommerceShop shop = p1.MakePayment(username,balance,amount);
            Console.WriteLine("Payment Successfull!");
        }

        catch(InsufficientWalletBalanceException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }    
}