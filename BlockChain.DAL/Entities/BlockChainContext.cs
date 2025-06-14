using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlockChain.DAL.Entities;

public partial class BlockChainContext : DbContext
{
    public BlockChainContext()
    {
    }

    public BlockChainContext(DbContextOptions<BlockChainContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Block> Blocks { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Utxo> Utxos { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=Winter\\SQLEXPRESS;Initial Catalog=BlockChain;Integrated Security=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Block>(entity =>
        {
            entity.HasKey(e => e.BlockId).HasName("PK__Blocks__144215F117C884B2");

            entity.Property(e => e.Hash).HasMaxLength(256);
            entity.Property(e => e.PreviousHash).HasMaxLength(256);
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A6B755C9E1F");

            entity.Property(e => e.TransactionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 8)");
            entity.Property(e => e.BlockHash).HasMaxLength(256);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Block).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.BlockId)
                .HasConstraintName("FK_Transactions_Blocks");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C13FCC7EE");

            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        modelBuilder.Entity<Utxo>(entity =>
        {
            entity.HasKey(e => e.Utxoid).HasName("PK__UTXOs__A2765265BC54986E");

            entity.ToTable("UTXOs");

            entity.Property(e => e.Utxoid)
                .ValueGeneratedNever()
                .HasColumnName("UTXOId");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 8)");
            entity.Property(e => e.IsSpent).HasDefaultValue(false);
            entity.Property(e => e.OwnerPublicKey).HasMaxLength(300);
            entity.Property(e => e.Timestamp).HasColumnType("datetime");

            entity.HasOne(d => d.Transaction).WithMany(p => p.Utxos)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK__UTXOs__Transacti__30F848ED");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
