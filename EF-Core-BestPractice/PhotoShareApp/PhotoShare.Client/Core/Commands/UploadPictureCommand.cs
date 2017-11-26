namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;

    public class UploadPictureCommand:ICommand
    {
        private readonly IPictureService pictureService;

        public UploadPictureCommand(IPictureService pictureService)
        {
            this.pictureService = pictureService;
        }

        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public string Execute(string command,params string[] data)
        {
            if (data.Length != 3)
            {
                throw new InvalidOperationException($"Command {data[0]} not valid");
            }

            var albumName = data[0];
            var pictureTitle = data[1];
            var pictureFilePath = data[2];

            return this.pictureService.UploadPicture(albumName, pictureTitle, pictureFilePath);
        }
    }
}
