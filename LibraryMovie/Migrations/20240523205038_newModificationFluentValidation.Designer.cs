﻿// <auto-generated />
using System;
using LibraryMovie.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibraryMovie.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240523205038_nene")]
    partial class nene
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LibraryMovie.Models.CategoryModel", b =>
                {
                    b.Property<int>("MovieCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MovieCategoryId"));

                    b.Property<string>("Theme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Theme");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("MovieCategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("MovieCategory", (string)null);
                });

            modelBuilder.Entity("LibraryMovie.Models.MoviesModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("RunningTime")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("title");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Movies", (string)null);
                });

            modelBuilder.Entity("LibraryMovie.Models.UsersModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("Password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Role");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "caiomonteiropro@gmail.com",
                            Name = "Caio",
                            Password = "123789",
                            Role = "admin"
                        });
                });

            modelBuilder.Entity("LibraryMovie.Models.CategoryModel", b =>
                {
                    b.HasOne("LibraryMovie.Models.UsersModel", "Users")
                        .WithMany("Categories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Users");
                });

            modelBuilder.Entity("LibraryMovie.Models.MoviesModel", b =>
                {
                    b.HasOne("LibraryMovie.Models.CategoryModel", "Category")
                        .WithMany("Movies")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("LibraryMovie.Models.UsersModel", "Users")
                        .WithMany("Movies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Category");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("LibraryMovie.Models.CategoryModel", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("LibraryMovie.Models.UsersModel", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}
