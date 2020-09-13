using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Infrastructure.Migrations
{
    public partial class SqlServerInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastActive = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Pegi = table.Column<byte>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    IsDigitalMedia = table.Column<bool>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CategoriesSubCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false),
                    SubCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriesSubCategories", x => new { x.CategoryId, x.SubCategoryId });
                    table.ForeignKey(
                        name: "FK_CategoriesSubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoriesSubCategories_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(maxLength: 130, nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    isMain = table.Column<bool>(nullable: false),
                    PublicId = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsLanaguages",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsLanaguages", x => new { x.ProductId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_ProductsLanaguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsLanaguages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsSubCategories",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    SubCategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsSubCategories", x => new { x.ProductId, x.SubCategoryId });
                    table.ForeignKey(
                        name: "FK_ProductsSubCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsSubCategories_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requirements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OS = table.Column<string>(maxLength: 30, nullable: true),
                    Processor = table.Column<string>(maxLength: 100, nullable: true),
                    RAM = table.Column<byte>(nullable: false),
                    GraphicsCard = table.Column<string>(maxLength: 100, nullable: true),
                    HDD = table.Column<int>(nullable: false),
                    IsNetworkConnectionRequire = table.Column<bool>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requirements_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "PC Description", "PC" },
                    { 2, "PS4 Description", "PS4" },
                    { 3, "XBOX One Description", "XONE" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Polish" },
                    { 2, "English" },
                    { 3, "German" },
                    { 4, "Russian" },
                    { 5, "French" },
                    { 6, "Italian" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "IsDigitalMedia", "Name", "Pegi", "Price", "ReleaseDate" },
                values: new object[,]
                {
                    { 7, null, @"Incididunt minim excepteur adipisicing Lorem labore irure incididunt proident sint id qui. Culpa exercitation adipisicing minim sit elit magna nisi pariatur do sint minim irure. Ut do nisi in fugiat aliquip proident. Eiusmod elit et aliquip consectetur eu irure.
                ", false, "Might & Magic: Heroes VII", (byte)12, 5.53m, new DateTime(2017, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, null, @"Incididunt ullamco quis eu consectetur. Elit nostrud ipsum amet minim non nostrud ipsum dolore magna magna. Ad deserunt elit velit esse aliqua quis proident in cupidatat quis. Ullamco ut in labore ad tempor aliqua aute quis amet occaecat irure. Amet deserunt velit ut ipsum ad anim mollit reprehenderit ea ipsum.
                ", false, "Forza Horizon 4", (byte)12, 33.02m, new DateTime(2017, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, null, @"Velit in ea aliqua sint veniam fugiat eiusmod. Incididunt cillum do pariatur cillum dolore occaecat ad. Minim aute laborum ex dolore. Ea exercitation minim et nostrud in elit eu esse amet eiusmod. Ad ut nostrud qui consectetur sunt consequat magna magna labore qui.
                ", true, "The Last of Us", (byte)16, 55.15m, new DateTime(2017, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, null, @"Voluptate ut in commodo eu dolore aliquip ex. Pariatur velit laborum anim cillum et sit irure sit. Ipsum cillum officia ipsum irure consectetur irure occaecat deserunt aliquip esse consectetur eu. Cupidatat sit consequat magna sit pariatur consequat. Enim labore commodo nisi sunt commodo.
                ", true, "Battlefield V", (byte)12, 34.82m, new DateTime(2017, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, @"Exercitation occaecat esse sunt elit adipisicing magna quis laborum. Sunt consequat nulla minim labore. Laborum ut irure cupidatat et ullamco minim occaecat id consequat officia non. Deserunt incididunt ea qui incididunt. Duis laborum proident do nulla anim laboris eiusmod incididunt velit.
                ", false, "Assassin’s Creed Odyssey", (byte)16, 26.81m, new DateTime(2017, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 1, null, @"Nulla amet commodo minim esse adipisicing commodo sint esse laboris adipisicing. Officia Lorem laboris ipsum labore mollit ipsum est enim elit exercitation quis deserunt. Nostrud dolore ut sint est ut officia voluptate consequat mollit. Nulla cupidatat mollit dolore non consequat amet Lorem. Magna dolor veniam anim aliquip aliquip esse consequat velit veniam tempor in.
                ", true, "The Witcher 3 Wild Hunt", (byte)18, 48.82m, new DateTime(2017, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, null, @"Ex consectetur nisi id laborum laboris. Officia eu culpa nisi sint incididunt tempor consequat reprehenderit cillum proident minim laboris. Eiusmod proident nulla laboris eiusmod excepteur fugiat adipisicing voluptate aliqua sunt anim est. Non tempor duis veniam et consequat ipsum ullamco. Culpa aute dolor commodo amet proident deserunt consequat pariatur reprehenderit officia.
                ", true, "Layers of Fear", (byte)18, 42.02m, new DateTime(2017, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 6, "Racing Description", "Racing" },
                    { 1, "RPG Description", "RPG" },
                    { 2, "FPS Description", "FPS" },
                    { 3, "Horror Description", "Horror" },
                    { 4, "MMO Description", "MMO" },
                    { 5, "RTS Description", "RTS" },
                    { 7, "Adventure Description", "Adventure" }
                });

            migrationBuilder.InsertData(
                table: "CategoriesSubCategories",
                columns: new[] { "CategoryId", "SubCategoryId" },
                values: new object[,]
                {
                    { 3, 3 },
                    { 2, 3 },
                    { 2, 6 },
                    { 3, 7 },
                    { 1, 6 },
                    { 1, 1 },
                    { 2, 1 },
                    { 1, 7 },
                    { 3, 1 },
                    { 1, 4 },
                    { 1, 2 },
                    { 2, 7 },
                    { 2, 2 },
                    { 3, 2 },
                    { 1, 3 },
                    { 1, 5 },
                    { 3, 6 }
                });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "DateAdded", "ProductId", "PublicId", "Url", "isMain" },
                values: new object[,]
                {
                    { 5, new DateTime(2020, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null, "http://placehold.it/200x300.jpg", true },
                    { 7, new DateTime(2020, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, null, "http://placehold.it/200x300.jpg", true },
                    { 6, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, null, "http://placehold.it/200x300.jpg", true },
                    { 1, new DateTime(2020, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, "http://placehold.it/200x300.jpg", true },
                    { 2, new DateTime(2020, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, null, "http://placehold.it/200x300.jpg", true },
                    { 3, new DateTime(2020, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, null, "http://placehold.it/200x300.jpg", true },
                    { 4, new DateTime(2020, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, null, "http://placehold.it/200x300.jpg", true }
                });

            migrationBuilder.InsertData(
                table: "ProductsLanaguages",
                columns: new[] { "ProductId", "LanguageId" },
                values: new object[,]
                {
                    { 4, 5 },
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 5 },
                    { 2, 2 },
                    { 2, 4 },
                    { 4, 6 },
                    { 3, 2 },
                    { 7, 4 },
                    { 4, 1 },
                    { 4, 2 },
                    { 6, 3 },
                    { 6, 1 },
                    { 4, 3 },
                    { 4, 4 },
                    { 5, 3 },
                    { 5, 2 }
                });

            migrationBuilder.InsertData(
                table: "ProductsSubCategories",
                columns: new[] { "ProductId", "SubCategoryId" },
                values: new object[,]
                {
                    { 7, 5 },
                    { 4, 3 },
                    { 6, 6 },
                    { 5, 7 },
                    { 5, 1 },
                    { 2, 1 },
                    { 1, 1 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Requirements",
                columns: new[] { "Id", "GraphicsCard", "HDD", "IsNetworkConnectionRequire", "OS", "Processor", "ProductId", "RAM" },
                values: new object[,]
                {
                    { 7, "NVIDIA GeForce RTX 2080Ti 11GB / AMD Radeon RX 5700XT 8GB", 30, true, "Windows 8/10", "Intel Core i5 4690 3.3 GHz / AMD Ryzen 5 3600x 3.8 GHz", 7, (byte)16 },
                    { 5, "None", 50, true, "None", "None", 5, (byte)0 },
                    { 4, "NVIDIA GeForce GTX 780 3GB / AMD Radeon R9 290X 4GB", 10, true, "Windows 10", "Intel Core i7 4790 3.6 GHz / AMD FX-9590 4.7 GHz", 4, (byte)2 },
                    { 3, "None", 10, true, "None", "None", 3, (byte)0 },
                    { 2, "None", 30, true, "None", "None", 2, (byte)0 },
                    { 1, "NVIDIA GeForce RTX 2080Ti 11GB / AMD Radeon RX 5700XT 8GB", 50, true, "Windows 7/8/10", "Intel Core i7 4790 3.6 GHz / AMD FX-9590 4.7 GHz", 1, (byte)6 },
                    { 6, "None", 30, true, "None", "None", 6, (byte)0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriesSubCategories_SubCategoryId",
                table: "CategoriesSubCategories",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ProductId",
                table: "Photos",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsLanaguages_LanguageId",
                table: "ProductsLanaguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSubCategories_SubCategoryId",
                table: "ProductsSubCategories",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_ProductId",
                table: "Requirements",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CategoriesSubCategories");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "ProductsLanaguages");

            migrationBuilder.DropTable(
                name: "ProductsSubCategories");

            migrationBuilder.DropTable(
                name: "Requirements");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
