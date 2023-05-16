using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FilmLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    FilmType = table.Column<int>(type: "INTEGER", nullable: false),
                    FilmStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Genre = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FilmId = table.Column<int>(type: "INTEGER", nullable: false),
                    Progress = table.Column<int>(type: "INTEGER", nullable: false),
                    ListItemStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FinishDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FilmListId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListItems_FilmLists_FilmListId",
                        column: x => x.FilmListId,
                        principalTable: "FilmLists",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ListItems_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Films",
                columns: new[] { "Id", "Description", "FilmStatus", "FilmType", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, "Over the course of several years, two convicts form a friendship, seeking consolation and, eventually, redemption through basic compassion.", 2, 1, 256, new DateTime(1994, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Shawshank Redemption" },
                    { 2, "Don Vito Corleone, head of a mafia family, decides to hand over his empire to his youngest son Michael. However, his decision unintentionally puts the lives of his loved ones in grave danger.", 2, 1, 320, new DateTime(1972, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Godfather" },
                    { 3, "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.", 2, 1, 321, new DateTime(2008, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Dark Knight" },
                    { 4, "The jury in a New York City murder trial is frustrated by a single member whose skeptical caution forces them to more carefully consider the evidence before jumping to a hasty verdict.", 2, 1, 320, new DateTime(1957, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "12 Angry Men" },
                    { 5, "In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.", 2, 1, 8464, new DateTime(1993, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Schindler's List" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListItems_FilmId",
                table: "ListItems",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_ListItems_FilmListId",
                table: "ListItems",
                column: "FilmListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListItems");

            migrationBuilder.DropTable(
                name: "FilmLists");

            migrationBuilder.DropTable(
                name: "Films");
        }
    }
}
