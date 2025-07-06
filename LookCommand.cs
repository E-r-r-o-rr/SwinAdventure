using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SwinAdventure
{
    public class LookCommand : Command
    {
        public LookCommand() : base(new string[] { "look" }) { }

        public override string Execute(Player p, string[] text) // Split string in main
        {
            if (AreYou(text[0]))
            {
                if (text.Length == 1)
                {
                    return p.CurrentLocation.FullDescription; 
                }

                if (text.Length < 3 && text.Length != 1|| text.Length > 5 || text.Length == 4)
                {
                    return "\nI don’t know how to look like that\n"; 
                }

                if (text[1].ToLower() != "at")
                {
                    return "\nWhat do you want me to look at?\n"; 
                }

                if (text.Length == 5 && text[3].ToLower() != "in")
                {
                    return "\nWhat do you want me to look in?\n"; 
                }

                if (text.Length == 3)
                {
                    return LookAtIn(text[2], p); 
                }

                if (text.Length == 5)
                {
                    if (FetchContainer(p, text[4]) == null)
                    {
                        return $"\nI can't find the {text[4]}\n"; 
                    }

                    else
                    {
                        return LookAtIn(text[2], FetchContainer(p, text[4]));
                    }
                }
            }

                return "\nError in look input\n";             
        }

        public IHaveInventory FetchContainer (Player p, string containerId)
        {
            GameObject obj = p.Locate(containerId);
            IHaveInventory container = obj as IHaveInventory;
            return container;
        }

        public string LookAtIn (string thingId, IHaveInventory container)
        {
            if (container.Locate(thingId)==null)
            {
                return $"\nI can't find the {thingId} in the {container.Name}\n"; 
            }

            else
            {
                return "\n" + container.Locate(thingId).FullDescription + "\n";
            }
        }
    }
}
