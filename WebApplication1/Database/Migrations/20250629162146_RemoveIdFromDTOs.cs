using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsNotes.Database.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIdFromDTOs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                table: "Exercises");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationInMinutes",
                table: "Exercises",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
