﻿// <auto-generated />
using System;
using AlmoxerifadoInteligente.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AlmoxerifadoInteligente.Migrations
{
    [DbContext(typeof(AlmoxarifadoDBContext))]
    partial class AlmoxarifadoDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AlmoxerifadoInteligente.Models.BenchmarkingItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Economia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdProduto")
                        .HasColumnType("int");

                    b.Property<int?>("IdProdutoNavigationIdProduto")
                        .HasColumnType("int");

                    b.Property<string>("LinkLoja1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkLoja2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeLoja1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeLoja2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PrecoLoja1")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecoLoja2")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdProdutoNavigationIdProduto");

                    b.ToTable("BenchmarkingItem", (string)null);
                });

            modelBuilder.Entity("AlmoxerifadoInteligente.Models.Log", b =>
                {
                    b.Property<int>("IdLog")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_log");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdLog"), 1L, 1);

                    b.Property<string>("CodigoRobo")
                        .IsRequired()
                        .HasMaxLength(4)
                        .IsUnicode(false)
                        .HasColumnType("varchar(4)");

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("datetime");

                    b.Property<string>("Etapa")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("IdProduto")
                        .HasColumnType("int")
                        .HasColumnName("id_produto");

                    b.Property<string>("InformacaoLog")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UsuarioRobo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("IdLog")
                        .HasName("PK__Logs__6CC851FEC09C9E3D");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("AlmoxerifadoInteligente.Models.Produto", b =>
                {
                    b.Property<int>("IdProduto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_produto");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProduto"), 1L, 1);

                    b.Property<string>("Descricao")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("descricao");

                    b.Property<bool?>("EmailStatus")
                        .HasColumnType("bit");

                    b.Property<int?>("EstoqueAtual")
                        .HasColumnType("int")
                        .HasColumnName("estoque_atual");

                    b.Property<int?>("EstoqueMinimo")
                        .HasColumnType("int")
                        .HasColumnName("estoque_minimo");

                    b.Property<decimal?>("Preco")
                        .HasColumnType("decimal(20,2)")
                        .HasColumnName("preco");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.HasKey("IdProduto")
                        .HasName("PK__Produtos__BA38A6B801B4C2DA");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("AlmoxerifadoInteligente.Models.BenchmarkingItem", b =>
                {
                    b.HasOne("AlmoxerifadoInteligente.Models.Produto", "IdProdutoNavigation")
                        .WithMany()
                        .HasForeignKey("IdProdutoNavigationIdProduto");

                    b.Navigation("IdProdutoNavigation");
                });
#pragma warning restore 612, 618
        }
    }
}
