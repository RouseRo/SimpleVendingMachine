using System;
using SimpleVendingMachine.VendingMachines;
using SimpleVendingMachine.StateMachines;
public class Menus
{
    public VendingMachines Vm;
    public Menus()
    {
        Vm = new VendingMachines();
    }

    public void DisplayTopLevel()
    {
        Vm.DisplayVMStatus();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("*** Simple Vending Machine ***");
            Console.WriteLine("1] Display Inventory");
            Console.WriteLine("2] Enter a Coin");
            Console.WriteLine("3] Select an Item to Purchase");
            Console.WriteLine("4] Abort (Reset)");
            Console.WriteLine("Q] Quit");

            Console.Write("Enter the number of your Selection: ");
            string selection = Console.ReadLine();

            if (selection == "1")
            {
                Vm.Inventory.DisplayInventory();
            }
            else if (selection == "2")
            {
                DisplayCoinMenu();
            }
            else if (selection == "3")
            {
                DisplayPurchaseMenu();
            }
            else if (selection == "4")
            {
                Vm.CoinBox.AbortCurrentCustomer();
                Vm.StateMachine.TransitionStateMachine(StateMachines.VMActions.Aborting);
                Vm.DisplayVMStatus();
                Vm.CoinBox.DisplayCoinBoxCoins();
            }
            else if (selection.ToUpper() == "Q")
            {
                Vm.StateMachine.TransitionStateMachine(StateMachines.VMActions.Quitting);
                Vm.DisplayVMStatus();
                Vm.CoinBox.DisplayCoinBoxCoins();
                Console.WriteLine();
                Console.WriteLine("Quitting - *** Enjoy Your Day ***");
                Console.WriteLine();
                Console.Write("type any key to exit:");
                Console.ReadKey();
                break;
            }
            else
            {
                Console.WriteLine("Please try again");
            }
        }
        return;        
    }

    public void DisplayCoinMenu()
    {
        while (true)
        {
            Vm.CoinBox.DisplayCoinBoxCoins();
            Console.WriteLine();
            Console.WriteLine("** Coin Selection Menu **");
            Console.WriteLine("1] 5 cents coin");
            Console.WriteLine("2] 10 cents coin");
            Console.WriteLine("3] 25 cents coin");
            Console.WriteLine("R] Return to Top Level Menu");
            Console.Write("Enter the number of your Coin Value: ");

            string coinSelection = Console.ReadLine();

            if (coinSelection == "1")
            {
                Console.WriteLine("Accepting 5 Cents Coin");
                Vm.CoinBox.AddToCustomerCoins(new int[] { 1, 0, 0 });
                Vm.StateMachine.TransitionStateMachine(StateMachines.VMActions.AcceptingACoin);
            }
            else if (coinSelection == "2")
            {
                Console.WriteLine("Accepting 10 Cents Coin");
                Vm.CoinBox.AddToCustomerCoins(new int[] { 0, 1, 0 });
                Vm.StateMachine.TransitionStateMachine(StateMachines.VMActions.AcceptingACoin);
            }
            else if (coinSelection == "3")
            {
                Console.WriteLine("Accepting 25 Cents Coin");
                Vm.CoinBox.AddToCustomerCoins(new int[] { 0, 0, 1 });
                Vm.StateMachine.TransitionStateMachine(StateMachines.VMActions.AcceptingACoin);
            }
            else if (coinSelection.ToUpper() == "R")
            {
                break;
            }
            else
            {
                Console.WriteLine("Please try again");
            }
        }
    }

    public void DisplayPurchaseMenu()
    {
        while(true)
        {
            Vm.DisplayVMStatus();
            Vm.Inventory.DisplayInventory();
            Vm.CoinBox.DisplayCoinBoxCoins();

            Console.WriteLine();
            Console.WriteLine("** Purchase Menu**");
            Console.WriteLine("1] Item 1 Costs 55 Cents");
            Console.WriteLine("2] Item 2 Costs 70 Cents");
            Console.WriteLine("3] Item 3 Costs 75 Cents");
            Console.WriteLine("R] Return to Top Level Menu");
            Console.Write("Enter the number of the Item to Purchase: ");

            string itemSelection = Console.ReadLine();

            if (itemSelection == "1")
            {
                this.PurchaseThisItem(1, Vm);
            }
            else if (itemSelection == "2")
            {
                this.PurchaseThisItem(2, Vm);
            }
            else if (itemSelection == "3")
            {
                this.PurchaseThisItem(3, Vm);
            }
            else if (itemSelection.ToUpper() == "R")
            {
                break;
            }
            else
            {
                Console.WriteLine("Please try again");
            }
        }
    }

    private void PurchaseThisItem(int itemNumber, VendingMachines vm)
    {
        Vm.StateMachine.TransitionStateMachine(StateMachines.VMActions.SelectingItemForPurchase);

        VendingMachines.PurchaseResults purchaseResult = Vm.Inventory.PurchaseThisItem(itemNumber, vm);

        if (purchaseResult == VendingMachines.PurchaseResults.PurchaseCompleted)
        {
            Vm.StateMachine.TransitionStateMachine(StateMachines.VMActions.PurchaseCompleted);
        }
        else if (purchaseResult == VendingMachines.PurchaseResults.NotCompletedNotEnoughMoney)
        {
            Vm.StateMachine.TransitionStateMachine(StateMachines.VMActions.PurchaseNotCompleted);
        }
        else if (purchaseResult == VendingMachines.PurchaseResults.PurchaseNotCompleted)
        {
            Vm.StateMachine.TransitionStateMachine(StateMachines.VMActions.PurchaseNotCompleted);
        }
    }
}