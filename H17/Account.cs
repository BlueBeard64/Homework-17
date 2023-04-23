using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H17
{
    internal class Account
    {
        public int AccountNumber { get; }
        public string AccountHolder { get; }
        private decimal Balance { get; set; }

        public bool IsDisabled { get; private set; }

        public Account(int account_number, string account_holder, decimal balance = 0)
        {
            AccountNumber = account_number;
            AccountHolder = account_holder;
            Balance = balance;

            IsDisabled = false;
        }

        public void Deposit(int amount) => 
            Balance += amount;

        public bool Withdraw(int amount)
        {
            if (Balance - amount < 0) return false;

            Balance -= amount;
            return true;
        }

        public decimal GetBalance() => 
            Balance;

        public void ToggleLock() =>
            IsDisabled = !IsDisabled;
    }
}
