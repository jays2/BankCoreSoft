using System.ComponentModel.DataAnnotations.Schema;

namespace BlueSoftCore.Server.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CityOfOrigin { get; set; }
        public int AccountId { get; set; }

        [NotMapped]
        public virtual Account Accounts { get; set; }
    }
}
