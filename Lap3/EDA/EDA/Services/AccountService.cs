using System;
using System.Collections.Generic;
using EDA.Events;

namespace EDA.Services
{
    public class AccountService
    {
        private readonly EventBus _bus;
        private readonly Dictionary<string, double> _accounts = new();

        public AccountService(EventBus eventBus)
        {
            _bus = eventBus;
        }

        public void CreateAccount(string accountId, string owner)
        {
            if (_accounts.ContainsKey(accountId))
            {
                Console.WriteLine($"[AccountService] Account {accountId} already exists.");
                return;
            }
            _accounts[accountId] = 0;
            Console.WriteLine($"{DateTime.Now} [AccountService] Created account {accountId} for {owner}");
            _bus.Publish(new AccountCreatedEvent(accountId, owner));
        }

        public void Deposit(string accountId, double money)
        {
            if (!_accounts.ContainsKey(accountId))
            {
                Console.WriteLine($"[AccountService] Account {accountId} does not exist.");
                return;
            }
            _accounts[accountId] += money;
            Console.WriteLine($"{DateTime.Now} [AccountService] Account deposited {money} to {accountId}");
            _bus.Publish(new MoneyDepositedEvent(accountId, money));
        }

        public void Withdraw(string accountId, double money)
        {
            if (!_accounts.ContainsKey(accountId))
            {
                Console.WriteLine($"[AccountService] Account {accountId} does not exist.");
                return;
            }
            if (_accounts[accountId] >= money)
            {
                _accounts[accountId] -= money;
                Console.WriteLine($"{DateTime.Now} [AccountService] Account withdrawn {money} from {accountId}");
                _bus.Publish(new MoneyWithdrawEvent(accountId, money));
            }
            else
            {
                Console.WriteLine("[AccountService] Error: Số dư không đủ");
            }
        }

        public void Transfer(string fromAccountId, string toAccountId, double amount)
        {
            if (!_accounts.ContainsKey(fromAccountId) || !_accounts.ContainsKey(toAccountId))
            {
                Console.WriteLine("[AccountService] One or both accounts do not exist.");
                return;
            }
            if (_accounts[fromAccountId] < amount)
            {
                Console.WriteLine("[AccountService] Error: Số dư không đủ để chuyển.");
                return;
            }
            _accounts[fromAccountId] -= amount;
            _accounts[toAccountId] += amount;
            Console.WriteLine($"{DateTime.Now} [AccountService] Transferred {amount} from {fromAccountId} to {toAccountId}");
            _bus.Publish(new TransferMoneyEvent
            {
                FromAccount = fromAccountId,
                ToAccount = toAccountId,
                Amount = amount
            });
        }
    }
}
