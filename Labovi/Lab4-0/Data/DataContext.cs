using Lab4_0.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab4_0.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
    }
}
