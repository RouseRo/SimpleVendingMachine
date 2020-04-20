namespace SimpleVendingMachine.StateMachines
{
	public class StateMachines
	{
		public int TotalCustomers;
		public int TotalTransactions;
		public int TotalAborts;
		public int TotalFailures;
		public int TotalSuccesses;

		public VMStates VMState;
		public VMActions VMAction;

		public enum VMStates
		{
			WaitingForACustomer,
			WaitingForItemSelection,
			PurchasingItem,
			Abort
		};

		public enum VMActions
		{
			Idle,
			AcceptingACoin,
			Aborting,
			SelectingItemForPurchase,
			PurchaseCompleted,
			PurchaseNotCompleted,
			Quitting
		}

		public StateMachines()
		{
			VMState = VMStates.WaitingForItemSelection;
		}

		public void TransitionStateMachine(VMActions vmA)
		{
			if (VMState == VMStates.WaitingForACustomer)
			{
				if (vmA == VMActions.AcceptingACoin)
				{
					this.TotalCustomers++;
					VMState = VMStates.WaitingForItemSelection;
					VMAction = VMActions.Idle;

				}
				else if (vmA == VMActions.Aborting)
				{
					this.TotalAborts++;
					VMAction = VMActions.Idle;
				}
			}
			else if (VMState == VMStates.WaitingForItemSelection)
			{
				if (vmA == VMActions.AcceptingACoin)
				{
					VMAction = VMActions.Idle;
				}
				else if (vmA == VMActions.Aborting)
				{
					this.TotalAborts++;
					VMState = VMStates.WaitingForACustomer;
					VMAction = VMActions.Idle;
				}
				else if (vmA == VMActions.SelectingItemForPurchase)
				{
					VMState = VMStates.PurchasingItem;
				}
			}
			else if (VMState == VMStates.PurchasingItem)
			{
				if (vmA == VMActions.PurchaseCompleted)
				{
					this.TotalSuccesses++;
					this.TotalTransactions++;
					VMState = VMStates.WaitingForACustomer;
				}
				else if (vmA == VMActions.PurchaseNotCompleted)
				{
					this.TotalFailures++;
					VMState = VMStates.WaitingForACustomer;
				}
			}
		}
	}
}