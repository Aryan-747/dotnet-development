using System;
public class InvalidCreditDataException : Exception
{
    public InvalidCreditDataException(string message) : base(message)
    {
       
    }
}
class CreditRiskProcessor
{
    public static bool ValidateCustomerDetails(int age,string employmentType,double monthlyIncome,double dues,int creditScore,int defaults)
    {
        // Age Validation
        if (age < 21 || age > 65)
            throw new InvalidCreditDataException("Invalid age");

        // Employment Type Validation
        if (employmentType != "Salaried" && employmentType != "Self-Employed")
            throw new InvalidCreditDataException("Invalid employment type");

        // Monthly Income Validation
        if (monthlyIncome < 20000)
            throw new InvalidCreditDataException("Invalid monthly income");

        // Credit Dues Validation (more than equal to 0)
        if (dues < 0)
            throw new InvalidCreditDataException("Invalid credit dues");

        // Credit Score Validation, it should be in this range
        if (creditScore < 300 || creditScore > 900)
            throw new InvalidCreditDataException("Invalid credit score");

        // Default Count Validation, it should be 0 or more than 0
        if (defaults < 0)
            throw new InvalidCreditDataException("Invalid default count");

        return true;
    }

    public static double CalculateCreditLimit(double monthlyIncome,double dues,int creditScore,int defaults)
    {
        double debtRatio = dues / (monthlyIncome * 12);

        if (creditScore < 600 || defaults >= 3 || debtRatio > 0.4)
            return 50000;

        if (creditScore >= 750 && defaults == 0 && debtRatio < 0.25)
            return 300000;

        return 150000;
    }
}
public class UserInterface
{
    public static void Main(string[] args)
    {
        try
        {
            Console.Write("Enter customer name: ");
            string name = Console.ReadLine();

            Console.Write("Enter age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Enter employment type: ");
            string employmentType = Console.ReadLine();

            Console.Write("Enter monthly income: ");
            double monthlyIncome = double.Parse(Console.ReadLine());

            Console.Write("Enter existing credit dues: ");
            double dues = double.Parse(Console.ReadLine());

            Console.Write("Enter credit score: ");
            int creditScore = int.Parse(Console.ReadLine());

            Console.Write("Enter number of loan defaults: ");
            int defaults = int.Parse(Console.ReadLine());

            // Validate Details
            CreditRiskProcessor.ValidateCustomerDetails(age, employmentType, monthlyIncome,dues, creditScore, defaults);

            double creditLimit = CreditRiskProcessor.CalculateCreditLimit(monthlyIncome, dues, creditScore, defaults);

            Console.WriteLine($"Customer Name:{name} Approved Credit Limit: ₹{creditLimit}");
        }

        catch (InvalidCreditDataException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
