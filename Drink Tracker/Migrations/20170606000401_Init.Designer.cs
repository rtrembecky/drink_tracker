using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Drink_Tracker;

namespace Drink_Tracker.Migrations
{
    [DbContext(typeof(AccountContext))]
    [Migration("20170606000401_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("Drink_Tracker.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Man");

                    b.Property<string>("Username");

                    b.Property<int>("WeightInKg");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Drink_Tracker.Bill", b =>
                {
                    b.Property<int>("BillId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Name");

                    b.HasKey("BillId");

                    b.HasIndex("AccountId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("Drink_Tracker.Drink", b =>
                {
                    b.Property<int>("DrinkId")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("ABV");

                    b.Property<string>("Name");

                    b.Property<string>("Type");

                    b.Property<int>("VolumeInMl");

                    b.HasKey("DrinkId");

                    b.ToTable("Drinks");
                });

            modelBuilder.Entity("Drink_Tracker.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BillId");

                    b.Property<int>("DrinkId");

                    b.Property<float>("DrinkPrice");

                    b.HasKey("ItemId");

                    b.HasIndex("BillId");

                    b.HasIndex("DrinkId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Drink_Tracker.Price", b =>
                {
                    b.Property<int>("PriceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DrinkId");

                    b.Property<float>("Value");

                    b.HasKey("PriceId");

                    b.HasIndex("DrinkId");

                    b.ToTable("Prices");
                });

            modelBuilder.Entity("Drink_Tracker.Ytem", b =>
                {
                    b.Property<int>("YtemId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Added");

                    b.Property<int>("ItemId");

                    b.HasKey("YtemId");

                    b.HasIndex("ItemId");

                    b.ToTable("Ytems");
                });

            modelBuilder.Entity("Drink_Tracker.Bill", b =>
                {
                    b.HasOne("Drink_Tracker.Account", "Account")
                        .WithMany("Bills")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Drink_Tracker.Item", b =>
                {
                    b.HasOne("Drink_Tracker.Bill", "Bill")
                        .WithMany("Items")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Drink_Tracker.Drink", "Drink")
                        .WithMany()
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Drink_Tracker.Price", b =>
                {
                    b.HasOne("Drink_Tracker.Drink", "Drink")
                        .WithMany("Prices")
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Drink_Tracker.Ytem", b =>
                {
                    b.HasOne("Drink_Tracker.Item", "Item")
                        .WithMany("Ytems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
