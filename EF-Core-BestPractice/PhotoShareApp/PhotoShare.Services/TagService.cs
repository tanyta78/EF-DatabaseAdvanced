namespace PhotoShare.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Data;
    using Models;

    public class TagService : ITagService
    {
        private readonly PhotoShareContext db;

        public TagService(PhotoShareContext db)
        {
            this.db = db;
        }
        public string AddTag(string tagName)
        {

            CheckCredentials();

            var newTag = new Tag
            {
                Name = tagName
            };

            if (db.Tags.Any(t => t.Name == tagName))
            {
                throw new ArgumentException($"Tag {tagName} exists!");
            }

            db.Tags.Add(newTag);

            db.SaveChanges();


            return tagName + " was added successfully!";
        }

        private static void CheckCredentials()
        {
            if (Session.User == null)
            {
                throw new ArgumentException("You should login first!");
            }

        }
    }
}
