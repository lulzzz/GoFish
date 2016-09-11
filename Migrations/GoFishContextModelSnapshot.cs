using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using GoFish;

namespace gofish.Migrations
{
    [DbContext(typeof(GoFishContext))]
    partial class GoFishContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("GoFish.OrderItemIdentifier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PurchaseOrderId");

                    b.Property<int>("PurchaseOrderItemId");

                    b.Property<int?>("ShipmentId");

                    b.HasKey("Id");

                    b.HasIndex("ShipmentId");

                    b.ToTable("OrderItemIdentifier");
                });

            modelBuilder.Entity("GoFish.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("GoFish.PurchaseOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("GoFish.PurchaseOrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PurchaseOrderId");

                    b.Property<int>("StockItem");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("PurchaseOrderItem");
                });

            modelBuilder.Entity("GoFish.Shipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("From");

                    b.Property<string>("To");

                    b.HasKey("Id");

                    b.ToTable("Shipments");
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

            modelBuilder.Entity("GoFish.OrderItemIdentifier", b =>
                {
                    b.HasOne("GoFish.Shipment")
                        .WithMany("PurchaseOrderItems")
                        .HasForeignKey("ShipmentId");
                });

            modelBuilder.Entity("GoFish.PurchaseOrderItem", b =>
                {
                    b.HasOne("GoFish.PurchaseOrder")
                        .WithMany("OrderItems")
                        .HasForeignKey("PurchaseOrderId");
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
