using Microsoft.EntityFrameworkCore;

namespace Wall.Models
{
    public class wallContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public wallContext(DbContextOptions<wallContext> options) : base(options) { }

        //DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}