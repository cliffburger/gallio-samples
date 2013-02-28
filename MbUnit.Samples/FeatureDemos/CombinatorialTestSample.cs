using System.Collections.Generic;
using System.Threading;
using MbUnit.Framework;

namespace MbUnit.Samples.FeatureDemos
{
	[TestFixture]
	[Parallelizable(TestScope.Self)]
	public class CombinatorialTestSample
	{
		public IEnumerable<double> Fica()
		{
			yield return 123.25;
			yield return 99.20;
			yield return 150.60;
			yield return 22.33;
			yield return 224.70;
		}

		public IEnumerable<double> Pension()
		{
			yield return 226.40;
			yield return 124.55;
			yield return 35.0;
			yield return 99.23;
			yield return 110.20;
		}

		public IEnumerable<double> Insurance()
		{
			yield return 33.77;
			yield return 235.22;
			yield return 168.20;
			yield return 77.33;
			yield return 111.22;
		}

		[Test][PairwiseJoin]
		public void CheckComputeWithholdingsPairwise(
			[Factory("Fica")] double fica,
			[Factory("Pension")] double pension,
			[Factory("Insurance")] double insurance)
		{
			Payroll pay = new Payroll();
			Thread.Sleep(100);
			double expected = fica + pension + insurance;
			double actual = pay.ComputeWithholdings(fica, pension, insurance);
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void CheckComputeWithholdingsCombinatorial(
			[Factory("Fica")] double fica,
			[Factory("Pension")] double pension,
			[Factory("Insurance")] double insurance)
		{
			Payroll pay = new Payroll();
			Thread.Sleep(100);
			double expected = fica + pension + insurance;
			double actual = pay.ComputeWithholdings(fica, pension, insurance);
			Assert.AreEqual(expected, actual);
		}
	}

	public class Payroll
	{
		public double ComputeWithholdings(double fica, double pension, double insurance)
		{
			return fica + pension + insurance;
		}
	}
}