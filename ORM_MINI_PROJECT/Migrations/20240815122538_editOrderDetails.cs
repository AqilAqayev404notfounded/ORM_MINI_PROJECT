using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORM_MINI_PROJECT.Migrations
{
    public partial class editOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 15, 12, 25, 38, 112, DateTimeKind.Utc).AddTicks(4379),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 16, 14, 21, 413, DateTimeKind.Utc).AddTicks(8195));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 15, 12, 25, 38, 112, DateTimeKind.Utc).AddTicks(4292),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 16, 14, 21, 413, DateTimeKind.Utc).AddTicks(7976));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDate",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 15, 12, 25, 38, 112, DateTimeKind.Utc).AddTicks(3809),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 16, 14, 21, 413, DateTimeKind.Utc).AddTicks(7186));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 15, 12, 25, 38, 112, DateTimeKind.Utc).AddTicks(3203),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 12, 16, 14, 21, 413, DateTimeKind.Utc).AddTicks(6213));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 16, 14, 21, 413, DateTimeKind.Utc).AddTicks(8195),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 15, 12, 25, 38, 112, DateTimeKind.Utc).AddTicks(4379));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Product",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 16, 14, 21, 413, DateTimeKind.Utc).AddTicks(7976),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 15, 12, 25, 38, 112, DateTimeKind.Utc).AddTicks(4292));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PaymentDate",
                table: "Payment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 16, 14, 21, 413, DateTimeKind.Utc).AddTicks(7186),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 15, 12, 25, 38, 112, DateTimeKind.Utc).AddTicks(3809));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 12, 16, 14, 21, 413, DateTimeKind.Utc).AddTicks(6213),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 15, 12, 25, 38, 112, DateTimeKind.Utc).AddTicks(3203));
        }
    }
}
