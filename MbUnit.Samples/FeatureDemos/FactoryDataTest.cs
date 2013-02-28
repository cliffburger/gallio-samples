using System.Collections.Generic;
using System.Drawing;
using Gallio.Framework;
using MbUnit.Framework;

namespace MbUnit.Samples.FeatureDemos
{
	[TestFixture]
	public class FactoryDataTest
	{
		public IEnumerable<Color> YieldData()
		{
			yield return Color.Red;
			yield return Color.FromArgb(255, 240, 240);
			yield return Color.FromKnownColor(KnownColor.Violet);
		}

		public IEnumerable<Dto> EnumerableDto()
		{
			return new List<Dto>()
			       	{
						new Dto() {PageColor = Color.AliceBlue, PageName = "Blue"},
						new Dto() {PageColor = Color.Red, PageName = "Red"}
			       	};
		}

		public IEnumerable<Color> EnumerableData()
		{
			return new [] {Color.Yellow, Color.FromKnownColor(KnownColor.AliceBlue), Color.FromName("Fuschia")};
		}

		[Test, Factory("YieldData")]
		public void YieldFactoryTest(Color value)
		{
			TestLog.Write("The color is {0} {1} {2}", value.A,value.B, value.G);
		}

		[Test, Factory("EnumerableData")]
		public void EnumerableDataTest(Color value)
		{
			TestLog.Write("The color is {0} {1} {2}", value.A, value.B, value.G);
		}

		[Test, Factory("EnumerableDto")]
		public void DtoFactory(Dto value)
		{
			TestLog.Write("The color is {0}", value.PageColor);
		}
	}

	public class Dto
	{
		public string PageName;
		public Color PageColor;

	}
	
}
