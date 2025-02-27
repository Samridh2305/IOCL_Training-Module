﻿// <auto-generated />
using System;
using IOCL_Training_Module.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IOCL_Training_Module.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20250225085035_UpdatedRecurringTraining")]
    partial class UpdatedRecurringTraining
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IOCL_Training_Module.Models.Completed", b =>
                {
                    b.Property<int>("SrNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SrNo"));

                    b.Property<string>("CompletedTraining")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EmpNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("SrNo");

                    b.HasIndex("CompletedTraining");

                    b.HasIndex("EmpNo");

                    b.ToTable("CompletedTrainings");
                });

            modelBuilder.Entity("IOCL_Training_Module.Models.Employee", b =>
                {
                    b.Property<string>("EmpNo")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ControllingEmp")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EmpDepartment")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("EmpDesignation")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FatherName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("Reporting")
                        .HasColumnType("bit");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("char(1)");

                    b.HasKey("EmpNo");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("IOCL_Training_Module.Models.EmployeeReporting", b =>
                {
                    b.Property<int>("SrNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SrNo"));

                    b.Property<string>("EmpNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reporting")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.HasKey("SrNo");

                    b.HasIndex("EmpNo");

                    b.HasIndex("Reporting");

                    b.ToTable("Reporting");
                });

            modelBuilder.Entity("IOCL_Training_Module.Models.NotCompleted", b =>
                {
                    b.Property<int>("SNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SNo"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EmpNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SNo");

                    b.HasIndex("Code");

                    b.HasIndex("EmpNo");

                    b.ToTable("NotCompletedTrainings");
                });

            modelBuilder.Entity("IOCL_Training_Module.Models.RecurringTraining", b =>
                {
                    b.Property<int>("SrNo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SrNo"));

                    b.Property<string>("EmpNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NextTrainingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TrainingID")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SrNo");

                    b.HasIndex("EmpNo");

                    b.HasIndex("TrainingID");

                    b.ToTable("RecurringTraining");
                });

            modelBuilder.Entity("IOCL_Training_Module.Models.Training", b =>
                {
                    b.Property<string>("TrainingID")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Department")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<string>("FPR")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FacultyName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SafetyTraining")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TrainingName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("Validity")
                        .HasColumnType("int");

                    b.Property<string>("Venue")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("TrainingID");

                    b.ToTable("Trainings");
                });

            modelBuilder.Entity("IOCL_Training_Module.Models.Completed", b =>
                {
                    b.HasOne("IOCL_Training_Module.Models.Training", "Training")
                        .WithMany()
                        .HasForeignKey("CompletedTraining")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IOCL_Training_Module.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmpNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Training");
                });

            modelBuilder.Entity("IOCL_Training_Module.Models.EmployeeReporting", b =>
                {
                    b.HasOne("IOCL_Training_Module.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmpNo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("IOCL_Training_Module.Models.Employee", "Manager")
                        .WithMany()
                        .HasForeignKey("Reporting")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("IOCL_Training_Module.Models.NotCompleted", b =>
                {
                    b.HasOne("IOCL_Training_Module.Models.Training", "Training")
                        .WithMany()
                        .HasForeignKey("Code")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IOCL_Training_Module.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmpNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Training");
                });

            modelBuilder.Entity("IOCL_Training_Module.Models.RecurringTraining", b =>
                {
                    b.HasOne("IOCL_Training_Module.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmpNo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IOCL_Training_Module.Models.Training", "Training")
                        .WithMany()
                        .HasForeignKey("TrainingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Training");
                });
#pragma warning restore 612, 618
        }
    }
}
