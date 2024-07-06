using EmployeeHR.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHR.Data
{
    public class HRDbContext : DbContext
    {
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }
        public HRDbContext(DbContextOptions<HRDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("DBCS");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }


    }
}
