using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pandora.Infrastructure.Migrations
{
    public partial class AddCustomerPlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PlanId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PlanId",
                table: "Customers",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Plans_PlanId",
                table: "Customers",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Plans_PlanId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_PlanId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Customers");
        }
    }
}
