using AnyBookWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AnyBookWeb.Data
{
    public class ApplicationDbContext :DbContext
    {
        protected readonly IConfiguration Configuration;
        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        public DbSet<Category> Categories { get; set; }
    }
}
