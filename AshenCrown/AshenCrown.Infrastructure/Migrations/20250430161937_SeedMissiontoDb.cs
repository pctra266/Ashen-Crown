using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AshenCrown.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedMissiontoDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Missions",
                columns: new[] { "Id", "Content", "IsComplete" },
                values: new object[] { 1, "The darkness is going, We need to ....", false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Missions",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
