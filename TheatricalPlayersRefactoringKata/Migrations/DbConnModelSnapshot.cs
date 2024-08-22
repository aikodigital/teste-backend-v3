﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheatricalPlayersRefactoringKata.Data;

#nullable disable

namespace TheatricalPlayersRefactoringKata.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class DbConnModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.20");

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Models.Play", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Lines")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.HasIndex("Type");

                    b.ToTable("Plays");
                });

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Models.PlayTypes", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DtInclusao")
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("PlayTypes");
                });

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Models.StatementLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<int>("Audience")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Costumer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Credits")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DtInclusao")
                        .HasColumnType("TEXT");

                    b.Property<string>("PlayId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PlayId");

                    b.HasIndex("DtInclusao", "PlayId", "Costumer")
                        .IsUnique();

                    b.ToTable("StatementLogs");
                });

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Models.Play", b =>
                {
                    b.HasOne("TheatricalPlayersRefactoringKata.Models.PlayTypes", null)
                        .WithMany()
                        .HasForeignKey("Type")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Models.StatementLog", b =>
                {
                    b.HasOne("TheatricalPlayersRefactoringKata.Models.Play", null)
                        .WithMany()
                        .HasForeignKey("PlayId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
