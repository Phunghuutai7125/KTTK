using EDA.Events;

namespace EDA.Services
{
    public class FraudDetectionService
    {
        public FraudDetectionService(EventBus eventBus)
        {
            eventBus.Subscribe<MoneyWithdrawEvent>(HandleMoneyWithdrawn);
        }

        private void HandleMoneyWithdrawn(MoneyWithdrawEvent evt)
        {
            if (evt.Amount > 10000)
            {
                Console.WriteLine($"[FRAUD ALERT] Large withdrawal detected: {evt.Amount} from {evt.AccountId}");
            }
        }
    }
}
