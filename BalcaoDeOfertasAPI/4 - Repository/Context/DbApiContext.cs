using Microsoft.EntityFrameworkCore;
using BalcaoDeOfertasAPI._1___Models;

namespace BalcaoDeOfertasAPI._4___Repository.Context
{
    public class DbApiContext : DbContext
    {
        public DbApiContext(DbContextOptions<DbApiContext> options) : base(options)
        { }

        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Oferta> Oferta { get; set; }
        public virtual DbSet<Moeda> Moeda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasKey(x => x.Id);
            modelBuilder.Entity<Oferta>().HasKey(x => x.Id);
            modelBuilder.Entity<Moeda>().HasKey(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}