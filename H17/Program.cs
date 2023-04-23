using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H17
{
    internal class Program
    {
        private static readonly Bank SystemBank = new();

        private static void Main()
        {
            Console.Clear();
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Bank Controls: ");
                Console.WriteLine("\t1. Create Account");
                Console.WriteLine("\t2. Deposit");
                Console.WriteLine("\t3. Withdraw");
                Console.WriteLine("\t4. View Balance");
                Console.WriteLine("\t5. Toggle Account Lock");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.Write("Menu Action: ");
                var input = Console.ReadLine();

                if (!int.TryParse(input, out var menu_input))
                {
                    Console.Clear();
                    continue;
                }

                Console.WriteLine("");
                switch (menu_input)
                {
                    case 1:
                        CreateAccount();
                        break;
                    case 2:
                        DepositBalance();
                        break;
                    case 3:
                        WithdrawBalance();
                        break;
                    case 4:
                        ViewAccountBalance();
                        break;
                    case 5:
                        ToggleAccountLock();
                        break;
                }

                Console.WriteLine();
                Console.WriteLine();
            } while (true);
            // ReSharper disable once FunctionNeverReturns
        }

        private static void ToggleAccountLock()
        {
            Console.Write("Account Number: ");
            var account_input = Console.ReadLine();

            if (!int.TryParse(account_input, out var account_number))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR OCCURRED: Cannot parse account number!");
                return;
            }

            var account = SystemBank.GetAccount(account_number);
            if (account == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR OCCURRED: An account with this number does not exist!");
                return;
            }

            account.ToggleLock();
            Console.WriteLine("Toggled Account Lock to " + account.IsDisabled);
        }

        private static void CreateAccount()
        {
            Console.Write("Account Holder: ");
            var account_holder = Console.ReadLine();

            Console.Write("Account Default Balance: ");
            var account_default_bal_input = Console.ReadLine();

            if (!decimal.TryParse(account_default_bal_input, out var account_default_bal) ||
                string.IsNullOrWhiteSpace(account_default_bal_input))
                account_default_bal = 0;

            SystemBank.CreateNewAccount(account_holder, account_default_bal);

            Console.WriteLine("Created new account!");
        }

        private static void DepositBalance()
        {
            Console.Write("Account Number: ");
            var account_input = Console.ReadLine();

            if (!int.TryParse(account_input, out var account_number))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR OCCURRED: Cannot parse account number!");
                return;
            }

            Console.Write("Deposit Amount: ");
            var account_deposit_input = Console.ReadLine();

            if (!decimal.TryParse(account_deposit_input, out var account_deposit))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR OCCURRED: Cannot parse deposit amount!");
                return;
            }

            SystemBank.Deposit(account_number, account_deposit);
            Console.WriteLine("Deposited £" + account_deposit + " into " + account_number);
        }

        private static void WithdrawBalance()
        {
            Console.Write("Account Number: ");
            var account_input = Console.ReadLine();

            if (!int.TryParse(account_input, out var account_number))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR OCCURRED: Cannot parse account number!");
                return;
            }

            Console.Write("Withdraw Amount: ");
            var account_withdraw_input = Console.ReadLine();

            if (!decimal.TryParse(account_withdraw_input, out var account_withdraw))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR OCCURRED: Cannot parse withdraw amount!");
                return;
            }

            if (SystemBank.Withdraw(account_number, account_withdraw))
            {
                Console.WriteLine("Withdrawn " + account_withdraw + " from account " + account_number);
                return;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR OCCURRED: Could not withdraw this amount from this account!");
        }

        private static void ViewAccountBalance()
        {
            Console.Write("Account Number: ");
            var account_input = Console.ReadLine();

            if (!int.TryParse(account_input, out var account_number))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR OCCURRED: Cannot parse account number!");
                return;
            }

            var account = SystemBank.GetAccount(account_number);
            if (account != null)
            {
                if (account.IsDisabled)
                {
                    Console.WriteLine("ERROR OCCURRED: This account is Locked!");
                    return;
                }

                Console.WriteLine("Account Balance: " + account.GetBalance());
                return;
            }

            Console.WriteLine("ERROR OCCURRED: This account does not exist!");
        }
    }
}
