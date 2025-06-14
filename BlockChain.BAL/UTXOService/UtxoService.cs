using BlockChain.DAL.Entities;
using BlockChain.DAL.TransactionRepository;
using BlockChain.DAL.UserRepository;
using BlockChain.DAL.UTXORepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.BAL.UTXOService
{
    public class UTXOService : IUtxoService
    {
        private readonly IUTXORepository _utxoRepo;
        private readonly IUserRepository _userRepo;
        private readonly ITransactionRepository _transactionRepo;


        public UTXOService(IUTXORepository utxoRepo,IUserRepository userRepository,ITransactionRepository transactionRepository)
        {
            _utxoRepo = utxoRepo;
            _userRepo = userRepository;
            _transactionRepo = transactionRepository;
        }

        public decimal GetBalance(string publicKey)
        {
            return _utxoRepo.GetBalance(publicKey);
        }

        public List<Utxo> GetUnspentOutputs(string publicKey)
        {
            return _utxoRepo.GetUTXOsByPublicKey(publicKey);
        }

        public void AddOutput(string publicKey, decimal amount, Guid transactionId)
        {

            var utxo = new Utxo
            {
                Utxoid = Guid.NewGuid(),
                OwnerPublicKey = publicKey,
                Amount = amount,
                TransactionId = transactionId,
                IsSpent = false
            };

            _utxoRepo.AddUTXO(utxo);
        }

        public void MarkOutputAsSpent(Guid utxoId)
        {
            _utxoRepo.MarkUTXOAsSpent(utxoId);
        }
        public void AssignInitialUTXO(string publicKey,string username, decimal amount = 100)
        {
            var rewardTransaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                Sender = "System(User-Creation)",
                Receiver = username,
                Amount = 100, // or any initial balance
                Timestamp = DateTime.UtcNow,
                Signature = "System-Signature",
                Status = "Completed"
            };
            _transactionRepo.AddTransaction(rewardTransaction);
            var utxo = new Utxo
            {
                Utxoid = Guid.NewGuid(),
                OwnerPublicKey = publicKey,
                Amount = amount,
                IsSpent = false,
                Timestamp = DateTime.UtcNow,
                TransactionId = rewardTransaction.TransactionId
            };

            _utxoRepo.AddUTXO(utxo);
        }
        public void MarkUtxUpdated(List<Transaction> transactions, string minerPublicKey)
        {
            foreach (var trans in transactions)
            {
                if (trans.Sender != "System")
                {
                    var senderUser = _userRepo.GetUserByUsername(trans.Sender);
                    var receiver = _userRepo.GetUserByUsername(trans.Receiver);
                    var utxos = _utxoRepo.GetUTXOsByPublicKey(senderUser.PublicKey);

                    decimal totalInp = utxos.Sum(x => x.Amount);
                    if (totalInp < trans.Amount)
                    {
                        // Skip this transaction or mark as failed
                        continue;
                    }
                    // 4. Mark UTXOs as spent
                    foreach (var utxo in utxos)
                    {
                        utxo.IsSpent = true;
                        _utxoRepo.MarkUTXOAsSpent(utxo.Utxoid);
                    }


                    // 5. Add new UTXO for receiver
                    var receiverUTXO = new Utxo
                    {
                        Utxoid = Guid.NewGuid(),
                        OwnerPublicKey = receiver.PublicKey,
                        Amount = trans.Amount,
                        IsSpent = false,
                        Timestamp = DateTime.UtcNow,
                        TransactionId = trans.TransactionId
                    };
                    _utxoRepo.AddUTXO(receiverUTXO);

                    // 6. Add change back to sender (if any)

                    if (totalInp > trans.Amount)
                    {
                        var changeUTXO = new Utxo
                        {
                            Utxoid = Guid.NewGuid(),
                            OwnerPublicKey = senderUser.PublicKey,
                            Amount = totalInp - trans.Amount,
                            IsSpent = false,
                            Timestamp = DateTime.UtcNow,
                            TransactionId = trans.TransactionId
                        };
                        _utxoRepo.AddUTXO(changeUTXO);
                    }
                }
                else
                {
                    // ✅ Reward transaction – add UTXO for miner
                    var rewardUTXO = new Utxo
                    {
                        Utxoid = Guid.NewGuid(),
                        OwnerPublicKey = minerPublicKey,
                        Amount = trans.Amount,
                        IsSpent = false,
                        Timestamp = DateTime.UtcNow,
                        TransactionId = trans.TransactionId
                    };
                    _utxoRepo.AddUTXO(rewardUTXO);
                }
                //
            }
        }
    }

}
