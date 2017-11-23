namespace BookShop
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using BookShop.Data;
    using BookShop.Initializer;
    using Models;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                //   DbInitializer.ResetDatabase(db);

                //o	AgeRestriction – enum (Minor, Teen, Adult)
                // string command = Console.ReadLine();
                // Console.Write(GetBooksByAgeRestriction(db, command));

                // Console.Write(GetGoldenBooks(db));

                // Console.Write(GetBooksByPrice(db));

                //int year = int.Parse(Console.ReadLine());
                //Console.Write(GetBooksNotRealeasedIn(db, year));

                //string input = Console.ReadLine();
                //Console.WriteLine(GetBooksByCategory(db, input));

                //string date = Console.ReadLine();
                //Console.Write(GetBooksReleasedBefore(db,date));

                //string input = Console.ReadLine();
                //Console.Write(GetAuthorNamesEndingIn(db, input));

                //string input = Console.ReadLine();
                //Console.Write(GetBookTitlesContaining(db, input));

                //string input = Console.ReadLine();
                //Console.Write(GetBooksByAuthor(db, input));

                //int input = int.Parse(Console.ReadLine());
                //Console.WriteLine(CountBooks(db, input));


                // Console.WriteLine(CountCopiesByAuthor(db));

               // Console.WriteLine(GetTotalProfitByCategory(db));

                // Console.WriteLine(GetMostRecentBooks(db));

                // Console.WriteLine(RemoveBooks(db));

            }
        }

        public static int RemoveBooks(BookShopContext db)
        {
            var books = db.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            foreach (var book in books)
            {
                db.Books.Remove(book);
            }

            db.SaveChanges();

            return books.Count();
        }

        public static void IncreasePrices(BookShopContext db)
        {
            var books = db.Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .ToList();

            foreach (var book in books)
            {
                book.Price += 5;
            }

            db.SaveChanges();
        }

        public static string GetMostRecentBooks(BookShopContext db)
        {

            var result = db.Categories
                .OrderBy(c => c.CategoryBooks.Count)
                .Select(c => new
                {
                    c.Name,
                    TopThree = c.CategoryBooks
                        .Select(cb => cb.Book)
                        .OrderByDescending(b => b.ReleaseDate)
                        .Take(3)
                        
                }).ToList();

            var sb = new StringBuilder();
            foreach (var item in result.OrderBy(c=>c.Name))
            {
                sb.AppendLine($"--{item.Name}");
                foreach (var book in item.TopThree)
                {
                    sb.AppendLine($"{book.Title} ({book.ReleaseDate.Value.Year})");
                }
            }
            return sb.ToString().Trim();
        }



        public static string GetTotalProfitByCategory(BookShopContext db)
        {
            var result = db.Categories
                .Select(c => new
                {
                    CategoryName = c.Name,
                    TotalProfit = c.CategoryBooks.Sum(cb => cb.Book.Copies * cb.Book.Price)
                })
                .OrderByDescending(c => c.TotalProfit)
                .ThenBy(c => c.CategoryName)
                .ToList();

            var sb = new StringBuilder();
            foreach (var item in result)
            {
                sb.AppendLine($"{item.CategoryName} ${item.TotalProfit:f2}");
            }
            return sb.ToString().Trim();
        }

        public static string CountCopiesByAuthor(BookShopContext db)
        {
            var authors = db.Authors
                .Select(a => new
                {
                    Name = $"{a.FirstName} {a.LastName}",
                    Copies = a.Books.Select(b => b.Copies).Sum()
                })
                .OrderByDescending(a => a.Copies)
                .ToList();

            var sb = new StringBuilder();
            foreach (var author in authors)
            {
                sb.AppendLine($"{author.Name} - {author.Copies}");
            }

            return sb.ToString().Trim();

            /*
            var authors = db.Authors
                .GroupBy(a => new
                {
                    FullName = a.FirstName + " " + a.LastName,
                    TotalCopies = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(a => a.Key.TotalCopies)
                .ToList();

            var sb = new StringBuilder();
            foreach (var author in authors)
            {
               sb.AppendLine($"{author.Key.FullName} - {author.Key.TotalCopies}");
            }

            return sb.ToString();

            //var authors = db.Books
            //     .GroupBy(b => b.Author)
            //     .Select(b => new
            //     {
            //         b.Key.FirstName,
            //         b.Key.LastName,
            //         Copies = b.Sum(c => c.Copies)
            //     })
            //     .OrderByDescending(c => c.Copies)
            //     .ToList();


            //var sb = new StringBuilder();
            //foreach (var author in authors)
            //{
            //    sb.AppendLine($"{author.FirstName} {author.LastName} - {author.Copies}");
            //}

            //return sb.ToString();*/
        }

        public static int CountBooks(BookShopContext db, int input)
        {
            var books = db.Books
                .Count(b => b.Title.Length > input);

            return books;
        }

        public static string GetBooksByAuthor(BookShopContext db, string input)
        {
            var books = db.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => new
                {
                    b.Title,
                    b.Author.FirstName,
                    b.Author.LastName
                })
                .ToList();


            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.FirstName} {book.LastName})");
            }

            return sb.ToString();
        }

        public static string GetBookTitlesContaining(BookShopContext db, string input)
        {
            var books = db.Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .OrderBy(b => b.Title)
                .Select(b => b.Title)
                .ToList();

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext db, string input)
        {
            var authors = db.Authors
                .Where(a => a.FirstName.ToLower().EndsWith(input.ToLower()))
                .Select(a => new
                {
                    Name = a.FirstName + " " + a.LastName
                })
                .OrderBy(a=>a.Name)
                .ToList();
            
            var sb = new StringBuilder();
            foreach (var author in authors)
            {
                sb.AppendLine(author.Name);
            }

            return sb.ToString();
        }

        public static string GetBooksReleasedBefore(BookShopContext db, string date)
        {
            DateTime givenDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            var books = db.Books
                .Where(b => b.ReleaseDate < givenDate)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new { b.Title, b.EditionType, b.Price })
                .ToList();

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:f2}");
            }
            /*var result = context.Books
            .Where(b => b.ReleaseDate < inputDate)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:f2}");
            return string.Join(Environment.NewLine, result);*/
            return sb.ToString();
        }

        public static string GetBooksByCategory(BookShopContext db, string input)
        {
            var categories = input.ToLower().Split(new[] { "\t", Environment.NewLine, " " }, StringSplitOptions.RemoveEmptyEntries).ToArray();


            var books = db.Books
                .Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .OrderBy(b => b.Title)
                .Select((book => book.Title))
                .ToList();

            var result = string.Join(Environment.NewLine, books);

            return result;
        }

        public static string GetBooksNotRealeasedIn(BookShopContext db, int year)
        {
            var books = db.Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select((book => book.Title))
                .ToList();

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString();
        }

        public static string GetBooksByPrice(BookShopContext db)
        {
            var books = db.Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select((book => new
                {
                    book.Title,
                    book.Price
                }))
                .ToList();

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - ${book.Price:f2}");
            }

            return sb.ToString();
        }

        public static string GetGoldenBooks(BookShopContext db)
        {
            var books = db.Books
                .Where(b => (Enum.GetName(typeof(EditionType), b.EditionType).ToLower() == "gold") && b.Copies < 5000)
                .OrderBy(b => b.BookId)
                .Select((book => book.Title))
                .ToList();

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString();
        }

        public static string GetBooksByAgeRestriction(BookShopContext db, string command)
        {

            var books = db.Books
                .Where(b => (Enum.GetName(typeof(AgeRestriction), b.AgeRestriction).ToLower() == command.ToLower()))
                .Select((book => book.Title))
                .ToList();


            books.Sort();

            var sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine(book);
            }

            return sb.ToString();
        }
    }
}
