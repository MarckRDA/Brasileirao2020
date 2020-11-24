﻿// <auto-generated />
using System;
using Domain.src.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Migrations
{
    [DbContext(typeof(BrasileiraoContext))]
    [Migration("20201124001919_JogadoresTimeTabelaCreate")]
    partial class JogadoresTimeTabelaCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Domain.src.Jogadores.Jogador", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gol")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("TimeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Fk_Time");

                    b.Property<Guid?>("TimeId1")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TimeId");

                    b.HasIndex("TimeId1");

                    b.ToTable("Jogadores");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Jogador");
                });

            modelBuilder.Entity("Domain.src.TabelaEstatistica.TabelaDeEstatisticaTime", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Derrotas")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Empate")
                        .HasColumnType("int");

                    b.Property<int>("GolsContra")
                        .HasColumnType("int");

                    b.Property<int>("GolsPro")
                        .HasColumnType("int");

                    b.Property<int>("PartidasDisputadas")
                        .HasColumnType("int");

                    b.Property<double>("PercentagemAproveitamento")
                        .HasColumnType("float");

                    b.Property<int>("Pontuacao")
                        .HasColumnType("int");

                    b.Property<int>("SaldoDeGols")
                        .HasColumnType("int");

                    b.Property<Guid>("TimeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Vitorias")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TabelasEstatistica");

                    b.HasDiscriminator<string>("Discriminator").HasValue("TabelaDeEstatisticaTime");
                });

            modelBuilder.Entity("Domain.src.Times.Time", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TabelaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TabelaId")
                        .IsUnique();

                    b.ToTable("Times");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Time");
                });

            modelBuilder.Entity("Domain.src.Users.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Perfil")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Domain.src.Jogadores.JogadorTime", b =>
                {
                    b.HasBaseType("Domain.src.Jogadores.Jogador");

                    b.HasDiscriminator().HasValue("JogadorTime");
                });

            modelBuilder.Entity("Domain.src.TabelaEstatistica.TabelaDeEstatisticaTimeCampeonatoBrasileirao", b =>
                {
                    b.HasBaseType("Domain.src.TabelaEstatistica.TabelaDeEstatisticaTime");

                    b.HasDiscriminator().HasValue("TabelaDeEstatisticaTimeCampeonatoBrasileirao");
                });

            modelBuilder.Entity("Domain.src.Times.TimeCampeonatoBrasileirao", b =>
                {
                    b.HasBaseType("Domain.src.Times.Time");

                    b.HasDiscriminator().HasValue("TimeCampeonatoBrasileirao");
                });

            modelBuilder.Entity("Domain.src.Jogadores.Jogador", b =>
                {
                    b.HasOne("Domain.src.Times.Time", null)
                        .WithMany()
                        .HasForeignKey("TimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.src.Times.Time", null)
                        .WithMany("Jogadores")
                        .HasForeignKey("TimeId1");
                });

            modelBuilder.Entity("Domain.src.Times.Time", b =>
                {
                    b.HasOne("Domain.src.TabelaEstatistica.TabelaDeEstatisticaTime", null)
                        .WithOne()
                        .HasForeignKey("Domain.src.Times.Time", "TabelaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.src.Times.Time", b =>
                {
                    b.Navigation("Jogadores");
                });
#pragma warning restore 612, 618
        }
    }
}
