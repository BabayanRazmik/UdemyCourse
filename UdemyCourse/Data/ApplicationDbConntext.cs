using Microsoft.EntityFrameworkCore;
using UdemyCourse.Models;

namespace UdemyCourse.Data
{
    public class ApplicationDbConntext : DbContext
    {
        public ApplicationDbConntext(DbContextOptions<ApplicationDbConntext> options) : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<ApplicationType> ApplicationType { get; set; }
        public DbSet<Product> Product { get; set; }


    }
}
