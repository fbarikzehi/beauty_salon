using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Beauty.Persistence.Migrations
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Account");

            migrationBuilder.EnsureSchema(
                name: "Application");

            migrationBuilder.EnsureSchema(
                name: "Appointment");

            migrationBuilder.EnsureSchema(
                name: "Customer");

            migrationBuilder.EnsureSchema(
                name: "Financial");

            migrationBuilder.EnsureSchema(
                name: "Line");

            migrationBuilder.EnsureSchema(
                name: "Personnel");

            migrationBuilder.EnsureSchema(
                name: "Product");

            migrationBuilder.EnsureSchema(
                name: "Salon");

            migrationBuilder.EnsureSchema(
                name: "Service");

            migrationBuilder.EnsureSchema(
                name: "Setting");

            migrationBuilder.EnsureSchema(
                name: "Sms");

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    Controller = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Icon = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    SubPermissionId = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Permissions_SubPermissionId",
                        column: x => x.SubPermissionId,
                        principalSchema: "Account",
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    PersianTitle = table.Column<string>(nullable: true),
                    HomeUrl = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    HashedPassword = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    DeviceId = table.Column<string>(nullable: true),
                    DeviceType = table.Column<int>(nullable: false),
                    Secret = table.Column<string>(nullable: true),
                    IsLocked = table.Column<bool>(nullable: false),
                    LockDescription = table.Column<string>(nullable: true),
                    LockDateTime = table.Column<DateTime>(nullable: true),
                    LockExpirationDatetime = table.Column<DateTime>(nullable: true),
                    LastLogin = table.Column<DateTime>(nullable: true),
                    CreateSmsDateTime = table.Column<DateTime>(nullable: true),
                    InitialUserChange = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Version = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialYears",
                schema: "Financial",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    IsCurrent = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lines",
                schema: "Line",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Salons",
                schema: "Salon",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    EstablishYear = table.Column<int>(nullable: false),
                    About = table.Column<string>(nullable: true),
                    OpeningTime = table.Column<TimeSpan>(nullable: false),
                    ClosingTime = table.Column<TimeSpan>(nullable: false),
                    AppointmentPrePayment = table.Column<bool>(nullable: false),
                    AppointmentPrePaymentPerecnt = table.Column<float>(nullable: false),
                    AppointmentRemindingSmsSendTime = table.Column<TimeSpan>(nullable: false),
                    OvertimePay = table.Column<float>(nullable: false),
                    DefaultPersonnelServicePerecnt = table.Column<float>(nullable: false),
                    PersonnelToPersonnelSalePerecnt = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServicePackages",
                schema: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    From = table.Column<DateTime>(nullable: true),
                    To = table.Column<DateTime>(nullable: true),
                    ExtendTo = table.Column<DateTime>(nullable: true),
                    DiscountPrice = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePackages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Calendars",
                schema: "Setting",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Year = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                schema: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                schema: "Sms",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    ReceptorNumber = table.Column<string>(nullable: true),
                    SenderNumber = table.Column<string>(nullable: true),
                    MessageId = table.Column<long>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StatusText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                schema: "Sms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    AllowSend = table.Column<bool>(nullable: false),
                    BeforeHours = table.Column<int>(nullable: false),
                    AfterHours = table.Column<int>(nullable: false),
                    IsSystemMessage = table.Column<bool>(nullable: false),
                    SystemMessageType = table.Column<int>(nullable: false),
                    ReceiverType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionActions",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ActionType = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PermissionId = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionActions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "Account",
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PermissionId = table.Column<short>(nullable: false),
                    RoleId = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "Account",
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Account",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.UniqueConstraint("AK_UserRoles_UserId", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Account",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Account",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                schema: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MemberCode = table.Column<string>(nullable: true),
                    Birthdate = table.Column<DateTime>(type: "date", nullable: false),
                    Avatar = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Account",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                schema: "Personnel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Code = table.Column<int>(nullable: false),
                    Birthdate = table.Column<DateTime>(type: "date", nullable: false),
                    JobStart = table.Column<DateTime>(type: "date", nullable: false),
                    JobEnd = table.Column<DateTime>(type: "date", nullable: true),
                    CooperationType = table.Column<int>(nullable: false),
                    Salary = table.Column<float>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Account",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "Service",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    DurationMinutes = table.Column<short>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    TakeItFreeCount = table.Column<int>(nullable: false),
                    Prepayment = table.Column<float>(nullable: false),
                    LineId = table.Column<short>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Lines_LineId",
                        column: x => x.LineId,
                        principalSchema: "Line",
                        principalTable: "Lines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "Salon",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SalonId = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Salons_SalonId",
                        column: x => x.SalonId,
                        principalSchema: "Salon",
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingDateTimes",
                schema: "Salon",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SalonId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    FromTime = table.Column<TimeSpan>(nullable: false),
                    ToTime = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingDateTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingDateTimes_Salons_SalonId",
                        column: x => x.SalonId,
                        principalSchema: "Salon",
                        principalTable: "Salons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalendarDates",
                schema: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Occasion = table.Column<string>(nullable: true),
                    HolidayType = table.Column<int>(nullable: false),
                    CalendarId = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalendarDates_Calendars_CalendarId",
                        column: x => x.CalendarId,
                        principalSchema: "Setting",
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    UnitId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Units_UnitId",
                        column: x => x.UnitId,
                        principalSchema: "Setting",
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageParameters",
                schema: "Sms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Index = table.Column<string>(nullable: true),
                    SmsMessageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageParameters_Messages_SmsMessageId",
                        column: x => x.SmsMessageId,
                        principalSchema: "Sms",
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissionActions",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PermissionActionId = table.Column<short>(nullable: false),
                    RolePermissionId = table.Column<int>(nullable: false),
                    PermissionActionModelId = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissionActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissionActions_PermissionActions_PermissionActionModelId",
                        column: x => x.PermissionActionModelId,
                        principalSchema: "Account",
                        principalTable: "PermissionActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermissionActions_RolePermissions_RolePermissionId",
                        column: x => x.RolePermissionId,
                        principalSchema: "Account",
                        principalTable: "RolePermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                schema: "Appointment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CustomerProfileId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Time = table.Column<TimeSpan>(nullable: false),
                    FollowingCode = table.Column<string>(nullable: true),
                    IsCanceled = table.Column<bool>(nullable: false),
                    IsDone = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Profiles_CustomerProfileId",
                        column: x => x.CustomerProfileId,
                        principalSchema: "Customer",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "Customer",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CustomerProfileId = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Profiles_CustomerProfileId",
                        column: x => x.CustomerProfileId,
                        principalSchema: "Customer",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LinePersonnels",
                schema: "Line",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PersonnelProfileId = table.Column<Guid>(nullable: false),
                    LineId = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinePersonnels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinePersonnels_Lines_LineId",
                        column: x => x.LineId,
                        principalSchema: "Line",
                        principalTable: "Lines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LinePersonnels_Profiles_PersonnelProfileId",
                        column: x => x.PersonnelProfileId,
                        principalSchema: "Personnel",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                schema: "Personnel",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PersonnelProfileId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Time = table.Column<TimeSpan>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_Profiles_PersonnelProfileId",
                        column: x => x.PersonnelProfileId,
                        principalSchema: "Personnel",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "Personnel",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PersonnelProfileId = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Profiles_PersonnelProfileId",
                        column: x => x.PersonnelProfileId,
                        principalSchema: "Personnel",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkingTime",
                schema: "Personnel",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PersonnelProfileId = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    FromTime = table.Column<TimeSpan>(nullable: false),
                    ToTime = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingTime_Profiles_PersonnelProfileId",
                        column: x => x.PersonnelProfileId,
                        principalSchema: "Personnel",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                schema: "Personnel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ServiceId = table.Column<short>(nullable: false),
                    PersonnelProfileId = table.Column<Guid>(nullable: false),
                    Percentage = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Profiles_PersonnelProfileId",
                        column: x => x.PersonnelProfileId,
                        principalSchema: "Personnel",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Services_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "Service",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerRatings",
                schema: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ServiceId = table.Column<short>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    Rate = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerRatings_Profiles_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerRatings_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "Service",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Details",
                schema: "Service",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ServiceId = table.Column<short>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Price = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Details_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "Service",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                schema: "Service",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ServiceId = table.Column<short>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    FromDateTime = table.Column<DateTime>(nullable: false),
                    ToDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "Service",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicePackageItems",
                schema: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ServiceId = table.Column<short>(nullable: false),
                    ServicePackageId = table.Column<int>(nullable: false),
                    AfterDiscountPrice = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePackageItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicePackageItems_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "Service",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicePackageItems_ServicePackages_ServicePackageId",
                        column: x => x.ServicePackageId,
                        principalSchema: "Service",
                        principalTable: "ServicePackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageSendSchedules",
                schema: "Sms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Time = table.Column<TimeSpan>(nullable: true),
                    CalendarDateId = table.Column<int>(nullable: false),
                    SmsMessageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageSendSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageSendSchedules_CalendarDates_CalendarDateId",
                        column: x => x.CalendarDateId,
                        principalSchema: "Setting",
                        principalTable: "CalendarDates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageSendSchedules_Messages_SmsMessageId",
                        column: x => x.SmsMessageId,
                        principalSchema: "Sms",
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                schema: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ServerPath = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Product",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentServices",
                schema: "Appointment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AppointmentId = table.Column<Guid>(nullable: false),
                    ServiceId = table.Column<short>(nullable: false),
                    PersonnelProfileId = table.Column<Guid>(nullable: true),
                    DoneDateTime = table.Column<DateTime>(nullable: true),
                    DonePersonnelProfileId = table.Column<Guid>(nullable: true),
                    IsDone = table.Column<bool>(nullable: false),
                    Time = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentServices_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalSchema: "Appointment",
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentServices_Profiles_DonePersonnelProfileId",
                        column: x => x.DonePersonnelProfileId,
                        principalSchema: "Personnel",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppointmentServices_Profiles_PersonnelProfileId",
                        column: x => x.PersonnelProfileId,
                        principalSchema: "Personnel",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalSchema: "Service",
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                schema: "Appointment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AppointmentId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<float>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discounts_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalSchema: "Appointment",
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                schema: "Appointment",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AppointmentId = table.Column<Guid>(nullable: false),
                    Amount = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalSchema: "Appointment",
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentServiceDetails",
                schema: "Appointment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AppointmentServiceId = table.Column<int>(nullable: false),
                    ServiceDetailId = table.Column<short>(nullable: false),
                    Count = table.Column<float>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    TotalPrice = table.Column<float>(nullable: false),
                    ServiceDetailModelId = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentServiceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentServiceDetails_AppointmentServices_AppointmentServiceId",
                        column: x => x.AppointmentServiceId,
                        principalSchema: "Appointment",
                        principalTable: "AppointmentServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentServiceDetails_Details_ServiceDetailModelId",
                        column: x => x.ServiceDetailModelId,
                        principalSchema: "Service",
                        principalTable: "Details",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Application",
                table: "Setting",
                columns: new[] { "Id", "CreateDateTime", "CreateUser", "IsDeleted", "Version" },
                values: new object[] { (short)1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), false, "دمو 0.1" });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionActions_PermissionId",
                schema: "Account",
                table: "PermissionActions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_SubPermissionId",
                schema: "Account",
                table: "Permissions",
                column: "SubPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionActions_PermissionActionModelId",
                schema: "Account",
                table: "RolePermissionActions",
                column: "PermissionActionModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionActions_RolePermissionId",
                schema: "Account",
                table: "RolePermissionActions",
                column: "RolePermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                schema: "Account",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                schema: "Account",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Account",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CustomerProfileId",
                schema: "Appointment",
                table: "Appointments",
                column: "CustomerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentServiceDetails_AppointmentServiceId",
                schema: "Appointment",
                table: "AppointmentServiceDetails",
                column: "AppointmentServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentServiceDetails_ServiceDetailModelId",
                schema: "Appointment",
                table: "AppointmentServiceDetails",
                column: "ServiceDetailModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentServices_AppointmentId",
                schema: "Appointment",
                table: "AppointmentServices",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentServices_DonePersonnelProfileId",
                schema: "Appointment",
                table: "AppointmentServices",
                column: "DonePersonnelProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentServices_PersonnelProfileId",
                schema: "Appointment",
                table: "AppointmentServices",
                column: "PersonnelProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentServices_ServiceId",
                schema: "Appointment",
                table: "AppointmentServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_AppointmentId",
                schema: "Appointment",
                table: "Discounts",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_AppointmentId",
                schema: "Appointment",
                table: "Payment",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CustomerProfileId",
                schema: "Customer",
                table: "Contacts",
                column: "CustomerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                schema: "Customer",
                table: "Profiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LinePersonnels_LineId",
                schema: "Line",
                table: "LinePersonnels",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_LinePersonnels_PersonnelProfileId",
                schema: "Line",
                table: "LinePersonnels",
                column: "PersonnelProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_PersonnelProfileId",
                schema: "Personnel",
                table: "Attendances",
                column: "PersonnelProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PersonnelProfileId",
                schema: "Personnel",
                table: "Contacts",
                column: "PersonnelProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                schema: "Personnel",
                table: "Profiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_PersonnelProfileId",
                schema: "Personnel",
                table: "Services",
                column: "PersonnelProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceId",
                schema: "Personnel",
                table: "Services",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingTime_PersonnelProfileId",
                schema: "Personnel",
                table: "WorkingTime",
                column: "PersonnelProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                schema: "Product",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                schema: "Product",
                table: "Products",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_SalonId",
                schema: "Salon",
                table: "Contacts",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkingDateTimes_SalonId",
                schema: "Salon",
                table: "WorkingDateTimes",
                column: "SalonId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRatings_CustomerId",
                schema: "Service",
                table: "CustomerRatings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRatings_ServiceId",
                schema: "Service",
                table: "CustomerRatings",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Details_ServiceId",
                schema: "Service",
                table: "Details",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ServiceId",
                schema: "Service",
                table: "Prices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePackageItems_ServiceId",
                schema: "Service",
                table: "ServicePackageItems",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePackageItems_ServicePackageId",
                schema: "Service",
                table: "ServicePackageItems",
                column: "ServicePackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_LineId",
                schema: "Service",
                table: "Services",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarDates_CalendarId",
                schema: "Setting",
                table: "CalendarDates",
                column: "CalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageParameters_SmsMessageId",
                schema: "Sms",
                table: "MessageParameters",
                column: "SmsMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSendSchedules_CalendarDateId",
                schema: "Sms",
                table: "MessageSendSchedules",
                column: "CalendarDateId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageSendSchedules_SmsMessageId",
                schema: "Sms",
                table: "MessageSendSchedules",
                column: "SmsMessageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissionActions",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "Setting",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "AppointmentServiceDetails",
                schema: "Appointment");

            migrationBuilder.DropTable(
                name: "Discounts",
                schema: "Appointment");

            migrationBuilder.DropTable(
                name: "Payment",
                schema: "Appointment");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "Customer");

            migrationBuilder.DropTable(
                name: "FinancialYears",
                schema: "Financial");

            migrationBuilder.DropTable(
                name: "LinePersonnels",
                schema: "Line");

            migrationBuilder.DropTable(
                name: "Attendances",
                schema: "Personnel");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "Personnel");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "Personnel");

            migrationBuilder.DropTable(
                name: "WorkingTime",
                schema: "Personnel");

            migrationBuilder.DropTable(
                name: "ProductImages",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "Salon");

            migrationBuilder.DropTable(
                name: "WorkingDateTimes",
                schema: "Salon");

            migrationBuilder.DropTable(
                name: "CustomerRatings",
                schema: "Service");

            migrationBuilder.DropTable(
                name: "Prices",
                schema: "Service");

            migrationBuilder.DropTable(
                name: "ServicePackageItems",
                schema: "Service");

            migrationBuilder.DropTable(
                name: "Histories",
                schema: "Sms");

            migrationBuilder.DropTable(
                name: "MessageParameters",
                schema: "Sms");

            migrationBuilder.DropTable(
                name: "MessageSendSchedules",
                schema: "Sms");

            migrationBuilder.DropTable(
                name: "PermissionActions",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "AppointmentServices",
                schema: "Appointment");

            migrationBuilder.DropTable(
                name: "Details",
                schema: "Service");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Product");

            migrationBuilder.DropTable(
                name: "Salons",
                schema: "Salon");

            migrationBuilder.DropTable(
                name: "ServicePackages",
                schema: "Service");

            migrationBuilder.DropTable(
                name: "CalendarDates",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "Messages",
                schema: "Sms");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Account");

            migrationBuilder.DropTable(
                name: "Appointments",
                schema: "Appointment");

            migrationBuilder.DropTable(
                name: "Profiles",
                schema: "Personnel");

            migrationBuilder.DropTable(
                name: "Services",
                schema: "Service");

            migrationBuilder.DropTable(
                name: "Units",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "Calendars",
                schema: "Setting");

            migrationBuilder.DropTable(
                name: "Profiles",
                schema: "Customer");

            migrationBuilder.DropTable(
                name: "Lines",
                schema: "Line");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Account");
        }
    }
}
