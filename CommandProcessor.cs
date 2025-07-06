using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SwinAdventure
{
    public class CommandProcessor
    {
        private List<Command> _commands;

        public CommandProcessor()
        {
            _commands = new List<Command>(); 
            _commands.Add(new LookCommand());
            _commands.Add(new MoveCommand());
        }

        public string Execute(Player p, string commandInput)
        {
            string[] command = commandInput.Split(' ');
            
            foreach (Command cmd in _commands)
            {
                if (cmd.AreYou(command[0]))
                {
                  return cmd.Execute(p, command);
                }
            }

            return $"\nUnknown Command: {command[0]}\nValid commands are:\nmove, go, head, leave: " +
                    $"To move across locations with directions: North/Up/N, South/Down/S, East/Right/E, West/Left/E" +
                    $"\nlook/look at (item)/look at (item) in (container): To observe your surroundings\n";
        }
    }
}   

