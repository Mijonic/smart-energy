using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartEnergy.MicroserviceAPI.Migrations
{
    public partial class removeddeviceusage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceUsages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceUsages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceID = table.Column<int>(type: "int", nullable: false),
                    IncidentID = table.Column<int>(type: "int", nullable: true),
                    SafetyDocumentID = table.Column<int>(type: "int", nullable: true),
                    WorkPlanID = table.Column<int>(type: "int", nullable: true),
                    WorkRequestID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceUsages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DeviceUsages_Incidents_IncidentID",
                        column: x => x.IncidentID,
                        principalTable: "Incidents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceUsages_SafetyDocuments_SafetyDocumentID",
                        column: x => x.SafetyDocumentID,
                        principalTable: "SafetyDocuments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceUsages_WorkPlans_WorkPlanID",
                        column: x => x.WorkPlanID,
                        principalTable: "WorkPlans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceUsages_WorkRequests_WorkRequestID",
                        column: x => x.WorkRequestID,
                        principalTable: "WorkRequests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceUsages_IncidentID",
                table: "DeviceUsages",
                column: "IncidentID");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceUsages_SafetyDocumentID",
                table: "DeviceUsages",
                column: "SafetyDocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceUsages_WorkPlanID",
                table: "DeviceUsages",
                column: "WorkPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceUsages_WorkRequestID",
                table: "DeviceUsages",
                column: "WorkRequestID");
        }
    }
}
