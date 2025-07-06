using System;

namespace SwinAdventure
{
    public class TakeCommand : Command
    {
        public TakeCommand() : base(new string[] {"take", "pickup", "grab"}) { }

        public override string Execute(Player p, string[] text)
        {
            if (text.Length != 2)
            {
                return "\nTake what?\n";
            }

            string itemId = text[1].ToLower();
            Item? item = p.CurrentLocation.Inventory.Take(itemId);
            if (item != null)
            {
                p.Inventory.Put(item);
                return $"\nYou take the {item.Name}.\n";
            }

            return $"\nThere is no {itemId} here to take.\n";
        }
    }
}
