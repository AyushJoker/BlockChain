using BlockChain.BAL.UTXOService;
using BlockChain.DAL.Entities;
using BlockChain.Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockChain.BAL
{
    internal class Helper
    {
        private const int Difficulty = 3;
        public static (string PublicKey, string PrivateKey) GenerateECDSAKeyPair()
        {
            using (var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256))
            {
                var privateKeyBytes = ecdsa.ExportECPrivateKey();
                var publicKeyBytes = ecdsa.ExportSubjectPublicKeyInfo();

                string privateKey = Convert.ToBase64String(privateKeyBytes);
                string publicKey = Convert.ToBase64String(publicKeyBytes);

                return (publicKey, privateKey);
            }
        }
        public static bool VerifyTransactionSignature(TransactionCreateDto dto, string senderPublicKey)
        {
            // Prepare message string — e.g., "Sender|Receiver|Amount" — exact depends on your signing scheme
            var message = $"{dto.Sender}-{dto.Receiver}-{dto.Amount}";

            // Convert message to bytes
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            // Hash the message
            using var sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(messageBytes);
            // Verify the hash using public key
            using var ecdsa = ECDsa.Create();
            byte[] publicKeyBytes = Convert.FromBase64String(senderPublicKey);
            ecdsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);

            byte[] signatureBytes = Convert.FromBase64String(dto.Signature);
            return ecdsa.VerifyHash(hash, signatureBytes);
        }
        public static List<Utxo> SelectUTXOs(string publicKey, decimal amount,IUtxoService utxoService)
        {
            var utxos = utxoService.GetUnspentOutputs(publicKey).ToList();
            decimal total = 0;
            var selected = new List<Utxo>();

            foreach (var utxo in utxos)
            {
                selected.Add(utxo);
                total += utxo.Amount;
                if (total >= amount) break;
            }

            if (total < amount)
                throw new Exception("Insufficient balance");

            return selected;
        }
        public static string MineBlock(Block block)
        {
            int nonce = 0;
            string hash;
            string data = $"{block.PreviousHash}{block.Timestamp}{JsonConvert.SerializeObject(block.Transactions)}";
            do
            {
                hash = Hash(data + nonce);
                nonce++;
            } while (!hash.StartsWith(new string('0', Difficulty)));

            block.Nonce = nonce;
            return hash;
        }

        public static string Hash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }


    }
}
