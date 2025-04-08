using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BikeDoctor.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceFlowEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CostApprovals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClientCI = table.Column<int>(type: "integer", nullable: false),
                    MotorcycleLicensePlate = table.Column<string>(type: "text", nullable: false),
                    EmployeeCI = table.Column<int>(type: "integer", nullable: false),
                    MotorcycleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostApprovals_Clients_ClientCI",
                        column: x => x.ClientCI,
                        principalTable: "Clients",
                        principalColumn: "CI",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CostApprovals_Motorcycles_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalTable: "Motorcycles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClientCI = table.Column<int>(type: "integer", nullable: false),
                    MotorcycleLicensePlate = table.Column<string>(type: "text", nullable: false),
                    EmployeeCI = table.Column<int>(type: "integer", nullable: false),
                    SurveyCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    MotorcycleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_Clients_ClientCI",
                        column: x => x.ClientCI,
                        principalTable: "Clients",
                        principalColumn: "CI",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Deliveries_Motorcycles_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalTable: "Motorcycles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClientCI = table.Column<int>(type: "integer", nullable: false),
                    MotorcycleLicensePlate = table.Column<string>(type: "text", nullable: false),
                    EmployeeCI = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QualityControls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClientCI = table.Column<int>(type: "integer", nullable: false),
                    MotorcycleLicensePlate = table.Column<string>(type: "text", nullable: false),
                    EmployeeCI = table.Column<int>(type: "integer", nullable: false),
                    MotorcycleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualityControls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QualityControls_Clients_ClientCI",
                        column: x => x.ClientCI,
                        principalTable: "Clients",
                        principalColumn: "CI",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QualityControls_Motorcycles_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalTable: "Motorcycles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Receptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CliendCI = table.Column<int>(type: "integer", nullable: false),
                    MotorcycleLicensePlate = table.Column<string>(type: "text", nullable: false),
                    EmployeeCI = table.Column<int>(type: "integer", nullable: false),
                    Reasons = table.Column<string>(type: "text", nullable: true),
                    Images = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClientCI = table.Column<int>(type: "integer", nullable: false),
                    MotorcycleLicensePlate = table.Column<string>(type: "text", nullable: false),
                    EmployeeCI = table.Column<int>(type: "integer", nullable: false),
                    MotorcycleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repairs_Clients_ClientCI",
                        column: x => x.ClientCI,
                        principalTable: "Clients",
                        principalColumn: "CI",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Repairs_Motorcycles_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalTable: "Motorcycles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SpareParts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CliendCI = table.Column<int>(type: "integer", nullable: false),
                    MotorcycleLicensePlate = table.Column<string>(type: "text", nullable: false),
                    EmployeeCI = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpareParts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LaborCost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameProduct = table.Column<string>(type: "text", nullable: false),
                    DescriptionProduct = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<string>(type: "text", nullable: false),
                    CostApprovalId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaborCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaborCost_CostApprovals_CostApprovalId",
                        column: x => x.CostApprovalId,
                        principalTable: "CostApprovals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diagnostic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Error = table.Column<string>(type: "text", nullable: false),
                    DetailOfError = table.Column<string>(type: "text", nullable: false),
                    ServiceType = table.Column<string>(type: "text", nullable: false),
                    TimeSpent = table.Column<int>(type: "integer", nullable: false),
                    DiagnosisId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnostic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnostic_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Control",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ControlName = table.Column<string>(type: "text", nullable: false),
                    DetailsControl = table.Column<string>(type: "text", nullable: false),
                    QualityControlId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Control", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Control_QualityControls_QualityControlId",
                        column: x => x.QualityControlId,
                        principalTable: "QualityControls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reparation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameReparation = table.Column<string>(type: "text", nullable: false),
                    DescriptionReparation = table.Column<string>(type: "text", nullable: true),
                    RepairId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reparation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reparation_Repairs_RepairId",
                        column: x => x.RepairId,
                        principalTable: "Repairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SparePart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameSparePart = table.Column<string>(type: "text", nullable: false),
                    DetailSparePart = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    SparePartsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SparePart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SparePart_SpareParts_SparePartsId",
                        column: x => x.SparePartsId,
                        principalTable: "SpareParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Control_QualityControlId",
                table: "Control",
                column: "QualityControlId");

            migrationBuilder.CreateIndex(
                name: "IX_CostApprovals_ClientCI",
                table: "CostApprovals",
                column: "ClientCI");

            migrationBuilder.CreateIndex(
                name: "IX_CostApprovals_MotorcycleId",
                table: "CostApprovals",
                column: "MotorcycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_ClientCI",
                table: "Deliveries",
                column: "ClientCI");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_MotorcycleId",
                table: "Deliveries",
                column: "MotorcycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnostic_DiagnosisId",
                table: "Diagnostic",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_LaborCost_CostApprovalId",
                table: "LaborCost",
                column: "CostApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_QualityControls_ClientCI",
                table: "QualityControls",
                column: "ClientCI");

            migrationBuilder.CreateIndex(
                name: "IX_QualityControls_MotorcycleId",
                table: "QualityControls",
                column: "MotorcycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_ClientCI",
                table: "Repairs",
                column: "ClientCI");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_MotorcycleId",
                table: "Repairs",
                column: "MotorcycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Reparation_RepairId",
                table: "Reparation",
                column: "RepairId");

            migrationBuilder.CreateIndex(
                name: "IX_SparePart_SparePartsId",
                table: "SparePart",
                column: "SparePartsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Control");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Diagnostic");

            migrationBuilder.DropTable(
                name: "LaborCost");

            migrationBuilder.DropTable(
                name: "Receptions");

            migrationBuilder.DropTable(
                name: "Reparation");

            migrationBuilder.DropTable(
                name: "SparePart");

            migrationBuilder.DropTable(
                name: "QualityControls");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "CostApprovals");

            migrationBuilder.DropTable(
                name: "Repairs");

            migrationBuilder.DropTable(
                name: "SpareParts");
        }
    }
}
