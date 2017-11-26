namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using PhotoShare.Services;

    public class ShareAlbumCommand : ICommand
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        private readonly AlbumRoleService albumRoleService;

        public ShareAlbumCommand(AlbumRoleService albumRoleService)
        {
            this.albumRoleService = albumRoleService;
        }


        public string Execute(string command, params string[] data)
        {
           
            if (data.Length != 3)
            {
                throw new InvalidOperationException($"Command {command} not valid");
            }

            var albumId = int.Parse(data[0]);
            var username = data[1];
            var permission = data[2];

            return this.albumRoleService.ShareAlbum(albumId, username, permission);
        }
    }
}
