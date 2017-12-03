namespace TeamBuilder.Services.Contracts
{
    using System;

    public interface IEventService
    {
        string CreateEvent(string name, string description, DateTime startDate, DateTime endDate);

        //There might be several events with the same name. Always pick the one with the latest start date!
        string ShowEvent(string eventName);
    }
}
