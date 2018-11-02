﻿// <auto-generated />
using System;
using Chama.Courses.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Chama.Courses.Persistence.Migrations
{
    [DbContext(typeof(EfDbContext))]
    partial class EfDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Chama.Courses.Domain.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<int>("MaximumStudents");

                    b.Property<Guid>("TeacherId");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId")
                        .IsUnique();

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Chama.Courses.Domain.Entities.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<Guid>("CourseId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Chama.Courses.Domain.Entities.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Chama.Courses.Domain.Entities.Course", b =>
                {
                    b.HasOne("Chama.Courses.Domain.Entities.Teacher", "Teacher")
                        .WithOne("Course")
                        .HasForeignKey("Chama.Courses.Domain.Entities.Course", "TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Chama.Courses.Domain.Entities.Student", b =>
                {
                    b.HasOne("Chama.Courses.Domain.Entities.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
