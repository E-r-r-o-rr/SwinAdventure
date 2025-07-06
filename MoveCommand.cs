using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SwinAdventure
{
    public class MoveCommand : Command
    {
        public MoveCommand() : base(new string[] { "move", "go", "head", "leave" }) { }
        
        public override string Execute(Player p, string[] text)
        {
            if (AreYou(text[0]))
            {
                if (text.Length < 2)
                {
                    return "\nWhere would you like to move? North, South, East or West?\n";
                }

                if (text.Length > 2)
                {
                    return "\nI can't move like that\n";
                }

                if (text.Length == 2)
                {
                    if (FetchPath(p, text[1]) == null)
                    {
                        return $"\nThere is no path {text[1]}\n"; 
                    }

                    return FetchPath(p, text[1]).MovePlayer(p); 
                }
            }

            return "\nError in move input\n";
        }

        public Path FetchPath(Player p, string path)
        {
            foreach (Path pa in p.CurrentLocation.locationPaths)
            {
                if (pa.AreYou(path))
                {
                   if(pa!= null) return pa;
                }            
            }

            return null; 
        }  
    }
}
