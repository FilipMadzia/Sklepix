﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sklepix.Data;

#nullable disable

namespace Sklepix.Data.Migrations
{
	[DbContext(typeof(SklepixContext))]
	partial class SklepixContextModelSnapshot : ModelSnapshot
	{
		protected override void BuildModel(ModelBuilder modelBuilder)
		{
#pragma warning disable 612, 618
			modelBuilder
				.HasAnnotation("ProductVersion", "6.0.18")
				.HasAnnotation("Relational:MaxIdentifierLength", 128);

			SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

			modelBuilder.Entity("Sklepix.Data.Entities.AisleEntity", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("int");

					SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

					b.Property<string>("Name")
						.HasColumnType("nvarchar(max)");

					b.HasKey("Id");

					b.ToTable("AisleEntity");

					b.HasData(
						new
						{
							Id = 1,
							Name = "Warzywa i owoce"
						},
						new
						{
							Id = 2,
							Name = "Napoje"
						},
						new
						{
							Id = 3,
							Name = "Pieczywo"
						});
				});

			modelBuilder.Entity("Sklepix.Data.Entities.CategoryEntity", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("int");

					SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

					b.Property<string>("Name")
						.HasColumnType("nvarchar(max)");

					b.HasKey("Id");

					b.ToTable("CategoryEntity");

					b.HasData(
						new
						{
							Id = 1,
							Name = "Warzywa"
						},
						new
						{
							Id = 2,
							Name = "Owoce"
						},
						new
						{
							Id = 3,
							Name = "Napoje"
						},
						new
						{
							Id = 4,
							Name = "Pieczywo"
						});
				});

			modelBuilder.Entity("Sklepix.Data.Entities.ProductEntity", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("int");

					SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

					b.Property<int?>("CategoryId")
						.HasColumnType("int");

					b.Property<int>("Count")
						.HasColumnType("int");

					b.Property<string>("Name")
						.HasColumnType("nvarchar(max)");

					b.Property<decimal>("Price")
						.HasColumnType("decimal(18,2)");

					b.Property<int?>("ShelfId")
						.HasColumnType("int");

					b.HasKey("Id");

					b.HasIndex("CategoryId");

					b.HasIndex("ShelfId");

					b.ToTable("ProductEntity");
				});

			modelBuilder.Entity("Sklepix.Data.Entities.ShelfEntity", b =>
				{
					b.Property<int>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("int");

					SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

					b.Property<int?>("AisleId")
						.HasColumnType("int");

					b.Property<int>("Number")
						.HasColumnType("int");

					b.HasKey("Id");

					b.HasIndex("AisleId");

					b.ToTable("ShelfEntity");
				});

			modelBuilder.Entity("Sklepix.Data.Entities.ProductEntity", b =>
				{
					b.HasOne("Sklepix.Data.Entities.CategoryEntity", "Category")
						.WithMany()
						.HasForeignKey("CategoryId");

					b.HasOne("Sklepix.Data.Entities.ShelfEntity", "Shelf")
						.WithMany()
						.HasForeignKey("ShelfId");

					b.Navigation("Category");

					b.Navigation("Shelf");
				});

			modelBuilder.Entity("Sklepix.Data.Entities.ShelfEntity", b =>
				{
					b.HasOne("Sklepix.Data.Entities.AisleEntity", "Aisle")
						.WithMany()
						.HasForeignKey("AisleId");

					b.Navigation("Aisle");
				});
#pragma warning restore 612, 618
		}
	}
}
