using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartEnergy.Infrastructure.Migrations
{
    public partial class Client_Cascade_Delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkPlans_WorkRequests_WorkRequestID",
                table: "WorkPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkRequests_Incidents_IncidentID",
                table: "WorkRequests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "WorkPlans",
                type: "datetime2",
                nullable: true,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPlans_WorkRequests_WorkRequestID",
                table: "WorkPlans",
                column: "WorkRequestID",
                principalTable: "WorkRequests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkRequests_Incidents_IncidentID",
                table: "WorkRequests",
                column: "IncidentID",
                principalTable: "Incidents",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkPlans_WorkRequests_WorkRequestID",
                table: "WorkPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkRequests_Incidents_IncidentID",
                table: "WorkRequests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "WorkPlans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkPlans_WorkRequests_WorkRequestID",
                table: "WorkPlans",
                column: "WorkRequestID",
                principalTable: "WorkRequests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkRequests_Incidents_IncidentID",
                table: "WorkRequests",
                column: "IncidentID",
                principalTable: "Incidents",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
