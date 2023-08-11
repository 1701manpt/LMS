﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LMS.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230811083913_mssql.local_migration_227")]
    partial class mssqllocal_migration_227
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LMS.Models.BorrowedHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BorrowedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("BorrowerId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BorrowerId");

                    b.ToTable("BorrowedHistories");
                });

            modelBuilder.Entity("LMS.Models.BorrowedItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BorrowHistoryId")
                        .HasColumnType("int");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BorrowHistoryId");

                    b.HasIndex("ItemId");

                    b.ToTable("BorrowedItems");
                });

            modelBuilder.Entity("LMS.Models.BorrowedItemTemp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("BorrowedItemTemps");
                });

            modelBuilder.Entity("LMS.Models.Borrower", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Borrowers");
                });

            modelBuilder.Entity("LMS.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("LMS.Models.Book", b =>
                {
                    b.HasBaseType("LMS.Models.Item");

                    b.Property<int>("NumberOfPages")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("LMS.Models.DVD", b =>
                {
                    b.HasBaseType("LMS.Models.Item");

                    b.Property<int>("RunTime")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("LMS.Models.Magazine", b =>
                {
                    b.HasBaseType("LMS.Models.Item");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("LMS.Models.BorrowedHistory", b =>
                {
                    b.HasOne("LMS.Models.Borrower", "Borrower")
                        .WithMany("BorrowedHistories")
                        .HasForeignKey("BorrowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Borrower");
                });

            modelBuilder.Entity("LMS.Models.BorrowedItem", b =>
                {
                    b.HasOne("LMS.Models.BorrowedHistory", "BorrowedHistory")
                        .WithMany("BorrowedItems")
                        .HasForeignKey("BorrowHistoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LMS.Models.Item", "Item")
                        .WithMany("BorrowedItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BorrowedHistory");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("LMS.Models.BorrowedItemTemp", b =>
                {
                    b.HasOne("LMS.Models.Item", "Item")
                        .WithMany("BorrowedItemTemps")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("LMS.Models.BorrowedHistory", b =>
                {
                    b.Navigation("BorrowedItems");
                });

            modelBuilder.Entity("LMS.Models.Borrower", b =>
                {
                    b.Navigation("BorrowedHistories");
                });

            modelBuilder.Entity("LMS.Models.Item", b =>
                {
                    b.Navigation("BorrowedItemTemps");

                    b.Navigation("BorrowedItems");
                });
#pragma warning restore 612, 618
        }
    }
}