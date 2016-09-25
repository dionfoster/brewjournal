using System;
using System.Data.Common;
using System.IO;
using BrewJournal.EF;

namespace BrewJournal.Tests
{
    public class DatabaseFixture : IDisposable
    {
        private readonly BrewContext _parentContext;
        private readonly DbTransaction _transaction;

        static DatabaseFixture()
        {
            var testPath = Path.GetDirectoryName(typeof(DatabaseFixture).Assembly.CodeBase.Replace("file:///", ""));
            AppDomain.CurrentDomain.SetData("DataDirectory", testPath);
                // For localdb connection string that uses |DataDirectory|
            using (var migrationsContext = new BrewContext())
            {
                migrationsContext.Database.Initialize(false); // Performs EF migrations
            }
        }

        public DatabaseFixture()
        {
            _parentContext = new BrewContext();
            _parentContext.Database.Connection.Open();
                // This could be a simple SqlConnection if using sql express, but if using localdb you need a context so that EF creates the database if it doesn't exist (thanks EF!)
            _transaction = _parentContext.Database.Connection.BeginTransaction();

            SeedDbContext = GetNewDbContext();
            WorkDbContext = GetNewDbContext();
            VerifyDbContext = GetNewDbContext();
        }

        public BrewContext SeedDbContext { get; }
        public BrewContext WorkDbContext { get; }
        public BrewContext VerifyDbContext { get; }

        private BrewContext GetNewDbContext()
        {
            var context = new BrewContext(_parentContext.Database.Connection);
            context.Database.UseTransaction(_transaction);
            return context;
        }

        public void Dispose()
        {
            SeedDbContext.Dispose();
            WorkDbContext.Dispose();
            VerifyDbContext.Dispose();
            _transaction.Dispose(); // Discard any inserts/updates since we didn't commit
            _parentContext.Dispose();
        }
    }
}