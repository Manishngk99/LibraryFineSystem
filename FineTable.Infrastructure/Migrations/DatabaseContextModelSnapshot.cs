﻿// <auto-generated />
using System;
using FineTable.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FineTable.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FineTable.Domain.Entities.EFine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int>("MemberType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("FineTable");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 50,
                            MemberType = 0
                        },
                        new
                        {
                            Id = 2,
                            Amount = 10,
                            MemberType = 1
                        });
                });

            modelBuilder.Entity("FineTable.Domain.Entities.EFineCollection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double?>("Amount")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("Days")
                        .HasColumnType("integer");

                    b.Property<int>("FineStatus")
                        .HasColumnType("integer");

                    b.Property<int>("MemberID")
                        .HasColumnType("integer");

                    b.Property<int>("MemberType")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("FineCollection");
                });
#pragma warning restore 612, 618
        }
    }
}
