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
                name: "Missions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsHumanVisible = table.Column<bool>(type: "bit", nullable: false),
                    IsZombieVisible = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Missions_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Squads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsHuman = table.Column<bool>(type: "bit", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Squads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Squads_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsHumanGlobal = table.Column<bool>(type: "bit", nullable: false),
                    IsZombieGlobal = table.Column<bool>(type: "bit", nullable: false),
                    ChatTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    SquadId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chats_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Chats_Squads_SquadId",
                        column: x => x.SquadId,
                        principalTable: "Squads",
                        principalColumn: "Id");
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

            migrationBuilder.CreateTable(
                name: "SquadMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rank = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    SquadId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SquadMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SquadMembers_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SquadMembers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SquadMembers_Squads_SquadId",
                        column: x => x.SquadId,
                        principalTable: "Squads",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SquadCheckIns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lng = table.Column<double>(type: "float", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    SquadId = table.Column<int>(type: "int", nullable: false),
                    SquadMemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SquadCheckIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SquadCheckIns_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SquadCheckIns_SquadMembers_SquadMemberId",
                        column: x => x.SquadMemberId,
                        principalTable: "SquadMembers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SquadCheckIns_Squads_SquadId",
                        column: x => x.SquadId,
                        principalTable: "Squads",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "EndTime", "GameState", "Name", "Nw_Lat", "Nw_Lng", "Se_Lat", "Se_Lng", "StartTime" },
                values: new object[,]
                {
                    { 1, "Human vs Zombie fun", null, "Registration", "First Game", 40.753, 73.983000000000004, null, null, new DateTime(2023, 3, 15, 12, 37, 52, 637, DateTimeKind.Local).AddTicks(9524) },
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
                table: "Missions",
                columns: new[] { "Id", "Description", "EndTime", "GameId", "IsHumanVisible", "IsZombieVisible", "Name", "StartTime" },
                values: new object[,]
                {
                    { 1, "Find a sock to equip yourself", new DateTime(2023, 3, 15, 12, 7, 52, 637, DateTimeKind.Utc).AddTicks(9768), 1, true, false, "Gather", new DateTime(2023, 3, 15, 11, 37, 52, 637, DateTimeKind.Utc).AddTicks(9767) },
                    { 2, "Survive the horde of zombies for 15 minutes ", new DateTime(2023, 3, 15, 11, 52, 52, 637, DateTimeKind.Utc).AddTicks(9771), 2, true, false, "Survive", new DateTime(2023, 3, 15, 11, 37, 52, 637, DateTimeKind.Utc).AddTicks(9770) },
                    { 3, "Kill 5 humans within 45 minutes ", new DateTime(2023, 3, 15, 12, 22, 52, 637, DateTimeKind.Utc).AddTicks(9772), 2, false, true, "Blood bath", new DateTime(2023, 3, 15, 11, 37, 52, 637, DateTimeKind.Utc).AddTicks(9771) }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "BiteCode", "GameId", "IsHuman", "IsPatientZero", "UserId" },
                values: new object[,]
                {
                    { 1, "231233", 1, false, true, 1 },
                    { 2, "112334", 1, true, false, 2 },
                    { 3, "928475", 2, true, false, 3 }
                });

            migrationBuilder.InsertData(
                table: "Squads",
                columns: new[] { "Id", "GameId", "IsHuman", "Name" },
                values: new object[,]
                {
                    { 1, 1, true, "Dream team" },
                    { 2, 1, true, "Survivors" },
                    { 3, 1, false, "Kill all humans" },
                    { 4, 2, false, "Zombie zombie" },
                    { 5, 2, true, "Win win" }
                });

            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "Id", "ChatTime", "GameId", "IsHumanGlobal", "IsZombieGlobal", "Message", "PlayerId", "SquadId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 3, 15, 11, 37, 52, 637, DateTimeKind.Utc).AddTicks(9750), 1, true, false, "One zombie close to me", 2, 1 },
                    { 2, new DateTime(2023, 3, 15, 11, 37, 52, 637, DateTimeKind.Utc).AddTicks(9752), 1, false, true, "Surround that human", 1, 2 },
                    { 3, new DateTime(2023, 3, 15, 11, 37, 52, 637, DateTimeKind.Utc).AddTicks(9753), 2, true, false, "Need help!", 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Kills",
                columns: new[] { "Id", "GameId", "KillerId", "Lat", "Lng", "Story", "TimeOfDeath", "VictimId" },
                values: new object[,]
                {
                    { 1, 1, 1, 33.321300000000001, 24.222999999999999, "Stabbed him very hard", new DateTime(2023, 3, 9, 6, 30, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 2, 2, 1, 23.3828, 82.992000000000004, "Shot him in the face", new DateTime(2023, 3, 9, 6, 30, 30, 0, DateTimeKind.Unspecified), 3 }
                });

            migrationBuilder.InsertData(
                table: "SquadMembers",
                columns: new[] { "Id", "GameId", "PlayerId", "Rank", "SquadId" },
                values: new object[,]
                {
                    { 1, 1, 2, "General", 1 },
                    { 2, 1, 3, "Soldier", 2 },
                    { 3, 2, 1, "Super Zombie", 4 }
                });

            migrationBuilder.InsertData(
                table: "SquadCheckIns",
                columns: new[] { "Id", "EndTime", "GameId", "Lat", "Lng", "SquadId", "SquadMemberId", "StartTime" },
                values: new object[] { 1, new DateTime(2023, 3, 15, 12, 22, 52, 638, DateTimeKind.Utc).AddTicks(42), 1, 55.229999999999997, 20.100999999999999, 1, 1, new DateTime(2023, 3, 15, 11, 37, 52, 638, DateTimeKind.Utc).AddTicks(41) });

            migrationBuilder.InsertData(
                table: "SquadCheckIns",
                columns: new[] { "Id", "EndTime", "GameId", "Lat", "Lng", "SquadId", "SquadMemberId", "StartTime" },
                values: new object[] { 2, new DateTime(2023, 3, 15, 12, 7, 52, 638, DateTimeKind.Utc).AddTicks(45), 1, 10.987, 40.500999999999998, 2, 2, new DateTime(2023, 3, 15, 11, 37, 52, 638, DateTimeKind.Utc).AddTicks(45) });

            migrationBuilder.InsertData(
                table: "SquadCheckIns",
                columns: new[] { "Id", "EndTime", "GameId", "Lat", "Lng", "SquadId", "SquadMemberId", "StartTime" },
                values: new object[] { 3, new DateTime(2023, 3, 15, 11, 47, 52, 638, DateTimeKind.Utc).AddTicks(47), 2, 70.566999999999993, 5.1109999999999998, 1, 3, new DateTime(2023, 3, 15, 11, 37, 52, 638, DateTimeKind.Utc).AddTicks(47) });

            migrationBuilder.CreateIndex(
                name: "IX_Chats_GameId",
                table: "Chats",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_PlayerId",
                table: "Chats",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_SquadId",
                table: "Chats",
                column: "SquadId");

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
                name: "IX_Missions_GameId",
                table: "Missions",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameId",
                table: "Players",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserId",
                table: "Players",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SquadCheckIns_GameId",
                table: "SquadCheckIns",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_SquadCheckIns_SquadId",
                table: "SquadCheckIns",
                column: "SquadId");

            migrationBuilder.CreateIndex(
                name: "IX_SquadCheckIns_SquadMemberId",
                table: "SquadCheckIns",
                column: "SquadMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_SquadMembers_GameId",
                table: "SquadMembers",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_SquadMembers_PlayerId",
                table: "SquadMembers",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SquadMembers_SquadId",
                table: "SquadMembers",
                column: "SquadId");

            migrationBuilder.CreateIndex(
                name: "IX_Squads_GameId",
                table: "Squads",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Kills");

            migrationBuilder.DropTable(
                name: "Missions");

            migrationBuilder.DropTable(
                name: "SquadCheckIns");

            migrationBuilder.DropTable(
                name: "SquadMembers");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Squads");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
