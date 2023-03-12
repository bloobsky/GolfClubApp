﻿// <auto-generated />
using System;
using GolfClubApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GolfClubApp.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("GolfClubApp.Data.Golfer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Handicap")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Golfers");
                });

            modelBuilder.Entity("GolfClubApp.Data.Tee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tees");
                });

            modelBuilder.Entity("GolfClubApp.Data.TeeBooking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookedTeeId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BookingTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BookedTeeId");

                    b.ToTable("TeeBookings");
                });

            modelBuilder.Entity("GolferTeeBooking", b =>
                {
                    b.Property<int>("GolfersId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TeeBookingsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("GolfersId", "TeeBookingsId");

                    b.HasIndex("TeeBookingsId");

                    b.ToTable("GolferTeeBooking");
                });

            modelBuilder.Entity("GolfClubApp.Data.TeeBooking", b =>
                {
                    b.HasOne("GolfClubApp.Data.Tee", "BookedTee")
                        .WithMany("Bookings")
                        .HasForeignKey("BookedTeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookedTee");
                });

            modelBuilder.Entity("GolferTeeBooking", b =>
                {
                    b.HasOne("GolfClubApp.Data.Golfer", null)
                        .WithMany()
                        .HasForeignKey("GolfersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GolfClubApp.Data.TeeBooking", null)
                        .WithMany()
                        .HasForeignKey("TeeBookingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GolfClubApp.Data.Tee", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
