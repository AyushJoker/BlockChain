using BlockChain.BAL.UTXOService;
using BlockChain.DAL.Entities;
using BlockChain.DAL.TransactionRepository;
using BlockChain.DAL.UserRepository;
using BlockChain.DAL.UTXORepository;
using BlockChain.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.BAL.TransactionService
{
    public class TransactionService:ITransactionService
    {
        private readonly ITransactionRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IUtxoService _utxoService;
        private readonly IUTXORepository _utxoRepoService;
        public TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository,IUtxoService utxoService, IUTXORepository uTXORepository) {
        _repository = transactionRepository;
            _userRepository = userRepository;
            _utxoService = utxoService;
            _utxoRepoService= uTXORepository;
        }

        public void AddTransaction(TransactionCreateDto transaction)
        {
            var senderUser = _userRepository.GetUserByUsername(transaction.Sender);
            if (senderUser == null)
                throw new Exception("Sender not found");

            // Validate signature
            bool isSignatureValid = Helper.VerifyTransactionSignature(transaction, senderUser.PublicKey);
            if (!isSignatureValid)
                throw new Exception("Invalid transaction signature");

            var utxos = Helper.SelectUTXOs(senderUser.PublicKey, transaction.Amount, _utxoService);

            decimal balance = utxos.Sum(u => u.Amount);

            if (balance < transaction.Amount)
                throw new Exception("Insufficient balance.");

            var trans = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                Sender = transaction.Sender,
                Receiver = transaction.Receiver,
                Amount = transaction.Amount,
                Timestamp = DateTime.UtcNow,
                Signature = transaction.Signature,
                Status = "Pending",
                BlockHash = null,
                BlockId = null
            };

            _repository.AddTransaction(trans);

        }

        public List<Transaction> GetAllTransactions()
        {
            return _repository.GetAllTransactions();
        }

        public List<Transaction> GetPendingTransactions()
        {
            return _repository.GetPendingTransactions();
        }

        public Transaction GetTransactionById(Guid transactionId)
        {
            return _repository.GetTransactionById(transactionId);
        }

    }
}
