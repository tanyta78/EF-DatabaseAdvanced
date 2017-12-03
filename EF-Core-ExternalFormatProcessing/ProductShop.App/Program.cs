namespace ProductShop.App
{
    using System;
    using System.IO;
    using Data;
    using Models;
    using Newtonsoft.Json;
    using System.Linq;
    using System.Xml.Linq;
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
                // QueryProductsInRangeXml(db);

                // Task 3.2 - Successfully Sold Products
                //QuerySuccessfullySoldProductsXml(db);

                // Task 3.3 - Categories By Products Count
                // QueryCategoriesByProductsCountXml(db);

                // Task 3.4 - Users and Products
                QueryUsersAndProductsXml(db);
            }

        }

        private static void QueryUsersAndProductsXml(ProductShopDbContext db)
        {
            //Get all users who have at least 1 sold product. Order them by the number of sold products (from highest to lowest), then by last name (ascending). Select only their first and last name, age and for each product - name and price.

            var users = db.Users
                .Include(u => u.SoldProducts)
                .Where(u => u.SoldProducts.Count > 0)
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
                            productName = p.Name,
                            prductPrice = p.Price
                        })
                    }
                });

            XDocument documentXml = new XDocument();

            XElement userListXml = new XElement("users");
            userListXml.SetAttributeValue("count", users.Count());

            foreach (var user in users)
            {
                XElement userXml = new XElement("user");

                if (user.firstName != null)
                {
                    userXml.SetAttributeValue("first-name", user.firstName);
                }

                userXml.SetAttributeValue("last-name", user.lastName);
                userXml.SetAttributeValue("age", user.age);

                XElement soldProductsXml = new XElement("sold-products");
                soldProductsXml.SetAttributeValue("count", user.soldProducts.count);

                foreach (var product in user.soldProducts.products)
                {
                    XElement productXml = new XElement("product");
                    productXml.SetAttributeValue("name", product.productName);
                    productXml.SetAttributeValue("price", product.prductPrice);

                    soldProductsXml.Add(productXml);
                }

                userXml.Add(soldProductsXml);
                userListXml.Add(userXml);
            }

            documentXml.Add(userListXml);
            documentXml.Save("ExportXml/usersAndProducts.xml");
        }
        

        private static void QueryCategoriesByProductsCountXml(ProductShopDbContext db)
        {
            //Get all categories. Order them by the number of products in that category (a product can be in many categories). For each category select its name, the number of products, the average price of those products and the total revenue (total price sum) of those products (regardless if they have a buyer or not).
            var categoriesordered = db.Categories
                .Include(c => c.CategoryProducts)
                .ThenInclude(cp => cp.Product)
                .OrderByDescending(c => c.CategoryProducts.Count)
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

            XDocument documentXml = new XDocument();

            XElement categoryListXml = new XElement("categories");

            foreach (var category in categories)
            {
                XElement categoryXml = new XElement("category");

                categoryXml.SetAttributeValue("name", category.name);
                categoryXml.SetElementValue("product-count", category.productsCount);
                categoryXml.SetElementValue("average-price", category.averagePrice);
                categoryXml.SetElementValue("total-revenue", category.totalRevenue);

                categoryListXml.Add(categoryXml);
            }

            documentXml.Add(categoryListXml);
            documentXml.Save("ExportXml/categoriesByProductCount.xml");
        }

        private static void QuerySuccessfullySoldProductsXml(ProductShopDbContext db)
        {
            //Get all users who have at least 1 sold item. Order them by last name, then by first name. Select the person's first and last name. For each of the sold products, select the product's name and price.

            var users = db.Users
                .Include(u => u.SoldProducts)
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
            XDocument documentXml = new XDocument();

            XElement userListXml = new XElement("users");

            foreach (var user in users)
            {
                XElement userXml = new XElement("user");

                if (user.firstName != null)
                {
                    userXml.SetAttributeValue("first-name", user.firstName);

                }

                userXml.SetAttributeValue("last-user", user.lastName);

                XElement productListXml = new XElement("sold-products");

                var products = user.soldProducts;

                foreach (var product in products)
                {
                    XElement productXml = new XElement("product");

                    productXml.SetElementValue("name", product.name);
                    productXml.SetElementValue("price", product.price);

                    productListXml.Add(productXml);
                }

                userXml.Add(productListXml);
                userListXml.Add(userXml);
            }

            documentXml.Add(userListXml);
            documentXml.Save("ExportXml/soldProducts.xml");
        }

        private static void QueryProductsInRangeXml(ProductShopDbContext db)
        {
            // Get all products in a specified price range between 1000 and 2000(inclusive) which have a buyer. Order them by price(from lowest to highest). Select only the product name, price and the full name of the buyer. Export the result to XML.
            var productsInRange = db.Products
                  .Include(p => p.Buyer)
                  .Where(p => p.Price >= 1000 && p.Price <= 2000 && p.BuyerId != null)
                  .OrderBy(p => p.Price)
                  .Select(p => new
                  {
                      name = p.Name,
                      price = p.Price,
                      buyer = p.Buyer.FirstName + " " + p.Buyer.LastName
                  });

            XDocument documentXml = new XDocument();

            XElement productListXml = new XElement("products");

            foreach (var product in productsInRange)
            {
                XElement productXml = new XElement("product");

                productXml.SetAttributeValue("name", product.name);
                productXml.SetAttributeValue("price", product.price);
                productXml.SetAttributeValue("buyer", product.buyer);

                productListXml.Add(productXml);
            }

            documentXml.Add(productListXml);
            documentXml.Save("ExportXml/productsInRange.xml");
        }

        private static void ExportXmlToFolder<TEntity>(TEntity entityType, string pathToExport)
        {
            XDeclaration declaration = new XDeclaration("1.0", "utf-8", null);
            XDocument xDoc = new XDocument(declaration);
            // Add data: xDoc.Add(new XElement("categories"));
            string result = xDoc.Declaration + Environment.NewLine + xDoc;
        }


        private static void ExportJsonToFolder<TEntity>(TEntity entityType, string pathToExport)
        {
            string json = JsonConvert.SerializeObject(entityType, Formatting.Indented);
            File.WriteAllText(pathToExport, json);
            Console.WriteLine(json);
        }

        private static void QueryProductsInRange()
        {
            //Get all products in a specified price range:  500 to 1000 (inclusive). Order them by price (from lowest to highest). Select only the product name, price and the full name of the seller. Export the result to JSON.
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
            //Get all users who have at least 1 sold item with a buyer. Order them by last name, then by first name. Select the person's first and last name. For each of the sold products (products with buyers), select the product's name, price and the buyer's first and last name.
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
            //Get all categories. Order them by the category’s name. For each category select its name, the number of products, the average price of those products and the total revenue (total price sum) of those products (regardless if they have a buyer or not).
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
            //Get all users who have at least 1 sold product. Order them by the number of sold products (from highest to lowest), then by last name (ascending). Select only their first and last name, age and for each product - name and price.
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
