using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commarce_Website.Migrations
{
    /// <inheritdoc />
    public partial class RegistrationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Register",
                columns: table => new
                {
                    cust_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cust_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cust_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cust_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cust_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cust_country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cust_city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cust_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cust_gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cust_image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Register", x => x.cust_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Register");
        }
    }
}
