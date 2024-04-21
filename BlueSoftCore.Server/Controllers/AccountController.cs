using Microsoft.AspNetCore.Mvc;
using BlueSoftCore.Server.Models;
using BlueSoftCore.Server.DTO;
using BlueSoftCore.Server.Services;

namespace BlueSoftCore.Server.Controllers
{
    /// <summary>
    /// Controller for managing accounts.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Create a new account.
        /// </summary>
        /// <param name="accountRequest">The account information to create.</param>
        /// <returns>The newly created account.</returns>
        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(AccountDTO accountRequest)
        {
            return await _accountService.CreateAccount(accountRequest);
        }

        /// <summary>
        /// Get the balance of an account.
        /// </summary>
        /// <param name="balanceRequest">The request containing the account ID.</param>
        /// <returns>The balance of the account.</returns>
        [HttpPost("balance")]
        public async Task<ActionResult<decimal>> GetAccountBalance(AccountBalanceDTO balanceRequest)
        {
            return await _accountService.GetAccountBalance(balanceRequest);
        }
    }
}
