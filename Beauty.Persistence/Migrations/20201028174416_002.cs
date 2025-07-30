using Microsoft.EntityFrameworkCore.Migrations;

namespace Beauty.Persistence.Migrations
{
    public partial class _002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHoliday",
                schema: "Setting",
                table: "CalendarDates",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHoliday",
                schema: "Setting",
                table: "CalendarDates");
        }
    }
}
