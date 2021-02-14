﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vulnerable.Net5.Data;

namespace Vulnerable.Net5.Data.Migrations
{
    [DbContext(typeof(AddressDbContext))]
    [Migration("20210214023815_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("Vulnerable.Domain.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ContinentId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CountryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProvinceId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ProvinceId1")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ContinentId");

                    b.HasIndex("CountryId");

                    b.HasIndex("Name");

                    b.HasIndex("ProvinceId");

                    b.HasIndex("ProvinceId1");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Vulnerable.Domain.Entities.Continent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Continents");
                });

            modelBuilder.Entity("Vulnerable.Domain.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ContinentId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ContinentId1")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ContinentId");

                    b.HasIndex("ContinentId1");

                    b.HasIndex("Name");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Vulnerable.Domain.Entities.Province", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ContinentId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CountryId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CountryId1")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ContinentId");

                    b.HasIndex("CountryId");

                    b.HasIndex("CountryId1");

                    b.HasIndex("Name");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("Vulnerable.Domain.Entities.City", b =>
                {
                    b.HasOne("Vulnerable.Domain.Entities.Continent", "Continent")
                        .WithMany()
                        .HasForeignKey("ContinentId");

                    b.HasOne("Vulnerable.Domain.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("Vulnerable.Domain.Entities.Province", "Province")
                        .WithMany()
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Vulnerable.Domain.Entities.Province", null)
                        .WithMany("Cities")
                        .HasForeignKey("ProvinceId1");

                    b.Navigation("Continent");

                    b.Navigation("Country");

                    b.Navigation("Province");
                });

            modelBuilder.Entity("Vulnerable.Domain.Entities.Country", b =>
                {
                    b.HasOne("Vulnerable.Domain.Entities.Continent", "Continent")
                        .WithMany()
                        .HasForeignKey("ContinentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Vulnerable.Domain.Entities.Continent", null)
                        .WithMany("Countries")
                        .HasForeignKey("ContinentId1");

                    b.Navigation("Continent");
                });

            modelBuilder.Entity("Vulnerable.Domain.Entities.Province", b =>
                {
                    b.HasOne("Vulnerable.Domain.Entities.Continent", "Continent")
                        .WithMany()
                        .HasForeignKey("ContinentId");

                    b.HasOne("Vulnerable.Domain.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Vulnerable.Domain.Entities.Country", null)
                        .WithMany("Provinces")
                        .HasForeignKey("CountryId1");

                    b.Navigation("Continent");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Vulnerable.Domain.Entities.Continent", b =>
                {
                    b.Navigation("Countries");
                });

            modelBuilder.Entity("Vulnerable.Domain.Entities.Country", b =>
                {
                    b.Navigation("Provinces");
                });

            modelBuilder.Entity("Vulnerable.Domain.Entities.Province", b =>
                {
                    b.Navigation("Cities");
                });
#pragma warning restore 612, 618
        }
    }
}
