using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OSRS.Domain.Entities;

namespace OSRS.Infrastructure
{
    public partial class OSRSContext : DbContext
    {
        public OSRSContext()
        {
        }

        public OSRSContext(DbContextOptions<OSRSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Alchemy> Alchemy { get; set; }
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
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            //ENTITIES
            modelBuilder.Entity<Alchemy>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("PK_Alchemy");

                entity.ToTable("Alchemy", "Alchemy");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
