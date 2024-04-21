using BlueSoftCore.Server.DTO;
using BlueSoftCore.Server.Models;

namespace BlueSoftCore.Server.Services
{
    public interface IClientService
    {
        Task<Client> CreateClient(ClientDTO clientRequest);
    }
}
