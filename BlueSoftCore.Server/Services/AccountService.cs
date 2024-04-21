using AutoMapper;
using BlueSoftCore.Server.Data;
using BlueSoftCore.Server.DTO;
using BlueSoftCore.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlueSoftCore.Server.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AccountService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<Account>> CreateAccount(AccountDTO accountRequest)
        {
            var account = _mapper.Map<Account>(accountRequest);

            if (account.Balance < 0)
            {
                return new BadRequestObjectResult("Balance cannot be negative");
            }

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return new OkObjectResult(account);
        }

        public async Task<ActionResult<decimal>> GetAccountBalance(AccountBalanceDTO balanceRequest)
        {
            var account = await _context.Accounts.FindAsync(balanceRequest.AccountId);

            if (account == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(account.Balance);
        }
    }
}
