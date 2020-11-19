using Microsoft.EntityFrameworkCore;

namespace Domain.src.Infra
{
    public class BrasileiraoContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;User= Id=sa;PWD=;Initial Catalog=Brasileirao");
        }        
    }
}