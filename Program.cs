using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using SplashKitSDK;

namespace SwinAdventure
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Hello! Welcome to SwinAdventure. Please type in your mighty name, hero!\nName: ");
            String userName = Console.ReadLine();
            Console.Write($"\nHello there {userName}! Describe yourself! (e.g., 'A valiant hero' or 'An intelligent adventurer...etc)\nDescription: ");
            String userDescription = Console.ReadLine();

            Locations DarkRoom = new Locations(new string[] { "room1", "darkroom", "start" }, "dark room", "a small dimly lit chamber used for waste dumping.");
            Locations whiteCorridor = new Locations(new string[] { "room2", "corridor", "white corridor", "imperial hallway" }, "Imperial Corridor", "a bright white corridor of an Imperial starship, occasionally patrolled by stormtroopers.");
            Path smallHatch = new Path(new string[] { "north", "n", "up" }, "hatch", "a small red colored hatch", whiteCorridor); 
            DarkRoom.locationPaths.Add( smallHatch );
            smallHatch.IsLocked = true;

            Player playerOne = new Player(userName, userDescription);
            Item lightSaber = new Item(new string[] { "lightsaber", "blade", "light saber" }, "green lightsaber", "Luke Skywalker's lightsaber.");
            Item blaster = new Item(new string[] { "blaster", "plasma gun", "gun" }, "rebel blaster", "A standard rebel blaster ");
            Bag playerBag = new Bag(new string[] { "bag" }, "black bag", "A small black bag");
            Item letter = new Item(new string[] { "letter", "note", "paper" }, "a letter", "A letter with writing on it. To read it, type 'read letter'");
            Item gem = new Item(new string[] { "gem", "stone" }, "red gem", "A glowing red gem that could be used to purchase items");
            Item key = new Item(new string[] { "key", "metal key", "small key" }, "brass key", "A small brass key that looks like it might unlock something. To use it, type 'use key'");

            playerOne.Inventory.Put(lightSaber);
            playerOne.Inventory.Put(blaster);
            playerOne.Inventory.Put(playerBag);
            playerBag.Inventory.Put(letter);
            DarkRoom.Inventory.Put(gem);
            DarkRoom.Inventory.Put(key);
            playerOne.CurrentLocation = DarkRoom;
            
            Console.WriteLine($"\nYour adventure begins now {userName}, {userDescription}!\n");
            Console.WriteLine($"You have regained conciousness after falling into the waste chambers!\n" +
                $"Currently, you are in: {DarkRoom.FullDescription}\nWhere would you like to look?\n");

            string Commands;
            CommandProcessor commandprocessor = new CommandProcessor();    
            bool gameRunning = true;
            

            do
            {
                Commands = Console.ReadLine();
                bool TemporaryCustomCommand = false;
                switch (Commands)
                {
                    case "read letter":
                        Console.WriteLine("\nI am now stuck in this star destroyer with no choice but to carry on with my mission. The new galactic empire has been \nsearching endlessly" +
                        " for the Jedi holocron. A device containing a list of the locations of Force-sensitive children \ninside of it. They are planning to eliminate any possibility" +
                        " of the jedi to rise again. I must stop them before it's \ntoo late. I will be leaving my Lightsaber and blaster in this bag to put a disguise on and inflitrate" +
                        " this ship.\nIf you are reading this then my mission has failed. May the force be with you. -Luke Skywalker\n"); 
                        TemporaryCustomCommand = true; break;

                    case "use key":
                        DarkRoom.Inventory.Take("key");
                        smallHatch.IsLocked = false;
                        Console.WriteLine($"\nthe {smallHatch.Name} is now unlocked\n");
                        TemporaryCustomCommand = true; break; 
                }

                if (!TemporaryCustomCommand)
                {
                    string result = commandprocessor.Execute(playerOne, Commands);
                    Console.WriteLine(result);
                }   

            } while (gameRunning == true); 
            
        }
        
    }
}
 
    
      


