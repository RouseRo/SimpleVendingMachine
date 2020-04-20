
namespace SimpleVendingMachine.Inventories
{
	using System;
	using SimpleVendingMachine.VendingMachines;
	using SimpleVendingMachine.Coins;
	public class Inventories
	{
		const int NUMBER_OF_ITEMS = 3;
		const int ITEM_1_COST = 55;
		const int ITEM_2_COST = 70;
		const int ITEM_3_COST = 75;

		public int[] InventoryArray = new int[NUMBER_OF_ITEMS];
		public int CurrentSelectedItem;

		int[] ItemCostArray = new int[NUMBER_OF_ITEMS];
		int[] OutputBenItems = new int[NUMBER_OF_ITEMS];

		public Inventories()
		{
			InventoryArray[0] = 10;
			InventoryArray[1] = 10;
			InventoryArray[2] = 10;
			ItemCostArray[0] = ITEM_1_COST;
			ItemCostArray[1] = ITEM_2_COST;
			ItemCostArray[2] = ITEM_3_COST;
			CurrentSelectedItem = 0;
			OutputBenItems[0] = 0;
			OutputBenItems[1] = 0;
			OutputBenItems[2] = 0;
		}

		public void DispenseThisItem(int itemNumber)
		{
			this.InventoryArray[itemNumber - 1]--;
			this.OutputBenItems[itemNumber - 1]++;
			Console.WriteLine();
			Console.WriteLine("***** Item: [{0}] moved to Output Ben *****", itemNumber);
		}

		private bool IsItemOutOfStock(int itemNumber)
		{
			bool result = false;
			if (this.InventoryArray[itemNumber - 1] <= 0) result = true;
			return result;
		}

		private bool AreCustomerFundsEnoughToPurchaseThisItem(int itemNumber, VendingMachines vm)
		{
			bool result = false;
			if (vm.CoinBox.CustomerCoinsValue >= vm.Inventory.ItemCostArray[itemNumber - 1])
			{
				result = true;
			}
			return result;
		}


		private void ComputeChangeAndMoveCustomersCoins(int itemNumber, VendingMachines vm)
		{
			int[] TargetChangeVector = new[] { 0, 0, 0 }
;			int TargetChangeVectorValue = 0;
			int TotalTargetChangeVectorValue = 0;

			if (vm.CoinBox.CustomerCoinsValue == vm.Inventory.ItemCostArray[itemNumber - 1])
			{
				vm.CoinBox.MoveCustomersCoinsToCoinBox(vm.CoinBox.CustomerCoins.CoinVector);
				return;
			}
			else if (vm.CoinBox.CustomerCoinsValue >= vm.Inventory.ItemCostArray[itemNumber - 1])
			{
				TargetChangeVectorValue = vm.CoinBox.CustomerCoinsValue - vm.Inventory.ItemCostArray[itemNumber - 1];
				TotalTargetChangeVectorValue = TargetChangeVectorValue;

				// Make change from all the coins in the CoinBox

				vm.CoinBox.MoveCustomersCoinsToCoinBox(vm.CoinBox.CustomerCoins.CoinVector);

				do
				{
					if (vm.CoinBox.CoinBoxCoins.CoinVector[2] > 0)
					{
						if (TargetChangeVectorValue >= Coins.COIN_3_VALUE)
						{
							vm.CoinBox.MoveCoinBoxCoinsToChangeBox(new int[] { 0, 0, 1 });
							TargetChangeVectorValue -= Coins.COIN_3_VALUE;
						}
						else
						{
							break;
						}
					}
					else
					{
						break;
					}

				} while (vm.CoinBox.ChangeBoxCoinsValue < TargetChangeVectorValue);

				do
				{
					if (vm.CoinBox.CoinBoxCoins.CoinVector[1] > 0)
					{
						if (TargetChangeVectorValue >= Coins.COIN_2_VALUE)
						{
							vm.CoinBox.MoveCoinBoxCoinsToChangeBox(new int[] { 0, 1, 0 });
							TargetChangeVectorValue -= Coins.COIN_2_VALUE;
						}
						else
						{
							break;
						}
					}
					else
					{
						break;
					}

				} while (vm.CoinBox.ChangeBoxCoinsValue < TargetChangeVectorValue);

				do
				{
					if (vm.CoinBox.CoinBoxCoins.CoinVector[0] > 0)
					{
						if (TargetChangeVectorValue >= Coins.COIN_1_VALUE)
						{
							vm.CoinBox.MoveCoinBoxCoinsToChangeBox(new int[] { 1, 0, 0 });
							TargetChangeVectorValue -= Coins.COIN_1_VALUE;
						}
						else
						{
							break;
						}
					}
					else
					{
						break;
					}

				} while (vm.CoinBox.ChangeBoxCoinsValue < TargetChangeVectorValue);

				if (vm.CoinBox.ChangeBoxCoinsValue < TotalTargetChangeVectorValue)
				{
					Console.WriteLine();
					Console.WriteLine("***** Sorry, CoinBox does not have the coins to make correct change. *****");
					Console.WriteLine("***** We owe you [{0}] cents. *****", TargetChangeVectorValue);
				}

				return;
			}
		}

		private  VendingMachines.PurchaseResults CompetePurchase(int itemNumber, VendingMachines vm)
		{
			VendingMachines.PurchaseResults result = VendingMachines.PurchaseResults.PurchaseInProgress; 

			this.DispenseThisItem(itemNumber);

			// Compute Change and Move Customer's Coins to CoinBox and ChangeBox
			this.ComputeChangeAndMoveCustomersCoins(itemNumber, vm);

			result = VendingMachines.PurchaseResults.PurchaseCompleted;
			return result;
		}

		public VendingMachines.PurchaseResults PurchaseThisItem(int itemNumber, VendingMachines vm)
		{
			VendingMachines.PurchaseResults result = VendingMachines.PurchaseResults.PurchaseInProgress;

			Console.WriteLine();
			Console.WriteLine("***** Item [{0}] Selected for Purchase. *****", itemNumber);
			CurrentSelectedItem = itemNumber;

			if (this.IsItemOutOfStock(itemNumber))
			{
				Console.WriteLine();
				Console.WriteLine("***** Sorry, item: [{0}] is Out-Of-Stock. *****", itemNumber);
				return VendingMachines.PurchaseResults.NotCompletedItemOutOfStock;
			}

			if (!this.AreCustomerFundsEnoughToPurchaseThisItem(itemNumber, vm))
			{
				Console.WriteLine();
				Console.WriteLine("***** Sorry, not enough money to purchase item: [{0}]. *****", itemNumber);
				return VendingMachines.PurchaseResults.NotCompletedNotEnoughMoney;
			}

			result = this.CompetePurchase(itemNumber, vm);

			return result;
		}

		public void DisplayInventory()
		{
			Console.WriteLine();
			Console.WriteLine("** Inventory **");
			Console.WriteLine("Item Counts: ITEM 1:[{0}], ITEM 2:[{1}], ITEM 3:[{2}]",
				InventoryArray[0], InventoryArray[1], InventoryArray[2]);
			Console.WriteLine("Output Ben Item Counts: ITEM 1:[{0}], ITEM 2:[{1}], ITEM 3:[{2}]",
				OutputBenItems[0], OutputBenItems[1], OutputBenItems[2]);
		}
	}
}
