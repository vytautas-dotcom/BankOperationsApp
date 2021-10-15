using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public delegate void AccountStateHandler(object sender, AccountEventArgs args);
    public class AccountEventArgs
    {
        public string Message { get; set; }
        public decimal Sum { get; set; }

        public AccountEventArgs(string message, decimal sum)
        {
            Message = message;
            Sum = sum;
        }
    }
}
