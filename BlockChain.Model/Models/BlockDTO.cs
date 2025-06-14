using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Model.Models
{
    public class BlockDTO
    {
        public int BlockId { get; set; }
        public int IndexNumber { get; set; }
        public DateTime Timestamp { get; set; }
        public string? PreviousHash { get; set; }
        public string? Hash { get; set; }
        public int Nonce { get; set; }
    }
}
