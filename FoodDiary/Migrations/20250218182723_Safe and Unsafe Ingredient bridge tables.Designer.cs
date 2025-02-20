﻿// <auto-generated />
using System;
using FoodDiary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodDiary.Migrations
{
    [DbContext(typeof(FoodDiaryContext))]
    [Migration("20250218182723_Safe and Unsafe Ingredient bridge tables")]
    partial class SafeandUnsafeIngredientbridgetables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("FoodDiary.Models.Entry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Symptomatic")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("FoodDiary.Models.EntryIngredient", b =>
                {
                    b.Property<int>("EntryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IngredientId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("EntryId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("EntryIngredients");
                });

            modelBuilder.Entity("FoodDiary.Models.EntrySymptom", b =>
                {
                    b.Property<int>("EntryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SymptomId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("EntryId", "SymptomId");

                    b.HasIndex("SymptomId");

                    b.ToTable("EntrySymptoms");
                });

            modelBuilder.Entity("FoodDiary.Models.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("FoodDiary.Models.SafeIngredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IngredientId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.ToTable("SafeIngredients");
                });

            modelBuilder.Entity("FoodDiary.Models.Symptom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Symptoms");
                });

            modelBuilder.Entity("FoodDiary.Models.UnsafeIngredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IngredientId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.ToTable("UnsafeIngredients");
                });

            modelBuilder.Entity("FoodDiary.Models.EntryIngredient", b =>
                {
                    b.HasOne("FoodDiary.Models.Entry", "Entry")
                        .WithMany("EntryIngredients")
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodDiary.Models.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entry");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("FoodDiary.Models.EntrySymptom", b =>
                {
                    b.HasOne("FoodDiary.Models.Entry", "Entry")
                        .WithMany("EntrySymptoms")
                        .HasForeignKey("EntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodDiary.Models.Symptom", "Symptom")
                        .WithMany()
                        .HasForeignKey("SymptomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entry");

                    b.Navigation("Symptom");
                });

            modelBuilder.Entity("FoodDiary.Models.SafeIngredient", b =>
                {
                    b.HasOne("FoodDiary.Models.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("FoodDiary.Models.UnsafeIngredient", b =>
                {
                    b.HasOne("FoodDiary.Models.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("FoodDiary.Models.Entry", b =>
                {
                    b.Navigation("EntryIngredients");

                    b.Navigation("EntrySymptoms");
                });
#pragma warning restore 612, 618
        }
    }
}
