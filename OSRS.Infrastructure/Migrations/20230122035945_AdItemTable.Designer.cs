﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OSRS.Infrastructure;

namespace OSRS.Infrastructure.Migrations
{
    [DbContext(typeof(DomainContext))]
    [Migration("20230122035945_AdItemTable")]
    partial class AdItemTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OSRS.Domain.Entities.AlchemyObject", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ItemCant")
                        .HasColumnType("int");

                    b.Property<int>("ItemHighAlchemy")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("ItemName")
                        .HasColumnType("int");

                    b.Property<int>("ItemPrice")
                        .HasColumnType("int");

                    b.Property<int>("NaturePrice")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK_Alchemy");

                    b.ToTable("Alchemy", "Alchemy");
                });

            modelBuilder.Entity("OSRS.Domain.Entities.Item.ItemObject", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Examine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HighAlch")
                        .HasColumnType("int");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Limit")
                        .HasColumnType("int");

                    b.Property<int>("LowAlch")
                        .HasColumnType("int");

                    b.Property<bool>("Members")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("ItemId")
                        .HasName("PK_Item");

                    b.ToTable("Item", "Item");
                });
#pragma warning restore 612, 618
        }
    }
}
