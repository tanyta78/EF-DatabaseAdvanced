namespace TeamBuilder.Services.Contracts
{
    public interface ITeamService
    {
        string CreateTeam(string teamName, string acronim, string description);

        string AddTeamTo(string eventName, string teamName);

        string KickMember(string teamName, string userName);

        string Disband(string teamName);

        string ShowTeam(string teamName);
    }
}
