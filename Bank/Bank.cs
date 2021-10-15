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
        T[] accounts;

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
                
        }
    }
}
