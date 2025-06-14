using BlockChain.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.DAL.UTXORepository
{
    public interface IUTXORepository
    {
        List<Utxo> GetUTXOsByPublicKey(string publicKey);
        void AddUTXO(Utxo utxo);
        void MarkUTXOAsSpent(Guid utxoId);
        decimal GetBalance(string publicKey);
    }
}
