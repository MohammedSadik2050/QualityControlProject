using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace eConLab.EntityFrameworkCore
{
    public static class eConLabDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<eConLabDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<eConLabDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
