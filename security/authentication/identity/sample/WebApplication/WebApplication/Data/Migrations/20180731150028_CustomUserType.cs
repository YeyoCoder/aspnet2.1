﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Data.Migrations
{
    public partial class CustomUserType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomTag",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomTag",
                table: "AspNetUsers");
        }
    }
}
