using System;
using System.Runtime.CompilerServices;

class Program
{
    static void Main()
    {
        //Task1();

        //Task2();

        Task3();

    }

    public enum AccountType
    {
        Savings,
        Checking,
        Current
    }

    public struct BankAccount
    {
        public AccountType Type;
        public double Balance;
        public int AccountNumber;
    }

    private static void Task3()
    {
        BankAccount[] accounts = new BankAccount[5];
        int count = 0;

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Menu: ");
            Console.WriteLine("1. Add a new account");
            Console.WriteLine("2. Display all accounts");
            Console.WriteLine("3. Exit");
            Console.WriteLine("Enter your choice: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddAccount(accounts, count++);
                    break;
                case 2:
                    DisplayAccounts(accounts);
                    break;
                case 3:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static void DisplayAccounts(BankAccount[] accounts)
    {
        Console.WriteLine("All accounts:");
        foreach (var account in accounts)
        {
            Console.WriteLine($"Account Number: {account.AccountNumber} " +
                $" Balance : {account.Balance}" +
                $" Type : {account.Type}");
        }
    }

    private static void AddAccount(BankAccount[] accounts, int count)
    {

        if (count > accounts.Length)
        {
            Console.WriteLine("Cannot add more accounts.");
            return;
        }

        BankAccount newAccount = new BankAccount();

        Console.WriteLine("Enter account number : ");
        string input = Console.ReadLine();
        newAccount.AccountNumber = int.Parse(input);

        Console.WriteLine("Enter balance : ");
        input = Console.ReadLine();
        newAccount.Balance = double.Parse(input);

        Console.WriteLine("Choose account type: ");
        Console.WriteLine("1. Savings");
        Console.WriteLine("2. Checking");
        Console.WriteLine("3. Current");
        Console.WriteLine("Enter your choice: ");

        int typeChoice = int.Parse(Console.ReadLine());

        switch (typeChoice)
        {
            case 1:
                newAccount.Type = AccountType.Savings;
                break;
            case 2:
                newAccount.Type = AccountType.Checking;
                break;
            case 3:
                newAccount.Type = AccountType.Current;
                break;
            default:
                Console.WriteLine("Invalid choice. Saving account will be choosen by default.");
                newAccount.Type = AccountType.Savings;
                break;
        }

        accounts[count] = newAccount;

    }

    private static void Task2()
    {
        try
        {
            int intValue = 10;
            long longValue = long.MaxValue;

            checked
            {
                intValue = (int)longValue;
            }

            Console.WriteLine("intValue: " + intValue);
        }
        catch (OverflowException)
        {
            Console.WriteLine("Overflow error, this value cannot be assigned to int.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error : " + ex.ToString());
        }
    }

    private static void Task1()
    {
        try
        {
            Console.WriteLine("Enter first number : ");
            string input1 = Console.ReadLine();

            Console.WriteLine("Enter second number : ");
            string input2 = Console.ReadLine();

            int number1 = Convert.ToInt32(input1);
            int number2 = Convert.ToInt32(input2);

            if (number2 == 0)
            {
                Console.WriteLine("Dividing by zero is not allowed.");
                return;
            }

            double result = (double)number1 / number2;

            Console.WriteLine("Result : ");

            Console.WriteLine("Currency format : {0:C}", result);
            Console.WriteLine("Integer format : {0}", (int)result);
            Console.WriteLine("Scientific format : {0:E}", result);
            Console.WriteLine("Fixed-point format : {0:F}", result);
            Console.WriteLine("General format : {0:G}", result);
            Console.WriteLine("Number format : {0:N}", result);
            Console.WriteLine("Hexadecimal format : {0:X}", (int)result);


        }
        catch (FormatException)
        {
            Console.WriteLine("Wrong format input. Please enter integer.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Entere number is out of scope.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occured.");
        }
    }
}