namespace PhotoShare.Services
{
    using System;
    using Contracts;
    using Data;
    using Models;
    using System.Linq;

    public class AlbumRoleService:IAlbumRoleService
    {
        private readonly PhotoShareContext db;

        public AlbumRoleService(PhotoShareContext db)
        {
            this.db = db;
        }
        
        public string ShareAlbum(int albumId, string username, string permission)
        {
            User user = this.FindAndCheckUser(username);
            Album album = this.FindAndCheckAlbum(albumId);
            
            CheckCredentials(user);

            AlbumRole albumRole = FindCheckAndCreateAlbumRole(permission, user, album);

           this.db.AlbumRoles.Add(albumRole);
            album.AlbumRoles.Add(albumRole);
            user.AlbumRoles.Add(albumRole);

            this.db.SaveChanges();

            return $"Username {username} added to album {album.Name} ({permission})";
        }

        private static void CheckCredentials(User user)
        {
            if (Session.User == null)
            {
                throw new ArgumentException("Invalid credentials! ");
            }

            if (user.Username != Session.User.Username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }
        }

        private AlbumRole FindCheckAndCreateAlbumRole(string permission, User user, Album album)
        {
            if (!Enum.TryParse(permission, out Role role))
            {
                throw new ArgumentException($"Permission must be either “Owner” or “Viewer”!");
            }

            var albumRole = new AlbumRole
            {
                Album = album,
                Role = role,
                User = user
            };
            
            //check if albumrole already exist
            if (this.db.AlbumRoles.Contains(albumRole))
            {
                throw new ArgumentException($"Username {user.Username} is already added to album {album.Name} ({permission})");
            }

            return albumRole;
        }

        private Album FindAndCheckAlbum(int albumId)
        {
            var album = this.db.Albums.FirstOrDefault(a => a.Id == albumId);

            if (album == null)
            {
                throw new ArgumentException($"Album {albumId} not found!");
            }

            return album;
        }

        private User FindAndCheckUser(string username)
        {
            var user = this.db.Users
                            .FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            return user;
        }
    }
}
