using MiniBank.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.BL.Services.Interfaces
{
    public interface ICardService
    {
        Task<List<Card>> GetAllCardsAsync();

        Task<Card> GetCardByIdAsync(int id);

        Task DeleteCardAsync(int id);

        Task CreateCardForUserAsync(int userId); 

        Task DepositAsync(string cardNumber, decimal amount);

        Task ToggleBlockCardAsync(int cardId);
    }
}
