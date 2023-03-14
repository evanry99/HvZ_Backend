using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HvZ.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GameState = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nw_Lat = table.Column<double>(type: "float", nullable: true),
                    Nw_Lng = table.Column<double>(type: "float", nullable: true),
                    Se_Lat = table.Column<double>(type: "float", nullable: true),
                    Se_Lng = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsPatientZero = table.Column<bool>(type: "bit", nullable: false),
                    IsHuman = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeOfDeath = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Story = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lat = table.Column<double>(type: "float", nullable: true),
                    Lng = table.Column<double>(type: "float", nullable: true),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    KillerId = table.Column<int>(type: "int", nullable: false),
                    VictimId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kills_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kills_Players_KillerId",
                        column: x => x.KillerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Kills_Players_VictimId",
                        column: x => x.VictimId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "EndTime", "GameState", "Name", "Nw_Lat", "Nw_Lng", "Se_Lat", "Se_Lng", "StartTime" },
                values: new object[,]
                {
                    { 1, "Human vs Zombie fun", null, "Registration", "First Game", 40.753, 73.983000000000004, null, null, new DateTime(2023, 3, 14, 14, 51, 29, 82, DateTimeKind.Local).AddTicks(623) },
                    { 2, "Very fun game join plz!", null, "In Progress", "Second Game", null, null, 33.924900000000001, 18.424099999999999, new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "IsAdmin", "LastName" },
                values: new object[,]
                {
                    { 1, "Håvard", false, "Madland" },
                    { 2, "An", false, "Nguyen" },
                    { 3, "Vilhelm", false, "Assersen" }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "BiteCode", "GameId", "IsHuman", "IsPatientZero", "UserId" },
                values: new object[] { 1, "231233", 1, false, true, 1 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "BiteCode", "GameId", "IsHuman", "IsPatientZero", "UserId" },
                values: new object[] { 2, "112334", 1, true, false, 2 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "BiteCode", "GameId", "IsHuman", "IsPatientZero", "UserId" },
                values: new object[] { 3, "928475", 2, true, false, 3 });

            migrationBuilder.InsertData(
                table: "Kills",
                columns: new[] { "Id", "GameId", "KillerId", "Lat", "Lng", "Story", "TimeOfDeath", "VictimId" },
                values: new object[] { 1, 1, 1, 33.321300000000001, 24.222999999999999, "Stabbed him very hard", new DateTime(2023, 3, 9, 6, 30, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.InsertData(
                table: "Kills",
                columns: new[] { "Id", "GameId", "KillerId", "Lat", "Lng", "Story", "TimeOfDeath", "VictimId" },
                values: new object[] { 2, 2, 1, 23.3828, 82.992000000000004, "Shot him in the face", new DateTime(2023, 3, 9, 6, 30, 30, 0, DateTimeKind.Unspecified), 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Kills_GameId",
                table: "Kills",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Kills_KillerId",
                table: "Kills",
                column: "KillerId");

            migrationBuilder.CreateIndex(
                name: "IX_Kills_VictimId",
                table: "Kills",
                column: "VictimId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameId",
                table: "Players",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserId",
                table: "Players",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kills");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
