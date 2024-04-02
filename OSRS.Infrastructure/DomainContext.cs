using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OSRS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OSRS.Domain.Entities.Item;
using OSRS.Domain.Entities.Movie;

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

        public virtual DbSet<AlchemyObject> Alchemy { get; set; }
        public virtual DbSet<MovieObject> Movie { get; set; }
        public virtual DbSet<ItemObject> Item { get; set; }
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

            //ENTITIES
            modelBuilder.Entity<AlchemyObject>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("PK_Alchemy");

                entity.ToTable("Alchemy", "Alchemy");
            });
            modelBuilder.Entity<MovieObject>(entity =>
            {
                entity.HasKey(e => e.Id)
                .HasName("PK_Movie");

                entity.ToTable("Movie", "Movie");
            });
            modelBuilder.Entity<ItemObject>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                .HasName("PK_Item");

                entity.ToTable("Item", "Item");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
