using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Events
{
    public class MoneyDepositedEvent :IEvent
    {
        public string AccountId { get; set; }
        public double Amount { get; set; }
        public MoneyDepositedEvent(string accountId, double amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}
