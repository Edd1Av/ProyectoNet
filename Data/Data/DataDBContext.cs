using Domain.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Data.Data
{
    public class DataDBContext : DbContext
    {
        public DataDBContext(DbContextOptions<DataDBContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>().HasIndex(x => x.CorreoElectronico).IsUnique();
            modelBuilder.Entity<Factura>().HasIndex(x => x.Folio).IsUnique();
        }


        public DbSet<Cliente> Usuarios { get; set; }
        public DbSet<Factura> Facturas { get; set; }
    }
}
