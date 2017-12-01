namespace ProductShop.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using Models;
    using Newtonsoft.Json;

    public class DatabaseInitialize
    {
        public static void ResetDatabase()
        {
            using (var db = new ProductShopDbContext())
            {
               // db.Database.EnsureDeleted();
                //db.Database.Migrate();
               // db.Database.EnsureCreated();
                Console.WriteLine("Successfull create!");

                // Task 2. - Import/Seed Data with Json
                //SeedUsersWithJson(db);
                //SeedProductsWithJson(db);
                //SeedCategoriesWithJson(db);
                //SeedCategoriesProducts(db);
                //Console.WriteLine("Successfully import data with Json!");
                // Task 2. - Import/Seed Data with Xml
                //SeedUsersWithXml(db);
               // SeedProductsWithXml(db);
               // SeedCategoriesWithXml(db);
               // SeedCategoriesProducts(db);
                Console.WriteLine("Successfully import data with Xml!");
            }
        }

        private static void SeedCategoriesWithXml(ProductShopDbContext db)
        {
            XDocument xmlDocument = XDocument.Load("../ProductShop.Data/ImportXml/categories.xml");

            //var categories = xmlDocument.Root.Elements().Select(ParseCategory).ToList();
            var categories = xmlDocument.XPathSelectElements("categories/category").Select(ParseCategory).ToList();

            db.Categories.AddRange(categories);
            db.SaveChanges();
        }

        private static void SeedProductsWithXml(ProductShopDbContext db)
        {
            XDocument xmlDocument = XDocument.Load("../ProductShop.Data/ImportXml/products.xml");

            var products = xmlDocument.Root.Elements().Select(ParseProduct).ToList();
            // var products = xmlDocument.XPathSelectElements("products/product").Select(ParseProduct).ToList();

            int countOfUsers = db.Users.Count();

            Random rnd = new Random();
            foreach (Product product in products)
            {
                product.SellerId = rnd.Next(1, countOfUsers + 1);
                if (product.SellerId % 5 != 0 && product.SellerId % 10 != 0)
                {
                    product.BuyerId = rnd.Next(1, countOfUsers + 1);
                }
            }

            db.Products.AddRange(products);
            db.SaveChanges();

        }

        public static Product ParseProduct(XElement productToParse)
        {
            string name = productToParse.Element("name")?.Value;
            decimal price = decimal.Parse(productToParse.Element("price").Value);

            Product parsedProduct = new Product()
            {
                Name = name,
                Price = price
            };

            return parsedProduct;
        }

        public static Category ParseCategory(XElement categoryToParse)
        {
            string name = categoryToParse.Element("name")?.Value;

            Category parsedCategory = new Category()
            {
                Name = name
            };

            return parsedCategory;
        }

        private static void SeedUsersWithXml(ProductShopDbContext db)
        {
           XDocument xmlDocument=XDocument.Load("../ProductShop.Data/ImportXml/users.xml");

            var users = xmlDocument.Root.Elements().Select(ParseUser).ToList();
           // var users = xmlDocument.XPathSelectElements("users/user").Select(ParseUser).ToList();

            db.Users.AddRange(users);
            db.SaveChanges();
        }

        public static User ParseUser(XElement userToParse)
        {
            string firstName = userToParse.Attribute("firstName")?.Value;
            string lastName = userToParse.Attribute("lastName")?.Value;
            int age = 0;

            if (userToParse.Attribute("age") != null)
            {
                age = int.Parse(userToParse.Attribute("age").Value);
            }

            User parsedUser;

            if (firstName == null)
            {
                parsedUser = new User()
                {
                    LastName = lastName,
                    Age = age
                };
            }
            else
            {
                parsedUser = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                };
            }

            return parsedUser;
        }
        private static void SeedCategoriesProducts(ProductShopDbContext db)
        {
            var products = db.Products;
            var categoriesCount = db.Categories.Count();
            var categoriesProducts = new HashSet<CategoryProduct>();

            Random rnd = new Random();
            foreach (var product in products)
            {
                var categoryId = rnd.Next(1, categoriesCount - 4);
                for (int i = 0; i < 3; i++)
                {
                    Category category = db.Categories.Find(categoryId + i);
                    var categoryProduct = new CategoryProduct();
                    categoryProduct.Product = product;
                    categoryProduct.Category = category;
                    categoriesProducts.Add(categoryProduct);
                }

            }

            db.CategoryProducts.AddRange(categoriesProducts);
            db.SaveChanges();
        }

        private static void SeedCategoriesWithJson(ProductShopDbContext db)
        {
            string jsonCategories = File.ReadAllText("../ProductShop.Data/ImportJson/categories.json");
            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(jsonCategories);

            db.Categories.AddRange(categories);
            db.SaveChanges();
        }

        private static void SeedProductsWithJson(ProductShopDbContext db)
        {
            string jsonProducts = File.ReadAllText("../ProductShop.Data/ImportJson/products.json");
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(jsonProducts);

            int countOfUsers = db.Users.Count();

            Random rnd = new Random();
            foreach (Product product in products)
            {
                product.SellerId = rnd.Next(1, countOfUsers + 1);
                if (product.SellerId % 5 != 0 && product.SellerId % 10 != 0)
                {
                    product.BuyerId = rnd.Next(1, countOfUsers + 1);
                }
            }

            db.Products.AddRange(products);
            db.SaveChanges();
        }

        private static void SeedUsersWithJson(ProductShopDbContext db)
        {
            string jsonUsers = File.ReadAllText("../ProductShop.Data/ImportJson/users.json");
            List<User> users = JsonConvert.DeserializeObject<List<User>>(jsonUsers);

            db.Users.AddRange(users);
            db.SaveChanges();
        }
    }
}
