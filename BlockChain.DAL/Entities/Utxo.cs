using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlockChain.DAL.Entities;

public partial class Utxo
{
    public Guid Utxoid { get; set; }

    public Guid TransactionId { get; set; }

    public string OwnerPublicKey { get; set; }

    public decimal Amount { get; set; }

    public bool IsSpent { get; set; }

    public DateTime Timestamp { get; set; }
    [JsonIgnore]
    public virtual Transaction? Transaction { get; set; }
}
