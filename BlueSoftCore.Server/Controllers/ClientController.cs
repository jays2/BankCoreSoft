using Microsoft.AspNetCore.Mvc;
using BlueSoftCore.Server.Models;
using BlueSoftCore.Server.DTO;
using BlueSoftCore.Server.Services;

namespace BlueSoftCore.Server.Controllers
{
    /// <summary>
    /// Controller for managing clients.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Create a new client.
        /// </summary>
        /// <param name="clientRequest">The client information to create.</param>
        /// <returns>The newly created client.</returns>
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(ClientDTO clientRequest)
        {
            var client = await _clientService.CreateClient(clientRequest);

            return Ok(client);
        }
    }
}
