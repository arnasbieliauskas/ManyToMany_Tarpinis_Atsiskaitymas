﻿// <auto-generated />
using ManyToMany_Tarpinis_Atsiskaitymas.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ManyToMany_Tarpinis_Atsiskaitymas.Migrations
{
    [DbContext(typeof(DbContextContext))]
    [Migration("20231128092236_one")]
    partial class one
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DepartmentLesson", b =>
                {
                    b.Property<string>("DepartmentsDepartmentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("LessonsLessonId")
                        .HasColumnType("int");

                    b.HasKey("DepartmentsDepartmentId", "LessonsLessonId");

                    b.HasIndex("LessonsLessonId");

                    b.ToTable("DepartmentLesson");
                });

            modelBuilder.Entity("DepartmentStudent", b =>
                {
                    b.Property<string>("DepartmentsDepartmentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("StudentsStudentId")
                        .HasColumnType("int");

                    b.HasKey("DepartmentsDepartmentId", "StudentsStudentId");

                    b.HasIndex("StudentsStudentId");

                    b.ToTable("DepartmentStudent");
                });

            modelBuilder.Entity("LessonStudent", b =>
                {
                    b.Property<int>("LessonsLessonId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsStudentId")
                        .HasColumnType("int");

                    b.HasKey("LessonsLessonId", "StudentsStudentId");

                    b.HasIndex("StudentsStudentId");

                    b.ToTable("LessonStudent");
                });

            modelBuilder.Entity("ManyToMany_Tarpinis_Atsiskaitymas.DataBase.Department", b =>
                {
                    b.Property<string>("DepartmentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("ManyToMany_Tarpinis_Atsiskaitymas.DataBase.Lesson", b =>
                {
                    b.Property<int>("LessonId")
                        .HasColumnType("int");

                    b.Property<string>("LessonDateAndTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LessonName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LessonId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("ManyToMany_Tarpinis_Atsiskaitymas.DataBase.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("StudentMail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentSurName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("DepartmentLesson", b =>
                {
                    b.HasOne("ManyToMany_Tarpinis_Atsiskaitymas.DataBase.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentsDepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManyToMany_Tarpinis_Atsiskaitymas.DataBase.Lesson", null)
                        .WithMany()
                        .HasForeignKey("LessonsLessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DepartmentStudent", b =>
                {
                    b.HasOne("ManyToMany_Tarpinis_Atsiskaitymas.DataBase.Department", null)
                        .WithMany()
                        .HasForeignKey("DepartmentsDepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManyToMany_Tarpinis_Atsiskaitymas.DataBase.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LessonStudent", b =>
                {
                    b.HasOne("ManyToMany_Tarpinis_Atsiskaitymas.DataBase.Lesson", null)
                        .WithMany()
                        .HasForeignKey("LessonsLessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManyToMany_Tarpinis_Atsiskaitymas.DataBase.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsStudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}