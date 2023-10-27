using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoReMusic.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class kindpropertyaddedtoinstrument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Kind",
                table: "Instruments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kind",
                table: "Instruments");
        }
    }
}
