//=================================================
//Copyright (c) Coalition of Good-Hearted Engineers 
//Free To Use To Find Comfort and Pease
//=================================================
namespace UsefulTime.Api.Brokers.Loggings
{
    public interface ILoggingBroker
    {
        void LogInformation(string message);
        void LogTrace(string message);
        void LogDebug(string message);
        void LogWarning(string message);
        void LogError(Exception exception);
        void LogCritical(Exception exception);
    }
}
