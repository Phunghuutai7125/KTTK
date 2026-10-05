using System;
using EDA.Events;

namespace EDA.Services
{
    public class AnalyticsService
    {
        public AnalyticsService(EventBus eventBus)
        {
            eventBus.Subscribe<AccountCreatedEvent>(OnAccountCreated);
            eventBus.Subscribe<MoneyDepositedEvent>(OnMoneyDeposited);
            eventBus.Subscribe<MoneyWithdrawEvent>(OnMoneyWithdrawn);
            eventBus.Subscribe<TransferMoneyEvent>(OnTransferMoney);
        }

        private void OnAccountCreated(AccountCreatedEvent evt)
        {
            Console.WriteLine($"[Analytics] Account created: {evt.AccountId} for {evt.Owner}");
        }

        private void OnMoneyDeposited(MoneyDepositedEvent evt)
        {
            Console.WriteLine($"[Analytics] Money deposited: {evt.Amount} to {evt.AccountId}");
        }

        private void OnMoneyWithdrawn(MoneyWithdrawEvent evt)
        {
            Console.WriteLine($"[Analytics] Money withdrawn: {evt.Amount} from {evt.AccountId}");
        }

        private void OnTransferMoney(TransferMoneyEvent evt)
        {
            Console.WriteLine($"[Analytics] Money transferred: {evt.Amount} from {evt.FromAccount} to {evt.ToAccount}");
        }
    }
}
