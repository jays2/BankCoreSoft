using BlueSoftCore.Server.Data;
using BlueSoftCore.Server.DTO;
using BlueSoftCore.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlueSoftCore.Server.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<Account>> Deposit(int accountId, decimal depositValue, string location)
        {
            var account = await _context.Accounts.FindAsync(accountId);

            if (account == null)
            {
                return new NotFoundResult();
            }

            account.Balance += depositValue;

            var transaction = new Transaction
            {
                Amount = depositValue,
                Date = DateTime.Now,
                Type = 0,
                PlaceOfEvent = location,
                AccountId = account.AccountId
            };

            _context.Transactions.Add(transaction);

            _context.Entry(account).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return new OkObjectResult(account);
        }

        public async Task<ActionResult<Account>> Withdraw(int accountId, decimal withdrawValue, string location)
        {
            var account = await _context.Accounts.FindAsync(accountId);

            if (account == null)
            {
                return new NotFoundResult();
            }

            if (account.Balance < withdrawValue)
            {
                return new BadRequestObjectResult("Insufficient balance");
            }

            account.Balance -= withdrawValue;

            var transaction = new Transaction
            {
                Amount = withdrawValue,
                Date = DateTime.Now,
                Type = 1,
                PlaceOfEvent = location,
                AccountId = account.AccountId
            };

            _context.Transactions.Add(transaction);

            _context.Entry(account).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return new OkObjectResult(account);
        }

        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByAccountId(int accountId)
        {
            var transactions = await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .ToListAsync();

            var groupedTransactions = transactions.GroupBy(t => new { t.Date.Year, t.Date.Month })
                .Select(group => new MonthTransactionsDTO
                {
                    Year = group.Key.Year,
                    Month = group.Key.Month,
                    Transactions = group.ToList()
                })
                .OrderByDescending(mt => mt.Year)
                .ThenByDescending(mt => mt.Month)
                .ToList();

            return new OkObjectResult(groupedTransactions);
        }

        public async Task<ActionResult<IEnumerable<Transaction>>> GetRecentTransactions(int accountId, int count)
        {
            var transactions = await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Date)
                .Take(count)
                .ToListAsync();

            return new OkObjectResult(transactions);
        }

        public async Task<List<ClientTransactionInfoDTO>> GetClientTransactionsByMonth(int month, int year)
        {
            var clientTransactions = await _context.Transactions
                .Where(t => t.Date.Month == month && t.Date.Year == year)
                .GroupBy(t => t.AccountId)
                .Select(group => new ClientTransactionInfoDTO
                {
                    AccountId = group.Key,
                    TransactionCount = group.Count()
                })
                .OrderByDescending(ct => ct.TransactionCount)
                .ToListAsync();

            return clientTransactions;
        }

        public async Task<IEnumerable<Client>> GetClientsWithHighValueWithdrawals()
        {
            var clientsWithHighValueWithdrawals = await _context.Transactions
                .Where(t => t.Amount > 1000000 && t.Type == 1) // Withdrawals exceeding $1,000,000
                .Join(_context.Clients, t => t.AccountId, c => c.AccountId, (t, c) => new { Transaction = t, Client = c })
                .Where(join => join.Transaction.PlaceOfEvent != join.Client.CityOfOrigin) // Withdrawals outside the city of origin
                .GroupBy(join => join.Client) // Group by client
                .Where(group => group.Sum(join => join.Transaction.Amount) > 1000000) // Total withdrawals exceeding $1,000,000
                .Select(group => group.Key) // Select client
                .ToListAsync();

            return clientsWithHighValueWithdrawals;
        }

    }
}
