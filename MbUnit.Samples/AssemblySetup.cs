
using MbUnit.Framework;
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace MbUnit.Samples
{
	[AssemblyFixture]
	public class AssemblySetup
	{
		[FixtureSetUp]
		public void RunBeforeAnyTests()
		{
			log4net.Config.XmlConfigurator.Configure();
		}
	}
}


 