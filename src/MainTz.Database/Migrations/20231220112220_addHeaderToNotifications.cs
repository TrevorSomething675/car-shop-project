using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainTz.Database.Migrations
{
    /// <inheritdoc />
    public partial class addHeaderToNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Header",
                table: "Notifications",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Header",
                table: "Notifications");
        }
    }
}
