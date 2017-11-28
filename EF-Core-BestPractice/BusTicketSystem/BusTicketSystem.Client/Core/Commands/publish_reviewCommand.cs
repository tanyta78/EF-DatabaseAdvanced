namespace BusTicketSystem.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Contracts;
    using Services.Contracts;

    public class publish_reviewCommand:ICommand
    {
        private readonly IReviewService reviewService;

        public publish_reviewCommand(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }
        public string Execute(string cmd, params string[] args)
        {
            /*if (args.Length != 4)
            {
                throw new InvalidOperationException($"Command {cmd} not valid");
            }*/

            var customerId = int.Parse(args[0]);
            var grade = double.Parse(args[1]);
            var buscompanyName = args[2];
            var content = String.Join("",args.Skip(3).ToList());

          return this.reviewService.PublishReview(customerId,grade,buscompanyName,content);

        }
    }
}
