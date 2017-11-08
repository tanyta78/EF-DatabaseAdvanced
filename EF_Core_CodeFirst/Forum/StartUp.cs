namespace Forum
{
    using System;
    using System.Linq;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query.Expressions;

    public class StartUp
    {
        public static void Main()
        {

            var context = new ForumDbContext();

            var tags = new[]
            {
                new Tag {Name = "C#"},
                new Tag {Name = "Programming"},
                new Tag {Name = "Java"},
                new Tag {Name = "SQL"}
            };

            var postTags = new[]
            {
                new PostTag() {PostId = 1, Tag = tags[0]},
                new PostTag() {PostId = 1, Tag = tags[1]},
                new PostTag() {PostId = 1, Tag = tags[2]},
                new PostTag() {PostId = 1, Tag = tags[3]},

            };
            
            context.Tags.AddRange(tags);
            context.PostsTags.AddRange(postTags);
            context.SaveChanges();

            //ResetDatabase(context);

            //with include

            //var categories = context.Categories
            //    .Include(c => c.Posts)
            //    .ThenInclude(p => p.Author)
            //    .Include(c => c.Posts)
            //    .ThenInclude(p=>p.Replies);

            //with Select
            var categories = context.Categories
                .Select(c => new
                {
                    Name = c.Name,
                    Posts = c.Posts.Select(p => new
                    {
                        Title = p.Title,
                        Content = p.Content,
                        AuthorUsername = p.Author.Username,
                        Replies = p.Replies.Select(r => new
                        {
                            Content = r.Content,
                            Author = r.Author.Username
                        }),
                        Tags = p.PostTags.Select(t=>t.Tag.Name)
                    })

                });

            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Name} ({category.Posts.Count()} posts)");

                foreach (var post in category.Posts)
                {
                    Console.WriteLine($"=={post.Title} {post.Content}");
                    Console.WriteLine($"====by {post.AuthorUsername}");

                    Console.WriteLine("Tags: " +string.Join(", ",post.Tags));
                    
                    foreach (var reply in post.Replies)
                    {
                        Console.WriteLine($"===={reply.Content} from {reply.Author}");
                    }
                }
            }

        }

        private static void ResetDatabase(ForumDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Database.Migrate();

            Seed(context);
        }

        private static void Seed(ForumDbContext context)
        {
            var users = new[]
            {
                new User("gosho", "1234"),
                new User("pesho", "1234"),
                new User("merry", "1234"),
                new User("cris", "1234")
            };

            context.Users.AddRange(users);

            var categories = new[]
            {
                new Category("C#"),
                new Category("Support"),
                new Category("ExternalProject"),
                new Category("EFCore"),
            };

            context.Categories.AddRange(categories);

            var posts = new[]
            {
                new Post("C# Rules", "Holla!", categories[0], users[0]),
                new Post("Rules", "Crush!", categories[1], users[2]),
                new Post("New api", "Yeeep!", categories[2], users[1]),
                new Post("No connection", "Nooooo!", categories[2], users[0]),

            };

            context.Posts.AddRange(posts);

            var replies = new[]
            {
                new Reply("Switch on TeemViewer!", posts[1], users[0]),
                new Reply("Good one!", posts[2], users[2]),

            };

            context.Replies.AddRange(replies);

            context.SaveChanges();
        }
    }
}
