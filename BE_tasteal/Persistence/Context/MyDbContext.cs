using BE_tasteal.Entity.Entity;
using Microsoft.EntityFrameworkCore;

namespace BE_tasteal.Persistence.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }

        #region DB Set
        public DbSet<Recipe_IngredientEntity> Recipe_Ingredient { get; set; }
        public DbSet<IngredientEntity> Ingredient { get; set; }
        public DbSet<Ingredient_TypeEntity> Ingredient_Type { get; set; }
        public DbSet<Nutrition_InfoEntity> Nutrition_Info { get; set; }
        public DbSet<Recipe_DirectionEntity> Recipe_Direction { get; set; }
        public DbSet<RecipeEntity> Recipe { get; set; }
        public DbSet<AccountEntity> Account { get; set; }
        public DbSet<Pantry_ItemEntity> Pantry_Item { get; set; }
        public DbSet<Cart_ItemEntity> Cart_Item { get; set; }
        public DbSet<CartEntity> Cart { get; set; }
        public DbSet<CookBook_RecipeEntity> CookBook_Recipe { get; set; }
        public DbSet<CookBookEntity> CookBook { get; set; }
        public DbSet<RatingEntity> Rating { get; set; }
        public DbSet<CommentEntity> Comment { get; set; }
        public DbSet<PlanEntity> Plan { get; set; }
        public DbSet<Recipe_OccasionEntity> Recipe_Occasion { get; set; }
        public DbSet<OccasionEntity> Occasion { get; set; }
        public DbSet<Plan_ItemEntity> Plan_Item { get; set; }
        public DbSet<PersonalCartItemEntity> PersonalCartItems { get; set; }
        public DbSet<PantryEntity> Pantry { get; set; }
        public DbSet<KeyWordEntity> KeyWords { get; set; }
        #endregion

        #region model creating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region add primary key
            modelBuilder.Entity<Recipe_IngredientEntity>().HasKey(p =>
                new { p.recipe_id, p.ingredient_id });

            modelBuilder.Entity<Recipe_DirectionEntity>().HasKey(p =>
               new { p.recipe_id, p.step });

            modelBuilder.Entity<Cart_ItemEntity>().HasKey(p =>
                new { p.ingredient_id, p.cartId });
            #endregion

            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region db save change
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries()
           .Where(x => x.Entity is RecipeEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entityEntry in entities)
            {
                ((RecipeEntity)entityEntry.Entity).createdAt = DateTime.Now;
                if (entityEntry.State == EntityState.Modified)
                {
                    ((RecipeEntity)entityEntry.Entity).updatedAt = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}
