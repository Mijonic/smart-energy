using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartEnergy.Infrastructure.Migrations
{
    public partial class Documents_2_Way_ID_Removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkPlanID",
                table: "WorkRequests");

            migrationBuilder.DropColumn(
                name: "WorkRequestID",
                table: "Incidents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkPlanID",
                table: "WorkRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkRequestID",
                table: "Incidents",
                type: "int",
                nullable: true);
        }
    }
}
