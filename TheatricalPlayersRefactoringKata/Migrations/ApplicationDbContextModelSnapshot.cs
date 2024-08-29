﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheatricalPlayersRefactoringKata;

#nullable disable

namespace TheatricalPlayersRefactoringKata.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Identity.Statement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Customer")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalAmountOwed")
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalEarnedCredits")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Statements");
                });

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Identity.StatementItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("AmountOwed")
                        .HasColumnType("TEXT");

                    b.Property<int>("EarnedCredits")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Seats")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StatementId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("StatementId");

                    b.ToTable("StatementItems");
                });

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Identity.StatementItem", b =>
                {
                    b.HasOne("TheatricalPlayersRefactoringKata.Identity.Statement", "Statement")
                        .WithMany("Items")
                        .HasForeignKey("StatementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Statement");
                });

            modelBuilder.Entity("TheatricalPlayersRefactoringKata.Identity.Statement", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
