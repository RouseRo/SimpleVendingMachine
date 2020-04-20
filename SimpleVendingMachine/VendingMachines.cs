
namespace SimpleVendingMachine.VendingMachines
{
	using System;
	using SimpleVendingMachine.CoinBoxes;
	using SimpleVendingMachine.StateMachines;
	using SimpleVendingMachine.Inventories;

	public class VendingMachines
	{
		public CoinBoxes CoinBox = new CoinBoxes();
		public Inventories Inventory = new Inventories();
		public StateMachines StateMachine = new StateMachines();

		public enum PurchaseResults
		{
			PurchaseCompleted,
			PurchaseNotCompleted,
			NotCompletedItemOutOfStock,
			NotCompletedNotEnoughMoney,
			PurchaseInProgress
		}

		public VendingMachines()
		{
			StateMachine.TotalCustomers = 0;
			StateMachine.TotalTransactions = 0;
			StateMachine.TotalAborts = 0;
			StateMachine.TotalFailures = 0;
			StateMachine.TotalSuccesses = 0;

			StateMachine.VMState = StateMachines.VMStates.WaitingForACustomer;
			StateMachine.VMAction = StateMachines.VMActions.Idle;
		}

		public void DisplayVMStatus()
		{
			Console.WriteLine();
			Console.WriteLine("*VM Status*");
			Console.WriteLine("Total Customer:[{0}]", StateMachine.TotalCustomers);
			Console.WriteLine("Total Transactions:[{0}]", StateMachine.TotalTransactions);
			Console.WriteLine("Total Aborts:[{0}]", StateMachine.TotalAborts);
			Console.WriteLine("Total Failures:[{0}]", StateMachine.TotalFailures);
			Console.WriteLine("Total Successes:[{0}]", StateMachine.TotalSuccesses);
		}
	}
}
