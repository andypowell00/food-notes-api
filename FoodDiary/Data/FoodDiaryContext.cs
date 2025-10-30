using Microsoft.EntityFrameworkCore;
using FoodDiary.Models;

namespace FoodDiary.Data
{
    public class FoodDiaryContext : DbContext
    {
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<EntrySymptom> EntrySymptoms { get; set; }
        public DbSet<EntryIngredient> EntryIngredients { get; set; }
        public DbSet<EntrySupplement> EntrySupplements { get; set; }
        public DbSet<SafeIngredient> SafeIngredients{ get; set; }
        public DbSet<UnsafeIngredient> UnsafeIngredients { get; set; }  
        public DbSet<Supplement> Supplements { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealIngredient> MealIngredients { get; set; }
        public DbSet<EntryMeal> EntryMeals { get; set; }

        public FoodDiaryContext(DbContextOptions<FoodDiaryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntrySymptom>().HasKey(es => new { es.EntryId, es.SymptomId });
            modelBuilder.Entity<EntryIngredient>().HasKey(ei => new { ei.EntryId, ei.IngredientId });
            modelBuilder.Entity<EntrySupplement>().HasKey(ei => new { ei.EntryId, ei.SupplementId });

            modelBuilder.Entity<MealIngredient>().HasKey(mi => new { mi.MealId, mi.IngredientId });
            modelBuilder.Entity<EntryMeal>().HasKey(em => new { em.EntryId, em.MealId });

            // Relationships for Meal ↔ Ingredient
            modelBuilder.Entity<MealIngredient>()
                .HasOne(mi => mi.Meal)
                .WithMany(m => m.MealIngredients)
                .HasForeignKey(mi => mi.MealId);

            modelBuilder.Entity<MealIngredient>()
                .HasOne(mi => mi.Ingredient)
                .WithMany()
                .HasForeignKey(mi => mi.IngredientId);

            // Relationships for Entry ↔ Meal
            modelBuilder.Entity<EntryMeal>()
                .HasOne(em => em.Entry)
                .WithMany(e => e.EntryMeals)
                .HasForeignKey(em => em.EntryId);

            modelBuilder.Entity<EntryMeal>()
                .HasOne(em => em.Meal)
                .WithMany(m => m.EntryMeals)
                .HasForeignKey(em => em.MealId);
        }
    }
}
