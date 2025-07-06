using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Locations : GameObject, IHaveInventory
    {
        // Fields:

        private Inventory _inventory;
        private List<Path> _paths; 

        // Constructors:

        public Locations(string [] idents, string name, string desc) : base(idents, name, desc)
        {
            _inventory = new Inventory();
            _paths = new List<Path>();   
        }

        // Methods:

        public GameObject Locate(string id)
        {
            if (AreYou(id))
            {
                return this;
            }

            return _inventory.Fetch(id);
        }

         public override string FullDescription
        {
            get 
            {
                return   $"\n{base.FullDescription} Around you, you can see:\n{_inventory.ItemList}Your paths are:\n{pathsList}";
            }
        }

        // Properties:

        public Inventory Inventory
        {
            get { return _inventory; }
        }

        public List <Path> locationPaths
        {
          get { return _paths;  }
          set { _paths = value; }
        }

        public string pathsList
        {
            get
            {
                string pathList = "";
                foreach (Path p in _paths)
                {
                    pathList += p.ShortDescription + "\n";
                }
                return pathList;
            }
        }
    }
}
    