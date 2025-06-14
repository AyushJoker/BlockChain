using BlockChain.BAL.BlockService;
using BlockChain.BAL.TransactionService;
using BlockChain.BAL.UserService;
using BlockChain.BAL.UTXOService;
using BlockChain.DAL.BlockRepository;
using BlockChain.DAL.Entities;
using BlockChain.DAL.TransactionRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.BAL.Services
{
    public class BlockChainService:IBlockChainService
    {
        private readonly IBlockRepository _blockRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly IUtxoService _utxoservice;
        private readonly IUserService _userService;

      
        public BlockChainService(IBlockRepository blockRepo, ITransactionRepository transactionRepo,IUtxoService uTXOService, IUserService userService)
        {
            _blockRepo = blockRepo;
            _transactionRepo =transactionRepo;
            _utxoservice = uTXOService;
            _userService = userService;

        }
        public List<Block> GetAllBlocks()
        {
            return _blockRepo.GetAllBlocks();
        }

        public Block GetLatestBlock()
        {
            var blocks = _blockRepo.GetAllBlocks();
            return blocks.LastOrDefault();
        }

        public Block MinePendingTransactions(string minerPublicKey)
        {
            var pendingTxs = _transactionRepo.GetPendingTransactions();
            var minerInfo=_userService.GetUserByPublicKey(minerPublicKey);
            if (!pendingTxs.Any())
                throw new Exception("No pending transactions to mine.");
            var rewardTransaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                Sender = "System",
                Receiver = minerInfo.Username,
                Amount = 10,
                Timestamp = DateTime.Now,
                Signature = "SYSTEM_REWARD",
                Status="Completed"
            };
            _transactionRepo.AddTransaction(rewardTransaction);
            pendingTxs.Add(rewardTransaction);

            var previousBlock = GetLatestBlock();
            var newBlock = new Block
            {
                Timestamp = DateTime.UtcNow,
                Transactions = pendingTxs.ToHashSet(),
                PreviousHash = previousBlock?.Hash ?? "0",
            }; 

            newBlock.Hash = Helper.MineBlock(newBlock);

            // Save the block
            _blockRepo.AddBlock(newBlock);
            //Mark UTXO updated
            _utxoservice.MarkUtxUpdated(pendingTxs, minerPublicKey);
            // Mark transactions as processed
            _transactionRepo.MarkTransactionsAsProcessed(pendingTxs,newBlock.Hash);

            return newBlock;
        }

      
        public bool IsChainValid()
        {
            var blocks = _blockRepo.GetAllBlocks();
            for (int i = 1; i < blocks.Count; i++)
            {
                var current = blocks[i];
                var previous = blocks[i - 1];
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                string recomputedHash = Helper.Hash($"{current.PreviousHash}{current.Timestamp}{JsonConvert.SerializeObject(current.Transactions, settings)}{current.Nonce}");

              //  string recomputedHash = Hash($"{current.PreviousHash}{current.Timestamp}{JsonConvert.SerializeObject(current.Transactions)}{current.Nonce}");
                if (current.Hash != recomputedHash || current.PreviousHash != previous.Hash)
                    return false;
            }

            return true;
        }
    }
}
