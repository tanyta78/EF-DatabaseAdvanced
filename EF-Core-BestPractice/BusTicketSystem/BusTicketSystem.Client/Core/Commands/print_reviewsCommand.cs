namespace BusTicketSystem.Client.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;

    public class print_reviewsCommand:ICommand
    {
        private readonly IReviewService reviewService;

        public print_reviewsCommand(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        public string Execute(string cmd, params string[] args)
        {
            if (args.Length != 1)
            {
                throw new InvalidOperationException($"Command {cmd} not valid");
            }

            return this.reviewService.PrintReview(int.Parse(args[0]));
        }
    }
}
