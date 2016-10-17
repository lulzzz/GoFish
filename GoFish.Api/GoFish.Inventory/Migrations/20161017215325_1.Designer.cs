using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GoFish.Inventory;

namespace GoFish.Inventory.Migrations
{
    [DbContext(typeof(InventoryDbContext))]
    [Migration("20161017215325_1")]
    partial class _1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("GoFish.Inventory.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("GoFish.Inventory.StockItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AdvertId");

                    b.Property<int?>("OwnerId");

                    b.Property<double>("Price");

                    b.Property<int?>("ProductTypeId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("StockItems");
                });

            modelBuilder.Entity("GoFish.Inventory.StockOwner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("StockOwners");
                });

            modelBuilder.Entity("GoFish.Inventory.StockItem", b =>
                {
                    b.HasOne("GoFish.Inventory.StockOwner", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId");

                    b.HasOne("GoFish.Inventory.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId");
                });
        }
    }
}
