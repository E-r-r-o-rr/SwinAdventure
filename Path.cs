using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class Path : GameObject
    {
        private Locations _destination;
        private bool _isLocked; 

        public Path(string[] ids, string name, string desc, Locations Destination) : base(ids, name, desc)
        {
            _destination = Destination;
            _isLocked = false;
        }

        public bool IsLocked
        {
            get { return _isLocked; }
            set { _isLocked = value; }
        }

        public Locations Destination
        {
            get { return _destination; }
            set { _destination = value; }
        }

        public override string ShortDescription
        {
            get
            {
                return $"{this.FirstId}: {base.FullDescription} that leads to the {Destination.Name}"; 
            }
        }

        public string MovePlayer(Player player)
        {
            if (IsLocked)
            {
                player.CurrentLocation = player.CurrentLocation;
                return $"\nYou cannot enter {Name} as it is locked\n";
            }

            else
            {
                player.CurrentLocation = Destination;
                return $"\nYou have walked through {FullDescription} and have now entered the {Destination.Name}\n";
            }
        }
    }
}
