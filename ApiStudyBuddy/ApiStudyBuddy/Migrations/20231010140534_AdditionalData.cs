using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiStudyBuddy.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserDecks",
                columns: new[] { "UserDeckId", "DeckId", "UserId" },
                values: new object[,]
                {
                    { 2, 2, 1 },
                    { 3, 3, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserDecks",
                keyColumn: "UserDeckId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserDecks",
                keyColumn: "UserDeckId",
                keyValue: 3);
        }
    }
}
