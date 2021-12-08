﻿// <auto-generated />
using System;
using Assignment2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Assignment2.Migrations
{
    [DbContext(typeof(SchoolCommunityContext))]
    partial class SchoolCommunityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Assignment2.Models.Advertisement", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommunityId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("CommunityId");

                    b.ToTable("Advertisement");
                });

            modelBuilder.Entity("Assignment2.Models.Community", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Budget")
                        .HasColumnType("money");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Community");
                });

            modelBuilder.Entity("Assignment2.Models.CommunityMembership", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("CommunityId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StudentId", "CommunityId");

                    b.HasIndex("CommunityId");

                    b.ToTable("CommunityMembership");
                });

            modelBuilder.Entity("Assignment2.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Assignment2.Models.Advertisement", b =>
                {
                    b.HasOne("Assignment2.Models.Community", "Community")
                        .WithMany()
                        .HasForeignKey("CommunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Community");
                });

            modelBuilder.Entity("Assignment2.Models.CommunityMembership", b =>
                {
                    b.HasOne("Assignment2.Models.Community", "Community")
                        .WithMany("CommunityMembership")
                        .HasForeignKey("CommunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignment2.Models.Student", "Student")
                        .WithMany("CommunityMembership")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Community");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Assignment2.Models.Community", b =>
                {
                    b.Navigation("CommunityMembership");
                });

            modelBuilder.Entity("Assignment2.Models.Student", b =>
                {
                    b.Navigation("CommunityMembership");
                });
#pragma warning restore 612, 618
        }
    }
}
