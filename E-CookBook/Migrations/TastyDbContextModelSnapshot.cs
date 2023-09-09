﻿// <auto-generated />
using E_CookBook.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace E_CookBook.Migrations
{
    [DbContext(typeof(TastyDbContext))]
    partial class TastyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("E_CookBook.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("E_CookBook.Models.Ingredient", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("E_CookBook.Models.IngredientSpecification", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("IngredientID")
                        .HasColumnType("int");

                    b.Property<int>("QuantityMetricID")
                        .HasColumnType("int");

                    b.Property<int>("Qunatity")
                        .HasColumnType("int");

                    b.Property<int>("RecipeID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IngredientID");

                    b.HasIndex("QuantityMetricID");

                    b.HasIndex("RecipeID");

                    b.ToTable("IngredientSpecification");
                });

            modelBuilder.Entity("E_CookBook.Models.PriceCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PriceCategory");
                });

            modelBuilder.Entity("E_CookBook.Models.QuantityMetric", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("QuantityMetric");
                });

            modelBuilder.Entity("E_CookBook.Models.Recipe", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<int>("CookingTime")
                        .HasColumnType("int");

                    b.Property<string>("Instructions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Portion")
                        .HasColumnType("int");

                    b.Property<int>("PriceCategoryID")
                        .HasColumnType("int");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("PriceCategoryID");

                    b.ToTable("Recipe");
                });

            modelBuilder.Entity("E_CookBook.Models.RecipeTag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("RecipeID")
                        .HasColumnType("int");

                    b.Property<int>("TagID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("RecipeID");

                    b.HasIndex("TagID");

                    b.ToTable("RecipeTag");
                });

            modelBuilder.Entity("E_CookBook.Models.Tag", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("E_CookBook.Models.IngredientSpecification", b =>
                {
                    b.HasOne("E_CookBook.Models.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_CookBook.Models.QuantityMetric", "QuantityMetric")
                        .WithMany()
                        .HasForeignKey("QuantityMetricID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_CookBook.Models.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("QuantityMetric");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("E_CookBook.Models.Recipe", b =>
                {
                    b.HasOne("E_CookBook.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_CookBook.Models.PriceCategory", "PriceCategory")
                        .WithMany()
                        .HasForeignKey("PriceCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("PriceCategory");
                });

            modelBuilder.Entity("E_CookBook.Models.RecipeTag", b =>
                {
                    b.HasOne("E_CookBook.Models.Recipe", "Recipe")
                        .WithMany("Tags")
                        .HasForeignKey("RecipeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("E_CookBook.Models.Tag", "Tag")
                        .WithMany("Recipes")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("E_CookBook.Models.Recipe", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("E_CookBook.Models.Tag", b =>
                {
                    b.Navigation("Recipes");
                });
#pragma warning restore 612, 618
        }
    }
}