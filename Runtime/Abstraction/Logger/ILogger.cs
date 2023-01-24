namespace Northgard.Core.Abstraction.Logger
{
    public interface ILogger
    {
        void LogMessage(string msg);
		void LogWarning(string msg);
		void LogError(string msg);
    }
}