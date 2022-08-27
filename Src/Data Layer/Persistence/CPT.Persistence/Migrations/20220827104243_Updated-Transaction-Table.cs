using Microsoft.EntityFrameworkCore.Migrations;

namespace CPT.Persistence.Migrations
{
    public partial class UpdatedTransactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qty",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Transactions");
        }
    }
}
