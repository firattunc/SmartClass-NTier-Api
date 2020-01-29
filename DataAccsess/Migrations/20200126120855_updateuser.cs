using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updateuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "soyad");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "kullaniciAdi");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "ad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "soyad",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "kullaniciAdi",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "ad",
                table: "Users",
                newName: "Email");
        }
    }
}
