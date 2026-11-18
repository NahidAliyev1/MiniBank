using MiniBank.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MiniBank.BL.Services.Interfaces;
public interface ITransactionService
{
    Task<List<Transactionn>> GetAllTransactionsAsync();

    Task<Transactionn> GetTransactionByIdAsync(int id);

    Task<List<Transactionn>> GetCardHistoryAsync(string cardNumber); 

    Task TransferMoneyAsync(string fromCardNumber, string toCardNumber, decimal amount);
}
