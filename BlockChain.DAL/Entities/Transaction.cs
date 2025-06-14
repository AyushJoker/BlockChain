using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlockChain.DAL.Entities;

public partial class Transaction
{
    public Guid TransactionId { get; set; }

    public string Sender { get; set; } = null!;

    public string Receiver { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime? Timestamp { get; set; }

    public string Signature { get; set; } = null!;

    public string? Status { get; set; }

    public string? BlockHash { get; set; }

    public int? BlockId { get; set; }

    public virtual Block? Block { get; set; }
    [JsonIgnore]
    public virtual ICollection<Utxo> Utxos { get; set; } = new List<Utxo>();
}
