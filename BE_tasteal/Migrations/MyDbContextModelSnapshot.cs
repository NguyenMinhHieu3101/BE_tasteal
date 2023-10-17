﻿// <auto-generated />
using System;
using BE_tasteal.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BE_tasteal.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BE_tasteal.Entity.Entity.AccountEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.Cart_ItemEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("account_id")
                        .HasColumnType("int");

                    b.Property<int>("amount")
                        .HasColumnType("int");

                    b.Property<int?>("ingredient_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("account_id");

                    b.HasIndex("ingredient_id");

                    b.ToTable("Cart_Item");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.CommentEntity", b =>
                {
                    b.Property<int>("comment_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("account_id")
                        .HasColumnType("int");

                    b.Property<string>("comment")
                        .HasColumnType("text");

                    b.Property<int>("recipe_id")
                        .HasColumnType("int");

                    b.HasKey("comment_id");

                    b.HasIndex("account_id");

                    b.HasIndex("recipe_id");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.CookBookEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("owner")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("owner");

                    b.ToTable("CookBook");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.CookBook_RecipeEntity", b =>
                {
                    b.Property<int>("cook_book_id")
                        .HasColumnType("int");

                    b.Property<int>("recipe_id")
                        .HasColumnType("int");

                    b.HasKey("cook_book_id", "recipe_id");

                    b.HasIndex("recipe_id");

                    b.ToTable("CookBook_Recipe");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.IngredientEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("image")
                        .HasColumnType("text");

                    b.Property<bool>("isLiquid")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.Property<int?>("nutrition_info_id")
                        .HasColumnType("int");

                    b.Property<decimal>("ratio")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int?>("type_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("nutrition_info_id");

                    b.HasIndex("type_id");

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.Ingredient_TypeEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar");

                    b.HasKey("id");

                    b.ToTable("Ingredient_Type");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.LoaiSanPhamEntity", b =>
                {
                    b.Property<int>("MaLoaiSanPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TenLoaiSanPham")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("MaLoaiSanPham");

                    b.ToTable("LoaiSanPham");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.Nutrition_InfoEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal?>("calcium")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal?>("calories")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal?>("carbohydrates")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal?>("cholesterol")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal?>("fat")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal?>("fiber")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal?>("iron")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal?>("potassium")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal?>("protein")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal?>("saturated_fat")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal?>("sodium")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal?>("sugars")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal?>("trans_fat")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal?>("vitaminD")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("id");

                    b.ToTable("Nutrition_Info");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.OccasionEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("start_at")
                        .HasColumnType("datetime(6)");

                    b.HasKey("id");

                    b.ToTable("Occasion");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.Pantry_ItemEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("account_id")
                        .HasColumnType("int");

                    b.Property<int>("amount")
                        .HasColumnType("int");

                    b.Property<int?>("ingredient_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("account_id");

                    b.HasIndex("ingredient_id");

                    b.ToTable("Pantry_Item");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.PlanEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("account_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("recipe_id")
                        .HasColumnType("int");

                    b.Property<int>("serving_size")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("account_id");

                    b.HasIndex("recipe_id");

                    b.ToTable("Plan");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.RatingEntity", b =>
                {
                    b.Property<int>("recipe_id")
                        .HasColumnType("int");

                    b.Property<int>("account_id")
                        .HasColumnType("int");

                    b.Property<int>("rating")
                        .HasColumnType("int");

                    b.HasKey("recipe_id", "account_id");

                    b.HasIndex("account_id");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.RecipeEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("active_time")
                        .HasColumnType("time(6)");

                    b.Property<int?>("author")
                        .HasColumnType("int");

                    b.Property<string>("author_note")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("image")
                        .HasColumnType("text");

                    b.Property<string>("introduction")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<bool>("is_private")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("nutrition_info_id")
                        .HasColumnType("int");

                    b.Property<float>("rating")
                        .HasColumnType("float");

                    b.Property<int>("serving_size")
                        .HasColumnType("int");

                    b.Property<TimeSpan?>("totalTime")
                        .HasColumnType("time(6)");

                    b.HasKey("id");

                    b.HasIndex("author");

                    b.HasIndex("nutrition_info_id");

                    b.ToTable("Recipe");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.Recipe_DirectionEntity", b =>
                {
                    b.Property<int>("recipe_id")
                        .HasColumnType("int");

                    b.Property<int>("step")
                        .HasColumnType("int");

                    b.Property<string>("direction")
                        .HasColumnType("text");

                    b.Property<string>("image")
                        .HasColumnType("text");

                    b.HasKey("recipe_id", "step");

                    b.ToTable("Recipe_Direction");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.Recipe_ImageEntity", b =>
                {
                    b.Property<int>("recipe_image_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("image")
                        .HasColumnType("text");

                    b.Property<int>("recipe_id")
                        .HasColumnType("int");

                    b.HasKey("recipe_image_id");

                    b.HasIndex("recipe_id");

                    b.ToTable("Recipe_Image");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.Recipe_IngredientEntity", b =>
                {
                    b.Property<int>("recipe_id")
                        .HasColumnType("int");

                    b.Property<int>("ingredient_id")
                        .HasColumnType("int");

                    b.Property<decimal>("amount")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<bool>("is_required")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("note")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("recipe_id", "ingredient_id");

                    b.HasIndex("ingredient_id");

                    b.ToTable("Recipe_Ingredient");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.Recipe_OccasionEntity", b =>
                {
                    b.Property<int>("recipe_id")
                        .HasColumnType("int");

                    b.Property<int>("occasion_id")
                        .HasColumnType("int");

                    b.HasKey("recipe_id", "occasion_id");

                    b.HasIndex("occasion_id");

                    b.ToTable("recipe_OccasionEntities");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.SanPhamEntity", b =>
                {
                    b.Property<int>("MaSanPham")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("MaLoaiSanPham")
                        .HasColumnType("int");

                    b.Property<string>("TenSanPham")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("MaSanPham");

                    b.HasIndex("MaLoaiSanPham");

                    b.ToTable("SanPham");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.Cart_ItemEntity", b =>
                {
                    b.HasOne("BE_tasteal.Entity.Entity.AccountEntity", "Account")
                        .WithMany()
                        .HasForeignKey("account_id");

                    b.HasOne("BE_tasteal.Entity.Entity.IngredientEntity", "Ingredient")
                        .WithMany()
                        .HasForeignKey("ingredient_id");

                    b.Navigation("Account");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.CommentEntity", b =>
                {
                    b.HasOne("BE_tasteal.Entity.Entity.AccountEntity", "Account")
                        .WithMany()
                        .HasForeignKey("account_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BE_tasteal.Entity.Entity.RecipeEntity", "Recipe")
                        .WithMany()
                        .HasForeignKey("recipe_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.CookBookEntity", b =>
                {
                    b.HasOne("BE_tasteal.Entity.Entity.AccountEntity", "account")
                        .WithMany()
                        .HasForeignKey("owner");

                    b.Navigation("account");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.CookBook_RecipeEntity", b =>
                {
                    b.HasOne("BE_tasteal.Entity.Entity.CookBookEntity", "cook_book")
                        .WithMany()
                        .HasForeignKey("cook_book_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BE_tasteal.Entity.Entity.RecipeEntity", "RecipeEntity")
                        .WithMany()
                        .HasForeignKey("recipe_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RecipeEntity");

                    b.Navigation("cook_book");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.IngredientEntity", b =>
                {
                    b.HasOne("BE_tasteal.Entity.Entity.Nutrition_InfoEntity", "nutrition_info")
                        .WithMany()
                        .HasForeignKey("nutrition_info_id");

                    b.HasOne("BE_tasteal.Entity.Entity.Ingredient_TypeEntity", "ingredient_type")
                        .WithMany()
                        .HasForeignKey("type_id");

                    b.Navigation("ingredient_type");

                    b.Navigation("nutrition_info");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.Pantry_ItemEntity", b =>
                {
                    b.HasOne("BE_tasteal.Entity.Entity.AccountEntity", "account")
                        .WithMany()
                        .HasForeignKey("account_id");

                    b.HasOne("BE_tasteal.Entity.Entity.IngredientEntity", "IngredientEntity")
                        .WithMany()
                        .HasForeignKey("ingredient_id");

                    b.Navigation("IngredientEntity");

                    b.Navigation("account");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.PlanEntity", b =>
                {
                    b.HasOne("BE_tasteal.Entity.Entity.AccountEntity", "AccountEntity")
                        .WithMany()
                        .HasForeignKey("account_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BE_tasteal.Entity.Entity.RecipeEntity", "RecipeEntity")
                        .WithMany()
                        .HasForeignKey("recipe_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountEntity");

                    b.Navigation("RecipeEntity");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.RatingEntity", b =>
                {
                    b.HasOne("BE_tasteal.Entity.Entity.AccountEntity", "account")
                        .WithMany()
                        .HasForeignKey("account_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BE_tasteal.Entity.Entity.RecipeEntity", "recipe")
                        .WithMany()
                        .HasForeignKey("recipe_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("account");

                    b.Navigation("recipe");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.RecipeEntity", b =>
                {
                    b.HasOne("BE_tasteal.Entity.Entity.AccountEntity", "account")
                        .WithMany()
                        .HasForeignKey("author");

                    b.HasOne("BE_tasteal.Entity.Entity.Nutrition_InfoEntity", "nutrition_info")
                        .WithMany()
                        .HasForeignKey("nutrition_info_id");

                    b.Navigation("account");

                    b.Navigation("nutrition_info");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.Recipe_DirectionEntity", b =>
                {
                    b.HasOne("BE_tasteal.Entity.Entity.RecipeEntity", "RecipeEntity")
                        .WithMany()
                        .HasForeignKey("recipe_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RecipeEntity");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.Recipe_ImageEntity", b =>
                {
                    b.HasOne("BE_tasteal.Entity.Entity.RecipeEntity", "RecipeEntity")
                        .WithMany()
                        .HasForeignKey("recipe_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RecipeEntity");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.Recipe_IngredientEntity", b =>
                {
                    b.HasOne("BE_tasteal.Entity.Entity.IngredientEntity", "ingredient")
                        .WithMany()
                        .HasForeignKey("ingredient_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BE_tasteal.Entity.Entity.RecipeEntity", "recipe")
                        .WithMany()
                        .HasForeignKey("recipe_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ingredient");

                    b.Navigation("recipe");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.Recipe_OccasionEntity", b =>
                {
                    b.HasOne("BE_tasteal.Entity.Entity.OccasionEntity", "OccasionEntity")
                        .WithMany()
                        .HasForeignKey("occasion_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BE_tasteal.Entity.Entity.RecipeEntity", "RecipeEntity")
                        .WithMany()
                        .HasForeignKey("recipe_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OccasionEntity");

                    b.Navigation("RecipeEntity");
                });

            modelBuilder.Entity("BE_tasteal.Entity.Entity.SanPhamEntity", b =>
                {
                    b.HasOne("BE_tasteal.Entity.Entity.LoaiSanPhamEntity", "LoaiSanPham")
                        .WithMany()
                        .HasForeignKey("MaLoaiSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoaiSanPham");
                });
#pragma warning restore 612, 618
        }
    }
}
