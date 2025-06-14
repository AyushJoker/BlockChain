using BlockChain.BAL.UserService;
using BlockChain.BAL.UTXOService;
using BlockChain.DAL.Entities;
using BlockChain.Model.Modelss;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlockChainApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private  readonly IUtxoService _utxoService;
        private readonly IUserService _userService;
        public UserController(IUserService service,IUtxoService utxoService)
        {
            _userService = service;
            _utxoService = utxoService;
        }
        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("userById")]
        public IActionResult GetUserByID(int id)
        {
            var user = _userService.GetUserById(id);
            return Ok(user);
        }
        [HttpGet("userBykey")]
        public IActionResult GetUserByPublicKey(string key)
        {
            var user = _userService.GetUserByPublicKey(key);
            return Ok(user);
        }

        [HttpPost("add")]
        public IActionResult CreateUser(UserCreateDto user)
        {
            _userService.CreateUser(user);
            return Ok(new { message = "User added successfully." });
        }
        [HttpGet("balance/{publicKey}")]
        public IActionResult GetBalance(string publicKey)
        {
            var decodedKey = WebUtility.UrlDecode(publicKey);
            var balance = _utxoService.GetBalance(decodedKey);
            return Ok(balance);
        }
    }
}
