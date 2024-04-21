using BlueSoftCore.Server.Models;
using Microsoft.AspNetCore.Mvc;
using BlueSoftCore.Server.DTO;

namespace BlueSoftCore.Server.Services
{
    public interface ITransactionService
    {
        Task<ActionResult<Account>> Deposit(int accountId, decimal depositValue, string location);
        Task<ActionResult<Account>> Withdraw(int accountId, decimal withdrawValue, string location);
        Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsByAccountId(int accountId);
        Task<ActionResult<IEnumerable<Transaction>>> GetRecentTransactions(int accountId, int count);
        Task<List<ClientTransactionInfoDTO>> GetClientTransactionsByMonth(int month, int year);
        Task<IEnumerable<Client>> GetClientsWithHighValueWithdrawals();

    }
}
