
namespace SimpleVendingMachine.CoinBoxes
{
	using System;
	using SimpleVendingMachine.Coins;
	public class CoinBoxes
	{
		const int COIN_BOX_LIMIT_IN_CENTS = 100;

		public Coins CustomerCoins = new Coins();
		public Coins CoinBoxCoins = new Coins();
		public Coins ChangeBoxCoins = new Coins();

		public int CustomerCoinsValue;
		public int CoinBoxCoinsValue;
		public int ChangeBoxCoinsValue;

		public CoinBoxes()
		{
		}

		public void AddToCustomerCoins(int [] cvA)
		{
			// Check if the CoinBox will be full with this addition
			if (!IsCoinBoxFull(cvA))
			{
				CustomerCoins.CoinVector = CustomerCoins.Add(cvA);
				this.CustomerCoinsValue = CoinBoxCoins.ComputeCoinVectorValueInCents(CustomerCoins.CoinVector);
			}
		}

		public void RemoveFromCustomerCoins( int [] cvA)
		{
			CustomerCoins.CoinVector = CustomerCoins.Remove(cvA);
			this.CustomerCoinsValue = CoinBoxCoins.ComputeCoinVectorValueInCents(CustomerCoins.CoinVector);
		}

		public void RemoveFromCoinBoxCoins(int[] cvA)
		{
			CoinBoxCoins.CoinVector = CoinBoxCoins.Remove(cvA);
			this.CoinBoxCoinsValue = CoinBoxCoins.ComputeCoinVectorValueInCents(CoinBoxCoins.CoinVector);
		}


		public void AddToCoinBoxCoins(int [] cvA)
		{
			CoinBoxCoins.CoinVector = CoinBoxCoins.Add(cvA);
			this.CoinBoxCoinsValue = CoinBoxCoins.ComputeCoinVectorValueInCents(CoinBoxCoins.CoinVector);

		}

		public void AddToChangeBoxCoins(int[] cvA)
		{
			ChangeBoxCoins.CoinVector = ChangeBoxCoins.Add(cvA);
			this.ChangeBoxCoinsValue = CoinBoxCoins.ComputeCoinVectorValueInCents(ChangeBoxCoins.CoinVector);
		}

		public void MoveCoinBoxCoinsToChangeBox(int[] cvA)
		{
			this.RemoveFromCoinBoxCoins(cvA);
			this.AddToChangeBoxCoins(cvA);
		}

		public void MoveCustomersCoinsToCoinBox(int[] cvA)
		{
			this.RemoveFromCustomerCoins(cvA);
			this.AddToCoinBoxCoins(cvA);
		}

		public bool IsCoinBoxFull(int[] cvA)
		{
			bool result = false;
			if (CustomerCoinsValue + CoinBoxCoins.ComputeCoinVectorValueInCents(cvA) > 100)
			{
				result = true;
				Console.WriteLine();
				Console.WriteLine("***** Sorry, the CoinBox is full *****");
			}
			return result;
		}

		private void ResetCustomerCoins()
		{
			CustomerCoins.CoinVector[0] = 0;
			CustomerCoins.CoinVector[1] = 0;
			CustomerCoins.CoinVector[2] = 0;
			CustomerCoinsValue = 0;
		}

		private void MoveCustomerCoinsToChangeBox()
		{
			ChangeBoxCoins.CoinVector[0] = CustomerCoins.CoinVector[0];
			ChangeBoxCoins.CoinVector[1] = CustomerCoins.CoinVector[1];
			ChangeBoxCoins.CoinVector[2] = CustomerCoins.CoinVector[2];
			this.ChangeBoxCoinsValue = this.CustomerCoinsValue;
			ResetCustomerCoins();
		}

		public void AbortCurrentCustomer()
		{
			Console.WriteLine();
			Console.WriteLine("***** Aborting and Returning all Customers Coins *****");
			Console.WriteLine("Returning Customer's Coins: 5[{0}], 10[{1}], 25[{2}] with a value of: [{3}]",
				CustomerCoins.CoinVector[0], CustomerCoins.CoinVector[1], CustomerCoins.CoinVector[2],
				CustomerCoins.ComputeCoinVectorValueInCents(this.CustomerCoins.CoinVector));

			// Reset Customer's Coins
			MoveCustomerCoinsToChangeBox();
		}

		public void DisplayCoinBoxCoins()
		{
			Console.WriteLine();
			Console.WriteLine("*Coin Boxes Status*");
			Console.WriteLine("Counts of Customer's Coins: 5[{0}], 10[{1}], 25[{2}]",
				CustomerCoins.CoinVector[0], CustomerCoins.CoinVector[1], CustomerCoins.CoinVector[2]);
			Console.WriteLine("Customer's Coins Value in Cents: [{0}]", 
				CustomerCoins.ComputeCoinVectorValueInCents(this.CustomerCoins.CoinVector));
			Console.WriteLine("----------");
			Console.WriteLine("Counts of Change Box Coins: 5[{0}], 10[{1}], 25[{2}]",
				ChangeBoxCoins.CoinVector[0], ChangeBoxCoins.CoinVector[1], ChangeBoxCoins.CoinVector[2]);
			Console.WriteLine("Change Box Coins Value in Cents: [{0}]",
				ChangeBoxCoins.ComputeCoinVectorValueInCents(this.ChangeBoxCoins.CoinVector));
			Console.WriteLine("----------");
			Console.WriteLine("Counts of Coin Box Coins: 5[{0}], 10[{1}], 25[{2}]",
				CoinBoxCoins.CoinVector[0], CoinBoxCoins.CoinVector[1], CoinBoxCoins.CoinVector[2]);
			Console.WriteLine("Coin Box Coins Value in Cents: [{0}]",
				CoinBoxCoins.ComputeCoinVectorValueInCents(this.CoinBoxCoins.CoinVector));
		}

	}
}