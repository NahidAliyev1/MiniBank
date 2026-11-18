using Microsoft.EntityFrameworkCore;
using MiniBank.BL.Services.Interfaces;
using MiniBank.DAL.Entities;
using MiniBank.DAL.Repositories.Concretes;
using MiniBank.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MiniBank.BL.Services.Concretes;
public class TransactionService : ITransactionService
{
    private readonly IRepository<Transactionn> _repo;
    private readonly IRepository<Card> _cardRepo;


    public TransactionService(IRepository<Transactionn> repo, IRepository<Card> cardRepo)
    {
        _repo = repo;
        _cardRepo = cardRepo;
    }

    public async Task<List<Transactionn>> GetAllTransactionsAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<List<Transactionn>> GetCardHistoryAsync(string cardNumber)
    {
        List<Transactionn>? tr = await _repo.GetAllQuery()
            .Where(t => t.FromCardNumber == cardNumber || t.ToCardNumber == cardNumber)
            .ToListAsync();

        if (tr is null)
        {
            throw new Exception("Xeta");
        }
        return tr;

    }

    public async Task<Transactionn> GetTransactionByIdAsync(int id)
    {
        Transactionn tr = await _repo.GetByIdAsync(id);
        if (tr is null)
        {
            throw new Exception("Bele bir tranzaksiya movcud deyildir");

        }
        return tr;
    }



    public async Task TransferMoneyAsync(string fromCardNumber, string toCardNumber, decimal amount)
    {
        if (amount <= 0)
        {
            throw new Exception("Məbləğ 0-dan böyük olmalıdır");
        }
        if (fromCardNumber == toCardNumber)
        {
            throw new Exception("Eyni karta pul köçürmək olmaz");
        }
        var fromCard = await _cardRepo.GetAllQuery()
            .FirstOrDefaultAsync(f => f.CardNumber == fromCardNumber);
        var toCard = await _cardRepo.GetAllQuery()
            .FirstOrDefaultAsync(t => t.CardNumber == toCardNumber);

        if (fromCard is null || toCard is null)
        {
            throw new Exception("Kartlardan biri və ya hər ikisi tapılmadı");
        }
        if (fromCard.IsBlocked || toCard.IsBlocked)
        {
            throw new Exception("Kartlardan biri blokdadır, köçürmə mümkün deyil");
        }

        if (fromCard.Balance < amount)
        {
            var failedTx = new Transactionn
            {
                FromCardNumber = fromCardNumber,
                ToCardNumber = toCardNumber,
                Amount = amount,
                TransactionDate = DateTime.Now,
                IsSucces = false,
                Description = "Balansda kifayət qədər vəsait yoxdur"
            };
            await _repo.CreateAsync(failedTx);
            await _repo.SaveChangesAsync();
            throw new Exception("Balansda kifayət qədər vəsait yoxdur");
        }
        fromCard.Balance -= amount;
        toCard.Balance += amount;

    
        var successTx = new Transactionn
        {
            FromCardNumber = fromCardNumber,
            ToCardNumber = toCardNumber,
            Amount = amount,
            TransactionDate = DateTime.Now,
            IsSucces = true,
            Description = "Uğurlu köçürmə"
        };

        await _repo.CreateAsync(successTx);

       
        await _repo.SaveChangesAsync();
    }
}
