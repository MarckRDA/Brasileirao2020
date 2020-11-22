using Domain.src.Users;
using Microsoft.EntityFrameworkCore;

namespace Domain.src.Infra
{
    public class BrasileiraoContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Initial Catalog = nome do banco de dados que será criado
            // PWD = password
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
        }   
              
    }
}