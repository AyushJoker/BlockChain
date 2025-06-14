using AutoMapper;
using BlockChain.BAL.UTXOService;
using BlockChain.DAL.Entities;
using BlockChain.DAL.UserRepository;
using BlockChain.Model.Modelss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.BAL.UserService
{
    public class UserService:IUserService
    {
        private readonly IUtxoService _utxoService;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepo, IMapper mapper,IUtxoService utxoService)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _utxoService = utxoService;
        }
        public List<UserReadDto> GetAllUsers()
        {
            var users = _userRepo.GetAllUsers();
            return _mapper.Map<List<UserReadDto>>(users); 
        }
        public UserReadDto GetUserById(int id) { 
            var user= _userRepo.GetUserById(id);
            return _mapper.Map<UserReadDto>(user);
        }
        public UserReadDto GetUserByPublicKey(string publicKey)
        {
            var user=_userRepo.GetUserByPublicKey(publicKey);
            return _mapper.Map<UserReadDto>(user);
        }
        public void CreateUser(UserCreateDto userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            var keys = Helper.GenerateECDSAKeyPair();
            user.PublicKey = keys.PublicKey;
            user.PrivateKey = keys.PrivateKey;
            _userRepo.AddUser(user);
            _utxoService.AssignInitialUTXO(user.PublicKey,userDTO.UserName);
        }
    }
}
