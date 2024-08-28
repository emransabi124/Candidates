using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidatesDataAccess.Migrations
{
    public partial class changeDataTypeGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_Candidates",
                table: "Candidates");

            // Step 2: Drop the existing 'Id' column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Candidates");

            // Step 3: Add a new 'Id' column without the IDENTITY property
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Candidates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()"
            );

            // Step 4: Reapply the primary key constraint
            migrationBuilder.AddPrimaryKey(
                name: "PK_Candidates",
                table: "Candidates",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_Candidates",
                table: "Candidates");

            // Step 2: Drop the new 'Id' column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Candidates");

            // Step 3: Recreate the original 'Id' column with the IDENTITY property
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Candidates",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // Step 4: Reapply the primary key constraint
            migrationBuilder.AddPrimaryKey(
                name: "PK_Candidates",
                table: "Candidates",
                column: "Id");
        }


    }
}
