using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Inventory
    {
        // Fields:

        private List<Item> _items;

        //Constructor: 

        public Inventory()
        {
            _items = new List<Item>();
        }

        // Methods:

        public bool HasItem (string id)
        {
            if (Fetch(id) != null)
            {
                return true;       
            }
            return false;
        }

        public void Put (Item itm)
        {
            _items.Add(itm);
        }

        public Item Take (string id)
        {
            Item itemtoTake = Fetch(id); 
            if (Fetch(id) != null)
            {
                _items.Remove(itemtoTake);
                 return itemtoTake;
            }
            return null; 
        }

        public Item Fetch (string id)
        {
            foreach (Item item in _items)
            {
                if (item.AreYou(id))
                {
                    return item;
                }
            }
            
            return null;
        }

        // Properties:

        public string ItemList
        {
            get
            {
                string inventoryList = ""; 
                foreach (Item item in _items)
                {
                    inventoryList += item.ShortDescription + "\n";
                }
                return inventoryList; 
            }
        }
    }
}
