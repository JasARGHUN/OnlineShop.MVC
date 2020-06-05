using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmployeeStore.Models
{
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "1",
                    Name = "admin",
                    NormalizedName = "ADMIN"
                }
            });

            var hasher = new PasswordHasher<ApplicationUser>();
            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = "1",
                UserName = "progmd@mail.ru",
                NormalizedUserName = "progmd@mail.ru",
                Email = "progmd@mail.ru",
                NormalizedEmail = "progmd@mail.ru",
                City = "SuperAdmin",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PasswordHash = hasher.HashPassword(null, "111111Test"),
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = "1"
            });

            builder.Entity<IdentityUserClaim<string>>().HasData(new List<IdentityUserClaim<string>> {
                new IdentityUserClaim<string>
                {
                    Id = 125,
                    UserId = "1",
                    ClaimType = "Create Role",
                    ClaimValue = "Create Role"
                },
                new IdentityUserClaim<string>
                {
                    Id = 135,
                    UserId = "1",
                    ClaimType = "Edit Role",
                    ClaimValue = "Edit Role"
                },
                new IdentityUserClaim<string>
                {
                    Id = 145,
                    UserId = "1",
                    ClaimType = "Delete Role",
                    ClaimValue = "Delete Role"
                },
            });
        }
    }
}
