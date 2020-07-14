using Microsoft.EntityFrameworkCore;

namespace QuickMeals.Models.Authentication
{
    public class AuthenticationContext : DbContext
    {
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) :base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Role>().HasData(new Role[]{
                new Role { RoleID = 0, RoleName = "Annonymous" },
                new Role { RoleID = 1, RoleName = "User" },
                new Role { RoleID = 2, RoleName = "Admin" }
            });
        }
    }
}
