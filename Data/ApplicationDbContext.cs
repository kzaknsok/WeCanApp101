using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyApp2._1._7.Models;

namespace MyApp2._1._7.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Word>()
                .HasOne(w => w.PostUser)
                .WithMany()
                .HasForeignKey(w => w.PostUserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Word>()
                .HasOne(w => w.UpdateUser)
                .WithMany()
                .HasForeignKey(w => w.UpdateUserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<MyApp2._1._7.Models.Word> Word { get; set; } = default!;
    }
}
