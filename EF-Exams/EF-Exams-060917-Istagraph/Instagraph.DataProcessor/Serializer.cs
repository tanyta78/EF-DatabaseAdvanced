using System;

using Instagraph.Data;

namespace Instagraph.DataProcessor
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using AutoMapper.QueryableExtensions;
    using Data.EntityConfig;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var notCommentPosts = context.Posts
                .Where(p => p.Comments.Count == 0)
                .OrderBy(p => p.Id)
                .ProjectTo<NonCommentedPostDto>()
                .ToArray();

            var jsonstring = JsonConvert.SerializeObject(notCommentPosts, Formatting.Indented);

            return jsonstring;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            var popularUsers = context.Users
                .Where(u => u.Posts
                    .Any(p => p.Comments
                        .Any(c => u.Followers
                            .Any(f => f.FollowerId == c.UserId))))
                .OrderBy(u => u.Id)
                .ProjectTo<PopularUserDto>()
                .ToArray();

            string jsonString = JsonConvert.SerializeObject(popularUsers, Formatting.Indented);

            return jsonString;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            var users = context.Users
                .Select(u => new
                {
                    Username = u.Username,
                    PostsCommentCount = u.Posts.Select(p => p.Comments.Count)

                });

            var dtos = new List<UserTopPostsDto>();

            var xDoc = new XDocument();
            xDoc.Add(new XElement("users"));

            foreach (var user in users)
            {
                int mostComment = 0;

                if (user.PostsCommentCount.Any())
                {
                    mostComment = user.PostsCommentCount.OrderByDescending(c => c).First();
                }
                
                var dto = new UserTopPostsDto()
                {
                    Username = user.Username,
                    MostComments = mostComment
                };
                
                dtos.Add(dto);
            }

            dtos = dtos.OrderByDescending(u => u.MostComments).ThenBy(u => u.Username).ToList();

            foreach (var u in dtos)
            {
                xDoc.Root.Add(new XElement("user",
                        new XElement("Username", u.Username),
                        new XElement("MostComments",u.MostComments)));
            }
            
            string result = xDoc.ToString();

            return result;
        }
    }
}
