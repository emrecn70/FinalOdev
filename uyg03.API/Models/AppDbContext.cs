using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using uyg03.API.Models;

namespace uyg03.Models
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }


    }
}
