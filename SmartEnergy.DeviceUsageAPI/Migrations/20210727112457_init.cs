using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartEnergy.DeviceUsageAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceUsage",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncidentID = table.Column<int>(type: "int", nullable: true),
                    WorkRequestID = table.Column<int>(type: "int", nullable: true),
                    WorkPlanID = table.Column<int>(type: "int", nullable: true),
                    SafetyDocumentID = table.Column<int>(type: "int", nullable: true),
                    DeviceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceUsage", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceUsage");
        }
    }
}
