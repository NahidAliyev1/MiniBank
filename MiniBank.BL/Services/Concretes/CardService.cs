using Microsoft.EntityFrameworkCore;
using MiniBank.BL.Services.Interfaces;
using MiniBank.DAL.Entities;
using MiniBank.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.BL.Services.Concretes
{
    public class CardService : ICardService
    {
        private readonly IRepository<Card> _repository;
        private readonly IRepository<User> _userRepository;

        public CardService(IRepository<Card> repository, IRepository<User> userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task CreateCardForUserAsync(int userId)
        {
            // 1. Müştəri bazada varmı?
            var user = await _userRepository.GetByIdAsync(userId);
            if (user is null)
                throw new Exception("Bu ID-li istifadəçi tapılmadı");

            // 2. Yeni kart obyekti yaradırıq (null göndərmirik!)
            Card newCard = new Card
            {
                UserId = userId,
                CardNumber = Random.Shared.NextInt64(1000000000000000, 9999999999999999).ToString(),
                Cvv = Random.Shared.Next(100, 999),
                ExpireDate = DateTime.Now.AddYears(3),
                Balance = 0,
                IsBlocked = false
            };

            await _repository.CreateAsync(newCard);
            await _repository.SaveChangesAsync();

        }

        public async Task DeleteCardAsync(int id)
        {
            Card card = await _repository.GetByIdAsync(id);
            if (card is null)
            {
                throw new Exception("Bu id li card tapilmadi");
            }
            _repository.DeleteAsync(card);
            await _repository.SaveChangesAsync();
        }

        public async Task DepositAsync(string cardNumber, decimal amount)
        {

            if (amount <= 0)
                throw new Exception("Məbləğ 0-dan böyük olmalıdır");

            Card? card = await _repository.GetAllQuery()
                .FirstOrDefaultAsync(c => c.CardNumber == cardNumber);

            if (card is null)
                throw new Exception("Kart tapılmadı");

            if (card.IsBlocked)
                throw new Exception("Kart blokdadır, pul yükləmək mümkün deyil");

            card.Balance += amount;

            // EF Core kartı izlədiyi üçün SaveChangesAsync kifayətdir
            await _repository.SaveChangesAsync();
        }

        public async Task<List<Card>> GetAllCardsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Card> GetCardByIdAsync(int id)
        {
            Card card = await _repository.GetByIdAsync(id);
            if (card is null)
            {
                throw new Exception("Bu id li kart tapilmadi");
            }
            return card;
        }

        public async Task ToggleBlockCardAsync(int cardId)
        {
            Card card = await _repository.GetByIdAsync(cardId);
            if (card is null)
            {
                throw new Exception("Bu id li kart tapilmadi");
            }
            card.IsBlocked = !card.IsBlocked;

            await _repository.SaveChangesAsync();
        }
    }
}
