﻿// <auto-generated />
using System;
using Domain.src.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Migrations
{
    [DbContext(typeof(BrasileiraoContext))]
    partial class BrasileiraoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid?>("TimeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TimeId");

                    b.ToTable("Jogadores");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Jogador");
                });

            modelBuilder.Entity("Domain.src.TabelaEstatistica.TabelaDeEstatisticaTime", b =>
                {
                    b.Property<Guid>("TimeId")
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

                    b.Property<int>("Vitorias")
                        .HasColumnType("int");

                    b.HasKey("TimeId");

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

                    b.HasKey("Id");

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
                        .WithMany("Jogadores")
                        .HasForeignKey("TimeId");
                });

            modelBuilder.Entity("Domain.src.TabelaEstatistica.TabelaDeEstatisticaTime", b =>
                {
                    b.HasOne("Domain.src.Times.Time", "Time")
                        .WithOne("Tabela")
                        .HasForeignKey("Domain.src.TabelaEstatistica.TabelaDeEstatisticaTime", "TimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Time");
                });

            modelBuilder.Entity("Domain.src.Times.Time", b =>
                {
                    b.Navigation("Jogadores");

                    b.Navigation("Tabela");
                });
#pragma warning restore 612, 618
        }
    }
}
