using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

using Newtonsoft.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Instagraph.Data;
using Instagraph.Models;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        private static string errorMsg = "Error: Invalid data.";
        private static string successMsg = "Successfully imported {0}.";

        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var pictures = JsonConvert.DeserializeObject<Picture[]>(jsonString);

            StringBuilder sb = new StringBuilder();
            
            var validatedPictures = new List<Picture>();

            foreach (var picture in pictures)
            {
                bool isValid = !string.IsNullOrWhiteSpace(picture.Path) && picture.Size > 0;
                
                bool pictureExist = context.Pictures.Any(p => p.Path == picture.Path) || validatedPictures.Any(p => p.Path == picture.Path);

                if (!isValid || pictureExist)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                validatedPictures.Add(picture);
                sb.AppendLine(String.Format(successMsg, $"Picture {picture.Path}"));
            }

            context.Pictures.AddRange(validatedPictures);
            context.SaveChanges();
            
            string result = sb.ToString().TrimEnd();

            return result;
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            UserDto[] deserializedUsers = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            var sb = new StringBuilder();

            var validatedUsers = new List<User>();

            foreach (var userDto in deserializedUsers)
            {
                bool isValid = !String.IsNullOrWhiteSpace(userDto.Username) 
                               && !String.IsNullOrWhiteSpace(userDto.Password) 
                               && userDto.Username.Length <= 30 
                               && userDto.Password.Length <= 20
                    && !string.IsNullOrWhiteSpace(userDto.ProfilePicture);

                var picture = context.Pictures.FirstOrDefault(p => p.Path == userDto.ProfilePicture);

                bool userExist = validatedUsers.Any(u => u.Username == userDto.Username);

                if (userExist || !isValid || picture == null)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var user = Mapper.Map<User>(userDto);
                user.ProfilePicture = picture;

                validatedUsers.Add(user);
                sb.AppendLine(String.Format(successMsg, $"User {user.Username}"));

            }

            context.Users.AddRange(validatedUsers);
            context.SaveChanges();
            
            string result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            UserFollowerDto[] deserializedFollowers = JsonConvert.DeserializeObject<UserFollowerDto[]>(jsonString);

            var sb = new StringBuilder();

            var validatedFollowers = new List<UserFollower>();

            foreach (var dto in deserializedFollowers)
            {
                int? userId = context.Users.FirstOrDefault(u => u.Username == dto.User)?.Id;
                int? followerId = context.Users.FirstOrDefault(u => u.Username == dto.Follower)?.Id;

                if (userId == null || followerId==null)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }
                
                bool isFollowed = validatedFollowers.Any(uf =>
                    uf.UserId == userId && uf.FollowerId==followerId);
                
                if (isFollowed)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var userFollower = new UserFollower()
                {
                    UserId = userId.Value,
                    FollowerId = followerId.Value
                };

               
                validatedFollowers.Add(userFollower);
                sb.AppendLine($"Successfully imported Follower {dto.Follower} to User {dto.User}.");
            }

            context.UsersFollowers.AddRange(validatedFollowers);
            context.SaveChanges();
            
            string result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);

            var elements = xDoc.Root.Elements();
            
            var sb = new StringBuilder();
            
            var validatedPosts = new List<Post>();

            foreach (var xElement in elements)
            {
                string caption = xElement.Element("caption")?.Value;
                string username = xElement.Element("user")?.Value;
                string picturePath = xElement.Element("picture")?.Value;


                var isNotNullInput = !String.IsNullOrWhiteSpace(caption) 
                    && !String.IsNullOrWhiteSpace(username) 
                    && !String.IsNullOrWhiteSpace(picturePath);

                if (!isNotNullInput)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var userId = context.Users.FirstOrDefault(u => u.Username == username)?.Id;
                var pictureId = context.Pictures.FirstOrDefault(p => p.Path == picturePath)?.Id;

                if (userId==null || pictureId==null)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }
                
                var post = new Post()
                {
                    Caption = caption,
                    UserId = userId.Value,
                    PictureId = pictureId.Value
                };
                
                validatedPosts.Add(post);
                sb.AppendLine(String.Format(successMsg, $"Post {caption}"));
            }

            context.Posts.AddRange(validatedPosts);
            context.SaveChanges();

            string result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var xDoc = XDocument.Parse(xmlString);

            var elements = xDoc.Root.Elements();

            var sb = new StringBuilder();

            var validatedComments = new List<Comment>();

            foreach (var element in elements)
            {
                string content = element.Element("content")?.Value;
                string username = element.Element("user")?.Value;
                string postIdAsString = element.Element("post")?.Attribute("id")?.Value;
                
                bool isNotNullInput = !String.IsNullOrWhiteSpace(content)
                                   && !String.IsNullOrWhiteSpace(username)
                                   && !String.IsNullOrWhiteSpace(postIdAsString);

                if (!isNotNullInput)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                int postId = int.Parse(postIdAsString);
                
                var userId = context.Users.FirstOrDefault(u => u.Username == username)?.Id;
                var postExist = context.Posts.Any(p => p.Id == postId);

                if (userId == null || !postExist)
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var comment = new Comment()
                {
                    Content = content,
                    UserId = userId.Value,
                    PostId = postId
                };


                validatedComments.Add(comment);
                sb.AppendLine(String.Format(successMsg, $"Comment {content}"));
            }
            
            context.Comments.AddRange(validatedComments);
            context.SaveChanges();
            
            string result = sb.ToString().TrimEnd();
            return result;
        }
    }
}
