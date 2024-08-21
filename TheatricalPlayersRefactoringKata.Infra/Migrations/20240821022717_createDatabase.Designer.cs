﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheatricalPlayersRefactoringKata.Infra.DataBase;

#nullable disable

namespace TheatricalPlayersRefactoringKata.Infra.Migrations
{
    [DbContext(typeof(TheatricalContext))]
    [Migration("20240821022717_createDatabase")]
    partial class createDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Customer")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Models.Performance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Audience")
                        .HasColumnType("int");

                    b.Property<int>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<int>("PlayId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("PlayId");

                    b.ToTable("Performances");
                });

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Models.Play", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Lines")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Plays");
                });

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Models.Performance", b =>
                {
                    b.HasOne("TheatricalPlayersRefactoringKata.Models.Invoice", "Invoice")
                        .WithMany("Performances")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheatricalPlayersRefactoringKata.Models.Play", "Play")
                        .WithMany("Performances")
                        .HasForeignKey("PlayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Play");
                });

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Models.Invoice", b =>
                {
                    b.Navigation("Performances");
                });

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Models.Play", b =>
                {
                    b.Navigation("Performances");
                });
#pragma warning restore 612, 618
        }
    }
}
