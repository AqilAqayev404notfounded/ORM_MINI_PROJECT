using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORM_MINI_PROJECT.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 16, 14, 21, 413, DateTimeKind.Utc).AddTicks(8195),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 16, 12, 29, 376, DateTimeKind.Utc).AddTicks(9373));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 16, 14, 21, 413, DateTimeKind.Utc).AddTicks(7976),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 16, 12, 29, 376, DateTimeKind.Utc).AddTicks(9260));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDate",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 16, 14, 21, 413, DateTimeKind.Utc).AddTicks(7186),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 16, 12, 29, 376, DateTimeKind.Utc).AddTicks(8679));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 16, 14, 21, 413, DateTimeKind.Utc).AddTicks(6213),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 16, 12, 29, 376, DateTimeKind.Utc).AddTicks(7929));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 16, 12, 29, 376, DateTimeKind.Utc).AddTicks(9373),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 16, 14, 21, 413, DateTimeKind.Utc).AddTicks(8195));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 16, 12, 29, 376, DateTimeKind.Utc).AddTicks(9260),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 16, 14, 21, 413, DateTimeKind.Utc).AddTicks(7976));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDate",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 16, 12, 29, 376, DateTimeKind.Utc).AddTicks(8679),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 16, 14, 21, 413, DateTimeKind.Utc).AddTicks(7186));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 16, 12, 29, 376, DateTimeKind.Utc).AddTicks(7929),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 16, 14, 21, 413, DateTimeKind.Utc).AddTicks(6213));
        }
    }
}
