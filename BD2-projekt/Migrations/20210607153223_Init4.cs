using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BD2_projekt.Migrations
{
    public partial class Init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoragePlaces",
                columns: table => new
                {
                    StoragePlacesID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Place = table.Column<string>(type: "TEXT", nullable: true),
                    Section = table.Column<string>(type: "TEXT", nullable: true),
                    Row = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoragePlaces", x => x.StoragePlacesID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UsersID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    City = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Street = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Number = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    PostCode = table.Column<string>(type: "TEXT", maxLength: 6, nullable: true),
                    Country = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    NIP = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Password = table.Column<byte[]>(type: "BLOB", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UsersID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductsID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ShortDescription = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    price = table.Column<double>(type: "REAL", nullable: false),
                    MeasureUnit = table.Column<string>(type: "TEXT", nullable: true),
                    StoragePlacesID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductsID);
                    table.ForeignKey(
                        name: "FK_Products_StoragePlaces_StoragePlacesID",
                        column: x => x.StoragePlacesID,
                        principalTable: "StoragePlaces",
                        principalColumn: "StoragePlacesID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsersID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.CartID);
                    table.ForeignKey(
                        name: "FK_Cart_Users_UsersID",
                        column: x => x.UsersID,
                        principalTable: "Users",
                        principalColumn: "UsersID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    UsersID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomersID = table.Column<int>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    CardNumber = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.UsersID);
                    table.ForeignKey(
                        name: "FK_Customers_Users_UsersID",
                        column: x => x.UsersID,
                        principalTable: "Users",
                        principalColumn: "UsersID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Distributors",
                columns: table => new
                {
                    UsersID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DistributorsID = table.Column<int>(type: "INTEGER", nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributors", x => x.UsersID);
                    table.ForeignKey(
                        name: "FK_Distributors_Users_UsersID",
                        column: x => x.UsersID,
                        principalTable: "Users",
                        principalColumn: "UsersID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartElements",
                columns: table => new
                {
                    CartElementID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductsID = table.Column<int>(type: "INTEGER", nullable: true),
                    NumberOfProducts = table.Column<int>(type: "INTEGER", nullable: false),
                    CartID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartElements", x => x.CartElementID);
                    table.ForeignKey(
                        name: "FK_CartElements_Cart_CartID",
                        column: x => x.CartID,
                        principalTable: "Cart",
                        principalColumn: "CartID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartElements_Products_ProductsID",
                        column: x => x.ProductsID,
                        principalTable: "Products",
                        principalColumn: "ProductsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoicesID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerUsersID = table.Column<int>(type: "INTEGER", nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ProductsID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoicesID);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerUsersID",
                        column: x => x.CustomerUsersID,
                        principalTable: "Customers",
                        principalColumn: "UsersID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Products_ProductsID",
                        column: x => x.ProductsID,
                        principalTable: "Products",
                        principalColumn: "ProductsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DistributorsProducts",
                columns: table => new
                {
                    DistributedProductsProductsID = table.Column<int>(type: "INTEGER", nullable: false),
                    DistributorsUsersID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorsProducts", x => new { x.DistributedProductsProductsID, x.DistributorsUsersID });
                    table.ForeignKey(
                        name: "FK_DistributorsProducts_Distributors_DistributorsUsersID",
                        column: x => x.DistributorsUsersID,
                        principalTable: "Distributors",
                        principalColumn: "UsersID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistributorsProducts_Products_DistributedProductsProductsID",
                        column: x => x.DistributedProductsProductsID,
                        principalTable: "Products",
                        principalColumn: "ProductsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderUnits",
                columns: table => new
                {
                    OrderUnitID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductsID = table.Column<int>(type: "INTEGER", nullable: true),
                    NumberOfProducts = table.Column<int>(type: "INTEGER", nullable: false),
                    unitPrice = table.Column<double>(type: "REAL", nullable: false),
                    InvoicesID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderUnits", x => x.OrderUnitID);
                    table.ForeignKey(
                        name: "FK_OrderUnits_Invoices_InvoicesID",
                        column: x => x.InvoicesID,
                        principalTable: "Invoices",
                        principalColumn: "InvoicesID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderUnits_Products_ProductsID",
                        column: x => x.ProductsID,
                        principalTable: "Products",
                        principalColumn: "ProductsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UsersID",
                table: "Cart",
                column: "UsersID");

            migrationBuilder.CreateIndex(
                name: "IX_CartElements_CartID",
                table: "CartElements",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_CartElements_ProductsID",
                table: "CartElements",
                column: "ProductsID");

            migrationBuilder.CreateIndex(
                name: "IX_DistributorsProducts_DistributorsUsersID",
                table: "DistributorsProducts",
                column: "DistributorsUsersID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerUsersID",
                table: "Invoices",
                column: "CustomerUsersID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ProductsID",
                table: "Invoices",
                column: "ProductsID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderUnits_InvoicesID",
                table: "OrderUnits",
                column: "InvoicesID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderUnits_ProductsID",
                table: "OrderUnits",
                column: "ProductsID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StoragePlacesID",
                table: "Products",
                column: "StoragePlacesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartElements");

            migrationBuilder.DropTable(
                name: "DistributorsProducts");

            migrationBuilder.DropTable(
                name: "OrderUnits");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Distributors");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "StoragePlaces");
        }
    }
}
