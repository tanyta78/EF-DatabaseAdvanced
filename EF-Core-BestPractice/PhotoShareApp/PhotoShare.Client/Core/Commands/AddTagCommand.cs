namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;
    using Utilities;

    public class AddTagCommand:ICommand
    {
        private readonly ITagService tagService;

        public AddTagCommand(ITagService tagService)
        {
            this.tagService = tagService;
        }


        // AddTag <tag>
        public string Execute(string command,params string[] data)
        {
            if (data.Length != 1)
            {
                throw new InvalidOperationException($"Command {command} not valid");
            }

            string tag = data[0].ValidateOrTransform();

            return this.tagService.AddTag(tag);
        }
    }
}
