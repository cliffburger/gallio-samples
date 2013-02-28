using System;
using Gallio.Common.Reflection;
using Gallio.Framework;
using Gallio.Framework.Data;
using Gallio.Framework.Pattern;
using MbUnit.Framework;


namespace MbUnit.Samples.FeatureDemos
{
	[TestFixture]
	public class GeneratedDataTest
	{
		[Test, FooData()]
		public void Test(int i, string text)
		{
			// Prints the values provided by our custom data sources into the test log, so we can assert them later.
			TestLog.Write("{0}: '{1}'", i, text);
		}
	}

	[AttributeUsage(PatternAttributeTargets.DataContext, AllowMultiple = false, Inherited = true)]
	public class FooDataAttribute : DataAttribute
	{
		protected const int Count = 10;
		protected override void PopulateDataSource(IPatternScope scope, DataSource dataSource, ICodeElementInfo codeElement)
		{
			for (int i = 1; i <= Count; i++)
			{
				var row = new object[] { i, "Hello from #" + i };
				dataSource.AddDataSet(new ItemSequenceDataSet(new IDataItem[] { new ListDataItem<object>(row, GetMetadata(), false) }, row.Length));
			}
		}
	}
	
	
}
