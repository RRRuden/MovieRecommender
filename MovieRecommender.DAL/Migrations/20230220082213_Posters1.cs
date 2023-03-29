using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRecommender.DAL.Migrations
{
    public partial class Posters1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genres",
                table: "Movies100K");

            migrationBuilder.DropColumn(
                name: "ImgURL",
                table: "Movies100K");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Genres",
                table: "Movies100K",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImgURL",
                table: "Movies100K",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
