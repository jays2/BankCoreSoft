namespace BlueSoftCore.Server.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public byte Type { get; set; } //1 for Business, 0 for Particular
        public decimal Balance { get; set; }
    }
}
