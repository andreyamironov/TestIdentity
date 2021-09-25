using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TestIdentity.Identity.Models;

#nullable disable

namespace TestIdentity.Identity.Data
{
    public partial class ApplicationEventDbContext : DbContext
    {
        public ApplicationEventDbContext()
        {
        }

        public ApplicationEventDbContext(DbContextOptions<ApplicationEventDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LogEvent> Events { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<LogEvent>(entity =>
            {
                entity.ToTable("Events", "log");

                entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
