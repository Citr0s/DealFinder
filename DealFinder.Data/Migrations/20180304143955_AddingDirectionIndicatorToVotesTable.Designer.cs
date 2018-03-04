﻿// <auto-generated />
using DealFinder.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DealFinder.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20180304143955_AddingDirectionIndicatorToVotesTable")]
    partial class AddingDirectionIndicatorToVotesTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("DealFinder.Data.Deals.Repository.DealRecord", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Summary");

                    b.Property<string>("Title");

                    b.Property<Guid?>("UserIdentifier");

                    b.HasKey("Identifier");

                    b.HasIndex("UserIdentifier");

                    b.ToTable("Deals");
                });

            modelBuilder.Entity("DealFinder.Data.Users.Repository.UserRecord", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Picture");

                    b.Property<string>("UserToken");

                    b.Property<string>("Username");

                    b.HasKey("Identifier");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DealFinder.Data.Votes.Repository.VoteRecord", b =>
                {
                    b.Property<Guid>("Identifier")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("DealIdentifier");

                    b.Property<bool>("Positive");

                    b.Property<Guid?>("UserIdentifier");

                    b.HasKey("Identifier");

                    b.HasIndex("DealIdentifier");

                    b.HasIndex("UserIdentifier");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("DealFinder.Data.Deals.Repository.DealRecord", b =>
                {
                    b.HasOne("DealFinder.Data.Users.Repository.UserRecord", "User")
                        .WithMany("Deals")
                        .HasForeignKey("UserIdentifier");
                });

            modelBuilder.Entity("DealFinder.Data.Votes.Repository.VoteRecord", b =>
                {
                    b.HasOne("DealFinder.Data.Deals.Repository.DealRecord", "Deal")
                        .WithMany()
                        .HasForeignKey("DealIdentifier");

                    b.HasOne("DealFinder.Data.Users.Repository.UserRecord", "User")
                        .WithMany()
                        .HasForeignKey("UserIdentifier");
                });
#pragma warning restore 612, 618
        }
    }
}
