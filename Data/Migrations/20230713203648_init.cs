using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sklepix.Data.Migrations
{
	public partial class init : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "AisleEntity",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AisleEntity", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "CategoryEntity",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_CategoryEntity", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "ShelfEntity",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Number = table.Column<int>(type: "int", nullable: false),
					AisleId = table.Column<int>(type: "int", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ShelfEntity", x => x.Id);
					table.ForeignKey(
						name: "FK_ShelfEntity_AisleEntity_AisleId",
						column: x => x.AisleId,
						principalTable: "AisleEntity",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "ProductEntity",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
					Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					Count = table.Column<int>(type: "int", nullable: false),
					CategoryId = table.Column<int>(type: "int", nullable: true),
					ShelfId = table.Column<int>(type: "int", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ProductEntity", x => x.Id);
					table.ForeignKey(
						name: "FK_ProductEntity_CategoryEntity_CategoryId",
						column: x => x.CategoryId,
						principalTable: "CategoryEntity",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_ProductEntity_ShelfEntity_ShelfId",
						column: x => x.ShelfId,
						principalTable: "ShelfEntity",
						principalColumn: "Id");
				});

			migrationBuilder.InsertData(
				table: "AisleEntity",
				columns: new[] { "Id", "Name" },
				values: new object[,]
				{
					{ 1, "Warzywa i owoce" },
					{ 2, "Napoje" },
					{ 3, "Pieczywo" }
				});

			migrationBuilder.InsertData(
				table: "CategoryEntity",
				columns: new[] { "Id", "Name" },
				values: new object[,]
				{
					{ 1, "Warzywa" },
					{ 2, "Owoce" },
					{ 3, "Napoje" },
					{ 4, "Pieczywo" }
				});

			migrationBuilder.CreateIndex(
				name: "IX_ProductEntity_CategoryId",
				table: "ProductEntity",
				column: "CategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_ProductEntity_ShelfId",
				table: "ProductEntity",
				column: "ShelfId");

			migrationBuilder.CreateIndex(
				name: "IX_ShelfEntity_AisleId",
				table: "ShelfEntity",
				column: "AisleId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "ProductEntity");

			migrationBuilder.DropTable(
				name: "CategoryEntity");

			migrationBuilder.DropTable(
				name: "ShelfEntity");

			migrationBuilder.DropTable(
				name: "AisleEntity");
		}
	}
}
