namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Services;
    using Services.Contracts;
    using Utilities;

    public class CreateAlbumCommand:ICommand
    {
        private readonly IAlbumService albumService;

        public CreateAlbumCommand(IAlbumService albumService)
        {
            this.albumService = albumService;
        }
        
        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string command, params string[] data)
        {
            if (data.Length<4)
            {
                throw new InvalidOperationException($"Command {data[0]} not valid");
            }
            
            var username = data[0];
            var albumTitle = data[1];
            var bgcolor = data[2];
            var tagNames = data.Skip(3);

            var validatedTags = new List<string>();
            foreach (var tagName in tagNames)
            {
                validatedTags.Add(tagName.ValidateOrTransform());
            }

          return  this.albumService.CreateAlbum(username, albumTitle, bgcolor, validatedTags);
        }
    }
}
