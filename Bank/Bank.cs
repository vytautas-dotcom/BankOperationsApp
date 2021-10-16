using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public enum AccountType
    {
        Demand,
        Deposit
    }
    public class Bank<T> where T : Account
    {
        List<T> accounts;

        public string Name { get; private set; }
        public Bank(string name)
        {
            Name = name;
        }

        public void Open(AccountType accountType,
                         decimal sum,
                         AccountStateHandler openHandler,
                         AccountStateHandler depositHandler,
                         AccountStateHandler withdrawHandler,
                         AccountStateHandler calculateHandler,
                         AccountStateHandler closeHandler)
        {
            T newAccount = default;

            newAccount = accountType switch
            {
                AccountType.Deposit => new DepositAccount(sum, 5, new DateTime(2021, 01, 01)) as T,
                AccountType.Demand => new DemandAccount(sum, 2, new DateTime(2021, 01, 01)) as T,
                _ => throw new ArgumentException("Something went wrong!")
            };

            if (newAccount == null) 
                throw new Exception("An error occurred while creating the account.");

            if (accounts == null) 
                accounts = new List<T> { newAccount };
            else
                accounts.Add(newAccount);

            newAccount.AccountOpened += openHandler;
            newAccount.Deposited += depositHandler;
            newAccount.Withdrawed += withdrawHandler;
            newAccount.CalculatedInterest += calculateHandler;
            newAccount.AccountClosed += closeHandler;
            
            newAccount.Open();
        }

        public void Deposit(decimal sum, Guid id)
        {
            T account = (from a in accounts where a.Id == id select a) as T;
            if (account == null)
                throw new Exception("Bad Id");

            account.Deposit(sum);
        }

        public void Withdraw(decimal sum, Guid id)
        {
            T account = (from a in accounts where a.Id == id select a) as T;
            if (account == null)
                throw new Exception("Bad Id");

            account.Withdraw(sum);
        }

        public void Calculate()
        {
            if (accounts.Count == 0) return;

            foreach (var account in accounts)
            {
                account.Calculate();
            }
        }

        public void Close(Guid id)
        {
            T account = (from a in accounts where a.Id == id select a) as T;

            account?.Close();

            accounts.Remove(account);
        }
    }
}
