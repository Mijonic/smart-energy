using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartEnergy.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Crews",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrewName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crews", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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

            migrationBuilder.CreateTable(
                name: "MultimediaAnchors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncidentID = table.Column<int>(type: "int", nullable: true),
                    WorkRequestID = table.Column<int>(type: "int", nullable: true),
                    WorkPlanID = table.Column<int>(type: "int", nullable: true),
                    SafetyDocumentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultimediaAnchors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NotificationAnchors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncidentID = table.Column<int>(type: "int", nullable: true),
                    WorkRequestID = table.Column<int>(type: "int", nullable: true),
                    WorkPlanID = table.Column<int>(type: "int", nullable: true),
                    SafetyDocumentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationAnchors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowErrors = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ShowWarnings = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ShowInfo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ShowSuccess = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ShowNonRequiredFields = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StateChangeAnchors",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkRequestID = table.Column<int>(type: "int", nullable: true),
                    WorkPlanID = table.Column<int>(type: "int", nullable: true),
                    SafetyDocumentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateChangeAnchors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Devices_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CrewID = table.Column<int>(type: "int", nullable: true),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    ConsumerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Crews_CrewID",
                        column: x => x.CrewID,
                        principalTable: "Crews",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MultimediaAttachments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MultimediaAnchorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultimediaAttachments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MultimediaAttachments_MultimediaAnchors_MultimediaAnchorID",
                        column: x => x.MultimediaAnchorID,
                        principalTable: "MultimediaAnchors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Icons",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SettingsID = table.Column<int>(type: "int", nullable: true),
                    IconType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icons", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Icons_Settings_SettingsID",
                        column: x => x.SettingsID,
                        principalTable: "Settings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Consumers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AccountID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    LocationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Consumers_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consumers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    Confirmed = table.Column<bool>(type: "bit", nullable: false),
                    ETA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ATA = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ETR = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IncidentDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkBeginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VoltageLevel = table.Column<double>(type: "float", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ResolutionID = table.Column<int>(type: "int", nullable: false),
                    CrewID = table.Column<int>(type: "int", nullable: true),
                    MultimediaAnchorID = table.Column<int>(type: "int", nullable: true),
                    NotificationAnchorID = table.Column<int>(type: "int", nullable: true),
                    WorkRequestID = table.Column<int>(type: "int", nullable: true),
                    WorkType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncidentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Incidents_Crews_CrewID",
                        column: x => x.CrewID,
                        principalTable: "Crews",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incidents_MultimediaAnchors_MultimediaAnchorID",
                        column: x => x.MultimediaAnchorID,
                        principalTable: "MultimediaAnchors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incidents_NotificationAnchors_NotificationAnchorID",
                        column: x => x.NotificationAnchorID,
                        principalTable: "NotificationAnchors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Incidents_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    NotificationAnchorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationAnchors_NotificationAnchorID",
                        column: x => x.NotificationAnchorID,
                        principalTable: "NotificationAnchors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StateChangeHistories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateChangeAnchorID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateChangeHistories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StateChangeHistories_StateChangeAnchors_StateChangeAnchorID",
                        column: x => x.StateChangeAnchorID,
                        principalTable: "StateChangeAnchors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StateChangeHistories_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Calls",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CallReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hazard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    ConsumerID = table.Column<int>(type: "int", nullable: true),
                    IncidentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calls", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Calls_Consumers_ConsumerID",
                        column: x => x.ConsumerID,
                        principalTable: "Consumers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calls_Incidents_IncidentID",
                        column: x => x.IncidentID,
                        principalTable: "Incidents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Calls_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resolutions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cause = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subcause = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Construction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncidentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resolutions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Resolutions_Incidents_IncidentID",
                        column: x => x.IncidentID,
                        principalTable: "Incidents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkRequests",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Purpose = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEmergency = table.Column<bool>(type: "bit", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    IncidentID = table.Column<int>(type: "int", nullable: false),
                    MultimediaAnchorID = table.Column<int>(type: "int", nullable: true),
                    StateChangeAnchorID = table.Column<int>(type: "int", nullable: true),
                    NotificationAnchorID = table.Column<int>(type: "int", nullable: true),
                    WorkPlanID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkRequests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkRequests_Incidents_IncidentID",
                        column: x => x.IncidentID,
                        principalTable: "Incidents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkRequests_MultimediaAnchors_MultimediaAnchorID",
                        column: x => x.MultimediaAnchorID,
                        principalTable: "MultimediaAnchors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkRequests_NotificationAnchors_NotificationAnchorID",
                        column: x => x.NotificationAnchorID,
                        principalTable: "NotificationAnchors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkRequests_StateChangeAnchors_StateChangeAnchorID",
                        column: x => x.StateChangeAnchorID,
                        principalTable: "StateChangeAnchors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkRequests_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkPlans",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Purpose = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    MultimediaAnchorID = table.Column<int>(type: "int", nullable: true),
                    StateChangeAnchorID = table.Column<int>(type: "int", nullable: true),
                    NotificationAnchorID = table.Column<int>(type: "int", nullable: true),
                    WorkRequestID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPlans", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkPlans_MultimediaAnchors_MultimediaAnchorID",
                        column: x => x.MultimediaAnchorID,
                        principalTable: "MultimediaAnchors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkPlans_NotificationAnchors_NotificationAnchorID",
                        column: x => x.NotificationAnchorID,
                        principalTable: "NotificationAnchors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkPlans_StateChangeAnchors_StateChangeAnchorID",
                        column: x => x.StateChangeAnchorID,
                        principalTable: "StateChangeAnchors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkPlans_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkPlans_WorkRequests_WorkRequestID",
                        column: x => x.WorkRequestID,
                        principalTable: "WorkRequests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsExecuted = table.Column<bool>(type: "bit", nullable: false),
                    DeviceID = table.Column<int>(type: "int", nullable: false),
                    WorkPlanID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Instructions_Devices_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Devices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instructions_WorkPlans_WorkPlanID",
                        column: x => x.WorkPlanID,
                        principalTable: "WorkPlans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SafetyDocuments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Details = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    OperationCompleted = table.Column<bool>(type: "bit", nullable: false),
                    TagsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    GroundingRemoved = table.Column<bool>(type: "bit", nullable: false),
                    Ready = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    MultimediaAnchorID = table.Column<int>(type: "int", nullable: true),
                    StateChangeAnchorID = table.Column<int>(type: "int", nullable: true),
                    NotificationAnchorID = table.Column<int>(type: "int", nullable: true),
                    WorkPlanID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyDocuments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SafetyDocuments_MultimediaAnchors_MultimediaAnchorID",
                        column: x => x.MultimediaAnchorID,
                        principalTable: "MultimediaAnchors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SafetyDocuments_NotificationAnchors_NotificationAnchorID",
                        column: x => x.NotificationAnchorID,
                        principalTable: "NotificationAnchors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SafetyDocuments_StateChangeAnchors_StateChangeAnchorID",
                        column: x => x.StateChangeAnchorID,
                        principalTable: "StateChangeAnchors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SafetyDocuments_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SafetyDocuments_WorkPlans_WorkPlanID",
                        column: x => x.WorkPlanID,
                        principalTable: "WorkPlans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceUsages",
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
                    table.PrimaryKey("PK_DeviceUsages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DeviceUsages_Devices_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Devices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Calls_ConsumerID",
                table: "Calls",
                column: "ConsumerID");

            migrationBuilder.CreateIndex(
                name: "IX_Calls_IncidentID",
                table: "Calls",
                column: "IncidentID");

            migrationBuilder.CreateIndex(
                name: "IX_Calls_LocationID",
                table: "Calls",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_LocationID",
                table: "Consumers",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Consumers_UserID",
                table: "Consumers",
                column: "UserID",
                unique: true,
                filter: "[UserID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_LocationID",
                table: "Devices",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceUsages_DeviceID",
                table: "DeviceUsages",
                column: "DeviceID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Icons_SettingsID",
                table: "Icons",
                column: "SettingsID");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_CrewID",
                table: "Incidents",
                column: "CrewID");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_MultimediaAnchorID",
                table: "Incidents",
                column: "MultimediaAnchorID",
                unique: true,
                filter: "[MultimediaAnchorID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_NotificationAnchorID",
                table: "Incidents",
                column: "NotificationAnchorID",
                unique: true,
                filter: "[NotificationAnchorID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_UserID",
                table: "Incidents",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_DeviceID",
                table: "Instructions",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_WorkPlanID",
                table: "Instructions",
                column: "WorkPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_MultimediaAttachments_MultimediaAnchorID",
                table: "MultimediaAttachments",
                column: "MultimediaAnchorID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationAnchorID",
                table: "Notifications",
                column: "NotificationAnchorID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserID",
                table: "Notifications",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Resolutions_IncidentID",
                table: "Resolutions",
                column: "IncidentID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SafetyDocuments_MultimediaAnchorID",
                table: "SafetyDocuments",
                column: "MultimediaAnchorID",
                unique: true,
                filter: "[MultimediaAnchorID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyDocuments_NotificationAnchorID",
                table: "SafetyDocuments",
                column: "NotificationAnchorID",
                unique: true,
                filter: "[NotificationAnchorID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyDocuments_StateChangeAnchorID",
                table: "SafetyDocuments",
                column: "StateChangeAnchorID",
                unique: true,
                filter: "[StateChangeAnchorID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyDocuments_UserID",
                table: "SafetyDocuments",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_SafetyDocuments_WorkPlanID",
                table: "SafetyDocuments",
                column: "WorkPlanID");

            migrationBuilder.CreateIndex(
                name: "IX_StateChangeHistories_StateChangeAnchorID",
                table: "StateChangeHistories",
                column: "StateChangeAnchorID");

            migrationBuilder.CreateIndex(
                name: "IX_StateChangeHistories_UserID",
                table: "StateChangeHistories",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CrewID",
                table: "Users",
                column: "CrewID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LocationID",
                table: "Users",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlans_MultimediaAnchorID",
                table: "WorkPlans",
                column: "MultimediaAnchorID",
                unique: true,
                filter: "[MultimediaAnchorID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlans_NotificationAnchorID",
                table: "WorkPlans",
                column: "NotificationAnchorID",
                unique: true,
                filter: "[NotificationAnchorID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlans_StateChangeAnchorID",
                table: "WorkPlans",
                column: "StateChangeAnchorID",
                unique: true,
                filter: "[StateChangeAnchorID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlans_UserID",
                table: "WorkPlans",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkPlans_WorkRequestID",
                table: "WorkPlans",
                column: "WorkRequestID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkRequests_IncidentID",
                table: "WorkRequests",
                column: "IncidentID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkRequests_MultimediaAnchorID",
                table: "WorkRequests",
                column: "MultimediaAnchorID",
                unique: true,
                filter: "[MultimediaAnchorID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkRequests_NotificationAnchorID",
                table: "WorkRequests",
                column: "NotificationAnchorID",
                unique: true,
                filter: "[NotificationAnchorID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkRequests_StateChangeAnchorID",
                table: "WorkRequests",
                column: "StateChangeAnchorID",
                unique: true,
                filter: "[StateChangeAnchorID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WorkRequests_UserID",
                table: "WorkRequests",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calls");

            migrationBuilder.DropTable(
                name: "DeviceUsages");

            migrationBuilder.DropTable(
                name: "Icons");

            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "MultimediaAttachments");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Resolutions");

            migrationBuilder.DropTable(
                name: "StateChangeHistories");

            migrationBuilder.DropTable(
                name: "Consumers");

            migrationBuilder.DropTable(
                name: "SafetyDocuments");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "WorkPlans");

            migrationBuilder.DropTable(
                name: "WorkRequests");

            migrationBuilder.DropTable(
                name: "Incidents");

            migrationBuilder.DropTable(
                name: "StateChangeAnchors");

            migrationBuilder.DropTable(
                name: "MultimediaAnchors");

            migrationBuilder.DropTable(
                name: "NotificationAnchors");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Crews");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
