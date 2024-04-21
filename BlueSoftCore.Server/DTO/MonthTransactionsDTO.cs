using BlueSoftCore.Server.Models;

namespace BlueSoftCore.Server.DTO
{
    public class MonthTransactionsDTO
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
