using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace stock_management_system.Migrations
{
    public partial class Stock_Management_System : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Lastname = table.Column<string>(maxLength: 30, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Phone = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.CheckConstraint("CK_Employee_PhoneNumber_Min_Value", "[Phone] >= 0");
                    table.CheckConstraint("CK_Employee_PhoneNumber_Max_Value", "[Phone] <= 9999999999");
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Phone = table.Column<long>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.CheckConstraint("CK_Supplier_PhoneNumber_Min_Value", "[Phone] >= 0");
                    table.CheckConstraint("CK_Supplier_PhoneNumber_Max_Value", "[Phone] <= 9999999999");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Sku = table.Column<string>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PhotoUri = table.Column<string>(nullable: true),
                    AlertQuantity = table.Column<int>(nullable: true),
                    SellingPrice = table.Column<int>(nullable: true),
                    MarginProfitability = table.Column<int>(nullable: true),
                    Units = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Sku);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Checkout",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checkout_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Checkin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    SubTotal = table.Column<int>(nullable: false),
                    Discount = table.Column<int>(nullable: true),
                    GrandTotal = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checkin_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Checkin_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductSku = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stock_Products_ProductSku",
                        column: x => x.ProductSku,
                        principalTable: "Products",
                        principalColumn: "Sku",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckoutLists",
                columns: table => new
                {
                    ProductSku = table.Column<string>(nullable: false),
                    CheckoutId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckoutLists", x => new { x.CheckoutId, x.ProductSku });
                    table.ForeignKey(
                        name: "FK_CheckoutLists_Checkout_CheckoutId",
                        column: x => x.CheckoutId,
                        principalTable: "Checkout",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckoutLists_Products_ProductSku",
                        column: x => x.ProductSku,
                        principalTable: "Products",
                        principalColumn: "Sku",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckinLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductSku = table.Column<string>(nullable: true),
                    CheckinId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckinLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckinLists_Checkin_CheckinId",
                        column: x => x.CheckinId,
                        principalTable: "Checkin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckinLists_Products_ProductSku",
                        column: x => x.ProductSku,
                        principalTable: "Products",
                        principalColumn: "Sku",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Productos para el consumo de la gente", "Insumos" },
                    { 2, "Productos para la higiene de la casa", "Limpieza" },
                    { 3, "Para el cuidado de la higiene personal", "Higiene" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "Lastname", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "2018-0133@est.itsc.edu.do", "Tremols", "Juan", 8097057474L },
                    { 2, "pedroreyes@gmail.com", "Reyes", "Pedro", 8097057424L },
                    { 3, "gabrielwakanda@gmail.com", "Wakanda", "Gabriel", 8093057474L }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "colgaishon@est.itsc.edu.do", "Compañia de higiene personal colgaishon", 8097057424L },
                    { 2, "fritolay@est.itsc.edu.do", "Compañia de insumos papita y bebida", 8094677424L }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Sku", "AlertQuantity", "CategoryId", "Description", "MarginProfitability", "Name", "PhotoUri", "SellingPrice", "Units" },
                values: new object[] { "IS000001", 5, 1, "Una paquete de papitas de papa pequeñas", null, "Papita fritolay papa", null, 250, 25 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Sku", "AlertQuantity", "CategoryId", "Description", "MarginProfitability", "Name", "PhotoUri", "SellingPrice", "Units" },
                values: new object[] { "IS000002", 5, 1, "Una paquete de papitas de dorito original pequeñas", null, "Papita doritos original", null, 350, 25 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Sku", "AlertQuantity", "CategoryId", "Description", "MarginProfitability", "Name", "PhotoUri", "SellingPrice", "Units" },
                values: new object[] { "IS000003", 5, 3, "Una paquete de pasta dental colgate en tamaño pequeñas", null, "Pasta dental colgate pequeña", null, 500, 10 });

            migrationBuilder.InsertData(
                table: "Stock",
                columns: new[] { "Id", "ProductSku", "Quantity", "Updated" },
                values: new object[] { 1, "IS000001", 10, new DateTime(2020, 7, 29, 15, 30, 57, 591, DateTimeKind.Utc).AddTicks(4198) });

            migrationBuilder.InsertData(
                table: "Stock",
                columns: new[] { "Id", "ProductSku", "Quantity", "Updated" },
                values: new object[] { 2, "IS000002", 15, new DateTime(2020, 7, 29, 15, 30, 57, 591, DateTimeKind.Utc).AddTicks(5475) });

            migrationBuilder.InsertData(
                table: "Stock",
                columns: new[] { "Id", "ProductSku", "Quantity", "Updated" },
                values: new object[] { 3, "IS000003", 5, new DateTime(2020, 7, 29, 15, 30, 57, 591, DateTimeKind.Utc).AddTicks(5522) });

            migrationBuilder.CreateIndex(
                name: "IX_Checkin_EmployeeId",
                table: "Checkin",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkin_SupplierId",
                table: "Checkin",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckinLists_CheckinId",
                table: "CheckinLists",
                column: "CheckinId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckinLists_ProductSku",
                table: "CheckinLists",
                column: "ProductSku");

            migrationBuilder.CreateIndex(
                name: "IX_Checkout_EmployeeId",
                table: "Checkout",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckoutLists_ProductSku",
                table: "CheckoutLists",
                column: "ProductSku");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ProductSku",
                table: "Stock",
                column: "ProductSku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_Email",
                table: "Suppliers",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckinLists");

            migrationBuilder.DropTable(
                name: "CheckoutLists");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "Checkin");

            migrationBuilder.DropTable(
                name: "Checkout");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
