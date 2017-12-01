namespace ProductShop.App
{
    using System;
    using System.IO;
    using Data;
    using Models;
    using Newtonsoft.Json;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class Program
    {
        public static void Main()
        {
            // JSON import files are in the project solution - ImportJson folder
            // JSON result files from each task are saved in the project solution - ExportJson folder

            // Task 1. - Create Database
            // Task 2. - Import/Seed Data with Json
            // DatabaseInitialize.ResetDatabase();
            Console.WriteLine("Database reset finished");
            //Task 3 Export data with Json
            using (var db = new ProductShopDbContext())
            {
                // Task 3.1 - Products In Range
                // QueryProductsInRange();

                // Task 3.2 - Successfully Sold Products
                // QuerySuccessfullySoldProducts(db);

                // Task 3.3 - Categories By Products Count
                //QueryCategoriesByProductsCount(db);

                // Task 3.4 - Users and Products
                //QueryUsersAndProducts(db);
            }
            //Task 3 Export data with Xml
            using (var db = new ProductShopDbContext())
            {
                // Task 3.1 - Products In Range
                // QueryProductsInRange();

                // Task 3.2 - Successfully Sold Products
                // QuerySuccessfullySoldProducts(db);

                // Task 3.3 - Categories By Products Count
                //QueryCategoriesByProductsCount(db);

                // Task 3.4 - Users and Products
                //QueryUsersAndProducts(db);
            }

        }

        private static void ExportJsonToFolder<TEntity>(TEntity entityType, string pathToExport)
        {
            string json = JsonConvert.SerializeObject(entityType, Formatting.Indented);
            File.WriteAllText(pathToExport, json);
            Console.WriteLine(json);
        }

        private static void QueryProductsInRange()
        {
            using (var db = new ProductShopDbContext())
            {
                var productsInRange = db.Products
                    .Where(p => p.Price >= 500 && p.Price <= 1000)
                    .OrderBy(p => p.Price)
                    .Select(p => new
                    {
                        name = p.Name,
                        price = p.Price,
                        seller = p.Seller.FirstName + " " + p.Seller.LastName
                    });

                ExportJsonToFolder(productsInRange, "ExportJson/productsInRange.json");
            }
        }

        private static void QuerySuccessfullySoldProducts(ProductShopDbContext db)
        {
            var users = db.Users
                .Where(u => u.SoldProducts.Count >= 1)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    soldProducts = u.SoldProducts.Select(sp => new
                    {
                        name = sp.Name,
                        price = sp.Price,
                        buyerFirstName = sp.Buyer.FirstName,
                        buyerLastName = sp.Buyer.LastName
                    })
                });

            ExportJsonToFolder(users, "ExportJson/successfullySoldProducts.json");
        }

        private static void QueryCategoriesByProductsCount(ProductShopDbContext db)
        {
            var categoriesordered = db.Categories
                .Include(c => c.CategoryProducts)
                .ThenInclude(cp => cp.Product)
                .OrderBy(c => c.Name)
                .ToList();

            var categories = categoriesordered
            .Select(c => new
            {
                name = c.Name,
                productsCount = c.CategoryProducts.Count,
                averagePrice = GetAveragePrice(c),
                totalRevenue = GetTotalRevenue(c)
            })
                .ToList();
            ExportJsonToFolder(categories, "ExportJson/categoriesByProductsCount.json");
        }

        private static decimal GetTotalRevenue(Category c)
        {
            if (c.CategoryProducts.Count == 0)
            {
                return 0;
            }
            return c.CategoryProducts.Sum(cp => cp.Product.Price);
        }

        private static decimal GetAveragePrice(Category c)
        {
            if (c.CategoryProducts.Count == 0)
            {
                return 0;
            }
            return c.CategoryProducts.Average(cp => cp.Product.Price);
        }

        private static void QueryUsersAndProducts(ProductShopDbContext db)
        {
            var users = db.Users
                .Where(u => u.SoldProducts.Count > 0)
                .Include(u => u.SoldProducts)
                .OrderByDescending(u => u.SoldProducts.Count)
                .ThenBy(u => u.LastName)
                .Select(u => new
                {
                    firstName = u.FirstName,
                    lastName = u.LastName,
                    age = u.Age,
                    soldProducts = new
                    {
                        count = u.SoldProducts.Count,
                        products = u.SoldProducts.Select(p => new
                        {
                            name = p.Name,
                            price = p.Price
                        })
                    }
                });

            var usersToSerialize = new
            {
                usersCount = users.Count(),
                users
            };
            
            ExportJsonToFolder(usersToSerialize, "ExportJson/usersAndProducts.json");
        }
    }
}
