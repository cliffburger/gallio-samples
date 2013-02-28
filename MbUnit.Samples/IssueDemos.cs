using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;

namespace MbUnit.Samples
{
	[TestFixture]
	public class IssueDemos
	{
		[Test]
		public void ShortStackTrace()
		{			
			try
			{
				throwAnException();
			}
			catch (Exception caughtException)
			{
				Assert.IsTrue(false, caughtException.StackTrace);
			}
		}

		private void throwAnException()
		{
			throw new Exception("Whoa!");
		}

		private const string StackTrace = @"
[failed] Test Daptiv.Internal.QUnitTests/QUnitTests/Run/Run({org.openqa.grid.common.exception.CapabilityNotPresentOnTheG
ridException: cannot find : {browserName=internet explorer, version=4}})
Execute
SharpTestsEx.AssertException: False Should Be True.[http://10.2.60.74:8088/tests/qunit/index.html]
org.openqa.grid.common.exception.CapabilityNotPresentOnTheGridException: cannot find : {browserName=internet explorer, v
ersion=4}
   at OpenQA.Selenium.Remote.RemoteWebDriver.UnpackAndThrowOnError(Response errorResponse) in c:\Projects\WebDriver\trun
k\dotnet\src\WebDriver\Remote\RemoteWebDriver.cs:line 964
   at OpenQA.Selenium.Remote.RemoteWebDriver.Execute(DriverCommand driverCommandToExecute, Dictionary`2 parameters) in c
:\Projects\WebDriver\trunk\dotnet\src\WebDriver\Remote\RemoteWebDriver.cs:line 805
   at OpenQA.Selenium.Remote.RemoteWebDriver.StartSession(ICapabilities desiredCapabilities) in c:\Projects\WebDriver\tr
unk\dotnet\src\WebDriver\Remote\RemoteWebDriver.cs:line 773
   at Daptiv.Internal.WebAutomation.Se.WebDriverFactory.CreateGridDriver(ISeleniumStartupSettings settings) in C:\source
\3\eproject\Root\Daptiv.Internal.WebAutomation\Se\WebDriverFactory.cs:line 93
   at Daptiv.Internal.WebAutomation.Se.WebDriverFactory.CreateDriver() in C:\source\3\eproject\Root\Daptiv.Internal.WebA
utomation\Se\WebDriverFactory.cs:line 54
   at Daptiv.Internal.WebAutomation.Se.WebDriverFactory.CreateWebDriver() in C:\source\3\eproject\Root\Daptiv.Internal.W
ebAutomation\Se\WebDriverFactory.cs:line 42
   at Daptiv.Internal.WebAutomation.Se.WebDriverContextService.CreateContext() in C:\source\3\eproject\Root\Daptiv.Inter
nal.WebAutomation\Se\WebDriverContextService.cs:line 59
   at Daptiv.Internal.QUnitTests.TestCaseFactory.RunTestSuite() in C:\source\3\eproject\Root\Daptiv.Internal.QUnitTests\
TestCaseFactory.cs:line 94
   at Daptiv.Internal.QUnitTests.TestCaseFactory.GetQUnitTestResults() in C:\source\3\eproject\Root\Daptiv.Internal.QUni
tTests\TestCaseFactory.cs:line 70
   at Daptiv.Internal.QUnitTests.QUnitTests.Run(QUnitTestResult result) in C:\source\3\eproject\Root\Daptiv.Internal.QUn
itTests\QUnitTests.cs:line 34@";

		[Test]
		public void LongStackTrace()
		{
			Assert.IsTrue(false, StackTrace);
		}

		[Test]
		public void ShortMessageWithBraces()
		{
			Assert.IsTrue(false, @"Hello {{{");
		}
	}
}
