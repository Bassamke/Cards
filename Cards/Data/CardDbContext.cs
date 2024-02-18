using Cards.Enums;
using Cards.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cards.Data;
public class CardDbContext : IdentityDbContext<IdentityUser>
{
    public CardDbContext(DbContextOptions<CardDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        RenameIdentityTables(modelBuilder);
        SeedInitialData(modelBuilder);


    }
    private void RenameIdentityTables(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IdentityUser>().ToTable("Users");
        modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
    }
    private void SeedInitialData(ModelBuilder modelBuilder)
    {
        try
        {
            string AdminRoleGuid = Guid.NewGuid().ToString();
            string MemberRoleGuid = Guid.NewGuid().ToString();
            string AdminUserGuid = Guid.NewGuid().ToString();
            string MemberUserGuid = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = AdminRoleGuid, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = MemberRoleGuid, Name = "Member", NormalizedName = "MEMBER" }
            );

            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser { Id = AdminUserGuid, UserName = "Admin@gmail.com", NormalizedUserName = "ADMIN@GMAIL.COM", Email = "Admin@gmail.com", NormalizedEmail = "ADMIN@GMAIL.COM", PasswordHash = hasher.HashPassword(null, "a123456") },
                new IdentityUser { Id = MemberUserGuid, UserName = "Member@gmail.com", NormalizedUserName = "MEMBER@GMAIL.COM", Email = "Member@gmail.com", NormalizedEmail = "MEMBER@GMAIL.COM", PasswordHash = hasher.HashPassword(null, "a123456") }
            );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = AdminUserGuid, RoleId = AdminRoleGuid },
                new IdentityUserRole<string> { UserId = MemberUserGuid, RoleId = MemberRoleGuid }
            );
        }
        catch(Exception ex)
        {
            Console.WriteLine( ex.ToString() );
        }
        
    }
    public DbSet<Card> cards { get; set; }

}
