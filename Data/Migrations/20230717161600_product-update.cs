using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sklepix.Data.Migrations
{
    public partial class productupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Margin",
                table: "ProductEntity",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Margin",
                table: "ProductEntity");
        }
    }
}
