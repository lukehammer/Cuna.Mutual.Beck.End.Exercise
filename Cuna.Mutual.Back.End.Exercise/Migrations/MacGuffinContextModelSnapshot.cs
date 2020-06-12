﻿// <auto-generated />
using System;
using Cuna.Mutual.Back.End.Exercise.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cuna.Mutual.Back.End.Exercise.Api.Migrations
{
    [DbContext(typeof(MacGuffinContext))]
    partial class MacGuffinContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cuna.Mutual.Back.End.Exercise.Api.Controllers.MacGuffin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MacGuffin");
                });

            modelBuilder.Entity("Cuna.Mutual.Back.End.Exercise.Api.Controllers.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Detail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MacGuffinId")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("Time")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("MacGuffinId");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("Cuna.Mutual.Back.End.Exercise.Api.Controllers.Status", b =>
                {
                    b.HasOne("Cuna.Mutual.Back.End.Exercise.Api.Controllers.MacGuffin", null)
                        .WithMany("Statuses")
                        .HasForeignKey("MacGuffinId");
                });
#pragma warning restore 612, 618
        }
    }
}
