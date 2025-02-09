using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commarce_Website.Migrations
{
    /// <inheritdoc />
    public partial class deleteImageField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cust_image",
                table: "tbl_Register");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cust_image",
                table: "tbl_Register",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
