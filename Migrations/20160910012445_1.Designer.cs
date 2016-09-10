using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GoFish;

namespace gofish.Migrations
{
    [DbContext(typeof(GoFishContext))]
    [Migration("20160910012445_1")]
    partial class _1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("GoFish.Catch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CaughtById");

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.Property<int?>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("CaughtById");

                    b.HasIndex("TypeId");

                    b.ToTable("Catches");
                });

            modelBuilder.Entity("GoFish.Dude", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Dudes");
                });

            modelBuilder.Entity("GoFish.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("GoFish.StockItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.Property<int?>("SellerId");

                    b.Property<int?>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("SellerId");

                    b.HasIndex("TypeId");

                    b.ToTable("StockItems");
                });

            modelBuilder.Entity("GoFish.Catch", b =>
                {
                    b.HasOne("GoFish.Dude", "CaughtBy")
                        .WithMany()
                        .HasForeignKey("CaughtById");

                    b.HasOne("GoFish.ProductType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });

            modelBuilder.Entity("GoFish.StockItem", b =>
                {
                    b.HasOne("GoFish.Dude", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId");

                    b.HasOne("GoFish.ProductType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });
        }
    }
}
