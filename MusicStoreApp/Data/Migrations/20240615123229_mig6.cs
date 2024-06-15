using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicStoreApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "UserPlaylists");

            migrationBuilder.AlterColumn<int>(
                name: "TotalTracks",
                table: "UserPlaylists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TotalTracks",
                table: "UserPlaylists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "UserPlaylists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
