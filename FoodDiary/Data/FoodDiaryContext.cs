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

        public FoodDiaryContext(DbContextOptions<FoodDiaryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EntrySymptom>().HasKey(es => new { es.EntryId, es.SymptomId });
            modelBuilder.Entity<EntryIngredient>().HasKey(ei => new { ei.EntryId, ei.IngredientId });
            modelBuilder.Entity<EntrySupplement>().HasKey(ei => new { ei.EntryId, ei.SupplementId });
        }
    }
}
