using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Data
{
    public class QrCoreContext : DbContext
    {

        private string _connectionString;
        public DbSet<User> User { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Invitation> Invitation { get; set; }
        public DbSet<Resident> Resident { get; set; }
        public DbSet<UserOpenInformation> UserOpenInformation { get; set; }

        public QrCoreContext(DbContextOptions<QrCoreContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                _connectionString = "server=localhost;port=3306;user=root;password=oInayChen*2HUI;database=qr_core;sslmode=none";
            }
            optionsBuilder.UseMySQL(_connectionString);
        }

        public QrCoreContext(string connectionString)
        {
            this._connectionString = connectionString;
        }

    }
}