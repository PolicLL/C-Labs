using Lab6.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
    }
}
