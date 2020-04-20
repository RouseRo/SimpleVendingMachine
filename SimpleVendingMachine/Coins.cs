namespace SimpleVendingMachine.Coins
{
	public class Coins
	{
		const int NUMBER_OF_COIN_TYPES = 3;
		public const int COIN_1_VALUE = 5;
		public const int COIN_2_VALUE = 10;
		public const int COIN_3_VALUE = 25;

		public int[] CoinVector = new int[NUMBER_OF_COIN_TYPES];

		public Coins()
		{
		}

		public int [] Add(int [] cvA) 
		{
			int[] cvC = new int[NUMBER_OF_COIN_TYPES];

			cvC[0] = this.CoinVector[0] + cvA[0];
			cvC[1] = this.CoinVector[1] + cvA[1];
			cvC[2] = this.CoinVector[2] + cvA[2];

			return cvC;
		}

		public int [] Remove(int [] cvA)
		{
			int[] cvC = new int[NUMBER_OF_COIN_TYPES];

			cvC[0] = this.CoinVector[0] - cvA[0];
			cvC[1] = this.CoinVector[1] - cvA[1];
			cvC[2] = this.CoinVector[2] - cvA[2];

			return cvC;
		}

		public int ComputeCoinVectorValueInCents(int[] cvA)
		{
			return cvA[0] * COIN_1_VALUE + cvA[1] * COIN_2_VALUE + cvA[2] * COIN_3_VALUE;
		}
	}
}
