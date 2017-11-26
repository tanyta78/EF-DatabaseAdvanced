namespace PhotoShare.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class AlbumTagService:IAlbumTagService
    {
        private readonly PhotoShareContext db;

        public AlbumTagService(PhotoShareContext db)
        {
            this.db = db;
        }
        
        public string AddTagTo(string albumName, string tagName)
        {
            User user = Session.User;

            CheckCredentials(albumName, user);

            var album = db.Albums
                .Include(a => a.AlbumTags)
                .ThenInclude(t => t.Tag)
                .Include(a => a.AlbumRoles)
                .FirstOrDefault(a => a.Name == albumName);

            var tag = db.Tags
                .Include(t => t.AlbumTags)
                .ThenInclude(a => a.Album)
                .FirstOrDefault(t => t.Name == tagName);
            
            CheckAlbumAndTag(albumName, tagName, album, tag);

            var albumTag = new AlbumTag()
            {
                Album = album,
                Tag = tag
            };

            db.AlbumTags.Add(albumTag);
            album.AlbumTags.Add(albumTag);
            tag.AlbumTags.Add(albumTag);

            db.SaveChanges();



            return $"Tag {tagName} added to {albumName}!";
        }

        private static void CheckAlbumAndTag(string albumName, string tagName, Album album, Tag tag)
        {
            if (album == null || tag == null)
            {
                throw new ArgumentException($"Either tag or album do not exist!");
            }

            if (album.AlbumTags.Any(at => at.Tag.Name == tagName) || tag.AlbumTags.Any(at => at.Album.Name == albumName))
            {
                throw new ArgumentException($"Connection between tag and album already exist!");
            }
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
