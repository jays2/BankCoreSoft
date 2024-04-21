using AutoMapper;
using BlueSoftCore.Server.Data;
using BlueSoftCore.Server.DTO;
using BlueSoftCore.Server.Models;

namespace BlueSoftCore.Server.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClientService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Client> CreateClient(ClientDTO clientRequest)
        {
            var client = _mapper.Map<Client>(clientRequest);

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return client;
        }
    }
}
