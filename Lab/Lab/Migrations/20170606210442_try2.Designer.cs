﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Lab.Data;

namespace Lab.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170606210442_try2")]
    partial class try2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lab.Models.Conducting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments");

                    b.Property<int?>("PatientId");

                    b.Property<DateTime>("TestDate");

                    b.Property<int?>("TestId");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("TestId");

                    b.ToTable("Conductings");
                });

            modelBuilder.Entity("Lab.Models.Demand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DemandName");

                    b.Property<int>("LowerBorder");

                    b.Property<int?>("TestId");

                    b.Property<int>("UpperBorder");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("Demands");
                });

            modelBuilder.Entity("Lab.Models.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("SpecializationId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SpecializationId");

                    b.HasIndex("UserId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Lab.Models.DoctorsPatiens", b =>
                {
                    b.Property<int>("DoctorId");

                    b.Property<int>("PatientId");

                    b.HasKey("DoctorId", "PatientId");

                    b.HasIndex("PatientId");

                    b.ToTable("DoctorsPatiens");
                });

            modelBuilder.Entity("Lab.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Antigen");

                    b.Property<int>("BloodGroup");

                    b.Property<int>("Height");

                    b.Property<int?>("UserId");

                    b.Property<int>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Lab.Models.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<int?>("ConductingId");

                    b.Property<int?>("DemandId");

                    b.HasKey("Id");

                    b.HasIndex("ConductingId");

                    b.HasIndex("DemandId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("Lab.Models.Specialization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SpecializationName");

                    b.HasKey("Id");

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("Lab.Models.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("TestName");

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("Lab.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsMale");

                    b.Property<string>("Lastname");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Lab.Models.Conducting", b =>
                {
                    b.HasOne("Lab.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");

                    b.HasOne("Lab.Models.Test", "Test")
                        .WithMany()
                        .HasForeignKey("TestId");
                });

            modelBuilder.Entity("Lab.Models.Demand", b =>
                {
                    b.HasOne("Lab.Models.Test", "Test")
                        .WithMany()
                        .HasForeignKey("TestId");
                });

            modelBuilder.Entity("Lab.Models.Doctor", b =>
                {
                    b.HasOne("Lab.Models.Specialization", "Specialization")
                        .WithMany()
                        .HasForeignKey("SpecializationId");

                    b.HasOne("Lab.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Lab.Models.DoctorsPatiens", b =>
                {
                    b.HasOne("Lab.Models.Patient", "Patient")
                        .WithMany("Doctors")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lab.Models.Doctor", "Doctor")
                        .WithMany("Patients")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Lab.Models.Patient", b =>
                {
                    b.HasOne("Lab.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Lab.Models.Result", b =>
                {
                    b.HasOne("Lab.Models.Conducting", "Conducting")
                        .WithMany()
                        .HasForeignKey("ConductingId");

                    b.HasOne("Lab.Models.Demand", "Demand")
                        .WithMany()
                        .HasForeignKey("DemandId");
                });
        }
    }
}
