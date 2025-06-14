using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.Model.Models
{
    public class TransactionDTO
    {
        public Guid TransactionId { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public string Signature { get; set; }
        public string Status { get; set; }
        public string BlockHash { get; set; }




    }
    public class TransactionCreateDto
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public decimal Amount { get; set; }
        public string Signature { get; set; }
    }
}
