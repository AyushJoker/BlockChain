using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlockChain.DAL.Entities;

public partial class Block
{
    public int BlockId { get; set; }

    public int IndexNumber { get; set; }

    public DateTime? Timestamp { get; set; }

    public string PreviousHash { get; set; } = null!;

    public string Hash { get; set; } = null!;

    public int Nonce { get; set; }
    [JsonIgnore]
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
