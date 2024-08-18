﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TheatricalPlayersRefactoringKata.API.infra;

#nullable disable

namespace TheatricalPlayersRefactoringKata.API.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Customer")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Models.Performance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Audience")
                        .HasColumnType("integer");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("integer");

                    b.Property<string>("PlayId")
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("PlayId");

                    b.ToTable("Performances");
                });

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Models.Play", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Lines")
                        .HasColumnType("integer");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Name");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Plays");
                });

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Models.Performance", b =>
                {
                    b.HasOne("TheatricalPlayersRefactoringKata.Models.Invoice", "Invoice")
                        .WithMany("Performances")
                        .HasForeignKey("InvoiceId");

                    b.HasOne("TheatricalPlayersRefactoringKata.Models.Play", "Play")
                        .WithMany()
                        .HasForeignKey("PlayId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("FK_Performance_PlayId");

                    b.Navigation("Invoice");

                    b.Navigation("Play");
                });

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Models.Invoice", b =>
                {
                    b.Navigation("Performances");
                });
#pragma warning restore 612, 618
        }
    }
}
