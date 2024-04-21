using BlueSoftCore.Server.DTO;
using BlueSoftCore.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlueSoftCore.Server.Services
{
    public interface IAccountService
    {
        Task<ActionResult<Account>> CreateAccount(AccountDTO accountRequest);
        Task<ActionResult<decimal>> GetAccountBalance(AccountBalanceDTO balanceRequest);
    }
}
