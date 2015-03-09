using System;
using Gallio.Common.Reflection;
using Gallio.Framework;
using Gallio.Framework.Pattern;
using Gallio.Model;

namespace Cliff.MbUnit
{
    // Based on http://trishkhoo.com/2011/07/giving-tests-a-second-chance/

    public class GiveThisTestASecondChanceAttribute : TestDecoratorPatternAttribute
    {
        private readonly int _maximumNumberOfAttempts;

        public GiveThisTestASecondChanceAttribute(int maximumNumberOfAttempts = 1)
        {
            _maximumNumberOfAttempts = maximumNumberOfAttempts;
            if (maximumNumberOfAttempts < 1)
            {
                throw new ArgumentOutOfRangeException("maximumNumberOfAttempts", @"The maximum number of attempts must be at least 1.");
            }
        }

        protected override void DecorateTest(IPatternScope scope,
            ICodeElementInfo codeElement)
        {
            scope.TestBuilder.TestInstanceActions.RunTestInstanceBodyChain
                .Around((state, decorator) =>
                    {
                        TestOutcome outcome = TestOutcome.Passed;

                        int failureCount = 0;
                        // we will try up to 'max' times to get a pass, 
                        // if we do, then break out and don't run the test anymore
                        for (int i = 0; i < _maximumNumberOfAttempts; i++)
                        {
                            string name = String.Format("Repetition #{0}", i + 1);
                            TestContext context = TestStep.RunStep(name, delegate
                            {
                                TestOutcome innerOutcome = decorator(state);
                                // if we get a fail, throw an error
                                if (innerOutcome.Status != TestStatus.Passed)
                                {
                                    throw new SilentTestException(innerOutcome);
                                }
                            }, null, false, codeElement);
                            outcome = context.Outcome;
                            // escape the loop if the test has passed, 
                            // otherwise increment the failure count
                            if (context.Outcome.Status == TestStatus.Passed)
                                break;
                            failureCount++;
                        }
                        TestLog.WriteLine(String.Format(
                          failureCount == _maximumNumberOfAttempts
                          ? "Tried {0} times to get a pass test result but didn't get it"
                          : "The test passed on attempt {1} out of {0}",
                            _maximumNumberOfAttempts, failureCount + 1));
                        return outcome;
                    }
                 );
        }


    }
}
