using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BlackRiver.Data
{
    public class BlackRiverDBContextFactory : IDesignTimeDbContextFactory<BlackRiverDBContext>
    {
        public BlackRiverDBContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<BlackRiverDBContext>()
                .UseSqlServer(BlackRiverConstants.ConnectionString);

            return new BlackRiverDBContext(options.Options);
        }
    }
}
