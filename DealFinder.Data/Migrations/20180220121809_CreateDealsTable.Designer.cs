﻿// <auto-generated />
using DealFinder.Data.Deals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DealFinder.Data.Migrations
{
    [DbContext(typeof(DealContext))]
    [Migration("20180220121809_CreateDealsTable")]
    partial class CreateDealsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("DealFinder.Data.Deals.DealRecord", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("DistanceInMeters");

                    b.Property<string>("Summary");

                    b.Property<string>("Title");

                    b.HasKey("Identifier");

                    b.ToTable("Deals");
                });

            modelBuilder.Entity("DealFinder.Data.Deals.LocationRecord", b =>
                {
                    b.Property<Guid>("Identifier");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.HasKey("Identifier");

                    b.ToTable("LocationRecord");
                });

            modelBuilder.Entity("DealFinder.Data.Deals.LocationRecord", b =>
                {
                    b.HasOne("DealFinder.Data.Deals.DealRecord")
                        .WithOne("LocationRecord")
                        .HasForeignKey("DealFinder.Data.Deals.LocationRecord", "Identifier")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}