using BlockChain.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.DAL.UTXORepository
{
    public class UTXORepository : IUTXORepository
    {
        private readonly BlockChainContext _context;

        public UTXORepository(BlockChainContext context)
        {
            _context = context;
        }

        public List<Utxo> GetUTXOsByPublicKey(string publicKey)
        {
            return _context.Utxos
                .Where(u => u.OwnerPublicKey == publicKey && !u.IsSpent)
            .ToList();
        }

        public List<Utxo> GetUTXOsByUser(string Username)
        {
            return _context.Utxos
                .Where(u => u.OwnerPublicKey == Username && !u.IsSpent)
            .ToList();
        }

        public void AddUTXO(Utxo utxo)
        {
            _context.Utxos.Add(utxo);
            _context.SaveChanges();
        }

        public void MarkUTXOAsSpent(Guid utxoId)
        {
            var utxo = _context.Utxos.Find(utxoId);
            if (utxo != null)
            {
                utxo.IsSpent = true;
                _context.SaveChanges();
            }
        }

        public decimal GetBalance(string publicKey)
        {
            return _context.Utxos
                .Where(u => u.OwnerPublicKey == publicKey && u.IsSpent==false)
                .Sum(u => u.Amount);
        }


    }

}
