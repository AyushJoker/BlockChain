using BlockChain.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.DAL.BlockRepository
{
    public interface IBlockRepository
    {
        List<Block> GetAllBlocks();
        Block GetBlockById(int blockId);
        Block GetLatestBlock();
        void AddBlock(Block block);
        // void MarkTransactionsAsProcessed(List<Transaction> transactions);

    }
}
