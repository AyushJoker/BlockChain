using BlockChain.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.BAL.BlockService
{
    public interface IBlockChainService
    {
      List<Block> GetAllBlocks();
    Block GetLatestBlock();
    Block MinePendingTransactions(string minerPublicKey);
    bool IsChainValid();
    
    }
}
