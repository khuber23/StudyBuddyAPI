using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiStudyBuddy.Migrations
{
    /// <inheritdoc />
    public partial class InitialCompositeWithUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeckGroups",
                columns: table => new
                {
                    DeckGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeckGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeckGroupDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    ReadOnly = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckGroups", x => x.DeckGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Decks",
                columns: table => new
                {
                    DeckId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeckName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeckDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    ReadOnly = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decks", x => x.DeckId);
                });

            migrationBuilder.CreateTable(
                name: "FlashCards",
                columns: table => new
                {
                    FlashCardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlashCardQuestion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlashCardQuestionImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlashCardAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlashCardAnswerImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    ReadOnly = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashCards", x => x.FlashCardId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "DeckGroupDecks",
                columns: table => new
                {
                    DeckGroupId = table.Column<int>(type: "int", nullable: false),
                    DeckId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckGroupDecks", x => new { x.DeckGroupId, x.DeckId });
                    table.ForeignKey(
                        name: "FK_DeckGroupDecks_DeckGroups_DeckGroupId",
                        column: x => x.DeckGroupId,
                        principalTable: "DeckGroups",
                        principalColumn: "DeckGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeckGroupDecks_Decks_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Decks",
                        principalColumn: "DeckId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeckFlashCards",
                columns: table => new
                {
                    DeckId = table.Column<int>(type: "int", nullable: false),
                    FlashCardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeckFlashCards", x => new { x.DeckId, x.FlashCardId });
                    table.ForeignKey(
                        name: "FK_DeckFlashCards_Decks_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Decks",
                        principalColumn: "DeckId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeckFlashCards_FlashCards_FlashCardId",
                        column: x => x.FlashCardId,
                        principalTable: "FlashCards",
                        principalColumn: "FlashCardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudySessions",
                columns: table => new
                {
                    StudySessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    DeckGroupId = table.Column<int>(type: "int", nullable: true),
                    DeckId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudySessions", x => x.StudySessionId);
                    table.ForeignKey(
                        name: "FK_StudySessions_DeckGroups_DeckGroupId",
                        column: x => x.DeckGroupId,
                        principalTable: "DeckGroups",
                        principalColumn: "DeckGroupId");
                    table.ForeignKey(
                        name: "FK_StudySessions_Decks_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Decks",
                        principalColumn: "DeckId");
                    table.ForeignKey(
                        name: "FK_StudySessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDeckGroups",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DeckGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDeckGroups", x => new { x.UserId, x.DeckGroupId });
                    table.ForeignKey(
                        name: "FK_UserDeckGroups_DeckGroups_DeckGroupId",
                        column: x => x.DeckGroupId,
                        principalTable: "DeckGroups",
                        principalColumn: "DeckGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDeckGroups_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDecks",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DeckId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDecks", x => new { x.UserId, x.DeckId });
                    table.ForeignKey(
                        name: "FK_UserDecks_Decks_DeckId",
                        column: x => x.DeckId,
                        principalTable: "Decks",
                        principalColumn: "DeckId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDecks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudySessionsFlashCards",
                columns: table => new
                {
                    StudySessionId = table.Column<int>(type: "int", nullable: false),
                    FlashCardId = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudySessionsFlashCards", x => new { x.StudySessionId, x.FlashCardId });
                    table.ForeignKey(
                        name: "FK_StudySessionsFlashCards_FlashCards_FlashCardId",
                        column: x => x.FlashCardId,
                        principalTable: "FlashCards",
                        principalColumn: "FlashCardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudySessionsFlashCards_StudySessions_StudySessionId",
                        column: x => x.StudySessionId,
                        principalTable: "StudySessions",
                        principalColumn: "StudySessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DeckGroups",
                columns: new[] { "DeckGroupId", "DeckGroupDescription", "DeckGroupName", "IsPublic", "ReadOnly" },
                values: new object[] { 1, "Solutions to commonly occurring problems in software design.", "Design Patterns", false, false });

            migrationBuilder.InsertData(
                table: "Decks",
                columns: new[] { "DeckId", "DeckDescription", "DeckName", "IsPublic", "ReadOnly" },
                values: new object[,]
                {
                    { 1, "Design patterns all about class instantiation", "Creational Design Patterns", false, false },
                    { 2, "Design patterns all about class and Object composition", "Structural Design Patterns", false, false },
                    { 3, "Design patterns all about Class's objects communication", "Behavorial Design Patterns", false, false }
                });

            migrationBuilder.InsertData(
                table: "FlashCards",
                columns: new[] { "FlashCardId", "FlashCardAnswer", "FlashCardAnswerImage", "FlashCardQuestion", "FlashCardQuestionImage", "IsPublic", "ReadOnly" },
                values: new object[,]
                {
                    { 1, "Creates an instance of several families of classes", null, "What is abstract factory", null, true, true },
                    { 2, "A class of which only a single instance can exist", null, "What is Singleton?", null, true, true },
                    { 3, "Add responsibilites to objects dynamically", null, "What is decorator?", null, true, true },
                    { 4, "A single class that represents an entire subsystem", null, "What is facade?", null, true, true },
                    { 5, "Sequentially access the elements of a collection", null, "What is iterator?", null, true, true }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "IsAdmin", "LastName", "PasswordHash", "ProfilePicture", "Username" },
                values: new object[,]
                {
                    { 1, "JohnDoe@gmail.com", "John", false, "Doe", "AQAAAAIAAYagAAAAEHkNcdcydJ6lCgu6hPLEdV8CezbujT87yOO2nMMXwe71pTX+CdelWp6WHAD+hNGN3w==", null, "JDoe1" },
                    { 2, "MaryJane@gmail.com", "Mary", false, "Jane", "AQAAAAIAAYagAAAAEFCYkHw0hLhF5AiysQpkKd5Y1DBCL0iJgPA/dQtXBzrbyuCHNqZOh8Db9rAZg1DrsA==", null, "MJane1" }
                });

            migrationBuilder.InsertData(
                table: "DeckFlashCards",
                columns: new[] { "DeckId", "FlashCardId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 5 }
                });

            migrationBuilder.InsertData(
                table: "DeckGroupDecks",
                columns: new[] { "DeckGroupId", "DeckId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "StudySessions",
                columns: new[] { "StudySessionId", "DeckGroupId", "DeckId", "EndTime", "IsCompleted", "StartTime", "UserId" },
                values: new object[] { 1, 1, 1, new DateTime(2023, 9, 11, 15, 35, 15, 0, DateTimeKind.Unspecified), false, new DateTime(2023, 9, 11, 15, 5, 15, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "UserDeckGroups",
                columns: new[] { "DeckGroupId", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "UserDecks",
                columns: new[] { "DeckId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "StudySessionsFlashCards",
                columns: new[] { "FlashCardId", "StudySessionId", "IsCorrect" },
                values: new object[,]
                {
                    { 1, 1, true },
                    { 2, 1, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeckFlashCards_FlashCardId",
                table: "DeckFlashCards",
                column: "FlashCardId");

            migrationBuilder.CreateIndex(
                name: "IX_DeckGroupDecks_DeckId",
                table: "DeckGroupDecks",
                column: "DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_StudySessions_DeckGroupId",
                table: "StudySessions",
                column: "DeckGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudySessions_DeckId",
                table: "StudySessions",
                column: "DeckId");

            migrationBuilder.CreateIndex(
                name: "IX_StudySessions_UserId",
                table: "StudySessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudySessionsFlashCards_FlashCardId",
                table: "StudySessionsFlashCards",
                column: "FlashCardId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDeckGroups_DeckGroupId",
                table: "UserDeckGroups",
                column: "DeckGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDecks_DeckId",
                table: "UserDecks",
                column: "DeckId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeckFlashCards");

            migrationBuilder.DropTable(
                name: "DeckGroupDecks");

            migrationBuilder.DropTable(
                name: "StudySessionsFlashCards");

            migrationBuilder.DropTable(
                name: "UserDeckGroups");

            migrationBuilder.DropTable(
                name: "UserDecks");

            migrationBuilder.DropTable(
                name: "FlashCards");

            migrationBuilder.DropTable(
                name: "StudySessions");

            migrationBuilder.DropTable(
                name: "DeckGroups");

            migrationBuilder.DropTable(
                name: "Decks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
