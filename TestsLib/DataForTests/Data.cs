using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GameShop.Domain.Dtos;
using GameShop.Domain.Dtos.ProductDtos;
using GameShop.Domain.Model;
using GameShop.Infrastructure;
using Xunit.Sdk;

namespace TestsLib.DataForTests
{
    public class Data : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {
                new ProductForSearchingDto()
                {
                    Id = 1,
                    Name = "The Witcher 3 Wild Hunt",
                    Price = 48.82M,
                    CategoryName = "PC",
                    Photo = new Photo()
                    {
                        Id = 1,
                        Url = "http://placehold.it/200x300.jpg",
                        isMain = true,
                        DateAdded = DateTime.Parse("2020-04-07")
                    }
                },
                new ProductForSearchingDto()
                {
                    Id = 2,
                    Name = "Assassin’s Creed Odyssey",
                    Price = 26.81M,
                    CategoryName = "PS4",
                    Photo = new Photo()
                    {
                        Id = 2,
                        Url = "http://placehold.it/200x300.jpg",
                        isMain = true,
                        DateAdded = DateTime.Parse("2020-06-28")
                    }
                },
                new ProductForSearchingDto()
                {
                    Id = 3,
                    Name = "Battlefield V",
                    Price = 34.82M,
                    CategoryName = "XONE",
                    Photo = new Photo()
                    {
                        Id = 3,
                        Url = "http://placehold.it/200x300.jpg",
                        isMain = true,
                        DateAdded = DateTime.Parse("2020-03-31")
                    }
                },
                new ProductForSearchingDto()
                {
                    Id = 4,
                    Name = "Layers of Fear",
                    Price = 42.02M,
                    CategoryName = "PC",
                    Photo = new Photo()
                    {
                        Id = 4,
                        Url = "http://placehold.it/200x300.jpg",
                        isMain = true,
                        DateAdded = DateTime.Parse("2020-01-03")
                    }
                },
                new ProductForSearchingDto()
                {
                    Id = 5,
                    Name = "The Last of Us",
                    Price = 55.15M,
                    CategoryName = "PS4",
                    Photo = new Photo()
                    {
                        Id = 5,
                        Url = "http://placehold.it/200x300.jpg",
                        isMain = true,
                        DateAdded = DateTime.Parse("2020-07-01")
                    }
                },
                new ProductForSearchingDto()
                {
                    Id = 6,
                    Name = "Forza Horizon 4",
                    Price = 33.02M,
                    CategoryName = "XONE",
                    Photo = new Photo()
                    {
                        Id = 6,
                        Url = "http://placehold.it/200x300.jpg",
                        isMain = true,
                        DateAdded = DateTime.Parse("2020-01-13")
                    }
                },
                new ProductForSearchingDto()
                {
                    Id = 7,
                    Name = "Might & Magic: Heroes VII",
                    Price = 5.53M,
                    CategoryName = "PC",
                    Photo = new Photo()
                    {
                        Id = 7,
                        Url = "http://placehold.it/200x300.jpg",
                        isMain = true,
                        DateAdded = DateTime.Parse("2020-07-17")
                    }
                },
            };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static IEnumerable<ProductForSearchingDto> ProductForSearchingDto()
        {
            return new List<ProductForSearchingDto>()
            {
                new ProductForSearchingDto()
                {
                    Id = 1,
                    Name = "The Witcher 3 Wild Hunt",
                    Price = 48.82M,
                    CategoryName = "PC",
                    Photo = new Photo()
                    {
                        Id = 1,
                        Url = "http://placehold.it/200x300.jpg",
                        isMain = true,
                        DateAdded = DateTime.Parse("2020-04-07")
                    }
                },
                new ProductForSearchingDto()
                {
                    Id = 2,
                    Name = "Assassin’s Creed Odyssey",
                    Price = 26.81M,
                    CategoryName = "PS4",
                    Photo = new Photo()
                    {
                        Id = 2,
                        Url = "http://placehold.it/200x300.jpg",
                        isMain = true,
                        DateAdded = DateTime.Parse("2020-06-28")
                    }
                },
                new ProductForSearchingDto()
                {
                    Id = 3,
                    Name = "Battlefield V",
                    Price = 34.82M,
                    CategoryName = "XONE",
                    Photo = new Photo()
                    {
                        Id = 3,
                        Url = "http://placehold.it/200x300.jpg",
                        isMain = true,
                        DateAdded = DateTime.Parse("2020-03-31")
                    }
                },
                new ProductForSearchingDto()
                {
                    Id = 4,
                    Name = "Layers of Fear",
                    Price = 42.02M,
                    CategoryName = "PC",
                    Photo = new Photo()
                    {
                        Id = 4,
                        Url = "http://placehold.it/200x300.jpg",
                        isMain = true,
                        DateAdded = DateTime.Parse("2020-01-03")
                    }
                },
                new ProductForSearchingDto()
                {
                    Id = 5,
                    Name = "The Last of Us",
                    Price = 55.15M,
                    CategoryName = "PS4",
                    Photo = new Photo()
                    {
                        Id = 5,
                        Url = "http://placehold.it/200x300.jpg",
                        isMain = true,
                        DateAdded = DateTime.Parse("2020-07-01")
                    }
                },
                new ProductForSearchingDto()
                {
                    Id = 6,
                    Name = "Forza Horizon 4",
                    Price = 33.02M,
                    CategoryName = "XONE",
                    Photo = new Photo()
                    {
                        Id = 6,
                        Url = "http://placehold.it/200x300.jpg",
                        isMain = true,
                        DateAdded = DateTime.Parse("2020-01-13")
                    }
                },
                new ProductForSearchingDto()
                {
                    Id = 7,
                    Name = "Might & Magic: Heroes VII",
                    Price = 5.53M,
                    CategoryName = "PC",
                    Photo = new Photo()
                    {
                        Id = 7,
                        Url = "http://placehold.it/200x300.jpg",
                        isMain = true,
                        DateAdded = DateTime.Parse("2020-07-17")
                    }
                },
            };
        }

        public static IEnumerable<ProductForModerationDto> ProductForModerationDto()
        {
            return new List<ProductForModerationDto>()
            {
                new ProductForModerationDto()
                {
                    Id = 1,
                    Name = "The Witcher 3 Wild Hunt",
                    Price = 48.82M,
                    CategoryName = "PC",
                    ReleaseDate = DateTime.Parse("2017-03-31"),
                    SubCategories = new string[]
                    {
                        "RPG"
                    }
                },
                new ProductForModerationDto()
                {
                    Id = 2,
                    Name = "Assassin’s Creed Odyssey",
                    Price = 26.81M,
                    CategoryName = "PS4",
                    ReleaseDate = DateTime.Parse("2017-07-31"),
                    SubCategories = new string[]
                    {
                        "RPG"
                    }
                },
                new ProductForModerationDto()
                {
                    Id = 3,
                    Name = "Battlefield V",
                    Price = 34.82M,
                    CategoryName = "XONE",
                    ReleaseDate = DateTime.Parse("2017-06-01"),
                    SubCategories = new string[]
                    {
                        "FPS"
                    }
                },
                new ProductForModerationDto()
                {
                    Id = 4,
                    Name = "Layers of Fear",
                    Price = 42.02M,
                    CategoryName = "PC",
                    ReleaseDate = DateTime.Parse("2017-05-16"),
                    SubCategories = new string[]
                    {
                        "Horror"
                    }
                },
                new ProductForModerationDto()
                {
                    Id = 5,
                    Name = "The Last of Us",
                    Price = 55.15M,
                    CategoryName = "PS4",
                    ReleaseDate = DateTime.Parse("2017-07-30"),
                    SubCategories = new string[]
                    {
                        "RPG",
                        "Adventure"
                    }
                },
                new ProductForModerationDto()
                {
                    Id = 6,
                    Name = "Forza Horizon 4",
                    Price = 33.02M,
                    CategoryName = "XONE",
                    ReleaseDate = DateTime.Parse("2017-08-21"),
                    SubCategories = new string[]
                    {
                        "Racing"
                    }
                },
                new ProductForModerationDto()
                {
                    Id = 7,
                    Name = "Might & Magic: Heroes VII",
                    Price = 5.53M,
                    CategoryName = "PC",
                    ReleaseDate = DateTime.Parse("2017-06-07"),
                    SubCategories = new string[]
                    {
                        "RTS"
                    }
                },
            };
        }

        public static IEnumerable<Product> Product(ApplicationDbContext context)
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "The Witcher 3 Wild Hunt",
                    Price = 48.82M,
                    IsDigitalMedia = true,
                    Pegi = 18,
                    ReleaseDate = DateTime.Parse("2017-03-31"),
                    Description = "Nulla amet commodo minim esse adipisicing commodo sint esse laboris adipisicing. Officia Lorem laboris ipsum labore mollit ipsum est enim elit exercitation quis deserunt. Nostrud dolore ut sint est ut officia voluptate consequat mollit. Nulla cupidatat mollit dolore non consequat amet Lorem. Magna dolor veniam anim aliquip aliquip esse consequat velit veniam tempor in.\r\n",
                    Category = new Category()
                    {
                        Id = 1
                    },
                    Requirements = new Requirements()
                    {
                        Id = 1,
                        OS = "Windows 7/8/10",
                        Processor = "Intel Core i7 4790 3.6 GHz / AMD FX-9590 4.7 GHz",
                        RAM = 6,
                        GraphicsCard = "NVIDIA GeForce RTX 2080Ti 11GB / AMD Radeon RX 5700XT 8GB",
                        HDD = 50,
                        IsNetworkConnectionRequire = true,
                        ProductId = 1
                    },
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 1,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-04-07"),
                            ProductId = 1
                        }
                    },
                    SubCategories = new List<ProductSubCategory>
                    {
                        new ProductSubCategory()
                        {
                            ProductId = 1,
                            SubCategoryId = 1
                        }
                    }
                },
                new Product()
                {
                    Id = 2,
                    Name = "Assassin’s Creed Odyssey",
                    Price = 26.81M,
                    IsDigitalMedia = false,
                    Pegi = 16,
                    ReleaseDate = DateTime.Parse("2017-07-31"),
                    Description = "Exercitation occaecat esse sunt elit adipisicing magna quis laborum. Sunt consequat nulla minim labore. Laborum ut irure cupidatat et ullamco minim occaecat id consequat officia non. Deserunt incididunt ea qui incididunt. Duis laborum proident do nulla anim laboris eiusmod incididunt velit.\r\n",
                    Category = new Category()
                    {
                        Id = 2
                    },
                    Requirements = new Requirements()
                    {
                        Id = 2,
                        OS = "None",
                        Processor = "None",
                        RAM = 0,
                        GraphicsCard = "None",
                        HDD = 30,
                        IsNetworkConnectionRequire = true,
                        ProductId = 2
                    },
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 2,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-06-28"),
                            ProductId = 2
                        }
                    },
                    SubCategories = new List<ProductSubCategory>
                    {
                        new ProductSubCategory()
                        {
                            ProductId = 2,
                            SubCategoryId = 1
                        }
                    }
                },
                new Product()
                {
                    Id = 3,
                    Name = "Battlefield V",
                    Price = 34.82M,
                    IsDigitalMedia = true,
                    Pegi = 12,
                    ReleaseDate = DateTime.Parse("2017-06-01"),
                    Description = "Voluptate ut in commodo eu dolore aliquip ex. Pariatur velit laborum anim cillum et sit irure sit. Ipsum cillum officia ipsum irure consectetur irure occaecat deserunt aliquip esse consectetur eu. Cupidatat sit consequat magna sit pariatur consequat. Enim labore commodo nisi sunt commodo.\r\n",
                    Category = new Category()
                    {
                        Id = 3
                    },
                    Requirements = new Requirements()
                    {
                        Id = 3,
                        OS = "None",
                        Processor = "None",
                        RAM = 0,
                        GraphicsCard = "None",
                        HDD = 10,
                        IsNetworkConnectionRequire = true,
                        ProductId = 3
                    },
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 3,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-03-31"),
                            ProductId = 3
                        }
                    },
                    SubCategories = new List<ProductSubCategory>
                    {
                        new ProductSubCategory()
                        {
                            ProductId = 3,
                            SubCategoryId = 2
                        }
                    }
                },
                new Product()
                {
                    Id = 4,
                    Name = "Layers of Fear",
                    Price = 42.02M,
                    IsDigitalMedia = true,
                    Pegi = 18,
                    ReleaseDate = DateTime.Parse("2017-05-16"),
                    Description = "Ex consectetur nisi id laborum laboris. Officia eu culpa nisi sint incididunt tempor consequat reprehenderit cillum proident minim laboris. Eiusmod proident nulla laboris eiusmod excepteur fugiat adipisicing voluptate aliqua sunt anim est. Non tempor duis veniam et consequat ipsum ullamco. Culpa aute dolor commodo amet proident deserunt consequat pariatur reprehenderit officia.\r\n",
                    Category = new Category()
                    {
                        Id = 1
                    },
                    Requirements = new Requirements()
                    {
                        Id = 4,
                        OS = "Windows 10",
                        Processor = "Intel Core i7 4790 3.6 GHz / AMD FX-9590 4.7 GHz",
                        RAM = 2,
                        GraphicsCard = "NVIDIA GeForce GTX 780 3GB / AMD Radeon R9 290X 4GB",
                        HDD = 10,
                        IsNetworkConnectionRequire =  true,
                        ProductId = 4
                    },
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 4,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-01-03"),
                            ProductId = 4
                        }
                    },
                    SubCategories = new List<ProductSubCategory>
                    {
                        new ProductSubCategory()
                        {
                            ProductId = 4,
                            SubCategoryId = 3
                        }
                    }
                },
                new Product()
                {
                    Id = 5,
                    Name = "The Last of Us",
                    Price = 55.15M,
                    IsDigitalMedia = true,
                    Pegi = 16,
                    ReleaseDate = DateTime.Parse("2017-07-30"),
                    Description = "Velit in ea aliqua sint veniam fugiat eiusmod. Incididunt cillum do pariatur cillum dolore occaecat ad. Minim aute laborum ex dolore. Ea exercitation minim et nostrud in elit eu esse amet eiusmod. Ad ut nostrud qui consectetur sunt consequat magna magna labore qui.\r\n",
                    Category = new Category()
                    {
                        Id = 2
                    },
                    Requirements = new Requirements()
                    {
                        Id = 5,
                        OS = "None",
                        Processor = "None",
                        RAM = 0,
                        GraphicsCard = "None",
                        HDD = 30,
                        IsNetworkConnectionRequire = true,
                        ProductId = 5
                    },
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 5,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-07-01"),
                            ProductId = 5
                        }
                    },
                    SubCategories = new List<ProductSubCategory>
                    {
                        new ProductSubCategory()
                        {
                            ProductId = 5,
                            SubCategoryId = 1
                        },
                        new ProductSubCategory()
                        {
                            ProductId = 5,
                            SubCategoryId = 7
                        }
                    }
                },
                new Product()
                {
                    Id = 6,
                    Name = "Forza Horizon 4",
                    Price = 33.02M,
                    IsDigitalMedia = false,
                    Pegi = 12,
                    ReleaseDate = DateTime.Parse("2017-08-21"),
                    Description = "Incididunt ullamco quis eu consectetur. Elit nostrud ipsum amet minim non nostrud ipsum dolore magna magna. Ad deserunt elit velit esse aliqua quis proident in cupidatat quis. Ullamco ut in labore ad tempor aliqua aute quis amet occaecat irure. Amet deserunt velit ut ipsum ad anim mollit reprehenderit ea ipsum.\r\n",
                    Category = new Category()
                    {
                        Id = 3
                    },
                    Requirements = new Requirements()
                    {
                        Id = 6,
                        OS = "None",
                        Processor = "None",
                        RAM = 0,
                        GraphicsCard = "None",
                        HDD = 30,
                        IsNetworkConnectionRequire = true,
                        ProductId = 6
                    },
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 6,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-01-13"),
                            ProductId = 6
                        }
                    },
                    SubCategories = new List<ProductSubCategory>
                    {
                        new ProductSubCategory()
                        {
                            ProductId = 6,
                            SubCategoryId = 6
                        }
                    }
                },
                new Product()
                {
                    Id = 7,
                    Name = "Might & Magic: Heroes VII",
                    Price = 5.53M,
                    IsDigitalMedia = false,
                    Pegi = 12,
                    ReleaseDate = DateTime.Parse("2017-06-07"),
                    Description = "Incididunt minim excepteur adipisicing Lorem labore irure incididunt proident sint id qui. Culpa exercitation adipisicing minim sit elit magna nisi pariatur do sint minim irure. Ut do nisi in fugiat aliquip proident. Eiusmod elit et aliquip consectetur eu irure.\r\n",
                    Category = new Category()
                    {
                        Id = 1
                    },
                    Requirements = new Requirements()
                    {
                        Id = 7,
                        OS = "Windows 8/10",
                        Processor = "Intel Core i5 4690 3.3 GHz / AMD Ryzen 5 3600x 3.8 GHz",
                        RAM = 16,
                        GraphicsCard = "NVIDIA GeForce RTX 2080Ti 11GB / AMD Radeon RX 5700XT 8GB",
                        HDD = 30,
                        IsNetworkConnectionRequire =  true,
                        ProductId = 7
                    },
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 7,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-07-17"),
                            ProductId = 7
                        }
                    },
                    SubCategories = new List<ProductSubCategory>
                    {
                        new ProductSubCategory()
                        {
                            ProductId = 7,
                            SubCategoryId = 5
                        }
                    }
                }
            };
        }


        public static IEnumerable<UserForListDto> UserForListDto()
        {
            return new List<UserForListDto>()
            {
                new UserForListDto()
                {
                    Id = 1,
                    UserName = "Holly",
                    Roles = new string[]
                    {
                        "Customer"
                    }
                },
                new UserForListDto()
                {
                    Id = 2,
                    UserName = "Michelle",
                    Roles = new string[]
                    {
                        "Customer"
                    }
                },
                new UserForListDto()
                {
                    Id = 3,
                    UserName = "Zelma",
                    Roles = new string[]
                    {
                        "Customer"
                    }
                },
                new UserForListDto()
                {
                    Id = 4,
                    UserName = "Angelina",
                    Roles = new string[]
                    {
                        "Customer"
                    }
                },
                new UserForListDto()
                {
                    Id = 5,
                    UserName = "Diana",
                    Roles = new string[]
                    {
                        "Customer"
                    }
                },
                new UserForListDto()
                {
                    Id = 6,
                    UserName = "Logan",
                    Roles = new string[]
                    {
                        "Customer"
                    }
                },
                new UserForListDto()
                {
                    Id = 7,
                    UserName = "Wilkins",
                    Roles = new string[]
                    {
                        "Customer"
                    }
                },
                new UserForListDto()
                {
                    Id = 8,
                    UserName = "Hahn",
                    Roles = new string[]
                    {
                        "Customer"
                    }
                },
                new UserForListDto()
                {
                    Id = 9,
                    UserName = "Miller",
                    Roles = new string[]
                    {
                        "Customer"
                    }
                },
                new UserForListDto()
                {
                    Id = 10,
                    UserName = "Morales",
                    Roles = new string[]
                    {
                        "Customer"
                    }
                },
                new UserForListDto()
                {
                    Id = 11,
                    UserName = "Admin",
                    Roles = new string[]
                    {
                        "Admin"
                    }
                },

            };
        }

        public static IEnumerable<ProductToEditDto> ProductToEditDto()
        {
            return new List<ProductToEditDto>()
            {
                new ProductToEditDto()
                {
                    Name = "The Witcher 3 Wild Hunt",
                    Description = "Nulla amet commodo minim esse adipisicing commodo sint esse laboris adipisicing. Officia Lorem laboris ipsum labore mollit ipsum est enim elit exercitation quis deserunt. Nostrud dolore ut sint est ut officia voluptate consequat mollit. Nulla cupidatat mollit dolore non consequat amet Lorem. Magna dolor veniam anim aliquip aliquip esse consequat velit veniam tempor in.\r\n",
                    Pegi = 18,
                    Price = 48.82M,
                    IsDigitalMedia = true,
                    ReleaseDate = DateTime.Parse("2017-03-31"),
                    CategoryId = 1,
                    Requirements = new RequirementsForEditDto()
                    {
                        OS = "Windows 7/8/10",
                        Processor = "Intel Core i7 4790 3.6 GHz / AMD FX-9590 4.7 GHz",
                        RAM = 6,
                        GraphicsCard = "NVIDIA GeForce RTX 2080Ti 11GB / AMD Radeon RX 5700XT 8GB",
                        HDD = 50,
                        IsNetworkConnectionRequire = true
                    },
                    LanguagesId = new int[]
                    {
                        1,2,5
                    },
                    SubCategoriesId = new int[]
                    {
                        1
                    },
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 1,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-04-07"),
                            ProductId = 1
                        }
                    }
                },
                new ProductToEditDto()
                {
                    Name = "Assassin’s Creed Odyssey",
                    Description = "Exercitation occaecat esse sunt elit adipisicing magna quis laborum. Sunt consequat nulla minim labore. Laborum ut irure cupidatat et ullamco minim occaecat id consequat officia non. Deserunt incididunt ea qui incididunt. Duis laborum proident do nulla anim laboris eiusmod incididunt velit.\r\n",
                    Pegi = 16,
                    Price = 26.81M,
                    IsDigitalMedia = false,
                    ReleaseDate = DateTime.Parse("2017-07-31"),
                    CategoryId = 2,
                    Requirements = new RequirementsForEditDto()
                    {
                        OS = "None",
                        Processor = "None",
                        RAM = 0,
                        GraphicsCard = "None",
                        HDD = 30,
                        IsNetworkConnectionRequire = true
                    },
                    LanguagesId = new int[]
                    {
                        2,4
                    },
                    SubCategoriesId = new int[]
                    {
                        1
                    },
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 2,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-06-28"),
                            ProductId = 2
                        }
                    }
                },
                new ProductToEditDto()
                {
                    Name = "Battlefield V",
                    Description = "Voluptate ut in commodo eu dolore aliquip ex. Pariatur velit laborum anim cillum et sit irure sit. Ipsum cillum officia ipsum irure consectetur irure occaecat deserunt aliquip esse consectetur eu. Cupidatat sit consequat magna sit pariatur consequat. Enim labore commodo nisi sunt commodo.\r\n",
                    Pegi = 12,
                    Price = 34.82M,
                    IsDigitalMedia = true,
                    ReleaseDate = DateTime.Parse("2017-06-01"),
                    CategoryId = 3,
                    Requirements = new RequirementsForEditDto()
                    {
                        OS = "None",
                        Processor = "None",
                        RAM = 0,
                        GraphicsCard = "None",
                        HDD = 10,
                        IsNetworkConnectionRequire = true
                    },
                    LanguagesId = new int[]
                    {
                        2
                    },
                    SubCategoriesId = new int[]
                    {
                        2
                    },
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 3,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-03-31"),
                            ProductId = 3
                        }
                    }
                },
                new ProductToEditDto()
                {
                    Name = "Layers of Fear",
                    Description = "Ex consectetur nisi id laborum laboris. Officia eu culpa nisi sint incididunt tempor consequat reprehenderit cillum proident minim laboris. Eiusmod proident nulla laboris eiusmod excepteur fugiat adipisicing voluptate aliqua sunt anim est. Non tempor duis veniam et consequat ipsum ullamco. Culpa aute dolor commodo amet proident deserunt consequat pariatur reprehenderit officia.\r\n",
                    Pegi = 18,
                    Price = 42.02M,
                    IsDigitalMedia = true,
                    ReleaseDate = DateTime.Parse("2017-05-16"),
                    CategoryId = 1,
                    Requirements = new RequirementsForEditDto()
                    {
                        OS = "Windows 10",
                        Processor = "Intel Core i7 4790 3.6 GHz / AMD FX-9590 4.7 GHz",
                        RAM = 2,
                        GraphicsCard = "NVIDIA GeForce GTX 780 3GB / AMD Radeon R9 290X 4GB",
                        HDD = 10,
                        IsNetworkConnectionRequire =  true
                    },
                    LanguagesId = new int[]
                    {
                        1,2,3,4,5,6
                    },
                    SubCategoriesId = new int[]
                    {
                        3
                    },
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 4,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-01-03"),
                            ProductId = 4
                        }
                    }
                },
                new ProductToEditDto()
                {
                    Name = "The Last of Us",
                    Description = "Velit in ea aliqua sint veniam fugiat eiusmod. Incididunt cillum do pariatur cillum dolore occaecat ad. Minim aute laborum ex dolore. Ea exercitation minim et nostrud in elit eu esse amet eiusmod. Ad ut nostrud qui consectetur sunt consequat magna magna labore qui.\r\n",
                    Pegi = 16,
                    Price = 55.15M,
                    IsDigitalMedia = true,
                    ReleaseDate = DateTime.Parse("2017-07-30"),
                    CategoryId = 2,
                    Requirements = new RequirementsForEditDto()
                    {
                        OS = "None",
                        Processor = "None",
                        RAM = 0,
                        GraphicsCard = "None",
                        HDD = 50,
                        IsNetworkConnectionRequire = true
                    },
                    LanguagesId = new int[]
                    {
                        2,3
                    },
                    SubCategoriesId = new int[]
                    {
                        1,7
                    },
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 5,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-07-01"),
                            ProductId = 5
                        }
                    }
                },
                new ProductToEditDto()
                {
                    Name = "Forza Horizon 4",
                    Description = "Incididunt ullamco quis eu consectetur. Elit nostrud ipsum amet minim non nostrud ipsum dolore magna magna. Ad deserunt elit velit esse aliqua quis proident in cupidatat quis. Ullamco ut in labore ad tempor aliqua aute quis amet occaecat irure. Amet deserunt velit ut ipsum ad anim mollit reprehenderit ea ipsum.\r\n",
                    Pegi = 12,
                    Price = 33.02M,
                    IsDigitalMedia = false,
                    ReleaseDate = DateTime.Parse("2017-08-21"),
                    CategoryId = 3,
                    Requirements = new RequirementsForEditDto()
                    {
                        OS = "None",
                        Processor = "None",
                        RAM = 0,
                        GraphicsCard = "None",
                        HDD = 30,
                        IsNetworkConnectionRequire = true
                    },
                    LanguagesId = new int[]
                    {
                        1,3
                    },
                    SubCategoriesId = new int[]
                    {
                        6
                    },
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 6,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-01-13"),
                            ProductId = 6
                        }
                    }
                },
                new ProductToEditDto()
                {
                    Name = "Might & Magic: Heroes VII",
                    Description = "Incididunt minim excepteur adipisicing Lorem labore irure incididunt proident sint id qui. Culpa exercitation adipisicing minim sit elit magna nisi pariatur do sint minim irure. Ut do nisi in fugiat aliquip proident. Eiusmod elit et aliquip consectetur eu irure.\r\n",
                    Pegi = 12,
                    Price = 5.53M,
                    IsDigitalMedia = false,
                    ReleaseDate = DateTime.Parse("2017-06-07"),
                    CategoryId = 1,
                    Requirements = new RequirementsForEditDto()
                    {
                        OS = "Windows 8/10",
                        Processor = "Intel Core i5 4690 3.3 GHz / AMD Ryzen 5 3600x 3.8 GHz",
                        RAM = 16,
                        GraphicsCard = "NVIDIA GeForce RTX 2080Ti 11GB / AMD Radeon RX 5700XT 8GB",
                        HDD = 30,
                        IsNetworkConnectionRequire =  true
                    },
                    LanguagesId = new int[]
                    {
                        4
                    },
                    SubCategoriesId = new int[]
                    {
                        5
                    },
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 7,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-07-17"),
                            ProductId = 7
                        }
                    }
                },
            };
        }

        public static IEnumerable<CategoryToReturnDto> CategoryToReturnDto()
        {
            return new List<CategoryToReturnDto>()
            {
                new CategoryToReturnDto {Id=1, Name="PC", Description="PC Description"},
                new CategoryToReturnDto {Id=2, Name="PS4", Description="PS4 Description"},
                new CategoryToReturnDto {Id=3, Name="XONE", Description="XBOX One Description"}
            };
        }

        public static IEnumerable<SubCategoryToReturnDto> SubCategoryToReturnDto()
        {
            return new List<SubCategoryToReturnDto>()
            {
                new SubCategoryToReturnDto {Id=1, Name="RPG", Description="RPG Description"},
                new SubCategoryToReturnDto {Id=2, Name="FPS", Description="FPS Description"},
                new SubCategoryToReturnDto {Id=3, Name="Horror", Description="Horror Description"},
                new SubCategoryToReturnDto {Id=4, Name="MMO", Description="MMO Description"},
                new SubCategoryToReturnDto {Id=5, Name="RTS", Description="RTS Description"},
                new SubCategoryToReturnDto {Id=6, Name="Racing", Description="Racing Description"},
                new SubCategoryToReturnDto {Id=7, Name="Adventure", Description="Adventure Description"}
            };
        }

        public static IEnumerable<LanguageToReturnDto> LanguageToReturnDto()
        {
            return new List<LanguageToReturnDto>()
            {
                new LanguageToReturnDto {Id=1, Name= "Polish"},
                new LanguageToReturnDto {Id=2, Name= "English"},
                new LanguageToReturnDto {Id=3, Name= "German"},
                new LanguageToReturnDto {Id=4, Name= "Russian"},
                new LanguageToReturnDto {Id=5, Name= "French"},
                new LanguageToReturnDto {Id=6, Name= "Italian"}
            };
        }

        public static IEnumerable<PhotoForReturnDto> PhotoForReturnDto()
        {
            return new List<PhotoForReturnDto>()
            {
                new PhotoForReturnDto()
                {
                    Id = 1,
                    Url = "http://placehold.it/200x300.jpg",
                    isMain = true,
                    DateAdded = DateTime.Parse("2020-04-07")
                },
                new PhotoForReturnDto()
                {
                    Id = 2,
                    Url = "http://placehold.it/200x300.jpg",
                    isMain = true,
                    DateAdded = DateTime.Parse("2020-06-28")
                },
                new PhotoForReturnDto()
                {
                    Id = 3,
                    Url = "http://placehold.it/200x300.jpg",
                    isMain = true,
                    DateAdded = DateTime.Parse("2020-03-31")
                },
                new PhotoForReturnDto()
                {
                    Id = 4,
                    Url = "http://placehold.it/200x300.jpg",
                    isMain = true,
                    DateAdded = DateTime.Parse("2020-01-03")
                },
                new PhotoForReturnDto()
                {
                    Id = 5,
                    Url = "http://placehold.it/200x300.jpg",
                    isMain = true,
                    DateAdded = DateTime.Parse("2020-07-01")
                },
                new PhotoForReturnDto()
                {
                    Id = 6,
                    Url = "http://placehold.it/200x300.jpg",
                    isMain = true,
                    DateAdded = DateTime.Parse("2020-01-13")
                },
                new PhotoForReturnDto()
                {
                    Id = 7,
                    Url = "http://placehold.it/200x300.jpg",
                    isMain = true,
                    DateAdded = DateTime.Parse("2020-07-17")
                }

            };
        }

        public static IEnumerable<User> Users()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = 1,
                    NormalizedUserName = "HOLLY",
                    NormalizedEmail = "HOLLYJONES@ISODRIVE.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEI+LO/SUfga+h6z1lp1C5zP+Wh0VCs69j/yFSm+FKyEpCyWjHHTrEkuswQbkqwt9vg==",
                    SecurityStamp = "O5AYUVT3JQJHN2LC5NGWBYBSRMUBWUGI",
                    LockoutEnabled = true,
                    UserName = "Holly",
                    Email = "hollyjones@isodrive.com",
                    Created = DateTime.Parse("2017-06-22"),
                    LastActive = DateTime.Parse("2017-06-22")
                },
                new User()
                {
                    Id = 1,
                    NormalizedUserName = "HOLLY",
                    NormalizedEmail = "HOLLYJONES@ISODRIVE.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEI+LO/SUfga+h6z1lp1C5zP+Wh0VCs69j/yFSm+FKyEpCyWjHHTrEkuswQbkqwt9vg==",
                    SecurityStamp = "O5AYUVT3JQJHN2LC5NGWBYBSRMUBWUGI",
                    LockoutEnabled = true,
                    UserName = "Michelle",
                    Email = "michellejones@isodrive.com",
                    Created = DateTime.Parse("2017-06-22"),
                    LastActive = DateTime.Parse("2017-06-22")
                },
                new User()
                {
                    Id = 1,
                    NormalizedUserName = "HOLLY",
                    NormalizedEmail = "HOLLYJONES@ISODRIVE.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEI+LO/SUfga+h6z1lp1C5zP+Wh0VCs69j/yFSm+FKyEpCyWjHHTrEkuswQbkqwt9vg==",
                    SecurityStamp = "O5AYUVT3JQJHN2LC5NGWBYBSRMUBWUGI",
                    LockoutEnabled = true,
                    UserName = "Zelma",
                    Email = "zelmajones@isodrive.com",
                    Created = DateTime.Parse("2017-06-22"),
                    LastActive = DateTime.Parse("2017-06-22")
                },
                new User()
                {
                    Id = 1,
                    NormalizedUserName = "HOLLY",
                    NormalizedEmail = "HOLLYJONES@ISODRIVE.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEI+LO/SUfga+h6z1lp1C5zP+Wh0VCs69j/yFSm+FKyEpCyWjHHTrEkuswQbkqwt9vg==",
                    SecurityStamp = "O5AYUVT3JQJHN2LC5NGWBYBSRMUBWUGI",
                    LockoutEnabled = true,
                    UserName = "Angelina",
                    Email = "angelinajones@isodrive.com",
                    Created = DateTime.Parse("2017-06-22"),
                    LastActive = DateTime.Parse("2017-06-22")
                },
                new User()
                {
                    Id = 1,
                    NormalizedUserName = "HOLLY",
                    NormalizedEmail = "HOLLYJONES@ISODRIVE.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEI+LO/SUfga+h6z1lp1C5zP+Wh0VCs69j/yFSm+FKyEpCyWjHHTrEkuswQbkqwt9vg==",
                    SecurityStamp = "O5AYUVT3JQJHN2LC5NGWBYBSRMUBWUGI",
                    LockoutEnabled = true,
                    UserName = "Diana",
                    Email = "dianajones@isodrive.com",
                    Created = DateTime.Parse("2017-06-22"),
                    LastActive = DateTime.Parse("2017-06-22")
                },
                new User()
                {
                    Id = 1,
                    NormalizedUserName = "HOLLY",
                    NormalizedEmail = "HOLLYJONES@ISODRIVE.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEI+LO/SUfga+h6z1lp1C5zP+Wh0VCs69j/yFSm+FKyEpCyWjHHTrEkuswQbkqwt9vg==",
                    SecurityStamp = "O5AYUVT3JQJHN2LC5NGWBYBSRMUBWUGI",
                    LockoutEnabled = true,
                    UserName = "Logan",
                    Email = "loganjones@isodrive.com",
                    Created = DateTime.Parse("2017-06-22"),
                    LastActive = DateTime.Parse("2017-06-22")
                },
                new User()
                {
                    Id = 1,
                    NormalizedUserName = "HOLLY",
                    NormalizedEmail = "HOLLYJONES@ISODRIVE.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEI+LO/SUfga+h6z1lp1C5zP+Wh0VCs69j/yFSm+FKyEpCyWjHHTrEkuswQbkqwt9vg==",
                    SecurityStamp = "O5AYUVT3JQJHN2LC5NGWBYBSRMUBWUGI",
                    LockoutEnabled = true,
                    UserName = "Wilkins",
                    Email = "wilkinsjones@isodrive.com",
                    Created = DateTime.Parse("2017-06-22"),
                    LastActive = DateTime.Parse("2017-06-22")
                },
                new User()
                {
                    Id = 1,
                    NormalizedUserName = "HOLLY",
                    NormalizedEmail = "HOLLYJONES@ISODRIVE.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEI+LO/SUfga+h6z1lp1C5zP+Wh0VCs69j/yFSm+FKyEpCyWjHHTrEkuswQbkqwt9vg==",
                    SecurityStamp = "O5AYUVT3JQJHN2LC5NGWBYBSRMUBWUGI",
                    LockoutEnabled = true,
                    UserName = "Hahn",
                    Email = "hahnjones@isodrive.com",
                    Created = DateTime.Parse("2017-06-22"),
                    LastActive = DateTime.Parse("2017-06-22")
                },
                new User()
                {
                    Id = 1,
                    NormalizedUserName = "HOLLY",
                    NormalizedEmail = "HOLLYJONES@ISODRIVE.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEI+LO/SUfga+h6z1lp1C5zP+Wh0VCs69j/yFSm+FKyEpCyWjHHTrEkuswQbkqwt9vg==",
                    SecurityStamp = "O5AYUVT3JQJHN2LC5NGWBYBSRMUBWUGI",
                    LockoutEnabled = true,
                    UserName = "Miller",
                    Email = "millerjones@isodrive.com",
                    Created = DateTime.Parse("2017-06-22"),
                    LastActive = DateTime.Parse("2017-06-22")
                },
                new User()
                {
                    Id = 1,
                    NormalizedUserName = "HOLLY",
                    NormalizedEmail = "HOLLYJONES@ISODRIVE.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEI+LO/SUfga+h6z1lp1C5zP+Wh0VCs69j/yFSm+FKyEpCyWjHHTrEkuswQbkqwt9vg==",
                    SecurityStamp = "O5AYUVT3JQJHN2LC5NGWBYBSRMUBWUGI",
                    LockoutEnabled = true,
                    UserName = "Morales",
                    Email = "moralesjones@isodrive.com",
                    Created = DateTime.Parse("2017-06-22"),
                    LastActive = DateTime.Parse("2017-06-22")
                },
                new User()
                {
                    Id = 1,
                    NormalizedUserName = "HOLLY",
                    NormalizedEmail = "HOLLYJONES@ISODRIVE.COM",
                    PasswordHash = "AQAAAAEAACcQAAAAEI+LO/SUfga+h6z1lp1C5zP+Wh0VCs69j/yFSm+FKyEpCyWjHHTrEkuswQbkqwt9vg==",
                    SecurityStamp = "O5AYUVT3JQJHN2LC5NGWBYBSRMUBWUGI",
                    LockoutEnabled = true,
                    UserName = "Admin",
                    Email = "admin@shop.eu"
                },
            };
        }

        public static IEnumerable<Photo> Photo()
        {
            return new List<Photo>()
            {
                new Photo()
                {
                    Id = 1,
                    Url = "http://placehold.it/200x300.jpg",
                    isMain = true,
                    DateAdded = DateTime.Parse("2020-04-07"),
                    ProductId = 1
                },
                new Photo()
                {
                    Id = 2,
                    Url = "http://placehold.it/200x300.jpg",
                    isMain = true,
                    DateAdded = DateTime.Parse("2020-06-28"),
                    ProductId = 2
                },
                new Photo()
                {
                    Id = 3,
                    Url = "http://placehold.it/200x300.jpg",
                    isMain = true,
                    DateAdded = DateTime.Parse("2020-03-31"),
                    ProductId = 3
                },
                new Photo()
                {
                    Id = 4,
                    Url = "http://placehold.it/200x300.jpg",
                    isMain = true,
                    DateAdded = DateTime.Parse("2020-01-03"),
                    ProductId = 4
                },
                new Photo()
                {
                    Id = 5,
                    Url = "http://placehold.it/200x300.jpg",
                    isMain = true,
                    DateAdded = DateTime.Parse("2020-07-01"),
                    ProductId = 5
                },
                new Photo()
                {
                    Id = 6,
                    Url = "http://placehold.it/200x300.jpg",
                    isMain = true,
                    DateAdded = DateTime.Parse("2020-01-13"),
                    ProductId = 6
                },
                new Photo()
                {
                    Id = 7,
                    Url = "http://placehold.it/200x300.jpg",
                    isMain = true,
                    DateAdded = DateTime.Parse("2020-07-17"),
                    ProductId = 7
                }

            };
        }

        public static IEnumerable<RequirementsForEditDto> RequirementsForEditDto()
        {
            return new List<RequirementsForEditDto>()
            {
                new RequirementsForEditDto()
                {
                    OS = "Windows 7/8/10",
                    Processor = "Intel Core i7 4790 3.6 GHz / AMD FX-9590 4.7 GHz",
                    RAM = 6,
                    GraphicsCard = "NVIDIA GeForce RTX 2080Ti 11GB / AMD Radeon RX 5700XT 8GB",
                    HDD = 50,
                    IsNetworkConnectionRequire = true
                },
                new RequirementsForEditDto()
                {
                    OS = "None",
                    Processor = "None",
                    RAM = 0,
                    GraphicsCard = "None",
                    HDD = 30,
                    IsNetworkConnectionRequire = true
                },
                new RequirementsForEditDto()
                {
                    OS = "None",
                    Processor = "None",
                    RAM = 0,
                    GraphicsCard = "None",
                    HDD = 10,
                    IsNetworkConnectionRequire = true
                },
                new RequirementsForEditDto()
                {
                    OS = "Windows 10",
                    Processor = "Intel Core i7 4790 3.6 GHz / AMD FX-9590 4.7 GHz",
                    RAM = 2,
                    GraphicsCard = "NVIDIA GeForce GTX 780 3GB / AMD Radeon R9 290X 4GB",
                    HDD = 10,
                    IsNetworkConnectionRequire =  true
                },
                new RequirementsForEditDto()
                {
                    OS = "None",
                    Processor = "None",
                    RAM = 0,
                    GraphicsCard = "None",
                    HDD = 50,
                    IsNetworkConnectionRequire = true
                },
                new RequirementsForEditDto()
                {
                    OS = "None",
                    Processor = "None",
                    RAM = 0,
                    GraphicsCard = "None",
                    HDD = 30,
                    IsNetworkConnectionRequire = true
                },
                new RequirementsForEditDto()
                {
                    OS = "Windows 8/10",
                    Processor = "Intel Core i5 4690 3.3 GHz / AMD Ryzen 5 3600x 3.8 GHz",
                    RAM = 16,
                    GraphicsCard = "NVIDIA GeForce RTX 2080Ti 11GB / AMD Radeon RX 5700XT 8GB",
                    HDD = 30,
                    IsNetworkConnectionRequire =  true
                }
                
            };

        }

        public static Product ExpectedProductForCreateAsync(ProductForCreationDto productForCreationDto, Requirements requirements, Category selectedCategory)
        {


            var expected = new Product()
            {
                Name = productForCreationDto.Name,
                Description = productForCreationDto.Description,
                Pegi = productForCreationDto.Pegi,
                Price = productForCreationDto.Price,
                IsDigitalMedia = productForCreationDto.IsDigitalMedia,
                ReleaseDate = productForCreationDto.ReleaseDate,
                Category = selectedCategory,
                Requirements = requirements,
                Languages = new List<ProductLanguage>(),
                SubCategories = new List<ProductSubCategory>()
            };

            var productLanguages = new List<ProductLanguage>()
            {
                new ProductLanguage()
                {
                    Product = expected,
                    Language = new Language() {Id = productForCreationDto.LanguagesId[0], Name="Polish"}
                },
                new ProductLanguage()
                {
                    Product = expected,
                    Language = new Language() {Id = productForCreationDto.LanguagesId[1], Name="French"}
                },
                new ProductLanguage()
                {
                    Product = expected,
                    Language = new Language() {Id = productForCreationDto.LanguagesId[2], Name="Italian"}
                }
            };
            expected.Languages = productLanguages;

            var productSubCategories = new List<ProductSubCategory>()
            {
                new ProductSubCategory()
                {
                    Product = expected,
                    SubCategory = new SubCategory() {Id = productForCreationDto.SubCategoriesId[0], Name="RPG", Description= "RPG Description"}
                },
                new ProductSubCategory()
                {
                    Product = expected,
                    SubCategory = new SubCategory() {Id = productForCreationDto.SubCategoriesId[1], Name="Horror", Description= "Horror Description"}
                }
            };
            expected.SubCategories = productSubCategories;

            return expected;
        }

        public static Product ExpectedProductForEditAsync(int id, ProductEditDto productToEditDto, Requirements requirements, Category selectedCategory)
        {


            var expected = new Product()
            {
                Id = id,
                Name = productToEditDto.Name,
                Description = productToEditDto.Description,
                Pegi = productToEditDto.Pegi,
                Price = productToEditDto.Price,
                IsDigitalMedia = productToEditDto.IsDigitalMedia,
                ReleaseDate = productToEditDto.ReleaseDate,
                Category = selectedCategory,
                Requirements = requirements,
                Languages = new List<ProductLanguage>(),
                SubCategories = new List<ProductSubCategory>()
            };

            var productLanguages = new List<ProductLanguage>()
            {
                new ProductLanguage()
                {
                    Product = expected,
                    Language = new Language() {Id = productToEditDto.LanguagesId[0], Name="Polish"}
                },
                new ProductLanguage()
                {
                    Product = expected,
                    Language = new Language() {Id = productToEditDto.LanguagesId[1], Name="French"}
                },
                new ProductLanguage()
                {
                    Product = expected,
                    Language = new Language() {Id = productToEditDto.LanguagesId[2], Name="Italian"}
                }
            };
            expected.Languages = productLanguages;

            var productSubCategories = new List<ProductSubCategory>()
            {
                new ProductSubCategory()
                {
                    Product = expected,
                    SubCategory = new SubCategory() {Id = productToEditDto.SubCategoriesId[0], Name="RPG", Description= "RPG Description"}
                },
                new ProductSubCategory()
                {
                    Product = expected,
                    SubCategory = new SubCategory() {Id = productToEditDto.SubCategoriesId[1], Name="Horror", Description= "Horror Description"}
                }
            };
            expected.SubCategories = productSubCategories;

            return expected;
        }

        public static IEnumerable<Product> ExpectedProductForGetWithPhotosOnly()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "The Witcher 3 Wild Hunt",
                    Price = 48.82M,
                    IsDigitalMedia = true,
                    Pegi = 18,
                    ReleaseDate = DateTime.Parse("2017-03-31"),
                    Description = "Nulla amet commodo minim esse adipisicing commodo sint esse laboris adipisicing. Officia Lorem laboris ipsum labore mollit ipsum est enim elit exercitation quis deserunt. Nostrud dolore ut sint est ut officia voluptate consequat mollit. Nulla cupidatat mollit dolore non consequat amet Lorem. Magna dolor veniam anim aliquip aliquip esse consequat velit veniam tempor in.\r\n",
                    Photos = new List<Photo>
                        {
                            new Photo()
                            {
                                Id = 1,
                                Url = "http://placehold.it/200x300.jpg",
                                isMain = true,
                                DateAdded = DateTime.Parse("2020-04-07"),
                                ProductId = 1
                            }
                        },
                },
                new Product()
                {
                    Id = 2,
                    Name = "Assassin’s Creed Odyssey",
                    Price = 26.81M,
                    IsDigitalMedia = false,
                    Pegi = 16,
                    ReleaseDate = DateTime.Parse("2017-07-31"),
                    Description = "Exercitation occaecat esse sunt elit adipisicing magna quis laborum. Sunt consequat nulla minim labore. Laborum ut irure cupidatat et ullamco minim occaecat id consequat officia non. Deserunt incididunt ea qui incididunt. Duis laborum proident do nulla anim laboris eiusmod incididunt velit.\r\n",
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 2,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-06-28"),
                            ProductId = 2
                        }
                    },
                },
                new Product()
                {
                    Id = 3,
                    Name = "Battlefield V",
                    Price = 34.82M,
                    IsDigitalMedia = true,
                    Pegi = 12,
                    ReleaseDate = DateTime.Parse("2017-06-01"),
                    Description = "Voluptate ut in commodo eu dolore aliquip ex. Pariatur velit laborum anim cillum et sit irure sit. Ipsum cillum officia ipsum irure consectetur irure occaecat deserunt aliquip esse consectetur eu. Cupidatat sit consequat magna sit pariatur consequat. Enim labore commodo nisi sunt commodo.\r\n",
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 3,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-03-31"),
                            ProductId = 3
                        }
                    },
                },
                new Product()
                {
                    Id = 4,
                    Name = "Layers of Fear",
                    Price = 42.02M,
                    IsDigitalMedia = true,
                    Pegi = 18,
                    ReleaseDate = DateTime.Parse("2017-05-16"),
                    Description = "Ex consectetur nisi id laborum laboris. Officia eu culpa nisi sint incididunt tempor consequat reprehenderit cillum proident minim laboris. Eiusmod proident nulla laboris eiusmod excepteur fugiat adipisicing voluptate aliqua sunt anim est. Non tempor duis veniam et consequat ipsum ullamco. Culpa aute dolor commodo amet proident deserunt consequat pariatur reprehenderit officia.\r\n",
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 4,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-01-03"),
                            ProductId = 4
                        }
                    },
                },
                new Product()
                {
                    Id = 5,
                    Name = "The Last of Us",
                    Price = 55.15M,
                    IsDigitalMedia = true,
                    Pegi = 16,
                    ReleaseDate = DateTime.Parse("2017-07-30"),
                    Description = "Velit in ea aliqua sint veniam fugiat eiusmod. Incididunt cillum do pariatur cillum dolore occaecat ad. Minim aute laborum ex dolore. Ea exercitation minim et nostrud in elit eu esse amet eiusmod. Ad ut nostrud qui consectetur sunt consequat magna magna labore qui.\r\n",
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 5,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-07-01"),
                            ProductId = 5
                        }
                    },
                },
                new Product()
                {
                    Id = 6,
                    Name = "Forza Horizon 4",
                    Price = 33.02M,
                    IsDigitalMedia = false,
                    Pegi = 12,
                    ReleaseDate = DateTime.Parse("2017-08-21"),
                    Description = "Incididunt ullamco quis eu consectetur. Elit nostrud ipsum amet minim non nostrud ipsum dolore magna magna. Ad deserunt elit velit esse aliqua quis proident in cupidatat quis. Ullamco ut in labore ad tempor aliqua aute quis amet occaecat irure. Amet deserunt velit ut ipsum ad anim mollit reprehenderit ea ipsum.\r\n",
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 6,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-01-13"),
                            ProductId = 6
                        }
                    },
                },
                new Product()
                {
                    Id = 7,
                    Name = "Might & Magic: Heroes VII",
                    Price = 5.53M,
                    IsDigitalMedia = false,
                    Pegi = 12,
                    ReleaseDate = DateTime.Parse("2017-06-07"),
                    Description = "Incididunt minim excepteur adipisicing Lorem labore irure incididunt proident sint id qui. Culpa exercitation adipisicing minim sit elit magna nisi pariatur do sint minim irure. Ut do nisi in fugiat aliquip proident. Eiusmod elit et aliquip consectetur eu irure.\r\n",
                    Photos = new List<Photo>
                    {
                        new Photo()
                        {
                            Id = 7,
                            Url = "http://placehold.it/200x300.jpg",
                            isMain = true,
                            DateAdded = DateTime.Parse("2020-07-17"),
                            ProductId = 7
                        }
                    },
                },
            };
            
        }

    }
}