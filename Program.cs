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
            DarkRoom.locationPaths.Add(smallHatch);
            smallHatch.IsLocked = true;

            Locations commandDeck = new Locations(new string[] { "deck", "command deck" }, "Command Deck", "the nerve centre of the star destroyer");
            Locations maintenanceShaft = new Locations(new string[] { "shaft", "maintenance", "node" }, "Maintenance Shaft", "a cramped shaft filled with humming wires");
            Locations engineeringBay = new Locations(new string[] { "engineering", "bay" }, "Engineering Bay", "the noisy heart of the destroyer's engines");
            Locations vaultAntechamber = new Locations(new string[] { "antechamber", "vault" }, "Vault Antechamber", "a hall with massive doors etched in Jedi glyphs");
            Locations holocronChamber = new Locations(new string[] { "chamber", "holocron" }, "Holocron Chamber", "the chamber housing the precious holocron");

            Path corridorToDeck = new Path(new string[] { "north", "n" }, "blast door", "the command deck door", commandDeck);
            Path corridorToShaft = new Path(new string[] { "east", "e" }, "maintenance hatch", "a hidden maintenance hatch", maintenanceShaft);
            Path corridorToDarkRoom = new Path(new string[] { "south", "s", "down" }, "waste hatch", "the hatch back to the waste chamber", DarkRoom);
            whiteCorridor.locationPaths.Add(corridorToDeck);
            whiteCorridor.locationPaths.Add(corridorToShaft);
            whiteCorridor.locationPaths.Add(corridorToDarkRoom);

            Path deckToCorridor = new Path(new string[] { "south", "s" }, "corridor door", "door back to corridor", whiteCorridor);
            commandDeck.locationPaths.Add(deckToCorridor);

            Path shaftToCorridor = new Path(new string[] { "west", "w" }, "corridor access", "passage back to corridor", whiteCorridor);
            maintenanceShaft.locationPaths.Add(shaftToCorridor);
            Path shaftToEngineering = new Path(new string[] { "south", "s" }, "service tunnel", "tunnel to engineering bay", engineeringBay);
            maintenanceShaft.locationPaths.Add(shaftToEngineering);

            Path engineeringToShaft = new Path(new string[] { "north", "n" }, "service tunnel", "tunnel back to the maintenance shaft", maintenanceShaft);
            engineeringBay.locationPaths.Add(engineeringToShaft);
            Path engineeringToAntechamber = new Path(new string[] { "east", "e" }, "vault access", "sealed door to the vault", vaultAntechamber);
            engineeringBay.locationPaths.Add(engineeringToAntechamber);

            Path antechamberToEngineering = new Path(new string[] { "west", "w" }, "engineering door", "door back to engineering", engineeringBay);
            vaultAntechamber.locationPaths.Add(antechamberToEngineering);
            Path antechamberToChamber = new Path(new string[] { "north", "n" }, "glyph door", "force sensitive door", holocronChamber);
            vaultAntechamber.locationPaths.Add(antechamberToChamber);
            Path chamberToAntechamber = new Path(new string[] { "south", "s" }, "exit", "exit to antechamber", vaultAntechamber);
            holocronChamber.locationPaths.Add(chamberToAntechamber);

            corridorToDeck.IsLocked = true;
            shaftToEngineering.IsLocked = true;
            engineeringToAntechamber.IsLocked = true;
            antechamberToChamber.IsLocked = true;

            Player playerOne = new Player(userName, userDescription);
            Item lightSaber = new Item(new string[] { "lightsaber", "blade", "light saber" }, "green lightsaber", "Luke Skywalker's lightsaber.");
            Item blaster = new Item(new string[] { "blaster", "plasma gun", "gun" }, "rebel blaster", "A standard rebel blaster ");
            Bag playerBag = new Bag(new string[] { "bag" }, "black bag", "A small black bag");
            Item letter = new Item(new string[] { "letter", "note", "paper" }, "a letter", "A letter with writing on it. To read it, type 'read letter'");
            Item gem = new Item(new string[] { "gem", "stone" }, "red gem", "A glowing red gem that could be used to purchase items");
            Item key = new Item(new string[] { "key", "metal key", "small key" }, "brass key", "A small brass key that looks like it might unlock something. To use it, type 'use key'");
            Item badge = new Item(new string[] { "badge", "id", "credentials" }, "ID badge", "An Imperial ID badge dropped by a careless trooper.");
            Item holocron = new Item(new string[] { "holocron", "jedi holocron" }, "Jedi Holocron", "A glowing repository of Force knowledge");
            Item tessa = new Item(new string[] { "tessa", "rebel" }, "Tessa Ryland", "A nervous rebel agent hiding behind crates.");

            playerOne.Inventory.Put(lightSaber);
            playerOne.Inventory.Put(blaster);
            playerOne.Inventory.Put(playerBag);
            playerBag.Inventory.Put(letter);
            DarkRoom.Inventory.Put(gem);
            DarkRoom.Inventory.Put(key);
            whiteCorridor.Inventory.Put(badge);
            whiteCorridor.Inventory.Put(tessa);
            holocronChamber.Inventory.Put(holocron);
            playerOne.CurrentLocation = DarkRoom;
            
            Console.WriteLine($"\nYour adventure begins now {userName}, {userDescription}!\n");
            Console.WriteLine($"You have regained conciousness after falling into the waste chambers!\n" +
                $"Currently, you are in: {DarkRoom.FullDescription}\nWhere would you like to look?\n");

            void DisplayInstructions(Player player)
            {
                Console.WriteLine("\nValid commands:");
                Console.WriteLine("look - observe the room or items");
                Console.WriteLine("move/go/head/leave <direction> - travel north, south, east or west");
                Console.WriteLine("take <item> - pick up an item");

                switch (player.CurrentLocation.Name)
                {
                    case "dark room":
                        Console.WriteLine("read letter, use key");
                        break;
                    case "Imperial Corridor":
                        Console.WriteLine("use badge, talk tessa");
                        break;
                    case "Maintenance Shaft":
                        Console.WriteLine("disable grid");
                        break;
                    case "Engineering Bay":
                        Console.WriteLine("fight tessa, spare tessa, leave tessa");
                        break;
                    case "Vault Antechamber":
                        Console.WriteLine("use gem");
                        break;
                    case "Holocron Chamber":
                        Console.WriteLine("fight vadun/attack, recover holocron, destroy holocron");
                        break;
                }

                Console.WriteLine("Type 'help' at any time to see this list again.\n");
            }

            DisplayInstructions(playerOne);

            string Commands;
            CommandProcessor commandprocessor = new CommandProcessor();    
            bool gameRunning = true;
            bool securityDisabled = false;
            bool tessaAlive = true;
            bool tessaAlly = false;
            bool finalChoiceMade = false;
            

            do
            {
                Commands = Console.ReadLine();
                bool TemporaryCustomCommand = false;
                switch (Commands)
                {
                    case "help":
                        DisplayInstructions(playerOne);
                        TemporaryCustomCommand = true; break;
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

                    case "use badge":
                        if (playerOne.CurrentLocation == whiteCorridor && (playerOne.Inventory.HasItem("badge") || whiteCorridor.Inventory.HasItem("badge")))
                        {
                            if (whiteCorridor.Inventory.HasItem("badge"))
                                whiteCorridor.Inventory.Take("badge");
                            else
                                playerOne.Inventory.Take("badge");
                            corridorToDeck.IsLocked = false;
                            Console.WriteLine("\nYou swipe the ID badge. The blast door slides open.\n");
                        }
                        else
                        {
                            Console.WriteLine("\nYou can't use that here.\n");
                        }
                        TemporaryCustomCommand = true; break;

                    case "talk tessa":
                        if (playerOne.CurrentLocation == whiteCorridor && tessaAlive && !tessaAlly)
                        {
                            Console.WriteLine("\nTessa whispers that the holocron is locked in a vault and she'll help if you disable the security grid.");
                            tessaAlly = true;
                        }
                        else
                        {
                            Console.WriteLine("\nThere's no response.\n");
                        }
                        TemporaryCustomCommand = true; break;

                    case "disable grid":
                        if (playerOne.CurrentLocation == maintenanceShaft && tessaAlly && !securityDisabled)
                        {
                            securityDisabled = true;
                            shaftToEngineering.IsLocked = false;
                            Console.WriteLine("\nYou reroute the wiring and shut down the alarms.\n");
                        }
                        else
                        {
                            Console.WriteLine("\nNothing happens.\n");
                        }
                        TemporaryCustomCommand = true; break;

                    case "fight tessa":
                        if (playerOne.CurrentLocation == engineeringBay && tessaAlive)
                        {
                            Console.WriteLine("\nTessa attacks! After a tense battle you gain the upper hand.");
                            Console.WriteLine("Will you spare her? type 'spare tessa' or leave her with 'leave tessa'.");
                        }
                        else
                        {
                            Console.WriteLine("\nNo one to fight here.\n");
                        }
                        TemporaryCustomCommand = true; break;

                    case "spare tessa":
                        if (playerOne.CurrentLocation == engineeringBay && tessaAlive)
                        {
                            Console.WriteLine("\nShe yields and pledges to help you reach the vault.");
                            tessaAlly = true;
                            engineeringToAntechamber.IsLocked = false;
                        }
                        TemporaryCustomCommand = true; break;

                    case "leave tessa":
                        if (playerOne.CurrentLocation == engineeringBay && tessaAlive)
                        {
                            Console.WriteLine("\nYou leave her behind and push onward alone.");
                            tessaAlly = false;
                            engineeringToAntechamber.IsLocked = false;
                            tessaAlive = false;
                        }
                        TemporaryCustomCommand = true; break;

                    case "use gem":
                        if (playerOne.CurrentLocation == vaultAntechamber && playerOne.Inventory.HasItem("gem"))
                        {
                            playerOne.Inventory.Take("gem");
                            antechamberToChamber.IsLocked = false;
                            Console.WriteLine("\nThe gem glows as it powers the lock. The door slides open.\n");
                        }
                        else
                        {
                            Console.WriteLine("\nThe gem does nothing.\n");
                        }
                        TemporaryCustomCommand = true; break;

                    case "fight vadun":
                    case "attack":
                        if (playerOne.CurrentLocation == holocronChamber)
                        {
                            Console.WriteLine("\nDarth Lord Va'dun strikes. You parry blaster bolts then clash sabers and prevail.\nChoose: 'recover holocron' or 'destroy holocron'.");
                        }
                        else
                        {
                            Console.WriteLine("\nThere's no one to attack here.\n");
                        }
                        TemporaryCustomCommand = true; break;

                    case "recover holocron":
                        if (playerOne.CurrentLocation == holocronChamber)
                        {
                            Console.WriteLine("\nYou secure the holocron and flee the star destroyer.\n");
                            if (tessaAlly)
                            {
                                Console.WriteLine("Tessa guides you to a hidden Rebel enclave where the Jedi legacy can live on.");
                            }
                            finalChoiceMade = true;
                        }
                        TemporaryCustomCommand = true; break;

                    case "destroy holocron":
                        if (playerOne.CurrentLocation == holocronChamber)
                        {
                            Console.WriteLine("\nWith a heavy heart you shatter the holocron, denying the Empire and the Jedi alike.\n");
                            finalChoiceMade = true;
                        }
                        TemporaryCustomCommand = true; break;
                }

                if (!TemporaryCustomCommand)
                {
                    string result = commandprocessor.Execute(playerOne, Commands);
                    Console.WriteLine(result);
                }

                if (finalChoiceMade)
                {
                    gameRunning = false;
                }

                if (gameRunning)
                {
                    DisplayInstructions(playerOne);
                }

            } while (gameRunning == true);

            Console.WriteLine("\nMay the Force be with you.\n");
        }
        
    }
}
 
    
      


