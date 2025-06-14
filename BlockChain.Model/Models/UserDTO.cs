using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Model.Modelss
{
    public class UserReadDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PublicKey { get; set; }
        // No PrivateKey here, so it's never exposed
    }
    public class UserCreateDto
    {
        public string UserName { get; set; }
    }
}
