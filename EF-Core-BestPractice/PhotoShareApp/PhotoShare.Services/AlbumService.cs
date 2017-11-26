namespace PhotoShare.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Models;

    public class AlbumService:IAlbumService
    {
        private readonly PhotoShareContext db;

        public AlbumService(PhotoShareContext db)
        {
            this.db = db;
        }

        public string CreateAlbum(string username, string albumTitle, string bgColor, List<string> validTagNames)
        {
            CheckCredentials(username);

            User user = this.FindAndCheckUser(username);
            
            this.FindAndCheckAlbum(albumTitle);
            
            Color colorValue = FindAndCheckColor(bgColor);
            
            List<Tag> tags = this.FindAndCheckTags(validTagNames);
            
            Album album = this.CreateAlbum(albumTitle, colorValue);
            
            this.CreateAndAddAlbumTags(album, tags);

            this.CreateAndAddAlbumRole(user, album);

            return $"Album {albumTitle} successfully created!";
        }

        private static void CheckCredentials(string username)
        {
            if (Session.User == null)
            {
                throw new ArgumentException("You should login first!");
            }
            //we do not need to check if user is owner, because he always owns it when create it, but i check it because of requirements
            if (username!= Session.User.Username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }
        }

        private void CreateAndAddAlbumRole(User user, Album album)
        {
           var albumRole = new AlbumRole
            {
                Album = album,
                User = user,
                Role = Role.Owner
            };

            this.db.AlbumRoles.Add(albumRole);
            album.AlbumRoles.Add(albumRole);

            this.db.SaveChanges();
        }

        private void CreateAndAddAlbumTags(Album album, List<Tag> tags)
        {
            var albumTags = new List<AlbumTag>();

            foreach (var tag in tags)
            {
                var albumTag = new AlbumTag()
                {
                    Tag = tag,
                    Album = album
                };

                albumTags.Add(albumTag);
            }

            this.db.AlbumTags.AddRange(albumTags);

            this.db.SaveChanges();
        }

        private Album CreateAlbum(string albumTitle, Color colorValue)
        {
            Album album = new Album
            {
                Name = albumTitle,
                BackgroundColor = colorValue
            };

            this.db.Albums.Add(album);
            this.db.SaveChanges();

            return album;

        }

        private List<Tag> FindAndCheckTags(List<string> validTagNames)
        {
            var tagsInDb = db.Tags.Select(t => t.Name).ToList();

            if (!validTagNames.All(t => tagsInDb.Contains(t)))
            {
                throw new ArgumentException("Invalid tags!");
            }

            var tags = this.db.Tags.Where(t => validTagNames.Contains(t.Name)).ToList();

            return tags;
        }

        private static Color FindAndCheckColor(string bgColor)
        {
            Color colorValue;

            if (!Enum.TryParse(bgColor, out colorValue))
            {
                throw new ArgumentException($"Color {bgColor} not found!");
            }

            return colorValue;
        }

        private void FindAndCheckAlbum(string albumTitle)
        {
            var albumExist = db.Albums
                            .Any(a => a.Name == albumTitle);

            if (albumExist)
            {
                throw new ArgumentException($"Album {albumTitle} exists!");
            }
        }

        private User FindAndCheckUser(string username)
        {
            var user = db.Users
                                .FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            return user;
        }
    }
}
