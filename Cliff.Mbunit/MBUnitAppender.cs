using Gallio.Framework;
using Gallio.Runtime;
using Gallio.Runtime.Logging;
using log4net.Appender;
using log4net.Core;

namespace Cliff.MbUnit
{
	public class MbUnitAppender : AppenderSkeleton
	{
		public MbUnitAppender()
		{
			ToTestLog = true;
		}

		/// <summary>
		/// This writes to <see cref="TestLog.ConsoleOutput"/>, which appears with the test result.
		/// </summary>
		public bool ToTestLog { get; set; }

		/// <summary>
		/// This will out diagnostic log. It is output immediately to the console, and will appear when you view a report as HTML
		/// </summary>
		public bool ToDiagnosticLog { get; set; }

		protected override void Append(LoggingEvent loggingEvent)
		{
			if (ToTestLog)
			{
				loggingEvent.WriteRenderedMessage(TestLog.ConsoleOutput);
			}

			if(ToDiagnosticLog)
			{
				RuntimeAccessor.Logger.Log(Map(loggingEvent.Level), loggingEvent.RenderedMessage);
			}
		}	

		private LogSeverity Map(Level logLevel)
		{
			if(logLevel.Value >= Level.Error.Value )
			{
				return LogSeverity.Error;
			}

			if (logLevel.Value >= Level.Warn.Value)
			{
				return LogSeverity.Warning;
			}

			if (logLevel.Value >= Level.Info.Value)
			{
				return LogSeverity.Info;
			}
			return LogSeverity.Debug;
		
		}
	}
}