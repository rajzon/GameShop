using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameShop.Infrastructure.Migrations
{
    public partial class ProductsSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "PC Description", "PC" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "PS4 Description", "PS4" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "XBOX One Description", "XONE" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Polish" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "English" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "German" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Russian" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "French" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[] { 6, "Italian" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "IsDigitalMedia", "Name", "Pegi", "Price", "ReleaseDate" },
                values: new object[] { 7, null, @"Incididunt minim excepteur adipisicing Lorem labore irure incididunt proident sint id qui. Culpa exercitation adipisicing minim sit elit magna nisi pariatur do sint minim irure. Ut do nisi in fugiat aliquip proident. Eiusmod elit et aliquip consectetur eu irure.
", false, "Might & Magic: Heroes VII", (byte)12, 5.53m, new DateTime(2017, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "IsDigitalMedia", "Name", "Pegi", "Price", "ReleaseDate" },
                values: new object[] { 6, null, @"Incididunt ullamco quis eu consectetur. Elit nostrud ipsum amet minim non nostrud ipsum dolore magna magna. Ad deserunt elit velit esse aliqua quis proident in cupidatat quis. Ullamco ut in labore ad tempor aliqua aute quis amet occaecat irure. Amet deserunt velit ut ipsum ad anim mollit reprehenderit ea ipsum.
", false, "Forza Horizon 4", (byte)12, 33.02m, new DateTime(2017, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "IsDigitalMedia", "Name", "Pegi", "Price", "ReleaseDate" },
                values: new object[] { 5, null, @"Velit in ea aliqua sint veniam fugiat eiusmod. Incididunt cillum do pariatur cillum dolore occaecat ad. Minim aute laborum ex dolore. Ea exercitation minim et nostrud in elit eu esse amet eiusmod. Ad ut nostrud qui consectetur sunt consequat magna magna labore qui.
", true, "The Last of Us", (byte)16, 55.15m, new DateTime(2017, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "IsDigitalMedia", "Name", "Pegi", "Price", "ReleaseDate" },
                values: new object[] { 3, null, @"Voluptate ut in commodo eu dolore aliquip ex. Pariatur velit laborum anim cillum et sit irure sit. Ipsum cillum officia ipsum irure consectetur irure occaecat deserunt aliquip esse consectetur eu. Cupidatat sit consequat magna sit pariatur consequat. Enim labore commodo nisi sunt commodo.
", true, "Battlefield V", (byte)12, 34.82m, new DateTime(2017, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "IsDigitalMedia", "Name", "Pegi", "Price", "ReleaseDate" },
                values: new object[] { 2, null, @"Exercitation occaecat esse sunt elit adipisicing magna quis laborum. Sunt consequat nulla minim labore. Laborum ut irure cupidatat et ullamco minim occaecat id consequat officia non. Deserunt incididunt ea qui incididunt. Duis laborum proident do nulla anim laboris eiusmod incididunt velit.
", false, "Assassin’s Creed Odyssey", (byte)16, 26.81m, new DateTime(2017, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "IsDigitalMedia", "Name", "Pegi", "Price", "ReleaseDate" },
                values: new object[] { 1, null, @"Nulla amet commodo minim esse adipisicing commodo sint esse laboris adipisicing. Officia Lorem laboris ipsum labore mollit ipsum est enim elit exercitation quis deserunt. Nostrud dolore ut sint est ut officia voluptate consequat mollit. Nulla cupidatat mollit dolore non consequat amet Lorem. Magna dolor veniam anim aliquip aliquip esse consequat velit veniam tempor in.
", true, "The Witcher 3 Wild Hunt", (byte)18, 48.82m, new DateTime(2017, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "IsDigitalMedia", "Name", "Pegi", "Price", "ReleaseDate" },
                values: new object[] { 4, null, @"Ex consectetur nisi id laborum laboris. Officia eu culpa nisi sint incididunt tempor consequat reprehenderit cillum proident minim laboris. Eiusmod proident nulla laboris eiusmod excepteur fugiat adipisicing voluptate aliqua sunt anim est. Non tempor duis veniam et consequat ipsum ullamco. Culpa aute dolor commodo amet proident deserunt consequat pariatur reprehenderit officia.
", true, "Layers of Fear", (byte)18, 42.02m, new DateTime(2017, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 6, "Racing Description", "Racing" });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "RPG Description", "RPG" });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "FPS Description", "FPS" });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "Horror Description", "Horror" });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 4, "MMO Description", "MMO" });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 5, "RTS Description", "RTS" });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 7, "Adventure Description", "Adventure" });

            migrationBuilder.InsertData(
                table: "CategoriesSubCategories",
                columns: new[] { "CategoryId", "SubCategoryId" },
                values: new object[] { 3, 3 });

            migrationBuilder.InsertData(
                table: "CategoriesSubCategories",
                columns: new[] { "CategoryId", "SubCategoryId" },
                values: new object[] { 2, 3 });

            migrationBuilder.InsertData(
                table: "CategoriesSubCategories",
                columns: new[] { "CategoryId", "SubCategoryId" },
                values: new object[] { 2, 6 });

            migrationBuilder.InsertData(
                table: "CategoriesSubCategories",
                columns: new[] { "CategoryId", "SubCategoryId" },
                values: new object[] { 3, 7 });

            migrationBuilder.InsertData(
                table: "CategoriesSubCategories",
                columns: new[] { "CategoryId", "SubCategoryId" },
                values: new object[] { 1, 6 });

            migrationBuilder.InsertData(
                table: "CategoriesSubCategories",
                columns: new[] { "CategoryId", "SubCategoryId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "CategoriesSubCategories",
                columns: new[] { "CategoryId", "SubCategoryId" },
                values: new object[] { 2, 1 });

            migrationBuilder.InsertData(
                table: "CategoriesSubCategories",
                columns: new[] { "CategoryId", "SubCategoryId" },
                values: new object[] { 1, 7 });

            migrationBuilder.InsertData(
                table: "CategoriesSubCategories",
                columns: new[] { "CategoryId", "SubCategoryId" },
                values: new object[] { 3, 1 });

            migrationBuilder.InsertData(
                table: "CategoriesSubCategories",
                columns: new[] { "CategoryId", "SubCategoryId" },
                values: new object[] { 1, 4 });

            migrationBuilder.InsertData(
                table: "CategoriesSubCategories",
                columns: new[] { "CategoryId", "SubCategoryId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "CategoriesSubCategories",
                columns: new[] { "CategoryId", "SubCategoryId" },
                values: new object[] { 2, 7 });

            migrationBuilder.InsertData(
                table: "CategoriesSubCategories",
                columns: new[] { "CategoryId", "SubCategoryId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "CategoriesSubCategories",
                columns: new[] { "CategoryId", "SubCategoryId" },
                values: new object[] { 3, 2 });

            migrationBuilder.InsertData(
                table: "CategoriesSubCategories",
                columns: new[] { "CategoryId", "SubCategoryId" },
                values: new object[] { 1, 3 });

            migrationBuilder.InsertData(
                table: "CategoriesSubCategories",
                columns: new[] { "CategoryId", "SubCategoryId" },
                values: new object[] { 1, 5 });

            migrationBuilder.InsertData(
                table: "CategoriesSubCategories",
                columns: new[] { "CategoryId", "SubCategoryId" },
                values: new object[] { 3, 6 });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "DateAdded", "ProductId", "Url", "isMain" },
                values: new object[] { 5, new DateTime(2020, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "http://placehold.it/200x300.jpg", true });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "DateAdded", "ProductId", "Url", "isMain" },
                values: new object[] { 7, new DateTime(2020, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "http://placehold.it/200x300.jpg", true });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "DateAdded", "ProductId", "Url", "isMain" },
                values: new object[] { 6, new DateTime(2020, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "http://placehold.it/200x300.jpg", true });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "DateAdded", "ProductId", "Url", "isMain" },
                values: new object[] { 1, new DateTime(2020, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "http://placehold.it/200x300.jpg", true });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "DateAdded", "ProductId", "Url", "isMain" },
                values: new object[] { 2, new DateTime(2020, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "http://placehold.it/200x300.jpg", true });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "DateAdded", "ProductId", "Url", "isMain" },
                values: new object[] { 3, new DateTime(2020, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "http://placehold.it/200x300.jpg", true });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "DateAdded", "ProductId", "Url", "isMain" },
                values: new object[] { 4, new DateTime(2020, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "http://placehold.it/200x300.jpg", true });

            migrationBuilder.InsertData(
                table: "ProductsLanaguages",
                columns: new[] { "ProductId", "LanguageId" },
                values: new object[] { 4, 5 });

            migrationBuilder.InsertData(
                table: "ProductsLanaguages",
                columns: new[] { "ProductId", "LanguageId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "ProductsLanaguages",
                columns: new[] { "ProductId", "LanguageId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "ProductsLanaguages",
                columns: new[] { "ProductId", "LanguageId" },
                values: new object[] { 1, 5 });

            migrationBuilder.InsertData(
                table: "ProductsLanaguages",
                columns: new[] { "ProductId", "LanguageId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "ProductsLanaguages",
                columns: new[] { "ProductId", "LanguageId" },
                values: new object[] { 2, 4 });

            migrationBuilder.InsertData(
                table: "ProductsLanaguages",
                columns: new[] { "ProductId", "LanguageId" },
                values: new object[] { 4, 6 });

            migrationBuilder.InsertData(
                table: "ProductsLanaguages",
                columns: new[] { "ProductId", "LanguageId" },
                values: new object[] { 3, 2 });

            migrationBuilder.InsertData(
                table: "ProductsLanaguages",
                columns: new[] { "ProductId", "LanguageId" },
                values: new object[] { 7, 4 });

            migrationBuilder.InsertData(
                table: "ProductsLanaguages",
                columns: new[] { "ProductId", "LanguageId" },
                values: new object[] { 4, 1 });

            migrationBuilder.InsertData(
                table: "ProductsLanaguages",
                columns: new[] { "ProductId", "LanguageId" },
                values: new object[] { 4, 2 });

            migrationBuilder.InsertData(
                table: "ProductsLanaguages",
                columns: new[] { "ProductId", "LanguageId" },
                values: new object[] { 6, 3 });

            migrationBuilder.InsertData(
                table: "ProductsLanaguages",
                columns: new[] { "ProductId", "LanguageId" },
                values: new object[] { 6, 1 });

            migrationBuilder.InsertData(
                table: "ProductsLanaguages",
                columns: new[] { "ProductId", "LanguageId" },
                values: new object[] { 4, 3 });

            migrationBuilder.InsertData(
                table: "ProductsLanaguages",
                columns: new[] { "ProductId", "LanguageId" },
                values: new object[] { 4, 4 });

            migrationBuilder.InsertData(
                table: "ProductsLanaguages",
                columns: new[] { "ProductId", "LanguageId" },
                values: new object[] { 5, 3 });

            migrationBuilder.InsertData(
                table: "ProductsLanaguages",
                columns: new[] { "ProductId", "LanguageId" },
                values: new object[] { 5, 2 });

            migrationBuilder.InsertData(
                table: "ProductsSubCategories",
                columns: new[] { "ProductId", "SubCategoryId" },
                values: new object[] { 7, 5 });

            migrationBuilder.InsertData(
                table: "ProductsSubCategories",
                columns: new[] { "ProductId", "SubCategoryId" },
                values: new object[] { 4, 3 });

            migrationBuilder.InsertData(
                table: "ProductsSubCategories",
                columns: new[] { "ProductId", "SubCategoryId" },
                values: new object[] { 6, 6 });

            migrationBuilder.InsertData(
                table: "ProductsSubCategories",
                columns: new[] { "ProductId", "SubCategoryId" },
                values: new object[] { 5, 7 });

            migrationBuilder.InsertData(
                table: "ProductsSubCategories",
                columns: new[] { "ProductId", "SubCategoryId" },
                values: new object[] { 5, 1 });

            migrationBuilder.InsertData(
                table: "ProductsSubCategories",
                columns: new[] { "ProductId", "SubCategoryId" },
                values: new object[] { 2, 1 });

            migrationBuilder.InsertData(
                table: "ProductsSubCategories",
                columns: new[] { "ProductId", "SubCategoryId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "ProductsSubCategories",
                columns: new[] { "ProductId", "SubCategoryId" },
                values: new object[] { 3, 2 });

            migrationBuilder.InsertData(
                table: "Requirements",
                columns: new[] { "Id", "GraphicsCard", "HDD", "IsNetworkConnectionRequire", "OS", "Processor", "ProductId", "RAM" },
                values: new object[] { 7, "NVIDIA GeForce RTX 2080Ti 11GB / AMD Radeon RX 5700XT 8GB", (ushort)30, true, "Windows 8/10", "Intel Core i5 4690 3.3 GHz / AMD Ryzen 5 3600x 3.8 GHz", 7, (byte)16 });

            migrationBuilder.InsertData(
                table: "Requirements",
                columns: new[] { "Id", "GraphicsCard", "HDD", "IsNetworkConnectionRequire", "OS", "Processor", "ProductId", "RAM" },
                values: new object[] { 5, "None", (ushort)50, true, "None", "None", 5, (byte)0 });

            migrationBuilder.InsertData(
                table: "Requirements",
                columns: new[] { "Id", "GraphicsCard", "HDD", "IsNetworkConnectionRequire", "OS", "Processor", "ProductId", "RAM" },
                values: new object[] { 4, "NVIDIA GeForce GTX 780 3GB / AMD Radeon R9 290X 4GB", (ushort)10, true, "Windows 10", "Intel Core i7 4790 3.6 GHz / AMD FX-9590 4.7 GHz", 4, (byte)2 });

            migrationBuilder.InsertData(
                table: "Requirements",
                columns: new[] { "Id", "GraphicsCard", "HDD", "IsNetworkConnectionRequire", "OS", "Processor", "ProductId", "RAM" },
                values: new object[] { 3, "None", (ushort)10, true, "None", "None", 3, (byte)0 });

            migrationBuilder.InsertData(
                table: "Requirements",
                columns: new[] { "Id", "GraphicsCard", "HDD", "IsNetworkConnectionRequire", "OS", "Processor", "ProductId", "RAM" },
                values: new object[] { 2, "None", (ushort)30, true, "None", "None", 2, (byte)0 });

            migrationBuilder.InsertData(
                table: "Requirements",
                columns: new[] { "Id", "GraphicsCard", "HDD", "IsNetworkConnectionRequire", "OS", "Processor", "ProductId", "RAM" },
                values: new object[] { 1, "NVIDIA GeForce RTX 2080Ti 11GB / AMD Radeon RX 5700XT 8GB", (ushort)50, true, "Windows 7/8/10", "Intel Core i7 4790 3.6 GHz / AMD FX-9590 4.7 GHz", 1, (byte)6 });

            migrationBuilder.InsertData(
                table: "Requirements",
                columns: new[] { "Id", "GraphicsCard", "HDD", "IsNetworkConnectionRequire", "OS", "Processor", "ProductId", "RAM" },
                values: new object[] { 6, "None", (ushort)30, true, "None", "None", 6, (byte)0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CategoriesSubCategories",
                keyColumns: new[] { "CategoryId", "SubCategoryId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "CategoriesSubCategories",
                keyColumns: new[] { "CategoryId", "SubCategoryId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "CategoriesSubCategories",
                keyColumns: new[] { "CategoryId", "SubCategoryId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "CategoriesSubCategories",
                keyColumns: new[] { "CategoryId", "SubCategoryId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "CategoriesSubCategories",
                keyColumns: new[] { "CategoryId", "SubCategoryId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "CategoriesSubCategories",
                keyColumns: new[] { "CategoryId", "SubCategoryId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "CategoriesSubCategories",
                keyColumns: new[] { "CategoryId", "SubCategoryId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "CategoriesSubCategories",
                keyColumns: new[] { "CategoryId", "SubCategoryId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "CategoriesSubCategories",
                keyColumns: new[] { "CategoryId", "SubCategoryId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "CategoriesSubCategories",
                keyColumns: new[] { "CategoryId", "SubCategoryId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "CategoriesSubCategories",
                keyColumns: new[] { "CategoryId", "SubCategoryId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "CategoriesSubCategories",
                keyColumns: new[] { "CategoryId", "SubCategoryId" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "CategoriesSubCategories",
                keyColumns: new[] { "CategoryId", "SubCategoryId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "CategoriesSubCategories",
                keyColumns: new[] { "CategoryId", "SubCategoryId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "CategoriesSubCategories",
                keyColumns: new[] { "CategoryId", "SubCategoryId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "CategoriesSubCategories",
                keyColumns: new[] { "CategoryId", "SubCategoryId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "CategoriesSubCategories",
                keyColumns: new[] { "CategoryId", "SubCategoryId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ProductsLanaguages",
                keyColumns: new[] { "ProductId", "LanguageId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsLanaguages",
                keyColumns: new[] { "ProductId", "LanguageId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsLanaguages",
                keyColumns: new[] { "ProductId", "LanguageId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "ProductsLanaguages",
                keyColumns: new[] { "ProductId", "LanguageId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsLanaguages",
                keyColumns: new[] { "ProductId", "LanguageId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "ProductsLanaguages",
                keyColumns: new[] { "ProductId", "LanguageId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsLanaguages",
                keyColumns: new[] { "ProductId", "LanguageId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsLanaguages",
                keyColumns: new[] { "ProductId", "LanguageId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsLanaguages",
                keyColumns: new[] { "ProductId", "LanguageId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsLanaguages",
                keyColumns: new[] { "ProductId", "LanguageId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "ProductsLanaguages",
                keyColumns: new[] { "ProductId", "LanguageId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "ProductsLanaguages",
                keyColumns: new[] { "ProductId", "LanguageId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "ProductsLanaguages",
                keyColumns: new[] { "ProductId", "LanguageId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsLanaguages",
                keyColumns: new[] { "ProductId", "LanguageId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsLanaguages",
                keyColumns: new[] { "ProductId", "LanguageId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsLanaguages",
                keyColumns: new[] { "ProductId", "LanguageId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsLanaguages",
                keyColumns: new[] { "ProductId", "LanguageId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "ProductsSubCategories",
                keyColumns: new[] { "ProductId", "SubCategoryId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsSubCategories",
                keyColumns: new[] { "ProductId", "SubCategoryId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsSubCategories",
                keyColumns: new[] { "ProductId", "SubCategoryId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "ProductsSubCategories",
                keyColumns: new[] { "ProductId", "SubCategoryId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "ProductsSubCategories",
                keyColumns: new[] { "ProductId", "SubCategoryId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "ProductsSubCategories",
                keyColumns: new[] { "ProductId", "SubCategoryId" },
                keyValues: new object[] { 5, 7 });

            migrationBuilder.DeleteData(
                table: "ProductsSubCategories",
                keyColumns: new[] { "ProductId", "SubCategoryId" },
                keyValues: new object[] { 6, 6 });

            migrationBuilder.DeleteData(
                table: "ProductsSubCategories",
                keyColumns: new[] { "ProductId", "SubCategoryId" },
                keyValues: new object[] { 7, 5 });

            migrationBuilder.DeleteData(
                table: "Requirements",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Requirements",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Requirements",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Requirements",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Requirements",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Requirements",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Requirements",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SubCategories",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
