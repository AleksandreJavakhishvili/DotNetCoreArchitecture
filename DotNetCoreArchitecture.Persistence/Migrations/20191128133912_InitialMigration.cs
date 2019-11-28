using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCoreArchitecture.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DotNetCoreArchitecture");

            migrationBuilder.CreateTable(
                name: "DomainEvents",
                schema: "DotNetCoreArchitecture",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EventData = table.Column<string>(type: "jsonb", nullable: false),
                    EventType = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                schema: "DotNetCoreArchitecture",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ArchiveIdentifier = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    xmin = table.Column<uint>(type: "xid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomainEvents",
                schema: "DotNetCoreArchitecture");

            migrationBuilder.DropTable(
                name: "Post",
                schema: "DotNetCoreArchitecture");
        }
    }
}
