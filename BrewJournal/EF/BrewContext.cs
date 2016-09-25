using System.Data.Common;
using System.Data.Entity;
using BrewJournal.Domain;

namespace BrewJournal.EF
{
    public class BrewContext : DbContext
    {
        public DbSet<Brew> Brews { get; set; }

        public BrewContext() : base("BrewContext")
        {
        }

        public BrewContext(DbConnection connection) : base(connection, false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(typeof(Brew).Assembly);
        }
    }
}