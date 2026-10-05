namespace EDA.Events
{
    public class MoneyWithdrawEvent : IEvent
    {
        public string AccountId { get; set; }
        public double Amount { get; set; }

        // Thêm constructor không tham số
        public MoneyWithdrawEvent() { }

        public MoneyWithdrawEvent(string accountId, double amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}
