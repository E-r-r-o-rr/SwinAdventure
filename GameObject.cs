using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public abstract class GameObject : IdentifiableObject
    {
        //Fields:

        private string _description; 
        private string _name;

        //Constructor: 

        public GameObject(string[] Ids, string name, string desc) : base(Ids)
        {
            _name = name;
            _description = desc;
        }

        // Properties:

        public string Name
        {
            get { return _name; }
        }

        public virtual string ShortDescription
        {
            get { return Name + " (" + FirstId + ")"; }
        }

        public virtual string FullDescription
        {
            get { return _description; }
        }
    }
}
