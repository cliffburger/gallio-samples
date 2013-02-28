using System;
using System.Diagnostics;
using Gallio.Framework;
using Gallio.Model;
using Gallio.Runtime;
using Gallio.Runtime.Logging;
using MbUnit.Framework;

namespace MbUnit.Samples.FeatureDemos
{
	[TestFixture]
	public class TestLifecycleSample
	{
		private void Write(string text)
		{
			TestLog.Write(text);
			RuntimeAccessor.Logger.Log(LogSeverity.Debug, text);
		}

		[FixtureInitializer]
		public void Initialize()
		{
			Write("Initialize ");
		}

		[TearDown]
		public void Teardown()
		{
			Write("Tear Down ");

			if (TestContext.CurrentContext.Outcome.Status == TestStatus.Failed)
			{
				Write("Failed. Do something here.");
			}
		}

		[Test]
		void This_test_passes()
		{
			Assert.IsTrue(true);
		}

		[Test]
		void This_test_fails()
		{
			Assert.IsTrue(false);
		}

		[Test]
		void This_test_throws()
		{
			throw new Exception("Will the test catch this?");
		}

		[Test]
		[Row(true)]
		[Row(false)]
		void Data_driven_tests(bool pass)
		{
			Assert.IsTrue(pass);
		}
	}
}
