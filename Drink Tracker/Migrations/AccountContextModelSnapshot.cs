using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Drink_Tracker.Models;

namespace Drink_Tracker.Migrations
{
    [DbContext(typeof(AccountContext))]
    partial class AccountContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Drink_Tracker.Models.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Username");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Drink_Tracker.Models.Bill", b =>
                {
                    b.Property<int>("BillId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountId");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Name");

                    b.HasKey("BillId");

                    b.HasIndex("AccountId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("Drink_Tracker.Models.Drink", b =>
                {
                    b.Property<int>("DrinkId")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("ABV");

                    b.Property<string>("Name");

                    b.Property<float>("PriceInKc");

                    b.Property<string>("Type");

                    b.Property<int>("VolumeInCl");

                    b.HasKey("DrinkId");

                    b.ToTable("Drinks");
                });

            modelBuilder.Entity("Drink_Tracker.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Added");

                    b.Property<int?>("BillId");

                    b.Property<int?>("DrinkId");

                    b.Property<string>("ImageSource");

                    b.HasKey("ItemId");

                    b.HasIndex("BillId");

                    b.HasIndex("DrinkId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Drink_Tracker.Models.Bill", b =>
                {
                    b.HasOne("Drink_Tracker.Models.Account")
                        .WithMany("Bills")
                        .HasForeignKey("AccountId");
                });

            modelBuilder.Entity("Drink_Tracker.Models.Item", b =>
                {
                    b.HasOne("Drink_Tracker.Models.Bill")
                        .WithMany("Items")
                        .HasForeignKey("BillId");

                    b.HasOne("Drink_Tracker.Models.Drink", "Drink")
                        .WithMany()
                        .HasForeignKey("DrinkId");
                });
        }
    }
}
