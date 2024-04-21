using Microsoft.AspNetCore.Mvc;
using BlueSoftCore.Server.Services;
using BlueSoftCore.Server.Models;
using BlueSoftCore.Server.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlueSoftCore.Server.Controllers
{
    /// <summary>
    /// Controller for managing transactions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>
        /// Deposit money into an account.
        /// </summary>
        /// <param name="accountId">The ID of the account to deposit into.</param>
        /// <param name="depositValue">The amount of money to deposit.</param>
        /// <param name="location">The location of the transaction.</param>
        /// <returns>The updated account information.</returns>
        [HttpPost("deposit")]
        public async Task<ActionResult<Account>> PostDeposit(int accountId, decimal depositValue, string location)
        {
            return await _transactionService.Deposit(accountId, depositValue, location);
        }

        /// <summary>
        /// Withdraw money from an account.
        /// </summary>
        /// <param name="accountId">The ID of the account to withdraw from.</param>
        /// <param name="withdrawValue">The amount of money to withdraw.</param>
        /// <param name="location">The location of the transaction.</param>
        /// <returns>The updated account information.</returns>
        [HttpPost("withdraw")]
        public async Task<ActionResult<Account>> PostWithdraw(int accountId, decimal withdrawValue, string location)
        {
            return await _transactionService.Withdraw(accountId, withdrawValue, location);
        }

        /// <summary>
        /// Get all transactions of an account.
        /// </summary>
        /// <param name="accountId">The ID of the account.</param>
        /// <returns>The list of transactions.</returns>
        [HttpGet("{accountId}/all")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByAccountId(int accountId)
        {
            return await _transactionService.GetTransactionsByAccountId(accountId);
        }

        /// <summary>
        /// Get the X most recent transactions of an account.
        /// </summary>
        /// <param name="accountId">The ID of the account.</param>
        /// <param name="count">The number of transactions to retrieve.</param>
        /// <returns>The list of recent transactions.</returns>
        [HttpGet("{accountId}/recent")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetRecentTransactions(int accountId, int count)
        {
            return await _transactionService.GetRecentTransactions(accountId, count);
        }

        /// <summary>
        /// Get the transactions for each client in a specific month.
        /// </summary>
        /// <param name="month">The month for which transactions are retrieved.</param>
        /// <param name="year">The year for which transactions are retrieved.</param>
        /// <returns>The list of client transaction information for the specified month.</returns>
        [HttpGet("{month}/{year}")]
        public async Task<ActionResult<List<ClientTransactionInfoDTO>>> GetClientTransactionsByMonth(int month, int year)
        {
            var clientTransactions = await _transactionService.GetClientTransactionsByMonth(month, year);
            return Ok(clientTransactions);
        }

        /// <summary>
        /// Get clients who withdraw money outside the city of origin of the account with total withdrawals exceeding 1,000,000.
        /// </summary>
        /// <returns>The list of clients who meet the criteria.</returns>
        [HttpGet("outsider")]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientsWithHighValueWithdrawals()
        {
            var clients = await _transactionService.GetClientsWithHighValueWithdrawals();
            return Ok(clients);
        }
    }
}
