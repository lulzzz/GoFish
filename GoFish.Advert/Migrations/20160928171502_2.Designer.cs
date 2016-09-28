using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GoFish.Advert;

namespace GoFish.Advert.Migrations
{
    [DbContext(typeof(AdvertisingDbContext))]
    [Migration("20160928171502_2")]
    partial class _2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("GoFish.Advert.Advert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AdvertiserId");

                    b.Property<int?>("CatchTypeId");

                    b.Property<string>("Pitch");

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("AdvertiserId");

                    b.HasIndex("CatchTypeId");

                    b.ToTable("Adverts");
                });

            modelBuilder.Entity("GoFish.Advert.Advertiser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Advertisers");
                });

            modelBuilder.Entity("GoFish.Advert.CatchType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("CatchTypes");
                });

            modelBuilder.Entity("GoFish.Advert.Advert", b =>
                {
                    b.HasOne("GoFish.Advert.Advertiser", "Advertiser")
                        .WithMany()
                        .HasForeignKey("AdvertiserId");

                    b.HasOne("GoFish.Advert.CatchType", "CatchType")
                        .WithMany()
                        .HasForeignKey("CatchTypeId");
                });
        }
    }
}
