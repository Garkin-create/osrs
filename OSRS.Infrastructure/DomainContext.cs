using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OSRS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OSRS.Domain.Entities.Project;

namespace OSRS.Infrastructure
{
    public partial class DomainContext : IdentityUserContext<IdentityUser>
    {
        public DomainContext()
        {
            
        }
        public DomainContext(DbContextOptions<DomainContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ProjectObject> Project { get; set; }
        public virtual DbSet<KeywordObject> Keyword { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()

               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ProjectObject>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK_Keyword");
                entity.ToTable("Project");
                entity.HasMany(e => e.Keyworks) 
                    .WithOne(e => e.Project)
                    .HasForeignKey(e => e.ProjectId);
            });
            modelBuilder.Entity<KeywordObject>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("PK_Keyword");
                entity.ToTable("Keyword");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
