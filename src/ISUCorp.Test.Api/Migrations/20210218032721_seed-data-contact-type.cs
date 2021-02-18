using Microsoft.EntityFrameworkCore.Migrations;

namespace ISUCorp.Test.Api.Migrations
{
    public partial class seeddatacontacttype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ContactType",
                columns: new[] { "ContactTypeId", "Name" },
                values: new object[] { 1, "Contact Type 1" });

            migrationBuilder.InsertData(
                table: "ContactType",
                columns: new[] { "ContactTypeId", "Name" },
                values: new object[] { 2, "Contact Type 2" });

            migrationBuilder.InsertData(
                table: "ContactType",
                columns: new[] { "ContactTypeId", "Name" },
                values: new object[] { 3, "Contact Type 3" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ContactType",
                keyColumn: "ContactTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ContactType",
                keyColumn: "ContactTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ContactType",
                keyColumn: "ContactTypeId",
                keyValue: 3);
        }
    }
}
