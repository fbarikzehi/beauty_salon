using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Beauty.Persistence.Migrations
{
    public partial class _003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthdate",
                schema: "Customer",
                table: "Profiles",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "Banks",
                schema: "Setting",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUser = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cheques",
                schema: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Number = table.Column<string>(nullable: true),
                    Fee = table.Column<float>(nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    BankId = table.Column<short>(nullable: false),
                    CustomerProfileId = table.Column<Guid>(nullable: false),
                    Details = table.Column<string>(nullable: true),
                    CreateDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cheques", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cheques_Banks_BankId",
                        column: x => x.BankId,
                        principalSchema: "Setting",
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cheques_Profiles_CustomerProfileId",
                        column: x => x.CustomerProfileId,
                        principalSchema: "Customer",
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cheques_BankId",
                schema: "Customer",
                table: "Cheques",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Cheques_CustomerProfileId",
                schema: "Customer",
                table: "Cheques",
                column: "CustomerProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cheques",
                schema: "Customer");

            migrationBuilder.DropTable(
                name: "Banks",
                schema: "Setting");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthdate",
                schema: "Customer",
                table: "Profiles",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);
        }
    }
}
