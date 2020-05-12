using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HealthPairDataAccess.Migrations
{
    public partial class migrationname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    FacilityId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FacilityName = table.Column<string>(maxLength: 80, nullable: false),
                    FacilityAddress1 = table.Column<string>(maxLength: 120, nullable: false),
                    FacilityCity = table.Column<string>(maxLength: 40, nullable: false),
                    FacilityState = table.Column<string>(maxLength: 40, nullable: false),
                    FacilityZipcode = table.Column<int>(nullable: false),
                    FacilityPhoneNumber = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.FacilityId);
                });

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    InsuranceId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InsuranceName = table.Column<string>(maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurances", x => x.InsuranceId);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    SpecialtyId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Specialty = table.Column<string>(maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.SpecialtyId);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InsuranceId = table.Column<int>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false, defaultValue: false),
                    PatientFirstName = table.Column<string>(maxLength: 80, nullable: false),
                    PatientLastName = table.Column<string>(maxLength: 80, nullable: false),
                    PatientPassword = table.Column<string>(maxLength: 80, nullable: false),
                    PatientAddress1 = table.Column<string>(maxLength: 120, nullable: false),
                    PatientCity = table.Column<string>(maxLength: 40, nullable: false),
                    PatientState = table.Column<string>(maxLength: 40, nullable: false),
                    PatientZipcode = table.Column<int>(nullable: false),
                    PatientBirthDay = table.Column<DateTime>(nullable: false),
                    PatientPhoneNumber = table.Column<long>(nullable: false),
                    PatientEmail = table.Column<string>(maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "InsuranceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    ProviderId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FacilityId = table.Column<int>(nullable: false),
                    SpecialtyId = table.Column<int>(nullable: false),
                    ProviderFirstName = table.Column<string>(maxLength: 80, nullable: false),
                    ProviderLastName = table.Column<string>(maxLength: 80, nullable: false),
                    ProviderPhoneNumber = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.ProviderId);
                    table.ForeignKey(
                        name: "FK_Providers_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "FacilityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Providers_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "SpecialtyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<int>(nullable: false),
                    ProviderId = table.Column<int>(nullable: false),
                    AppointmentDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "ProviderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceProviders",
                columns: table => new
                {
                    IPId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InsuranceId = table.Column<int>(nullable: false),
                    ProviderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceProviders", x => x.IPId);
                    table.ForeignKey(
                        name: "FK_InsuranceProviders_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "InsuranceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsuranceProviders_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "ProviderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Facilities",
                columns: new[] { "FacilityId", "FacilityAddress1", "FacilityCity", "FacilityName", "FacilityPhoneNumber", "FacilityState", "FacilityZipcode" },
                values: new object[,]
                {
                    { 1, "1700 James Bowie Drive", "Baytown", "Houston Methodist San Jacinto Hospital Alexander Campus", 2814208765L, "TX", 77520 },
                    { 15, "3101 North Tarrant Parkway", "Fort Worth", "Medical Center of Alliance", 8176391100L, "TX", 76177 },
                    { 14, "5500 Colleyville Boulevard", "Colleyville", "Baylor Emergency Medical Center", 2142946350L, "TX", 76034 },
                    { 13, "1776 North Us 287, Suite 100", "Mansfield", "Baylor Emergency Medical Center", 2142946300L, "TX", 76063 },
                    { 12, "12500 South Freeway, Suite 100", "Burleson", "Baylor Emergency Medical Center", 2142946250L, "TX", 76028 },
                    { 10, "1975 Alpha, Suite 100", "Rockwall", "Baylor Emergency Medical Center", 2142946200L, "TX", 75087 },
                    { 9, "1300 N Main Ave", "Big Lake", "Reagan Memorial Hospital", 3258842561L, "TX", 76932 },
                    { 11, "620 South Main, Suite 100", "Keller", "Baylor Emergency Medical Center", 2142946100L, "TX", 76248 },
                    { 7, "600 Elizabeth Street", "Corpus Christi", "Christus Spohn Hospital Corpus Christi Shoreline", 3619024690L, "TX", 78404 },
                    { 6, "25440 I 45 North", "The Woodlands", "Woodlands Specialty Hospita", 2816028160L, "TX", 77386 },
                    { 5, "4000 24th Street", "Lubbock", "Covenant Medical Center – Lakeside", 8067250536L, "TX", 79410 },
                    { 4, "215 Chisholm Trail", "Jacksboro", "Faith Community Hospital", 9405676633L, "TX", 76458 },
                    { 3, "301 West Expressway 83", "McAllen", "McCallen Medical Center", 9566324000L, "TX", 78590 },
                    { 2, "16750 Red Oak Drive", "Houston", "Providence Hospital of North Houston LLC", 2814537110L, "TX", 77090 },
                    { 8, "13725 Northwest Blvd", "Corpus Christi", "The Corpus Christi Medical Center – Northwest", 3617674300L, "TX", 78410 }
                });

            migrationBuilder.InsertData(
                table: "Insurances",
                columns: new[] { "InsuranceId", "InsuranceName" },
                values: new object[,]
                {
                    { 10, "EmblemHealth" },
                    { 15, "None (Cash)" },
                    { 14, "Highmark Group" },
                    { 13, "Independence Health Group Inc." },
                    { 12, "CVS" },
                    { 11, "Wellcare" },
                    { 9, "Sendero" },
                    { 8, "Celtic" },
                    { 7, "Christus" },
                    { 6, "Superior" },
                    { 5, "FirstCare" },
                    { 4, "Molina" },
                    { 3, "Community" },
                    { 2, "Humana" },
                    { 1, "Cigna" }
                });

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "SpecialtyId", "Specialty" },
                values: new object[,]
                {
                    { 13, "Anesthesiologist" },
                    { 12, "Pediatrician" },
                    { 11, "Orthopedist" },
                    { 10, "Otolaryngologist" },
                    { 9, "Endocrinologist" },
                    { 8, "Neurologist" },
                    { 4, "Urologist" },
                    { 6, "Psychiatrist" },
                    { 5, "Gastroenterologist" },
                    { 14, "Pulmonologist" },
                    { 3, "Cardiologist" },
                    { 2, "Dermatologist" },
                    { 1, "Ophthalmologist" },
                    { 7, "Internist" },
                    { 15, "Proctologist" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "InsuranceId", "IsAdmin", "PatientAddress1", "PatientBirthDay", "PatientCity", "PatientEmail", "PatientFirstName", "PatientLastName", "PatientPassword", "PatientPhoneNumber", "PatientState", "PatientZipcode" },
                values: new object[,]
                {
                    { 1, 1, true, "8401 Ronnie St", new DateTime(2000, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "White Settlement", "ifawdry0@si.edu", "Josh", "Kraus", "Password1", 7167787419L, "TX", 76108 },
                    { 6, 1, true, "123 Test Street", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test City", "TestEmail@test.com", "TestPatientFirstName", "TestPatientLastName", "TestPassword", 1234567890L, "Test State", 12345 },
                    { 2, 3, true, "2500 Victory Ave", new DateTime(1988, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dallas", "wotimony1@shop-pro.jp", "Devin", "Holt", "Password2", 3619033062L, "TX", 75201 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "InsuranceId", "PatientAddress1", "PatientBirthDay", "PatientCity", "PatientEmail", "PatientFirstName", "PatientLastName", "PatientPassword", "PatientPhoneNumber", "PatientState", "PatientZipcode" },
                values: new object[,]
                {
                    { 3, 5, "5919 Peg Street", new DateTime(2002, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Houston", "lsiward2@cnbc.com", "Zach", "Zellner", "Password3", 2452488647L, "TX", 77092 },
                    { 4, 7, "10810 Spring Cypress Rd.", new DateTime(1999, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tomball", "mindgs3@microsoft.com", "Don", "Robbins", "Password4", 8823701130L, "TX", 77375 },
                    { 5, 7, "10810 Spring Cypress Rd.", new DateTime(1999, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tomball", "mindgs3@microsoft.com", "Don", "Robbins", "Password4", 8823701130L, "TX", 77375 }
                });

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "ProviderId", "FacilityId", "ProviderFirstName", "ProviderLastName", "ProviderPhoneNumber", "SpecialtyId" },
                values: new object[,]
                {
                    { 25, 10, "Zora", "Paolucci", 1909364291L, 8 },
                    { 27, 12, "Rock", "Sharville", 5864186454L, 8 },
                    { 36, 8, "Byrle", "Shuttleworth", 2717398518L, 8 },
                    { 14, 14, "Georg", "Yakunchikov", 3464020514L, 10 },
                    { 23, 8, "Leonora", "Pitford", 2138363970L, 10 },
                    { 26, 11, "Jayson", "Wookey", 8982665744L, 10 },
                    { 28, 13, "Dani", "Broadwell", 4221047432L, 10 },
                    { 5, 5, "Donald", "Mullen", 9016226625L, 11 },
                    { 17, 2, "Alfons", "Shee", 6445427083L, 11 },
                    { 2, 2, "Zach", "Holt", 3619033062L, 13 },
                    { 38, 12, "Eadie", "Taill", 7997088826L, 12 },
                    { 16, 1, "Jorie", "Atwool", 9363687510L, 8 },
                    { 21, 6, "Dex", "Rawstron", 5243227555L, 13 },
                    { 11, 11, "Onfre", "Grazier", 8503233858L, 14 },
                    { 19, 4, "Melli", "Hansford", 5024459183L, 14 },
                    { 20, 5, "Kathryne", "Pawlaczyk", 6981610466L, 14 },
                    { 32, 14, "Portia", "Treadwell", 3609514708L, 14 },
                    { 37, 14, "Hy", "Hamflett", 1206169437L, 14 },
                    { 40, 11, "Gearalt", "Dows", 7618424391L, 11 },
                    { 12, 12, "Mauricio", "Rowes", 3259067361L, 8 },
                    { 35, 7, "Phillip", "Sharville", 9529171778L, 7 },
                    { 4, 4, "Josh", "Robins", 8823701130L, 8 },
                    { 10, 10, "Kendall", "Mulqueen", 9033544326L, 1 },
                    { 24, 9, "Whitney", "Sevior", 6174183997L, 1 },
                    { 41, 6, "Lisa", "Simons", 5821908432L, 1 },
                    { 42, 12, "Leroy", "Gerkins", 2337452877L, 1 },
                    { 43, 1, "Ki'Nir", "Habadayah", 4997874451L, 1 },
                    { 9, 9, "Boothe", "Gurrado", 9373233203L, 2 },
                    { 29, 14, "Virginia", "McAvinchey", 8769325946L, 2 },
                    { 39, 2, "Timi", "Kestian", 2894355254L, 2 },
                    { 15, 15, "Nanny", "Stead", 7705745366L, 3 },
                    { 6, 6, "Maury", "Chitter", 2577977019L, 4 },
                    { 8, 8, "Michail", "Minkin", 4487593448L, 4 },
                    { 22, 7, "Dorie", "O'Dreain", 5123716488L, 4 },
                    { 33, 4, "Tucky", "Dreher", 1313561327L, 4 },
                    { 3, 3, "Josh", "Zellner", 2452488647L, 5 },
                    { 1, 1, "Devin", "Kraus", 7167787419L, 6 },
                    { 13, 13, "Elana", "Dollman", 8696441600L, 6 },
                    { 31, 6, "Pooh", "Florio", 3317751153L, 6 },
                    { 18, 3, "Netta", "Fincken", 5252048919L, 7 },
                    { 30, 15, "Neala", "Cianelli", 4504879923L, 15 },
                    { 7, 7, "Philomena", "Capon", 2834986226L, 8 },
                    { 34, 15, "Gardie", "Drakes", 6921919943L, 15 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "AppointmentDate", "PatientId", "ProviderId" },
                values: new object[,]
                {
                    { 2, new DateTime(2020, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 },
                    { 1, new DateTime(2020, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1 },
                    { 3, new DateTime(2020, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "InsuranceProviders",
                columns: new[] { "IPId", "InsuranceId", "ProviderId" },
                values: new object[,]
                {
                    { 52, 1, 10 },
                    { 55, 1, 13 },
                    { 73, 1, 31 },
                    { 77, 1, 35 },
                    { 46, 1, 4 },
                    { 49, 1, 7 },
                    { 69, 1, 27 },
                    { 78, 1, 36 },
                    { 56, 1, 14 },
                    { 70, 1, 28 },
                    { 47, 1, 5 },
                    { 59, 1, 17 },
                    { 82, 1, 40 },
                    { 2, 1, 2 },
                    { 5, 2, 2 },
                    { 26, 9, 2 },
                    { 32, 11, 2 },
                    { 35, 12, 2 },
                    { 38, 13, 2 },
                    { 41, 14, 2 },
                    { 53, 1, 11 },
                    { 62, 1, 20 },
                    { 43, 15, 1 },
                    { 34, 12, 1 },
                    { 31, 11, 1 },
                    { 28, 10, 1 },
                    { 66, 1, 24 },
                    { 83, 1, 41 },
                    { 84, 1, 42 },
                    { 85, 1, 43 },
                    { 57, 1, 15 },
                    { 6, 2, 3 },
                    { 9, 3, 3 },
                    { 12, 4, 3 },
                    { 18, 6, 3 },
                    { 21, 7, 3 },
                    { 74, 1, 32 },
                    { 24, 8, 3 },
                    { 33, 11, 3 },
                    { 42, 14, 3 },
                    { 45, 15, 3 },
                    { 4, 2, 1 },
                    { 7, 3, 1 },
                    { 10, 4, 1 },
                    { 13, 5, 1 },
                    { 16, 6, 1 },
                    { 19, 7, 1 },
                    { 22, 8, 1 },
                    { 27, 9, 3 },
                    { 76, 1, 34 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ProviderId",
                table: "Appointments",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceProviders_InsuranceId",
                table: "InsuranceProviders",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceProviders_ProviderId",
                table: "InsuranceProviders",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_InsuranceId",
                table: "Patients",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_FacilityId",
                table: "Providers",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_SpecialtyId",
                table: "Providers",
                column: "SpecialtyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "InsuranceProviders");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "Specialties");
        }
    }
}
