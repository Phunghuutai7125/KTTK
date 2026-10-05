namespace EDA.Services
{
    internal class MoneyWithdrawnEvent
    {
        private string accountId;
        private double money;

        public MoneyWithdrawnEvent(string accountId, double money)
        {
            this.accountId = accountId;
            this.money = money;
        }
    }
}