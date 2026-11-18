using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniBank.BL.Services.Interfaces;

namespace MiniBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cards = await _cardService.GetAllCardsAsync();
            return Ok(cards);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var card = await _cardService.GetCardByIdAsync(id);
                return Ok(card);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("create-for-user/{userId}")]
        public async Task<IActionResult> CreateForUser(int userId)
        {
            try
            {
                await _cardService.CreateCardForUserAsync(userId);
                return Ok("İstifadəçi üçün yeni kart yaradıldı");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit(string cardNumber, decimal amount)
        {
            try
            {
                await _cardService.DepositAsync(cardNumber, amount);
                return Ok("Balans uğurla artırıldı");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/toggle-block")]
        public async Task<IActionResult> ToggleBlock(int id)
        {
            try
            {
                await _cardService.ToggleBlockCardAsync(id);
                return Ok("Kartın blok statusu dəyişdirildi");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _cardService.DeleteCardAsync(id);
                return Ok("Kart silindi");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
