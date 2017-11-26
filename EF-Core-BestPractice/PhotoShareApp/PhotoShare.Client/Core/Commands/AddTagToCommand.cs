namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services;
    using Services.Contracts;
    using Utilities;

    public class AddTagToCommand:ICommand
    {
        private readonly IAlbumTagService albumTagService;

        public AddTagToCommand(IAlbumTagService albumTagService)
        {
            this.albumTagService = albumTagService;
        }
        // AddTagTo <albumName> <tag>
        public string Execute(string command, params string[] data)
        {
            if (data.Length != 2)
            {
                throw new InvalidOperationException($"Command {data[0]} not valid");
            }

            var albumName = data[0];
            var tagName = data[1].ValidateOrTransform();

            return albumTagService.AddTagTo(albumName, tagName);
        }
    }
}
