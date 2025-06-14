using BlockChain.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.BAL.UTXOService
{
    public interface IUtxoService //Unspent Transaction Output
    {
        decimal GetBalance(string publicKey);
        List<Utxo> GetUnspentOutputs(string publicKey);
        void AddOutput(string publicKey, decimal amount, Guid transactionId);
        void MarkOutputAsSpent(Guid utxoId);
     
            void AssignInitialUTXO(string publicKey, string username,decimal amount = 100);
        void MarkUtxUpdated(List<Transaction> transactions,string minerPublicKey);


    }
}
