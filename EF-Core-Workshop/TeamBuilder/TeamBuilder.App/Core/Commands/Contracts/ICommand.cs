namespace TeamBuilder.App.Core.Commands.Contracts
{
    public interface ICommand
    {
        string Execute(string cmd, params string[] args);
    }
}
