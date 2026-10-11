using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieControllerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddWatchAndFavorites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Favorites",
                table: "Movies",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WatchedMovie",
                table: "Movies",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favorites",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "WatchedMovie",
                table: "Movies");
        }
    }
}
