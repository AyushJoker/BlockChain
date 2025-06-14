using BlockChain.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.DAL.TransactionRepository
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAllTransactions();
        Transaction GetTransactionById(Guid transactionId);
        void AddTransaction(Transaction transaction);
        List<Transaction> GetPendingTransactions(); // For mining pool
        void MarkTransactionsAsProcessed(List<Transaction> transactions, string blockhash);
    }
}
