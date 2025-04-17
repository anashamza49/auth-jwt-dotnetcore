using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Authentification.JWT.DAL.Data
{
    public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
        public MyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseSqlServer(
                "Data Source=OUJC51MK13;Initial Catalog=authDb;Integrated Security=True;TrustServerCertificate=Yes;",
                b => b.MigrationsAssembly("Authentification.JWT.DAL"));

            return new MyDbContext(optionsBuilder.Options);
        }
    }
}
