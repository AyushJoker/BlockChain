using BlockChain.BAL.TransactionService;
using BlockChain.DAL.Entities;
using BlockChain.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlockChainApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;
        public TransactionController(ITransactionService transactionService) {
         _service = transactionService;
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var transactions = _service.GetAllTransactions();
            return Ok(transactions);
        }

        [HttpGet("pending")]
        public IActionResult GetPending()
        {
            var pending = _service.GetPendingTransactions();
            return Ok(pending);
        }

        [HttpPost("add")]
        public IActionResult AddTransaction([FromBody] TransactionCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _service.AddTransaction(dto);
                return Ok("Transaction added successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet("transactionById")]
        public IActionResult GetTransactionByID(Guid guid)
        {
            var trans = _service.GetTransactionById(guid);
            return Ok(trans);
        }
    }
}
