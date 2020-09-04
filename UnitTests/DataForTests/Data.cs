using System;
using System.Collections.Generic;
using System.Linq;
using GameShop.Domain.Dtos;
using GameShop.Domain.Model;

namespace UnitTests.DataForTests
{
    public class Data
    {
        public static List<ProductForSearchingDto> ProductForSearchingDto()
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
        public static List<ProductForModerationDto> ProductForModerationDto()
        {
            return new List<ProductForModerationDto>()
            {
                new ProductForModerationDto()
                {
                    Id = 1,
                    Name = "The Witcher 3 Wild Hunt",
                    Price = 48.82M,
                    CategoryName = "PC",
                },
                new ProductForModerationDto()
                {
                    Id = 2,
                    Name = "Assassin’s Creed Odyssey",
                    Price = 26.81M,
                    CategoryName = "PS4",
              
                },
                new ProductForModerationDto()
                {
                    Id = 3,
                    Name = "Battlefield V",
                    Price = 34.82M,
                    CategoryName = "XONE",
         
                },
                new ProductForModerationDto()
                {
                    Id = 4,
                    Name = "Layers of Fear",
                    Price = 42.02M,
                    CategoryName = "PC",
      
                },
                new ProductForModerationDto()
                {
                    Id = 5,
                    Name = "The Last of Us",
                    Price = 55.15M,
                    CategoryName = "PS4",
       
                },
                new ProductForModerationDto()
                {
                    Id = 6,
                    Name = "Forza Horizon 4",
                    Price = 33.02M,
                    CategoryName = "XONE",

                },
                new ProductForModerationDto()
                {
                    Id = 7,
                    Name = "Might & Magic: Heroes VII",
                    Price = 5.53M,
                    CategoryName = "PC",

                },
            };
        }
    }
}