using Lab5.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
    }
}
