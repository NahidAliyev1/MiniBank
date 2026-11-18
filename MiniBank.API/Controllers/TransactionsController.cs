using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniBank.BL.Services.Interfaces;

namespace MiniBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var transaction = await _transactionService.GetTransactionByIdAsync(id);
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("history/{cardNumber}")]
        public async Task<IActionResult> GetCardHistory(string cardNumber)
        {
            try
            {
                var history = await _transactionService.GetCardHistoryAsync(cardNumber);
                return Ok(history);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer(string fromCardNumber, string toCardNumber, decimal amount)
        {
            try
            {
                await _transactionService.TransferMoneyAsync(fromCardNumber, toCardNumber, amount);
                return Ok("Köçürmə uğurla tamamlandı");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
