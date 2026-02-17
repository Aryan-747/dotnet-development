using System;

class BankAccount
{
    static void Main()
    {
        int balance = 10000;

        try
        {
            Console.WriteLine("Enter withdrawal amount:");
            int amount = int.Parse(Console.ReadLine());

            // 1. Throw exception if amount <= 0
            if (amount <= 0)
            {
                throw new ArgumentException("Amount Should Be More Than 0.");
            }

            // 2. Throw exception if amount > balance
            if (amount > balance)
            {
                throw new InvalidOperationException("Insufficient balance.");
            }

            // 3. Deduct amount if valid
            balance -= amount;
            Console.WriteLine("Withdrawal successful.");
            Console.WriteLine("Remaining Balance: " + balance);
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input. Please enter a numeric value.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            // 4. Log transaction
            Console.WriteLine("Transaction attempt completed.");
        }
    }
}
