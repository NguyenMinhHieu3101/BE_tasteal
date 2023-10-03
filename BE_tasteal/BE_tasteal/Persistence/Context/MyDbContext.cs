﻿using BE_tasteal.Entity.Entity;
using Microsoft.EntityFrameworkCore;

namespace BE_tasteal.Persistence.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }

        #region DB Set
        public DbSet<SanPhamEntity> sanPhams { get; set; }
        public DbSet<LoaiSanPhamEntity> loaiSanPhams { get; set; }

        public DbSet<Measurement_UnitEntity> measurementUnit { get; set; }
        public DbSet<Recipe_IngredientEntity> recipe_Ingredient { get; set; }
        public DbSet<IngredientEntity> ingredient { get; set; }
        public DbSet<Ingredient_TypeEntity> ingredient_Type { get; set; }
        public DbSet<Nutrition_InfoEntity> nutrition_Info { get; set; }
        public DbSet<Recipe_DirectionEntity> recipe_Direction { get; set; }
        public DbSet<RecipeEntity> recipe { get; set; }
        public DbSet<AccountEntity> accountEntities { get; set; }
        public DbSet<Pantry_ItemEntity> pantry_ItemEntities { get; set; }
        public DbSet<Cart_ItemEntity> cart_ItemEntities { get; set; }
        public DbSet<Recipe_ImageEntity> recipe_ImageEntities { get; set; }
        public DbSet<CookBook_RecipeEntity> cookBook_RecipeEntities { get; set; }
        public DbSet<CookBookEntity> cookBookEntities { get; set; }
        public DbSet<RatingEntity> ratingEntities { get; set; }
        public DbSet<CommentEntity> commentEntities { get; set; }

        #endregion

        #region model creating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region add primary key
            modelBuilder.Entity<Recipe_IngredientEntity>().HasKey(p =>
                new { p.recipe_id, p.ingredient_id });

            modelBuilder.Entity<Recipe_DirectionEntity>().HasKey(p =>
               new { p.recipe_id, p.step });

            modelBuilder.Entity<CookBook_RecipeEntity>().HasKey(p =>
               new { p.cook_book_id, p.recipe_id });

            modelBuilder.Entity<RatingEntity>().HasKey(p =>
               new { p.recipe_id, p.account_id });
            #endregion

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
