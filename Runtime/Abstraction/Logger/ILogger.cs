namespace Northgard.Core.Abstraction.Logger
{
    public interface ILogger
    {
        void LogMessage(string msg, object context);
		void LogWarning(string msg, object context);
		void LogError(string msg, object context);
    }
}