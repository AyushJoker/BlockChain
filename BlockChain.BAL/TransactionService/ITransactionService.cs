using BlockChain.DAL.Entities;
using BlockChain.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.BAL.TransactionService
{
    public interface ITransactionService
    {
        List<Transaction> GetAllTransactions();
        Transaction GetTransactionById(Guid transactionId);
        void AddTransaction(TransactionCreateDto transaction);
        List<Transaction> GetPendingTransactions(); // For mining pool
    }
}
