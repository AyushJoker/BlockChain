using BlockChain.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.DAL.UserRepository
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserById(int userId);
        void AddUser(User user);
        User GetUserByPublicKey(string publicKey);
        User GetUserByUsername(string publicKey);
    }
}
