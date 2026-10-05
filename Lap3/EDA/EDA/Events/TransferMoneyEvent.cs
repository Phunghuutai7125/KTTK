namespace EDA.Events
{
    public class TransferMoneyEvent : IEvent
    {
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public double  Amount { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
