using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HvZ.Migrations
{
    public partial class MissionLatLng : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Lat",
                table: "Missions",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Lng",
                table: "Missions",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChatTime",
                value: new DateTime(2023, 3, 23, 11, 16, 47, 459, DateTimeKind.Utc).AddTicks(458));

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 2,
                column: "ChatTime",
                value: new DateTime(2023, 3, 23, 11, 16, 47, 459, DateTimeKind.Utc).AddTicks(462));

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 3,
                column: "ChatTime",
                value: new DateTime(2023, 3, 23, 11, 16, 47, 459, DateTimeKind.Utc).AddTicks(463));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2023, 3, 23, 12, 16, 47, 459, DateTimeKind.Local).AddTicks(204));

            migrationBuilder.UpdateData(
                table: "Missions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 23, 11, 46, 47, 459, DateTimeKind.Utc).AddTicks(491), new DateTime(2023, 3, 23, 11, 16, 47, 459, DateTimeKind.Utc).AddTicks(488) });

            migrationBuilder.UpdateData(
                table: "Missions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 23, 11, 31, 47, 459, DateTimeKind.Utc).AddTicks(497), new DateTime(2023, 3, 23, 11, 16, 47, 459, DateTimeKind.Utc).AddTicks(496) });

            migrationBuilder.UpdateData(
                table: "Missions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 23, 12, 1, 47, 459, DateTimeKind.Utc).AddTicks(499), new DateTime(2023, 3, 23, 11, 16, 47, 459, DateTimeKind.Utc).AddTicks(498) });

            migrationBuilder.UpdateData(
                table: "SquadCheckIns",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 23, 12, 1, 47, 459, DateTimeKind.Utc).AddTicks(567), new DateTime(2023, 3, 23, 11, 16, 47, 459, DateTimeKind.Utc).AddTicks(567) });

            migrationBuilder.UpdateData(
                table: "SquadCheckIns",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 23, 11, 46, 47, 459, DateTimeKind.Utc).AddTicks(570), new DateTime(2023, 3, 23, 11, 16, 47, 459, DateTimeKind.Utc).AddTicks(570) });

            migrationBuilder.UpdateData(
                table: "SquadCheckIns",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 23, 11, 26, 47, 459, DateTimeKind.Utc).AddTicks(572), new DateTime(2023, 3, 23, 11, 16, 47, 459, DateTimeKind.Utc).AddTicks(571) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "Missions");

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 1,
                column: "ChatTime",
                value: new DateTime(2023, 3, 23, 8, 57, 56, 167, DateTimeKind.Utc).AddTicks(9358));

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 2,
                column: "ChatTime",
                value: new DateTime(2023, 3, 23, 8, 57, 56, 167, DateTimeKind.Utc).AddTicks(9360));

            migrationBuilder.UpdateData(
                table: "Chats",
                keyColumn: "Id",
                keyValue: 3,
                column: "ChatTime",
                value: new DateTime(2023, 3, 23, 8, 57, 56, 167, DateTimeKind.Utc).AddTicks(9361));

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "StartTime",
                value: new DateTime(2023, 3, 23, 9, 57, 56, 167, DateTimeKind.Local).AddTicks(9185));

            migrationBuilder.UpdateData(
                table: "Missions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 23, 9, 27, 56, 167, DateTimeKind.Utc).AddTicks(9374), new DateTime(2023, 3, 23, 8, 57, 56, 167, DateTimeKind.Utc).AddTicks(9371) });

            migrationBuilder.UpdateData(
                table: "Missions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 23, 9, 12, 56, 167, DateTimeKind.Utc).AddTicks(9376), new DateTime(2023, 3, 23, 8, 57, 56, 167, DateTimeKind.Utc).AddTicks(9376) });

            migrationBuilder.UpdateData(
                table: "Missions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 23, 9, 42, 56, 167, DateTimeKind.Utc).AddTicks(9378), new DateTime(2023, 3, 23, 8, 57, 56, 167, DateTimeKind.Utc).AddTicks(9377) });

            migrationBuilder.UpdateData(
                table: "SquadCheckIns",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 23, 9, 42, 56, 167, DateTimeKind.Utc).AddTicks(9420), new DateTime(2023, 3, 23, 8, 57, 56, 167, DateTimeKind.Utc).AddTicks(9420) });

            migrationBuilder.UpdateData(
                table: "SquadCheckIns",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 23, 9, 27, 56, 167, DateTimeKind.Utc).AddTicks(9422), new DateTime(2023, 3, 23, 8, 57, 56, 167, DateTimeKind.Utc).AddTicks(9422) });

            migrationBuilder.UpdateData(
                table: "SquadCheckIns",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 3, 23, 9, 7, 56, 167, DateTimeKind.Utc).AddTicks(9423), new DateTime(2023, 3, 23, 8, 57, 56, 167, DateTimeKind.Utc).AddTicks(9423) });
        }
    }
}
