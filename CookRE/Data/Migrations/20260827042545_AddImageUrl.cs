using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookRE.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "steps",
                table: "Recipes",
                newName: "Steps");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Recipes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "Steps",
                table: "Recipes",
                newName: "steps");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Recipes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
