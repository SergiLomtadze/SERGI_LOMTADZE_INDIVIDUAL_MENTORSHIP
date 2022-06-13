using ExadelMentorship.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExadelMentorship.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<WeatherHistory> WeatherHistories { get; set; }
        public DbSet<ReportUser> ReportUsers { get; set; }
    }
}
