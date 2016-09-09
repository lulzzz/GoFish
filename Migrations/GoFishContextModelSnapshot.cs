using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
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

                    b.Property<int?>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Catches");
                });

            modelBuilder.Entity("GoFish.CatchType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("CatchTypes");
                });

            modelBuilder.Entity("GoFish.Catch", b =>
                {
                    b.HasOne("GoFish.CatchType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });
        }
    }
}
