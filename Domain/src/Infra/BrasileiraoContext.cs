using System;
using Domain.src.Jogadores;
using Domain.src.TabelaEstatistica;
using Domain.src.Times;
using Domain.src.Users;
using Microsoft.EntityFrameworkCore;

namespace Domain.src.Infra
{
    public class BrasileiraoContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Jogador> Jogadores { get; set;}
        public DbSet<JogadorTime> JogadoresTime { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<TimeCampeonatoBrasileirao> TimesCampeonatoBrasileiro { get; set; }
        public DbSet<TabelaDeEstatisticaTime> TabelasEstatistica { get; set; }
        public DbSet<TabelaDeEstatisticaTimeCampeonatoBrasileirao> TabelasEstatisticaCampeonatoBrasileiro { get; set;}

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer("Data Source=localhost;User Id=sa;PWD=Sasuke3634;Initial Catalog=Brasileirao");
        } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
            .HasKey(u => u.Id);
            modelBuilder.Entity<Usuario>()
            .Property(u => u.Name).IsRequired();
            modelBuilder.Entity<Usuario>()
            .Property(u => u.Senha).IsRequired();

            modelBuilder.Entity<Jogador>()
            .HasKey(j => j.Id);
            
            modelBuilder.Entity<Jogador>()
            .Property(j => j.Nome).IsRequired();
                        
            modelBuilder.Entity<Time>()
            .HasKey(t => t.Id);
            
            modelBuilder.Entity<TabelaDeEstatisticaTime>()
            .Property<Guid>("TimeId");

            modelBuilder.Entity<TabelaDeEstatisticaTime>()
            .HasKey("TimeId");

        }   
              
    }
}