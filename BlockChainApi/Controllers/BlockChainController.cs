using BlockChain.BAL.BlockService;
using BlockChain.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace BlockChainApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockChainController : ControllerBase
    {
        private readonly IBlockChainService _blockchainService;
        public BlockChainController(IBlockChainService service)
        {
            _blockchainService = service;
        }
        // GET: api/blockchain
        [HttpGet]
        public IActionResult GetFullChain()
        {
            var chain = _blockchainService.GetAllBlocks();
            return Ok(chain);
        }

        // GET: api/blockchain/latest
        [HttpGet("latest")]
        public IActionResult GetLatestBlock()
        {
            var latestBlock = _blockchainService.GetLatestBlock();
            return Ok(latestBlock);
        }

        // POST: api/blockchain/mine
        [HttpPost("mine")]
        public IActionResult MinePendingBlocks(string minerPublicKey)
        {
            var decodedKey = WebUtility.UrlDecode(minerPublicKey);
            var newBlock = _blockchainService.MinePendingTransactions(decodedKey);
            if (newBlock == null)
                return BadRequest("No transactions available to mine.");

            return Ok(newBlock);
        }

        // GET: api/blockchain/validate
        [HttpGet("validate")]
        public IActionResult ValidateChain()
        {
            bool isValid = _blockchainService.IsChainValid();
            return Ok(new { valid = isValid });
        }



    }
}
