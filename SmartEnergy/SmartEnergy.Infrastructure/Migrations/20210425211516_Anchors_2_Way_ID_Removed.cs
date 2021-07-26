using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartEnergy.Infrastructure.Migrations
{
    public partial class Anchors_2_Way_ID_Removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SafetyDocumentID",
                table: "StateChangeAnchors");

            migrationBuilder.DropColumn(
                name: "WorkPlanID",
                table: "StateChangeAnchors");

            migrationBuilder.DropColumn(
                name: "WorkRequestID",
                table: "StateChangeAnchors");

            migrationBuilder.DropColumn(
                name: "IncidentID",
                table: "NotificationAnchors");

            migrationBuilder.DropColumn(
                name: "SafetyDocumentID",
                table: "NotificationAnchors");

            migrationBuilder.DropColumn(
                name: "WorkPlanID",
                table: "NotificationAnchors");

            migrationBuilder.DropColumn(
                name: "WorkRequestID",
                table: "NotificationAnchors");

            migrationBuilder.DropColumn(
                name: "IncidentID",
                table: "MultimediaAnchors");

            migrationBuilder.DropColumn(
                name: "SafetyDocumentID",
                table: "MultimediaAnchors");

            migrationBuilder.DropColumn(
                name: "WorkPlanID",
                table: "MultimediaAnchors");

            migrationBuilder.DropColumn(
                name: "WorkRequestID",
                table: "MultimediaAnchors");

            migrationBuilder.DropColumn(
                name: "ResolutionID",
                table: "Incidents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SafetyDocumentID",
                table: "StateChangeAnchors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkPlanID",
                table: "StateChangeAnchors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkRequestID",
                table: "StateChangeAnchors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IncidentID",
                table: "NotificationAnchors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SafetyDocumentID",
                table: "NotificationAnchors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkPlanID",
                table: "NotificationAnchors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkRequestID",
                table: "NotificationAnchors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IncidentID",
                table: "MultimediaAnchors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SafetyDocumentID",
                table: "MultimediaAnchors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkPlanID",
                table: "MultimediaAnchors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkRequestID",
                table: "MultimediaAnchors",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResolutionID",
                table: "Incidents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
