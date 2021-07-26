using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartEnergy.LocationAPI.Migrations
{
    public partial class initlocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MorningPriority = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    NoonPriority = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    NightPriority = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
