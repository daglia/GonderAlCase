using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Transaction>()
                .HasOne(t => t.SenderUser)
                .WithMany(u => u.SentTransactions)
                .HasForeignKey(t => t.Sender)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Transaction>()
                .HasOne(t => t.ReceiverUser)
                .WithMany(u => u.ReceivedTransactions)
                .HasForeignKey(t => t.Receiver)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
