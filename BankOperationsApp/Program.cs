using Bank;
using System;

namespace BankOperationsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank<Account> bank = new Bank<Account>("RoyalBank");

            Console.WriteLine("Enter Y to continue, or N to close");
            while (Console.ReadLine().ToLower() == "y")
            {
                ConsoleColor color = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("1 Open account\n" +
                                  "2 Withdraw\n" +
                                  "3 Deposite\n" +
                                  "4 Close account\n");
                Console.BackgroundColor = color;

                try
                {
                    int operation = Convert.ToInt32(Console.ReadLine());

                    switch (operation)
                    {
                        case 1: OpenAccount(bank); break;
                        case 2: Withdraw(bank); break;
                        case 3: Deposit(bank); break;
                        case 4: CloseAccount(bank); break;
                    }
                }
                catch (Exception ex)
                {
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
                Console.WriteLine("Enter Y to continue, or N to close");
            }
        }
        private static void OpenAccount(Bank<Account> bank)
        {
            Console.WriteLine("Enter initial sum");
            decimal sum = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Select 1 for Demand account or 2 for Deposit account.");
            int type = Convert.ToInt32(Console.ReadLine());

            AccountType account;
            account = type switch
            {
                1 => AccountType.Demand,
                2 => AccountType.Deposit,
                _ => throw new Exception("There is no such type.")
            };

            bank.Open(account,
                      sum,
                      openHandler: (obj, args) => Console.WriteLine(args.Message),
                      depositHandler: (obj, args) => Console.WriteLine(args.Message),
                      withdrawHandler: (obj, args) => Console.WriteLine(args.Message),
                      calculateHandler: (obj, args) => Console.WriteLine(args.Message),
                      closeHandler: (obj, args) => Console.WriteLine(args.Message));

        }
        private static void Deposit(Bank<Account> bank)
        {
            Console.WriteLine("Enter account id");
            Guid id = Guid.Parse(Console.ReadLine());
            Console.WriteLine("Enter sum");
            decimal sum = Convert.ToDecimal(Console.ReadLine());

            bank.Deposit(sum, id);
        }
        private static void Withdraw(Bank<Account> bank)
        {
            Console.WriteLine("Enter account id");
            Guid id = Guid.Parse(Console.ReadLine());
            Console.WriteLine("Enter sum");
            decimal sum = Convert.ToDecimal(Console.ReadLine());

            bank.Withdraw(sum, id);
        }

        private static void CloseAccount(Bank<Account> bank)
        {
            Console.WriteLine("Enter account id");
            Guid id = Guid.Parse(Console.ReadLine());

            bank.Close(id);
        }
    }
}
