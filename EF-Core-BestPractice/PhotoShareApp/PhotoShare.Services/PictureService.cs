namespace PhotoShare.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class PictureService:IPictureService
    {
        private readonly PhotoShareContext db;

        public PictureService(PhotoShareContext db)
        {
            this.db = db;
        }
        
        public string UploadPicture(string albumName, string pictureTitle, string pictureFilePath)
        {
            User user = Session.User;

            CheckCredentials(albumName, user);
            
            Album album = FindAndCheckAlbum(albumName);

            var picture = new Picture
            {
                Path = pictureFilePath,
                Title = pictureTitle,
                Album = album,
                UserProfile = user
            };

            db.Pictures.Add(picture);
            album.Pictures.Add(picture);


            db.SaveChanges();

            return $"Picture {pictureTitle} added to {albumName}!";
        }

        private Album FindAndCheckAlbum(string albumName)
        {
            var album = db.Albums
                                .Include(a => a.Pictures)
                                .Include(a => a.AlbumRoles)
                                .FirstOrDefault(a => a.Name == albumName);

            if (album == null)
            {
                throw new ArgumentException($"Album {albumName} not found!");
            }

            return album;
        }

        private static void CheckCredentials(string albumName, User user)
        {
            if (user == null)
            {
                throw new InvalidOperationException("Invalid credentials! ");
            }
            
            //check if user is owner of the album
            var role = user.AlbumRoles.SingleOrDefault(ar => ar.Album.Name == albumName);

            if (role == null || !role.Role.Equals(Role.Owner))
            {
                throw new InvalidOperationException($"Invalid credentials!");
            }
        }
    }
}
