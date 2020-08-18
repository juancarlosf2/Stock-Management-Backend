﻿using System;
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
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
                    Phone = table.Column<long>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
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
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
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
                    Units = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
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
                columns: new[] { "Id", "CreatedDate", "Description", "Name", "Updated" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 8, 18, 3, 24, 11, 639, DateTimeKind.Utc).AddTicks(1766), "Productos para el consumo de la gente", "Insumos", new DateTime(2020, 8, 18, 3, 24, 11, 639, DateTimeKind.Utc).AddTicks(1771) },
                    { 2, new DateTime(2020, 8, 18, 3, 24, 11, 639, DateTimeKind.Utc).AddTicks(5505), "Productos para la higiene de la casa", "Limpieza", new DateTime(2020, 8, 18, 3, 24, 11, 639, DateTimeKind.Utc).AddTicks(5513) },
                    { 3, new DateTime(2020, 8, 18, 3, 24, 11, 639, DateTimeKind.Utc).AddTicks(5774), "Para el cuidado de la higiene personal", "Higiene", new DateTime(2020, 8, 18, 3, 24, 11, 639, DateTimeKind.Utc).AddTicks(5778) }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreatedDate", "Email", "Lastname", "Name", "Phone", "Updated" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 8, 18, 3, 24, 11, 637, DateTimeKind.Utc).AddTicks(6904), "2018-0133@est.itsc.edu.do", "Tremols", "Juan", 8097057474L, new DateTime(2020, 8, 18, 3, 24, 11, 637, DateTimeKind.Utc).AddTicks(6917) },
                    { 2, new DateTime(2020, 8, 18, 3, 24, 11, 639, DateTimeKind.Utc).AddTicks(373), "pedroreyes@gmail.com", "Reyes", "Pedro", 8097057424L, new DateTime(2020, 8, 18, 3, 24, 11, 639, DateTimeKind.Utc).AddTicks(378) },
                    { 3, new DateTime(2020, 8, 18, 3, 24, 11, 639, DateTimeKind.Utc).AddTicks(521), "gabrielwakanda@gmail.com", "Wakanda", "Gabriel", 8093057474L, new DateTime(2020, 8, 18, 3, 24, 11, 639, DateTimeKind.Utc).AddTicks(523) }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "CreatedDate", "Email", "Name", "Phone", "Updated" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 8, 18, 3, 24, 11, 639, DateTimeKind.Utc).AddTicks(6747), "colgaishon@est.itsc.edu.do", "Compañia de higiene personal colgaishon", 8097057424L, new DateTime(2020, 8, 18, 3, 24, 11, 639, DateTimeKind.Utc).AddTicks(6751) },
                    { 2, new DateTime(2020, 8, 18, 3, 24, 11, 639, DateTimeKind.Utc).AddTicks(8651), "fritolay@est.itsc.edu.do", "Compañia de insumos papita y bebida", 8094677424L, new DateTime(2020, 8, 18, 3, 24, 11, 639, DateTimeKind.Utc).AddTicks(8654) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Sku", "AlertQuantity", "CategoryId", "CreatedDate", "Description", "Name", "PhotoUri", "Quantity", "SellingPrice", "Units", "Updated" },
                values: new object[] { "IS000001", 5, 1, new DateTime(2020, 8, 18, 3, 24, 11, 639, DateTimeKind.Utc).AddTicks(9483), "Una paquete de papitas de papa pequeñas", "Papita fritolay papa", "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAkGBxMTEhITExIVFhUXFRcZGBgYFRcXGRcYGBcXGBsYFxcaHSghGBolHRcXITEiJSkrLi4uFx8zODMtNygtLisBCgoKDg0OGxAQGzIlICYtLS0tLS0tLS0tLS0vLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLf/AABEIAL8BCAMBEQACEQEDEQH/xAAbAAEAAgMBAQAAAAAAAAAAAAAAAQQDBQYCB//EAEUQAAEBBQQGBwYEAwgCAwAAAAECAAMRITEEEkFxBSIyUWGBBhMjQqGxwTNikeHw8QcUUtFDY5IVJFNygqKysxbCZIPS/8QAGwEBAAIDAQEAAAAAAAAAAAAAAAIEAQMFBgf/xAA7EQACAQIDBAgEBAUEAwAAAAAAAQIDEQQhMQUSQVETYXGBkaHB0RQiMrEVM+HwI1JykvEGNEJTQ2Ky/9oADAMBAAIRAxEAPwD7MT1khKDACb2pSGOUmARj2eO/KbAIw7PfjmwAG5q1j9mAgdnWcfT7sAAua1Y/dgAEO034ZsBq9NvFxQpCim8TQwpATLcTbeKqUIQ6OVrtlnDQjJu6KSCqMVPVR4rj6t5l7RxTdukfiW+jh/KZlKJ/iE5qbHxmKasqz8beY3I/y+RB4vI/6owaDq139VZ/3P3G7HhHyPSYDvA82iqk4u7qX72Zt1GUBJx8fRrMailmqlu9kbNcDyq6JXzyUr0aLxMouyrPulIbt+HkiAhBx8T5tlYhvJ1X/dIbvUT1SUz8WVJOKvKd12sLPgeesT+pq6mnpJ+ZKz5DrU4LEd8fVpqrPhUa72jG71D8xAx6wR3xacMXiIvKq/7mzHRx/lPZfKJj1kTm1hbTxK/8329iPRQ/lPDy3gHWej4xh4Sbb+JYn/sv2L2RjoY8j2bek1ex+IaP4hW/7X5meiX8p6/NJI9pEcVEsePnxqvxZjolyLdiMQUCM4QM4fUm72x6sqlOTk289Xfl1laukmi1GHZ78c27BoANzVrH7MBA7Os4+n3YABc1qx+7AAIdpvwzYBD+JhuykwAi9r0hhlNgJI6yYlBgBN7YlCuHkwAmOqmShU0pWbAI93v7/GtaMAjDVO1gc6TYADdkqZNMfNgIGrtzjTHOvJgJAuzVMGmPmwEAQ1js4DOkmAp6VMEpedy8ARwVq0zg3K2xBPDbzWjT7tH9zfh381jWv9HgzT4FvHSozTvDNF5T5lB9YXgMp82jvJL51buJX5EosxxrmW1SqrgZPZcn6LR30DwlD0USVDImDW8PRhXdjDdiy4srxR1gRwa9Q2W+ks1ka5VEkZjo9Yofi1yOx92V5NJEOnT0PRvq1SmHEAw+LdGvhKFWnuXRqjOSd7Eo0QeB+BbkrY9Ryyat1G114mR7oxITJI+DWq2zlTpPK7IxrbzNc90PKJDcSdKvSgpNG9TiyumwPEyCSRh821tOebTv2GbosI0WYazQ/icEN5GV3odHH+owbbGE5cfJEXItdW7digi2Z9HR1zkFeRm0C9K3ald5a1EHckQAEcgfi3q9jQaw+89W2/T0KeIfz2NnGGqdrA50m3WK4BuyVMmmPmwEDV25xpjnXkwEgXZqmDTHzYCAIax2cBnSTAId7ubvClKsAIjrJkkVFKVkwAi9sShXDyYCT/Lrj9FgB93bx9ayqwEf9n1yowE57eHpwYAPf2sPocWAgfzOWOdOTABHv7OH0ODABx2MPTiwFbSTkrdrCdiEsxOldoNXxVPpaE4LimTpu0kzWWJ7GYMoN88w0nGWfI6c0Z1PWnUxSvZoiongwbS5U2SzPTp3Ft+Hob8siMpWNk4swE6N6/CYCnTW/oU51G8jIpe6TWJ1rZQyIqPMxlLVWr5k7nhaG1VIXRJMwkQam3KDumTyZnc2zBXxa/h9o/8AGr4muVHjEzPkJMNYCNJ1ya5iMNTrpO/Ya4zlHgVXzspq3ExWHnh1d6G+ElLQqLetwauLT0N6geUvDFtVLEz31yJOKsaq32gJKiowT5NOKdR3WpLRHQaGdXbO5TCDy4k8yInhiW+hYOn0dCMer75nLqu82y7nt4enBrJrA9/aw+hxYCB/M5Y505MAEe/s4fQ4MAHHYw9OLAMvZ/XOrAD7uxj61nRgB/l0x+iwEq1fZz34sAMpp2sRXOWbARhe7+75ZMBNZnbwHlJgAnNcjhh4MBCdb2kt2GfowAGMlyGGDAAYyOxgfKbAIzu9zf8APNgNEp11b1aRsnWTkcG8DtXD/D4ppaPNdj9ndHTpS34JntuNPU2hKYmDSo0nVmoIw3ZXNfp/pW4sR6uBev4A3EmATERF9WERAwETAiU29xhMHTwsVdXkbcJs+tjPmvux58+xftE2B5pC2uXb92/cOErjBHUlZEFFM1KVOm4NfdOdZJ3suQqxwWEqypThKbXHetw5JepxVr6Z2129eOxaEruPFovdUgAlCikkCFIgtVcd12O3Q2bg61KNRQaur6sz2b8RrUnaQ6XyKT8QfRsEZ7Cw7+mTXgzc2P8AEl0YB64Wg70KCx43T5tiSuUqmwKi/Lmn25e6Nu46XWJ5R+kcFxd/8wG52Ipz4Rb7MypLZmKhrB92f2uWVW90oRS8Qclp9C3JnVnB/Q/A1dBUWqfgzL+eSh3efFKHJoskAAmkD9RbtbPr4hJKpBqD0ZqlQ357tPOXLiaiw9OHDy1osqSVO1i6l6QR2uCRGd00icSIN1bRqJ05Zp6G+rsyrSoOtLJrh1G6tDu6SG8PtDCvD1nE1U5byuYVKg1OLJmhtjovrQ7ciYiCrIVbt7No7ziv5nb38rmupKybO6gIRG3u+WTe7OWTWZ28B5SYAJzXI4YeDAQnW9pLdhn6MABjJchhgwAGMjsYHymwDG73N/zzYATCSZpxNc55MAUbvs578WAlQ6vZnH6wYARd1hMnDObAIS6zHd4MAAiL5qMMmAJF/WMiPuwEJ7TalD1zyYCQb+qZAfZgIBjqGgxyYBGfV4b/ABYCjpVxC6ROBrwVLzg3D27hOlodKtY/bj7lnDTtLd5lSDeMlTuXbliywTFajBKQVE7gkRLdrYeFTq774Gms3ay4nwe16QU/ePX69p4pSzwiYgZAQHJu/J3d2e1wlNU4Rpx4ZH3vR6RZLAi9/BswKs0IvKPxBbox+SmupHhMTPp8TKS/5Sdu95Hy/oF0PVbR1z5SkuozI2nizrKCSaAEzO+W+FOlR6TN6HqcdtJYGnGlTV5W7kuvr6u86TR3RzRFreLd2dTy85OuUreELExJS4ggHFO7i22NKlN2ic2W1No0YKdS1paXS9LeZo+kPRlw70hZbG5vweBBeXlXiApZjCUjdQs/BtU6SVRRR0cFtKtUwtWtUt8unbb3sZPxD6OWWxO3JdX77x4RrLiAlKSSYQ33fi2a9KMErGvZO0sRiqzhO1rcEXtIdCrI50cq0vQ86xNnCzrADrCkQAEP1EBpOio097jY0LbWJliujhbd3rLLhf2LNp0ahFicvdKvV9UkOw7s7kFKEEpuoBu661wjVUBE5tncSgnU7kVunbxclg1eTb+Z5t8+q3ccx0wRotDh28sK19eVpKQFrNyEyXiVxKYQlxhUNrn0dluanQw88fKo4YmPy8ckvBrI+l2e2C0WdxaBLrHaVEbiRrDkYjk3F2/R36capwow6KpKm+DaKq1RLeWSyN5V6LJJevHsIzPKZA8AW9bsejetfhGPm/2yriZWjbmdZCXWY7vBvTlEARF81GGTAEi/rGRH3YCE9ptSh655MBIN/VMgPswEAx1DQY5MAjPq8N/iwAm7qiYOOcmAlR6vZnH6wYBDq51jyYCIXNescM51YBCHaeGcqsAhe7TdhlxYBd6zWpDnxYB7ThDnX7MBMes1aQ58GAiN7s92OXBgEYdn45zowFXSz3qnL0e4oxpOEvFq2NSeHmnpuv7Gyl9aNHorSKXgAJgrdvybwUqUoSs9Dosy9Inl2wW4jCzvR8Ukerd/YudOb7URir16a60fGui1l6602Z1+t6gH/LEFX+0Fumo3kkerq1ujoTnyT8eHmfZfxStZd6NfgbTwodDjfUAR/Tea9iJWgzyGzKXSYqEXzueukH9x0S8S7kUOA7SR+tcEXsypRVm0Z/w6VkZg3jMbeX/KV+7l4ZGj/BvR112/fQkopdpyQCSeM1gf6S2vCx1kdT/UVVb1OiuCb8f8eZGhP7xp21PapcApB3FKUuof1F78C2IfNXb5f4IVf4Oyox4zflr7FL8Q0/mtKWWyiYShIPDrVxX8EIBbGI+aaibNjPoMNVxD4ad2nmb78XLQBYOr/wAV87TySS8PghtuKdoWKOxKe/i4vlmXNAaYs2k7MXT1IK7oD10ZGIhroxhGYImDDFs05xqxsyOMwtbZ9ffhpf5X6Pr5r0PlfTTo8qxPy7jeQoXnajUpjCB94UPIyjBqlSnuSselwWOWLo71rNar26md7+HNpv6LSP8ADePUf7r48FtV2jHfwbXL/JwMfHdxj60n6ehjt2loSTLjUt5ijhLq8jW2bzoYtIcreCJJeKCoynIjwPi3sdkxiqGWt8/TyKGIvvG+hDtPDOVW6ZXEL3absMuLALvWa1Ic+LAPacIc6/ZgJj1mrSHPgwERvdnuxy4MAjDs/HOdGARualY45yowEx6uVY8mAhIubU48/NgAF3WMwaDObAITv93d4UowAiOuNkYZcGAEX5pkBXzwYArX2ZQrhXLJgJJvyTIiv0GARiLg2hjlxYBGVzvb/GtWA1+nHF5wt0TAqhOsoxh4Nz9q1HTwk5Lq+6NtD8xGhsujkO4YmIm3hKmInPsOkkXukSL1gtwGNmenmEE+jen2O06c2aou1eHaj5r+Dlk6y3JVCTp2tfMwQP8Akfg3VoRvM7O1au7hXHm0vX0Oq/Ge16tkcgwJWt4f9CboPxeeDTxbySKn+nqW9XlN8Ebt7p7R9usRFofIQlQSXiC8CFpUkhUsTrJkRVp9LTnC0mU54TE4LE/Ink8na6aPPRLpbYFJU4dEOHbowd9YbnWJqVi9jeKoxma4sp1qa+XQYzBYt/x6qb3ter2y04cCvozpFo2y2hbh08BD1Tx49f3ryesUqIQV0hNdJDGZLRhUpwlZeJsng8biaKm4u0VZLq6l+/Iv6KtGjn1vW9cLD20qd6ykkqQhKbqK7IUYgSnXjHYnCVS61K0/iaeH6OatC/K139zVdPnlne22x2e1PS7cpQt4oiUVKIQgKV3BALn5VbXXcXNKTyLuylXhSqVaEbyyXu7cewr2PRei7Jak2lGkUAIiQ761CjrJKYXgbxTOkObRUaUZbykba20MXiaDoTpXb42f20ucv05087t9ovJUUOnTpQdlSDF6okEy7oMAATuO9tVWpvyudDZmDqYWneSu5NXz0XM6H8Kz/cbQP/kK/wCp02nE/wC1kc3aqtio/wBPqzBbXcFkGhLedoyvBMqs6roc5DpLycQSkw3Ej5N39iVXOE8tH6FTFKzR0EJ3+7u8KUbtlUER1xsjDLgwAi/NMgK+eDAFa+zKFcK5ZMBJN+SZEV+gwCMRcG0McuLAIyud7f41qwAG7qmZNDnJgCTc2px5+bAQmXtJ7sWACU1bOGOUsmAYx7m75ZsAO8bGPrJgBnNFMcGAKn7OW/DL1YCVTkjaxwYAaQG3j6zYBhDv7/nkwFTSRg6Ve2pEYyjv+Lc/aqvg6nZ6m2h+Yjn3S4qbwM1aJ0zdWNAWFu1UWkpOShAt3v8AT9Zb7gytXurSR8s/DLTbjR7y1ItPWdaVpdhKHalw6srCqcTT3W9FTqKndyOrtCjUxMIOn9Ot721sfRX2m7K+IWbGp7AQClOnRMKwF9UYcG11cfhov+J52/ycmFKvD6JW7G/Q8Cx6NXtWJCM7Ld/3ITDxaEMdgamjXhY2/FY6OlSX9z9WeXuhdFJBUHDpRAiEiKlHgEkzLbnVwqV7ol+IY95b7NXoPQdnQXjy02MKW9UVJdpdX3blHdQBshW8tUpYzD2cp89LaG6rtDFyjGEZtW43zfazb2bSVms17qLCtClVuukO70KRMaNsW08JD6X5WKlRYnEW6SbdubbOMt+j7U/tD20PEWVZWAAh4tUHaE7ITACdYn3i2n4unN33l4ncwtWjRoqmnNPVtJZ/oVrV0VtDyEHdidwjNDwiMd9aMlXp8ZLx/Ut09o0abd3Ul2r/AAZnX4b2hV2Lx3AwJIMgDxMzLclt8KcppSjpz4Favt6mrqEXfr9l7ndOLC6stnS4dAQEyf1KNVHef2bn7TxdOnS6KLu+Jw9+pXqurM5zSKMW4lB5WNrN10KfR68qoCjyV829NsaNoz7vUpYnVHS4x7m75Zt2iqDvGxj6yYAZzRTHBgCp+zlvwy9WAlU5I2scGAGkBt4+s2AYQ7+/55MAEpK2sMcp5sATL2k92LAQmftKYYeTABuVsYengwDGH8P6xrVgB3DYx9ZsAMtjZxx8+DAFS9nzxyrzYCTAbG1jj58WASqNvH1kwDCP8T6wpRgKOmvYPCragPheHzaltFXws+w20fzEaDR6YzbwFfkdM21meQUC09nYjocQpEKkd6Njn7R0Xdp0iq0lAU6fQXSXWAayTmRe43jub2tW0pKfB/chGvOVLo29P3+hvShBF53CG4YcG8xtPA9HLpIfS/3b26jNOb0keEP8C3KjNxNrhyMyXrbo4khuhT6DTeJ5BQNdbLVGTQW9N3ZapUrZmvuRNG3bziizexuNEaJCjeVsivHg17Z2DljKl5fQtevq/fAoYrFuKtHU2VtWASQACYAnfCjXNs45w+SGRUowuszWWgt5ZScndlpGntaItepSsYZd6BAf3i9SKPjrt67ZP0y7ijidUdXjD+H9Y1q3XKwO4bGPrNgBlsbOOPnwYAqXs+eOVebASYDY2scfPiwCVRt4+smAYR/ifWFKMAG9W3h6SzYAmftK4YeTAQDf25Qph5sABvaqpJFDSkhNgEe53d/jWlWAEw1RsnHOs6MAJuyTMGpr5MAOpsTjXGmTASRdmmZNcfJgEIaw2t2dZVYCId/vbvClaMBT0wm84elUjdpTwapj/wDbVP6WbKX1o1VldwSG+dyzbZ0zMGrPUyWrPa4SMwag0bvbO2xKktypmjRUo3zQs2inQWVu1LRHaTG8g8jMci3pqXw9eGWj4cDRKc1kz3a9Fxmmfn8242L2G03Kg7rlx/X7m2lirfUal9Y1pbizoTpu04l6NaEispC+LQW6uBtUoEu7Eomnq0470naEW31ISrxRtrFoaE1y4Yn9m6+F2LVn8+I+WPLi/b7lGtjL5QLr20ACAkA1zE7QhQh0dNWS4FeNO7uyg9XExbymKxDrT3mWoxsis+bTEkUXzuMWtQlYGfoKiP5kGQvpnxAMvFva7I/Lb7PsUMTqjqI9zu7/ABrSrdYrAmGqNk451nRgBN2SZg1NfJgB1Nica40yYCSLs0zJrj5MAhDWG1uzrKrARDv97d4UrRgJAvaxkoUFKTEmAAX9uUKYebAQD1kqQ5sABvalIY5SYBGPZ+OU6MAjd7PfjnwYAVXNWsfswA9nxjyp92Aki5rVjy4sAhd7TfhnxYCIR7TwylVgKulRfcvV0ggyyEWq45Xw1Rf+r+xspfWjXWeaEng3z7dvE6T1DU5KzJBsIEP7Yl2krWtKUJE1KIAGZLXaNes2oQu3yRFxROjNOOlqKUvBeCb10xSq6O8EqAJTEiYlNu9gcVWp3lU08e40VKaZed6UStKVJIUlQBSaggiII4EFttbbUbuMo6cGiEaHFEfmk/oT/SGq/iuHt+VHwRPopc2T+eOEshBn45JK0EkuodAuJXRpMKUoBQJQQFCOySkKAO43VA8w2ie166ak9H58PuiSoxMbxcW5OJrurJyNsY2PDVSR4ehpxBXbcZMvQlN5D7C89J+CU/u3utjq1F9vojn4n6kdFGPZ+OU6N1isI3ez3458GAFVzVrH7MAPZ8Y8qfdgJIua1Y8uLAIXe034Z8WAiEe08MpVYBC9r0hhlNgJA6ydIc2AKPWSEoMAJvagkRjlJgEZdXjv8WARgLmJxzYAk3NUzj9mAhPZ1nH0+7AALmsZx+7AAIa+BwzYBCfWYbspMBV0wm+5fKEoO1eCS2jFfkT7H9idP612mp0Y8i7S3z5uzcTptGZTUqn1GUQ2LWJGq0qAbRYwvZvvCI0L0OyUZmHWEcUxqGt0LqjVcdbLwvn52XeReqM2lOqi5Lza6wh1WN8u1xhDC5fjhANDD9JaShpb5uy69bB2NHoh6pblylC3xuWazi65uJCFF0lUVreQClEFJuzgIEibX8QlCrOUlHOcs5Xd1vWySzS1z56PIhHRHqyW1++dOXqi9urszlZNn6slD1aLyit2rWVVJSAFCst+KlKlSqSgrXU5L575pOys1lzvp2mbs9rtT1aes616pHVIKXlnSgpSq4FKUtwYvCSSFBMFapArMxVOnF7m6k7u6m3dq9klL6ctL5Z9QzJc6RePXpdpfC4t8i6tIT7P8o6fQdxjtKJMTEgFUMIJUIU4b0o5qLyfPfcc+xdmdjNzcWFBSt4gvusgEqCVEFaAqI1iKpJSYEzkqZhKlValFSUbaq60fZ1q+fcZRcaqZPLxpRBr7WuAOTW6UbtBl3oTrOFox61R/wBqW9xsmV6TXX6I5+I+pHRRl1eO/wAW6hXEYC5icc2AJNzVM4/ZgIT2dZx9PuwAC5rGcfuwACGvgcM2AQn1mG7KTACL2uJAYZTYCVDrJiUGAHW2Jb8PJgBnJMlCppnPNgI93v7/AJ5MBNNU7eB8psAGrJczhj4sBA1ducaY515MAAuzXMYY+DAAIax2MB5SYBDvdzd8s2AqaZm4elMk3FSpOG5quNdsPP8Apf2NlL60aDRS9UBvntbKdzp8C+1aX1AEtJRlOVoq76szJXtbh29SULAUmIORBiCCJpUCAQRMQa5RweMjLehBp9y+/Aw2uJjcaHQhV+DxSoFIU8eLeFINQm+TdjiRMwEW2VsPjVG0oWWvyqPnu6kVKHMwJ0E4EAkLSLqEFIePAlSUJCUhaQqC9UARNQADENWeMqu97Xu3mldNu7tllnn1PQluoI0C5SEhAeIASEaj56glAJgkkKiQmJA/SJCAY8ZVbblZ53zinnxenHjz1Y3USrQbmiUqQLqUEO3i3aSlIupBCFAGAlGsBBsLF1eLTzvmk83rqhuoyK0O5gpNyAKkq1SpJSpCEu0lBBBQQlIGrCWZbCxNW6d+azzum7u/O7d8zNkZrHYkO712MVGKlKUpalGEJqUSTKQFA0KlWVS1+GllZeCMJWLDaHqZPLxsxBp9Jrgkt0MOrtBmx6DzdPQK9ZGNJXRi3sNkfRLt9ChidUdJ7vf3/PJuuViaap28D5TYANWS5nDHxYCBq7c40xzryYABdmuYwx8GAAQ1jsYDykwCHe7m75ZsAIjNMkiopnLJgBF7Ylvw8mAk/wAuuP0WAH3dvH1rKrAMvafXKjAM9vD04MAHv7WH0OLAQP5nL1pyYAI9/Zw+hwYAOOxh6cWAf9f1zqwFPTXsHt3YumOfOe5qmPdsNU/pZspfWjm7AsCreDnRnVmowV2zqcC29tYAi3Zw+yKUPmq/M/L9e8hdtmltultbk3SSUVaKsuonGHMyaA0kFvghRrEgQ3CPo04yI1Y2jdHWqexN0Ce/ANsbvkipbiYH6d8I8Mcw3OxuBpV1nlLmte/mjZCTXYYC3mKmDr0/qi/uWFJMlqxIzOXMZmjdPA7P6ePSTdo+bNU6m7kiFutzTxOzdyO9TbfNPl1ewjO+pibkSNh4e0ZEGg0w8lButhYmJG26DRuvoVvJ+EDH0b1WyPol2lHE6o6jL2n1yo3XKwz28PTgwAe/tYfQ4sBA/mcvWnJgAj39nD6HBgA47GHpxYB/1/XOrAD7uxj61nRgB/l0x+iwEnV2J78fJgBlNM1GornLNgI97v7vlkwE11jt4DykwAa01yOGHgwEDW25QphnXkwAG9Jchhh4sABjqnYwPlNgEe73N/zzYCnpqTl4EzTATriIzaltL/az7DbR/MR810hpi6+6sd2EczPyI8W4WApWh0nF/Y6sY3RmeaSilr1zKRodK6QN4TwbKVyehW0VpJTt8h7+lQOYjMfBpGGt5NM+vaNtSXiVPEmIURA8LoPqWzTd7s581Z2MlieglZOCofAD92xSknJtiaySLDy6W2S3WQV0V+oAge7j9bm5GJ2VSqTVRZLilxNyqvQsl2IjDya70UU1GKsvI1qTseX9nImJ5Mq4dw+ZZozGaeTKaqt5DFU1CrKK/fEtR0MT8yavDUyctpR5FUG7eHjaNyMjo+hgKQ+hWKOMoKw5N6LYzvCXaU8Tqjpfe7+75ZN2SqTXWO3gPKTABrTXI4YeDAQNbblCmGdeTAAb0lyGGHiwAGOqdjA+U2AR7vc3/PNgBMJJmk1Nc55MAJu7E9+PkwEqHVzE4sAIu64mThnNgEJdZju8GAARF/EYZMASL+sZQ+7AQntKyh6/ZgAN/VMofZgAMdTAY5MAjPq8N+c2Ao6cVccrSKGE+fybn7VdsJPu+6N1D8xHxDSKz+afR/xV/C8YeEGoYdLoYW5L7HWib1xN3Hg0jL1NLaHIUotNMla5iDkhhix2/QTSUL7kmovI4kbQzhPk0oPgaMRDSRsrJbjeW7oorHwMj4tVzTaMOOSZ1jqQhRr8clYpPUF392xuC5heP+qBKhFAxE7o3w3NXqTlRV2rx811r18iaipvLUz2e1pUkKSoKBoRjD6Lb414uN0yDg07MxvXSTOn1ublYvAUK7c18r5rTw9rG2E5RyNZpEXUKMRADi3JWyakXeMk/Ffp5m9TucWtd5V4mpbrUsJklJ2MO523RYXA8UBUI/8Af9m7eAoxpRajxKeId2jfwl1mO7wa+VwBEX8RhkwBIv6xlD7sBCe0rKHr9mAA39Uyh9mAAx1MBjkwCM+rw35zYATd1BMHHOTAFHq5CcWAAdXOseTAALuvWOGc2AQh2nhnKrAIXu03YZcWAFN/WpD7sAPacIc6/ZgJJv6tIc+DAI3uz3Y5cGAiMOz8c50YChpwXXKkVoY8/k3N2v8A7Ofd/wDSN2H/ADEfI+l2i1IX+YSNVUAv3VUBPAy55tx9n4mMo9E3mtOw6cWeNGWqCFxwEmvtG3Uxu1jGrCRJKcWA8otBQoKQYFJiDxDLB5m3s2k+tWXiTB4JqHqODaqiepqsllwPoejNJIfISRAKhrJjQ4w3hrMJqa6yjODg+ovF5ATaUpqCuyCVz0Fg/NsKpGSDTRStLgogUbP6d3ENzMZBYRdJD6b5rk+a9TdTlv5PU8fmoCYP9JLaI4+i4/UiXRu5q9OvlvXd12mE5lW7cAOLYe06eiRONK2bZyidGKBjEksliVIkztOha1JS8vThdA5x/Zutsd7zqO/L1KeK4d50kIdp4Zyq3bKghe7TdhlxYAU39akPuwA9pwhzr9mAkm/q0hz4MAje7PdjlwYCIw7PxznRgEbupWOOcmAkHq5VjyYCALm3ONMfNgAF3WM0mgzmJMAh3+7u8KUqwAiOsNkYZVlRgBF6aZAVw8mAHX2JQrhXLJgJJvSTIiuHkwCMdUbQxyrOrARHud7f41rRgKelzdcvARFQF7kDOeQLUtpQ38LUj1fbM20Xaojm3qgQRAEEYiIIOEMW8FGLTvodQ4bTOgHib6nSRdqEjDJvQ4bHQlaM3nzJXdrHOPrWtMlJKTkW6UUpZpjfZjNuJG15tncG8eQtaqAnIFsPdjqzFzq+jXRt4e0Dl6pUJHYSI7jEAnmcmp1p1Zu1PT7+PoRdSK1Z0LvR9rdQUXZEDGInDibpkeMm1TlUpre3X++wwpQllc6+wPFPEIWqSoQMKRBrwjBt9Ko66UyvJKDaReduoYxazGlu6M1OVzLAGrbJQjUW7NXT1T0I3a0NfaHEDKYNP2bx+0dmSoVf4abi9LZtdXsW6dTeWZie2ZUI3fJtK2diopycHbu9ySqRvqai12cDWAz/AHZRqt/KybNh0RPt3hmm8lAzSIn/AJN7DYlPdpSfX6fqUcU80joId/u7vClKt2iqCI6w2RhlWVGAEXppkBXDyYAdfYlCuFcsmAkm9JMiK4eTAIx1RtDHKs6sBEe53t/jWtGAkG7qmajQ1rITYADc25xpj5sBCZe0phj5MAG9Wxh6SyYBjH+H9YZsAO8bGPrJgBnsbOOH1JgCp+z54ZV5sBJgdjaxw+psA4Dbx9ZsAwh/E+scmAr2671TwPKlChyIIE204icYUpSnpZ3JQTclY5lDqKEDEJAJGJAm3gJT3lex1lkSHUmrynZkjXWjRjtVUhrUMRNaMkRYujTtaoB2OJhIfNruHlXxEt2Ly4vka5zUFmdPo/QzlzNKAVfqIERlubs06EaWer5vUqSqSmXptPO9kQMb96BIFoVJpZIlGL4nl0ZQBAg0KatksjMjKiMZkcg26KlfNruIu1irpDSCHIi8UYmiRNRyGGdG1VaipZyfcThBzyRr7P0gv0dqAzDc2ptCaeay7f0N3w9uJsBbARHwaK2tSirvXl+8vMh0Tua14YBuBBFgzdFXyUgIVQpJh7wM/Pwb12xK/wDFqU3xzXdl9rFLExyUjoMY/wAP6wzb0hTB3jYx9ZMAM9jZxw+pMAVP2fPDKvNgJMDsbWOH1NgHAbePrNgGEP4n1jkwAblbeHpPNgCZe05Y+TAQmftJbsGACclbOGGU8mAYw7n1jmwA7hsY+s2AGUkUxxYAqXs578cvVgJVLY2scWAYRG3j6yYBhHv/AFhkwGp04pZCU3STUw3YSzj8G4G3astyNKPHN92nn9i1hYq7kyjZ7IRMkj3f3i3lJ3jF72TLty9Z36UJgoDOAa7s/aVKlT6KpDnnZPXmaqlOUndMwKfOVKEUHMCHg22dXATmnKLXO2Xil6K5LcqpZM2aEAAXYXaiH1Vu/TjCNNdFbd1VtP8AJVbbfzanhTxtbk2SsY1Pmg5ZWJKJj6vg0VB6tGd4JEI1ofJsVXuQlNcE35DXIrvnj4yS8CeIQI/EtyFtas1bJdiNipw4mp/sSKipSyompNTzatPEylnxN6nZWSLTqyhNA1OdRvUXuWQltDZExPEREGnF2dwaXR9s6t+AqgXHkZK8CS3fwlRU6kKy7+zQ1VI70XE77GHc3/PNvaHMB3DYx9ZsAMpIpjiwBUvZz345erASqWxtY4sAwiNvH1kwDCPf+sMmACc1bWGGUs2AJn7SW7BgISb+1KHLzYADe1TIChykwCM7nd3+NaMAJhqDZOOfFgBNySZg1x8mAK1Nmca40yzYCSLk0zJrj5MAhDXG0cM+DAISv97d4UqwGjXbFLfvOEE/AT8SW8XtivN4tqL0VvX7s6FGCVNXPag3Gqp2uzcim9M5tpgssjYjF1obdGL1BlcaQKKTGI/bc1/CY2rQdlnHiiE6UZ9paRaEKMUqnuNfm3cpV6VXODz5PU0OMo5MyAE8G37jepG6R7u0nFs9HcxcxPHgAIxpKcM25W0sVThSdOL+Z5di43NkItu/Aw9Y3m7m+w6xs7wsSFhsSzB6bU00CLrZiwaDpFZbqwsYt2MNLJwf7RB8zrtC2ovXDqNCkRPFMjPMN7fCzc6MJPWyObUVptF0mGoNk458WsEATckmYNcfJgCtTZnGuNMs2Aki5NMya4+TAIQ1xtHDPgwCEr/e3eFKsAAvaxkRQZTYAkX9qUOXmwER6yVIc2ARvalIY5SowCMez8cp0YBG72e/HPgwC9c1ax5cGAez4x5U+7ATDq9aseXFgIhd7TfhnxYAaF6cJwylVsN2VwjndEuzdKzVair4mLeAk+lqOfN/qdR5KxbtLwJES2uqrKwjmad7aItojTsbDCoDFtivwFzz1id/i0t2QuReRiy0zO8ZXb4CiiP9R/dpqrXjpJkbJnh9peGMebTar1FaU34mPlXA1tq04syRANOngaazlmN4myaWeJqbw41+LKuEpy0yMKRfRpxGIg1V4GXAlvIsJ0og4treEmjN0WHWkUfqDQdCa4AvOyFCRBbHQqXUyN7FXTtgU9dQTGI3VOXHOW9utQpWpqXGP2/ffyIb2ZY6GPVKswdKkpK1JPA7ZgN029RsyalQW7pd+/qU8QrTN5G72e/HPg3QNAvXNWseXBgHs+MeVPuwEw6vWrHlxYCIXe034Z8WAQj2nhlKrAIX9ekMMp1YCYdZOkObAFHrJJlBgBN7UEiMcpMAjLq8d/iwAGAuGpxzYAk3NUzJ+zAQns9qcfTPNgAFzWMwfuwACGuaHDNgKmlkKU6WUGBUCkcIiGW8tS2hV6PDSfPLxyNtFXmjl7dpV45FxHaECEQEgClSTOE28pTVO1i/a5o7XpC1viCBCZpdIFKgR4w5Rg01ToRbcnft/aM58DA4sFsWrXSoJnOAE4yxJhywPBpTrYaK+UWZYVod6MFHiIkNrWJpsWZLvRz8jZVDIsdamYsR/Zz6kC2OnpGbMk6NfUMWfEUhZkjQb1WB9GfF0xYzu+jzzGAzI/doSxkeAsZH2ilCCRD4hoxxCebM2MQ0Is4D4hpfFRMWPP8AY7yg+MR+7Z+JgLHoaAeHvD+oM+LiuAsWHOinzqafAtrnWpzykjKNvYdIPBJbtZG8CLYp1HTd4u5iUUza6JdwU9CaPClY4GEFf8R8W9JsSadOaXO/j/gp4nVGzBgLhqcc27ZWCTc1TMn7MBCez2px9M82AAXNYzB+7AAIa5ocM2AQn1mG7wYARe1xIDDKbAFDrNmUPrBgJVrezlvwYAZyTJWJpnPNgI93v7/nkwExhI7eB8psABAkuZwx8WAhOr7Se7HP0YABCa5jDHwYABCZ2MB5SYDT9JHsEp/QoyESKDcOLcLbqk4QiufoW8Ilds0abORsIA46sfiW4kMNiZaLzXuWnKCJ/LWpWyVEcVp8NaTWI7NxMtYea9yHS01xMbvRFtVSJ/8At/ctY/CazX0ryI9PDmZ0aEtZJBhH/O2PwatwS8f0MfEQCdAWkGAuR3FXybP4NWetvH9B8TAk9HrRGBuR3X1fs2fwavwa8f0MfEwMydAP0kazsHMn/wBWw9h1pauPi/YfEx6w/wBA2gTUtE/fV/8Ali2FVS1j5+w+Kj1kK0FaANYIIw1j+zRexK+qa8X7GfiYGJ50etEIwTd/zejSWx8QuXj+g+IgYv8Axu0wvJCRkuDZ/CcQ9UvH9B8RAx/+P2uoSOS0+RY9k1tN1eI+IhzMZ0DazR3DJ4nyiz8Krr/in3oz08OZicaCtCzMH+pJ9Wg8BiV9NPzj7memhz+5fs2glDuvCRue3f8AiptEsDj75Q84+5JVqXM3egnTwFaVRjBN2KyoxnGZJIk3X2Rh69Hf6ZWva2nXyK2InCVt03EYSO3gfKbdoqgECS5nDHxYCE6vtJ7sc/RgAEJrmMMfBgAEJnYwHlJgHvdzd8s2AGc0yTiKZyyYAdb2ct+DAf/Z", 10, 250, 25, new DateTime(2020, 8, 18, 3, 24, 11, 639, DateTimeKind.Utc).AddTicks(9486) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Sku", "AlertQuantity", "CategoryId", "CreatedDate", "Description", "Name", "PhotoUri", "Quantity", "SellingPrice", "Units", "Updated" },
                values: new object[] { "IS000002", 5, 1, new DateTime(2020, 8, 18, 3, 24, 11, 640, DateTimeKind.Utc).AddTicks(3719), "Una paquete de papitas de dorito original pequeñas", "Papita doritos original", "https://images-na.ssl-images-amazon.com/images/I/81S5rKXF10L._SY879_.jpg", 10, 350, 25, new DateTime(2020, 8, 18, 3, 24, 11, 640, DateTimeKind.Utc).AddTicks(3723) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Sku", "AlertQuantity", "CategoryId", "CreatedDate", "Description", "Name", "PhotoUri", "Quantity", "SellingPrice", "Units", "Updated" },
                values: new object[] { "IS000003", 5, 3, new DateTime(2020, 8, 18, 3, 24, 11, 640, DateTimeKind.Utc).AddTicks(3859), "Una paquete de pasta dental colgate en tamaño pequeñas", "Pasta dental colgate pequeña", "https://papeleria-y-cacharreria-la-economia.000webhostapp.com/img/productos/crema%20_colgate_peque%C3%B1a.jpg", 5, 500, 10, new DateTime(2020, 8, 18, 3, 24, 11, 640, DateTimeKind.Utc).AddTicks(3860) });

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
