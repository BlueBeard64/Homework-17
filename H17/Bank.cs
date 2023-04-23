using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace H17
{
    internal class Bank
    {
        private readonly List<Account> _accounts = new();

        public Bank()
        {
        }

        public void CreateNewAccount(string account_holder, decimal initial_balance = 0)
        {
            var new_account = new Account(_accounts.Count + 1, account_holder, initial_balance);

            _accounts.Add(new_account);
        }

        public void Deposit(int account, decimal amount)
        {
            if (!_accounts.Exists(x => x.AccountNumber == account)) return;

            var acc = _accounts.First(x => x.AccountNumber == account);

            if (acc.IsDisabled) return;

            acc.Deposit((int)amount);
        }

        public bool Withdraw(int account, decimal amount)
        {
            if (!_accounts.Exists(x => x.AccountNumber == account)) return false;

            var acc = _accounts.First(x => x.AccountNumber == account);

            return !acc.IsDisabled && acc.Withdraw((int)amount);
        }

        public decimal ViewBalance(int account)
        {
            if (!_accounts.Exists(x => x.AccountNumber == account)) return -1;

            var acc = _accounts.First(x => x.AccountNumber == account);

            if (acc.IsDisabled) return -1;

            return acc.GetBalance();
        }

        public Account GetAccount(int account) => 
            !_accounts.Exists(x => x.AccountNumber == account) ? null : _accounts.First(x => x.AccountNumber == account);
    }
}
