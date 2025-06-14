using BlockChain.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.DAL.TransactionRepository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BlockChainContext _context;
        public TransactionRepository(BlockChainContext context)
        {
            _context = context;
        }



        public void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }

        public List<Transaction> GetAllTransactions()
        {
            return _context.Transactions.ToList();
        }


        public List<Transaction> GetPendingTransactions()
        {
            return _context.Transactions.Where(t => t.Status == "Pending").ToList();
        }


        public Transaction GetTransactionById(Guid transactionId)
        {
            return _context.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
        }
        public void MarkTransactionsAsProcessed(List<Transaction> transactions, string bockhash)
        {
            foreach (var tx in transactions)
            {
                var existingTx = _context.Transactions.FirstOrDefault(t => t.TransactionId == tx.TransactionId);
                if (existingTx != null)
                {
                    existingTx.Status = "Completed";
                    existingTx.BlockId = tx.BlockId;
                    existingTx.BlockHash = bockhash;
                }
            }

            _context.SaveChanges();

        }

    }
}
