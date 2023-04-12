﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelRequisitionSystem.Data;

#nullable disable

namespace TravelRequisitionSystem.Migrations
{
    [DbContext(typeof(TravelDbContext))]
    [Migration("20230128225527_EnumConvert")]
    partial class EnumConvert
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TravelRequisitionSystem.Data.TravelRequest", b =>
                {
                    b.Property<int>("TravelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TravelId"), 1L, 1);

                    b.Property<string>("ChargeCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DepartureDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DestinationCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DestinationLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RequisitionStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RequisitonNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SourceCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SourceLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TravelClass")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TravelerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TripType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TravelId");

                    b.ToTable("TravelRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
