using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GSI.WebApi.DB
{
    public class MainDbContext : DbContext
    {


        public MainDbContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=192.168.1.180;Database=GSI;User Id=teste;Password=teste;TrustServerCertificate=True");
        }


        public virtual DbSet<Employee> Employees { get; set; }
    }
}
