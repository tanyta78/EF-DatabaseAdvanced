namespace TeamBuilder.Services.Contracts
{
   public interface IInvitationService
    {
        string InviteToTeam(string teamName, string userName);

        string AcceptInvite(string teamName);

        string DeclineInvite(string teamName);
    }
}
