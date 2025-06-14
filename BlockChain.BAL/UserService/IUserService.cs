using BlockChain.DAL.Entities;
using BlockChain.Model.Modelss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.BAL.UserService
{
    public interface IUserService
    {
        List<UserReadDto> GetAllUsers();
        UserReadDto GetUserById(int userId);
        void CreateUser(UserCreateDto user);
        UserReadDto GetUserByPublicKey(string publicKey);
    }
}
