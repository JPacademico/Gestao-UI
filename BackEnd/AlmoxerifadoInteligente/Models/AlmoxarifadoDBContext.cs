using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AlmoxerifadoInteligente.Models;

namespace AlmoxerifadoInteligente.Models
{
    public partial class AlmoxarifadoDBContext : DbContext
    {
        public AlmoxarifadoDBContext()
        {
        }

        public AlmoxarifadoDBContext(DbContextOptions<AlmoxarifadoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<Produto> Produtos { get; set; } = null!;

        public virtual DbSet<BenchmarkingItem> BenchmarkingItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=PC03LAB2533\\SENAI;Database=AlmoxarifadoDB;User Id=sa;Password=senai.123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BenchmarkingItem>(entity =>
            {
                entity.ToTable("BenchmarkingItem");

                entity.HasIndex(e => e.IdProdutoNavigationIdProduto, "IX_BenchmarkingItem_IdProdutoNavigationIdProduto");

                entity.Property(e => e.Economia).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PrecoLoja1).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PrecoLoja2).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Email)
                     .HasColumnType("nvarchar(max)")
                     .IsRequired(false); 

                entity.HasOne(d => d.IdProdutoNavigationIdProdutoNavigation)
                    .WithMany(p => p.BenchmarkingItems)
                    .HasForeignKey(d => d.IdProdutoNavigationIdProduto);
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.HasKey(e => e.IdLog)
                    .HasName("PK__Logs__6CC851FEC09C9E3D");

                entity.Property(e => e.IdLog).HasColumnName("id_log");

                entity.Property(e => e.CodigoRobo)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Datetime).HasColumnType("datetime");

                entity.Property(e => e.Etapa)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IdProduto).HasColumnName("id_produto");

                entity.Property(e => e.InformacaoLog)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioRobo)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.IdProduto)
                    .HasName("PK__Produtos__BA38A6B801B4C2DA");

                entity.Property(e => e.IdProduto).HasColumnName("id_produto");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("descricao");

                entity.Property(e => e.EstoqueAtual).HasColumnName("estoque_atual");

                entity.Property(e => e.EstoqueMinimo).HasColumnName("estoque_minimo");

                entity.Property(e => e.Preco)
                    .HasColumnType("decimal(20, 2)")
                    .HasColumnName("preco");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<AlmoxerifadoInteligente.Models.BenchmarkingItem>? BenchmarkingItem { get; set; }
    }
}
