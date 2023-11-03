using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Models
{
    public class ExpenseDBContext : DbContext
    {
        public ExpenseDBContext(DbContextOptions<ExpenseDBContext> options) : base(options)
        { }

        public virtual DbSet<ExpenseReport> ExpenseReport { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
