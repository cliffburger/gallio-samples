using System;
using Gallio.Framework;
using MbUnit.Framework;

namespace MbUnit.Samples.Filtering
{

    public static class Filters
    {
        public const string Filtering = "Filtering";        
    }


    public abstract class FilterBase
    {
        [Test]
        public void Show()
        {
            TestLog.WriteLine(GetType().Name);
            Console.WriteLine(GetType().Name);
        }
    }


    [TestFixture]
    [Category(Filters.Filtering)]
    public class FilterTest:FilterBase
    {

    }

    [Category("A")]
    public class FilterA: FilterTest
    {
        
    }

    [Category("A")]
    [Category("B")]
    public class FilterAandB : FilterTest
    {

    }

    [Category("A")]
    [Category("C")]
    public class FilterAandC : FilterTest
    {

    }

    [TestFixture]
    [Category("NoFilter")]
    public class NoFilterCategory : FilterBase
    {

    }
}
