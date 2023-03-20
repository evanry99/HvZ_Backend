using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HvZ.Migrations
{
    public partial class AddUsernameDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChatTime",
                value: new DateTime(2023, 3, 20, 10, 41, 26, 34, DateTimeKind.Utc).AddTicks(1079));

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 2,
                column: "ChatTime",
                value: new DateTime(2023, 3, 20, 10, 41, 26, 34, DateTimeKind.Utc).AddTicks(1082));

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 3,
                column: "ChatTime",
                value: new DateTime(2023, 3, 20, 10, 41, 26, 34, DateTimeKind.Utc).AddTicks(1083));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2023, 3, 20, 11, 41, 26, 34, DateTimeKind.Local).AddTicks(700));

            migrationBuilder.UpdateData(
                table: "Missions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 20, 11, 11, 26, 34, DateTimeKind.Utc).AddTicks(1108), new DateTime(2023, 3, 20, 10, 41, 26, 34, DateTimeKind.Utc).AddTicks(1108) });

            migrationBuilder.UpdateData(
                table: "Missions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 20, 10, 56, 26, 34, DateTimeKind.Utc).AddTicks(1113), new DateTime(2023, 3, 20, 10, 41, 26, 34, DateTimeKind.Utc).AddTicks(1112) });

            migrationBuilder.UpdateData(
                table: "Missions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 20, 11, 26, 26, 34, DateTimeKind.Utc).AddTicks(1114), new DateTime(2023, 3, 20, 10, 41, 26, 34, DateTimeKind.Utc).AddTicks(1114) });

            migrationBuilder.UpdateData(
                table: "SquadCheckIns",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 20, 11, 26, 26, 34, DateTimeKind.Utc).AddTicks(1193), new DateTime(2023, 3, 20, 10, 41, 26, 34, DateTimeKind.Utc).AddTicks(1192) });

            migrationBuilder.UpdateData(
                table: "SquadCheckIns",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 20, 11, 11, 26, 34, DateTimeKind.Utc).AddTicks(1198), new DateTime(2023, 3, 20, 10, 41, 26, 34, DateTimeKind.Utc).AddTicks(1198) });

            migrationBuilder.UpdateData(
                table: "SquadCheckIns",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 20, 10, 51, 26, 34, DateTimeKind.Utc).AddTicks(1200), new DateTime(2023, 3, 20, 10, 41, 26, 34, DateTimeKind.Utc).AddTicks(1199) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserName",
                value: "Hav");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserName",
                value: "Al");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserName",
                value: "Will");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChatTime",
                value: new DateTime(2023, 3, 15, 11, 37, 52, 637, DateTimeKind.Utc).AddTicks(9750));

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 2,
                column: "ChatTime",
                value: new DateTime(2023, 3, 15, 11, 37, 52, 637, DateTimeKind.Utc).AddTicks(9752));

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 3,
                column: "ChatTime",
                value: new DateTime(2023, 3, 15, 11, 37, 52, 637, DateTimeKind.Utc).AddTicks(9753));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2023, 3, 15, 12, 37, 52, 637, DateTimeKind.Local).AddTicks(9524));

            migrationBuilder.UpdateData(
                table: "Missions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 15, 12, 7, 52, 637, DateTimeKind.Utc).AddTicks(9768), new DateTime(2023, 3, 15, 11, 37, 52, 637, DateTimeKind.Utc).AddTicks(9767) });

            migrationBuilder.UpdateData(
                table: "Missions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 15, 11, 52, 52, 637, DateTimeKind.Utc).AddTicks(9771), new DateTime(2023, 3, 15, 11, 37, 52, 637, DateTimeKind.Utc).AddTicks(9770) });

            migrationBuilder.UpdateData(
                table: "Missions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 15, 12, 22, 52, 637, DateTimeKind.Utc).AddTicks(9772), new DateTime(2023, 3, 15, 11, 37, 52, 637, DateTimeKind.Utc).AddTicks(9771) });

            migrationBuilder.UpdateData(
                table: "SquadCheckIns",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 15, 12, 22, 52, 638, DateTimeKind.Utc).AddTicks(42), new DateTime(2023, 3, 15, 11, 37, 52, 638, DateTimeKind.Utc).AddTicks(41) });

            migrationBuilder.UpdateData(
                table: "SquadCheckIns",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 15, 12, 7, 52, 638, DateTimeKind.Utc).AddTicks(45), new DateTime(2023, 3, 15, 11, 37, 52, 638, DateTimeKind.Utc).AddTicks(45) });

            migrationBuilder.UpdateData(
                table: "SquadCheckIns",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 15, 11, 47, 52, 638, DateTimeKind.Utc).AddTicks(47), new DateTime(2023, 3, 15, 11, 37, 52, 638, DateTimeKind.Utc).AddTicks(47) });
        }
    }
}
