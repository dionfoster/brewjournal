using System.Data.Entity.Infrastructure;
using ApprovalTests;
using ApprovalTests.Reporters;
using BrewJournal.Tests.Testability;
using Xunit;

namespace BrewJournal.Tests.Verifications
{
    public class SchemaTests : DatabaseFixture
    {
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void VerifySchemaChange()
        {
            var sqlScript = ((IObjectContextAdapter)WorkDbContext).ObjectContext.CreateDatabaseScript();

            Approvals.Verify(sqlScript);
        }
    }
}