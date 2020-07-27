using log4net;

namespace ITPI.MAP.ExtractLoadManager
{
	/// <summary>
	/// Class responsible for logging to the console window and file.
	/// </summary>
	public class Logger: ILogger
	{
		private static readonly ILog log =
			LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		/// Log error messages.
		/// </summary>
		/// <param name="message">The message to display.</param>
		public void Error(string message)
		{
			log.Error(message);
		}

		/// <summary>
		/// Log information messages.
		/// </summary>
		/// <param name="message">The message to display.</param>
		public void Info(string message)
		{
			log.Info(message);
		}

		/// <summary>
		/// Log warning messages.
		/// </summary>
		/// <param name="message">The message to display.</param>
		public void Warn(string message)
		{
			log.Warn(message);
		}
	}
}
