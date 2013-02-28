using MbUnit.Framework;

namespace MbUnit.Samples.FeatureDemos
{
	[TestFixture]
	[Parallelizable(TestScope.Self)]
	public class ExternalDataSource
	{
		[Test, CsvData(FilePath = "Data.csv", HasHeader = true)]
		public void ShoppingCartTotalWithSingleItem(decimal unitPrice, decimal quantity, string item)
		{
			var shoppingCart = new ShoppingCart();
			shoppingCart.Add(item, unitPrice, quantity);
			Assert.AreEqual(unitPrice * quantity, shoppingCart.TotalCost);
		}
	}

	public class ShoppingCart
	{
		public void Add(string item, decimal unitPrice, decimal quantity)
		{
			TotalCost = unitPrice*quantity;
		}

		public decimal TotalCost
		{
			get;set; 
		}
	}
}
