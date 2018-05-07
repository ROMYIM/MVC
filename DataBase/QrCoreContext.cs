using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Data
{
    public class QrCoreContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Invitation> Invitation { get; set; }
        public DbSet<UserOpenInformation> UserOpenInformation { get; set; }

        public QrCoreContext(DbContextOptions<QrCoreContext> options) : base(options)
        {
            
        }
    }
}