using Microsoft.EntityFrameworkCore;
using ParcialTickets.Data.Entities;
using Parcial2.Models;

namespace ParcialTickets.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Entrance> Entradas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Entrance>().HasIndex(e => e.Description).IsUnique();
            modelBuilder.Entity<Ticket>().HasIndex(t => t.Document).IsUnique();
        }
        public DbSet<Parcial2.Models.TicketViewModel> TicketViewModel { get; set; }

        
    }
}
