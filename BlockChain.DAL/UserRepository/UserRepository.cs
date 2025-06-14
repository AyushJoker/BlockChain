using BlockChain.DAL.Entities;
using BlockChain.Model.Modelss;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.DAL.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly BlockChainContext _context;

        public UserRepository(BlockChainContext context)
        {
            _context = context;
        }
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public User GetUserByPublicKey(string publicKey)
        {
            return _context.Users.FirstOrDefault(u => u.PublicKey == publicKey);
        }

        public void AddUser(User user)
        {

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == username);
        }
    }
}
