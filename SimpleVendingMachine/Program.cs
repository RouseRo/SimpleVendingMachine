//*************************************************************************************************
//*************************************************************************************************
/// <summary>
///  Title:  Simple Vending Machine
///  Version: 1.0
///  Last Upadted: 04-19-2020
///  Author:  Robert Keith Rouse  
///  LinkinID: https://www.linkedin.com/in/robert-rouse-6a6688/
///  
///  History:
///     04-19-2020 Created
/// 
///  Requirements:
///     The goal is to make a simple vending machine with he following:
///     
///     User Inputs:
///         Coin Input: 5, 10, 25 coins
///         ID of Selection to purchase
///         Abort(reset)
///         
///     Display Outputs:
///         Running Total (balance of funds)
///         Dispense Status (sucess or failure)
///         Iventory Status
///         
///     Inventory:
///         Item 1 which costs 55 cents
///         Item 2 which costs 70 cents
///         Item 3 which costs 75 cents
///         Each item begins with an inventoru count of 10
///         
///      Rules:
///         1)  If the user has not inserted sufficient funds and intends to purchase an item,
///             the system should alert the user and return the coins inserted.
///         2)  The funds the system can take in at a time is 1 dollar(100 cents)
///         3)  The abort(reset) will return the funds entered to the coin return.
///         
///  Design Elements and Feature in this Solution:
///     Tools:
///         Visual Studio Community 19 Version 16.5.4
///         Microsoft .NET Framework Version 4.8.03752
///         
///     User Interface:
///         The Console is used for all inputs and outputs. There are three simple menus 
///         (Top Level, Coin Input and Purchase Selection). Users have to return to the
///         Top Level menu to Abort(reset) or Quit. All the important information is 
///         output to the console when a value changes do to a user action.
///         
///     Import Notices:
///         1) If a menu imput is not recognized the user is asked to try again.
///         2) The user is notifed if an iten is out-of-stock.
///         3) The user is notified if there is not enough money to purchase an item
///             and allows the user to enter more coins.
///         4) The user has to abort the purchase to get his/her coins returned.
///         5) If the system can not give the correct changes because of lack of the
///             proper coins the users is notified of the amount owed.
///                 
///     Features:
///         1) All calculations are done in cents.
///         2) Money is stored an a coin vector of rank 3 with the first position
///            containg the count of 5 cent coins; the second, 10 cent coins and
///            the third position 25 cent coins.  There is no seperate coin vector
///            class.  For simplicty, a int [3] array is used.
///         3) The vending machines that three coin boxes that can hold coin vectors.
///            They are the 1) Customer's Coin Box for input coins; 2) the Change Box
///            and 3) the Coin Box for the vending's profits and for making change.
///         4) Change is always returned with the largest available coins first.     
///            
/// 
/// 
/// </summary>
//*************************************************************************************************
//*************************************************************************************************
namespace SimpleVendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            Menus menu = new Menus();
            menu.DisplayTopLevel();
        }
    }
}
