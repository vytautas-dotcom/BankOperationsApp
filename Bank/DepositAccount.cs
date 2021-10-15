using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class DepositAccount : Account
    {
        public DepositAccount(decimal sum,
                             int percent,
                             DateTime date) : base(sum, percent, date)
        { }

        protected override void OnOpened(AccountEventArgs e)
        {
            base.OnOpened(new AccountEventArgs($"New Deposit Account {this.Id} is opened", this.Sum));
        }

        protected override void OnDeposited(AccountEventArgs e)
        {
            if (Convert.ToInt32(DateTime.Now - time) % 30 == 0)
            {
                base.Deposit(Sum);
            }
            else
            {
                OnOpened(new AccountEventArgs($"Only after 30 days is possible to make new deposit to account --{this.Id}--", this.Sum));
            }
        }
        protected override void OnWithdrawed(AccountEventArgs e)
        {
            if (Convert.ToInt32(DateTime.Now - time) % 30 == 0)
            {
                base.Withdraw(Sum);
            }
            else
            {
                OnOpened(new AccountEventArgs($"Only after 30 days is possible to withdraw from account --{this.Id}--", this.Sum));
            }
        }
        protected override void OnCalculated(AccountEventArgs e)
        {
            if (Convert.ToInt32(DateTime.Now - time) % 30 == 0)
            {
                base.Calculate();
            }
        }
    }
}
