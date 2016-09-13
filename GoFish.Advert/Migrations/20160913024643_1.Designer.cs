using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoFish.Advert.Migrations
{
    [DbContext(typeof(AdvertisingContext))]
    [Migration("20160913024643_1")]
    partial class _1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("GoFish.Advert.Advert", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AdvertiserId");

                    b.Property<int?>("CatchTypeId");

                    b.Property<bool>("InInventory");

                    b.Property<double>("Price");

                    b.Property<int>("Quantity");

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
