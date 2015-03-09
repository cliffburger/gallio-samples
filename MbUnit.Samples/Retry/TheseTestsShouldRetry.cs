using Cliff.MbUnit;
using MbUnit.Framework;

namespace MbUnit.Samples.Retry
{
    [TestFixture]
    public class TheseTestsShouldRetry
    {
        private int _testOneTries;
        private int _testTwoTries;
        private int _testThreeTries;

        [FixtureSetUp] // Fixture setup does NOT rerun
        public void FixtureSetup()
        {
            _testOneTries = 0;
            _testTwoTries = 0;
        }

        [SetUp] // Test setup doesrerun
        public void Setup()
        {
            _testThreeTries = 0;
        }

        [Test]
        [GiveThisTestASecondChance(2)]
        public void ThisshouldPassBecauseItPassesOnTheSecondChance()
        {
            if (_testOneTries++ < 1)
            {
                Assert.Fail("Oh No!");
            }
            else
            {
                Assert.IsTrue(true, "Good job!");
            }
        }

        [Test]
        [GiveThisTestASecondChance(2)]
        public void ThisShouldFailBecauseItFailsTwice()
        {
            if (_testTwoTries++ < 2)
            {
                Assert.Fail("Oh No!");
            }
            else
            {
                Assert.IsTrue(true, "Nice try!");
            }
        }

        [Test]
        [GiveThisTestASecondChance(2)]
        public void ThisWillFailBecauseVariableAreNotResetInSetUp()
        {
            if (_testThreeTries ++ < 1)
            {
                Assert.Fail("Oh No!");
            }
            else
            {
                Assert.IsTrue(true, "Nice try!");
            }
        }
    }
}
