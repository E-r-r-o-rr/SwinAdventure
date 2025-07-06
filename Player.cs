using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Player : GameObject, IHaveInventory
    {
        // Fields:

        private Inventory _inventory;
        private Locations _location; 

        // Constructors:

        public Player(string name, string desc) : base(new string[] { "me", "inventory"  }, name, desc)
        {
            _inventory = new Inventory();
        }

        // Methods:

        public GameObject Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }

            if (_inventory != null && _inventory.Fetch(id) != null)
            {
                return _inventory.Fetch(id);
            }

            if (_location != null && _location.Inventory != null)
            {
                return _location.Inventory.Fetch(id);
            }

            return null;
        }

        // Properties:

        public override string FullDescription
        {
            get 
            {
                return   $"You are {Name}, {base.FullDescription}. You are carrying:\n{_inventory.ItemList}";
            }
        }

        public Inventory Inventory
        {
            get { return _inventory; }
        }

        public Locations CurrentLocation
        {
            get { return _location; }
            set { _location = value; }
        }
    }
}
