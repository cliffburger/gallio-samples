using Gallio.Runtime;
using Gallio.Runtime.Logging;
using MbUnit.Framework;

namespace MbUnit.Samples
{
    [TestFixture]
    public class BasicTest
    {
        [Test]
        public void ABasicTest()
        {
            RuntimeAccessor.Logger.Log(LogSeverity.Important, "Congratulations, you've run a test!");
        }
    }
}
