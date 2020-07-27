namespace ITPI.MAP.ExtractLoadManager
{
	/// <summary>
	/// Interface used for logging.
	/// </summary>
	public interface ILogger
	{
		void Info(string message);

		void Error(string message);

		void Warn(string message);
	}
}
