using System.ComponentModel.DataAnnotations.Schema;

namespace BlueSoftCore.Server.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public byte Type { get; set; } // 1 for Debit, 0 for Deposit
        public int AccountId { get; set; }
        public string PlaceOfEvent { get; set; }

        [NotMapped]
        public Account? Accounts { get; set; }
    }
}
