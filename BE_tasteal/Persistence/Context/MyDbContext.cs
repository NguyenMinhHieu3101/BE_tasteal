using BE_tasteal.Entity.Entity;
using Microsoft.EntityFrameworkCore;

namespace BE_tasteal.Persistence.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }

        #region DB Set
        public DbSet<Recipe_IngredientEntity> recipe_Ingredient { get; set; }
        public DbSet<IngredientEntity> IngredientEntity { get; set; }
        public DbSet<Ingredient_TypeEntity> ingredient_Type { get; set; }
        public DbSet<Nutrition_InfoEntity> nutrition_Info { get; set; }
        public DbSet<Recipe_DirectionEntity> recipe_Direction { get; set; }
        public DbSet<RecipeEntity> RecipeEntity { get; set; }
        public DbSet<AccountEntity> accountEntities { get; set; }
        public DbSet<Pantry_ItemEntity> pantry_ItemEntities { get; set; }
        public DbSet<Cart_ItemEntity> cart_ItemEntities { get; set; }
        public DbSet<CartEntity> cart { get; set; }
        public DbSet<CookBook_RecipeEntity> cookBook_RecipeEntities { get; set; }
        public DbSet<CookBookEntity> cookBookEntities { get; set; }
        public DbSet<RatingEntity> ratingEntities { get; set; }
        public DbSet<CommentEntity> commentEntities { get; set; }
        public DbSet<PlanEntity> planEntities { get; set; }
        public DbSet<Recipe_OccasionEntity> recipe_OccasionEntities { get; set; }
        public DbSet<OccasionEntity> occasionEntities { get; set; }
        public DbSet<Plan_ItemEntity> plan_ItemEntities { get; set; }
        public DbSet<PersonalCartItemEntity> personalCartItems { get; set; }
        #endregion

        #region model creating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region add primary key
            modelBuilder.Entity<Recipe_IngredientEntity>().HasKey(p =>
                new { p.recipe_id, p.ingredient_id });

            modelBuilder.Entity<Recipe_DirectionEntity>().HasKey(p =>
               new { p.recipe_id, p.step });

            modelBuilder.Entity<RatingEntity>().HasKey(p =>
               new { p.recipe_id, p.account_id });

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
