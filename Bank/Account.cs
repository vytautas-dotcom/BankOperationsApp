using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public abstract class  Account : IAccount
    {
        protected internal event AccountStateHandler Withdrawed;
        protected internal event AccountStateHandler Deposited;
        protected internal event AccountStateHandler AccountOpened;
        protected internal event AccountStateHandler AccountClosed;
        protected internal event AccountStateHandler CalculatedInterest;

        
        protected DateTime time;
        public decimal Sum { get; private set; }
        public int InterestRate { get; private set; }
        public Guid Id { get; private set; }

        public Account(decimal sum, int interestRate, DateTime date)
        {
            Sum = sum;
            InterestRate = interestRate;
            Id = Guid.NewGuid();
            time = date;
        }

        private void EventCaller(AccountEventArgs e, AccountStateHandler handler)
        {
            if(e != null) handler?.Invoke(this, e);
        }

        protected virtual void OnOpened(AccountEventArgs e) => EventCaller(e, AccountOpened);
        protected virtual void OnDeposited(AccountEventArgs e) => EventCaller(e, Deposited);
        protected virtual void OnCalculated(AccountEventArgs e) => EventCaller(e, CalculatedInterest);
        protected virtual void OnWithdrawed(AccountEventArgs e) => EventCaller(e, Withdrawed);
        protected virtual void OnClosed(AccountEventArgs e) => EventCaller(e, AccountClosed);


        protected internal virtual void Open() => OnOpened(new AccountEventArgs($"New account --{Id}-- created at {time}", Sum));
        public virtual void Deposit(decimal sum) => OnDeposited(new AccountEventArgs($"Deposited sum {sum}, Total: {sum + Sum}", sum));
        public virtual void Withdraw(decimal sum)
        {
            if (sum < Sum) OnWithdrawed(new AccountEventArgs($"Withdrawed sum {sum}, Total: {Sum - sum}", sum));
            else if (sum == Sum) OnWithdrawed(new AccountEventArgs($"Withdrawed sum {sum}, Congratulations you are out of money!", sum));
            else OnWithdrawed(new AccountEventArgs($"Your wishes are greater than your account. You need {Sum - sum} more.", sum));
        }
        protected internal virtual void Calculate()
        {
            decimal increment = Sum * (decimal)Math.Pow((1 + InterestRate), Convert.ToDouble(time.Subtract(DateTime.Now)));
            Sum += increment;
            OnCalculated(new AccountEventArgs($"Calculated sum per period {Convert.ToDouble(time.Subtract(DateTime.Now))} is {increment}", Sum));
        }
        protected internal virtual void Close() => OnClosed(new AccountEventArgs($"Account {Id} is closed. Account balance {Sum}", Sum));
    }
}
