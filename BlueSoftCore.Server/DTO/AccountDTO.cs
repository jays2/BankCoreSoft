namespace BlueSoftCore.Server.DTO
{
    public class AccountDTO
    {
        public byte Type { get; set; } //1 for Business, 0 for Particular
        public decimal Balance { get; set; }
    }
}
