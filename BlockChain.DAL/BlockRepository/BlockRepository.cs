using BlockChain.DAL.Entities;
using BlockChain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.DAL.BlockRepository
{
    public class BlockRepository : IBlockRepository
    {
        private readonly BlockChainContext _dbContext;
        public BlockRepository(BlockChainContext blockChainContext)
        {
            _dbContext = blockChainContext;
        }

        public List<Block> GetAllBlocks()
        {
            return _dbContext.Blocks
                .Include(b => b.Transactions) // assuming navigation property
                .ToList();
        }

        public Block GetBlockById(int blockId)
        {
            return _dbContext.Blocks
                .Include(b => b.Transactions)
                .FirstOrDefault(b => b.BlockId == blockId);
        }

        public Block GetLatestBlock()
        {
            return _dbContext.Blocks
                .OrderByDescending(b => b.Timestamp)
                .Include(b => b.Transactions)
                .FirstOrDefault();
        }

        public void AddBlock(Block block)
        {
            _dbContext.Blocks.Add(block);

            _dbContext.SaveChanges();
        }


    }
}
