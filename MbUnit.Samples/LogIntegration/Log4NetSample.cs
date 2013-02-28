using System;
using Cliff.MbUnit;
using MbUnit.Framework;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;

namespace MbUnit.Samples.LogIntegration
{
	[TestFixture]
	public class Log4NetSample
	{
		private readonly ILog _logger = LogManager.GetLogger(typeof(Log4NetSample));

		[FixtureSetUp] 
		public void ConfigureLog4Net()
		{
			//var appenders = new IAppender[]
			//{
			//    new ConsoleAppender { Threshold = Level.All }, 
			//    new MbUnitAppender {Threshold = Level.All, ToTestLog = true, ToRunTimeLog = true}
			//};
			//BasicConfigurator.Configure(appenders);

			XmlConfigurator.Configure();
		}

		[Test]
		public void TestMethod1()
		{
			_logger.Error("Oh no!");
		}

		[Test]
		public void ExceptionTest()
		{
			try
			{
				var i =  1 / int.Parse("0");
				Console.Write(i);
			}
			catch (Exception ex)
			{
				_logger.Error("Caught that exception!", ex);
			}
			
		}
	}
}
