using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class DemandAccount : Account
    {
        public DemandAccount(decimal sum, 
                             int percent,
                             DateTime date) : base(sum, percent, date)
        { }

        protected override void OnOpened(AccountEventArgs e)
        {
            base.OnOpened(new AccountEventArgs($"New Demand Account {this.Id} is opened", this.Sum));
        }
    }
}
