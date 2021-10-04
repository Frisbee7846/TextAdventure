using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Channels;


namespace TextAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Text Adventure");
            Hero hero = new Hero();
            Console.Write(hero.health);
            Monster monster = new Monster(150, "temp ", 50, 5);
            
            while (hero.location != "quit")
            {
                switch (hero.location)
                {
                    case "newgame":
                        NewGame(hero);
                        break;
                    case "tableroom":
                        TableRoom(hero);
                        break;
                    case "corridor":
                        Corridor(hero);
                        break;
                    case "lockedroom":
                        LockedRoom(hero);
                        break;
                    case "pitfall":
                        Pitfall(hero);
                        break;
                    case "thirdroom":
                        ThirdRoom(hero);
                        break;
                    case "emptyroom":
                        EmptyRoom(hero);
                        break;
                    case "backoutside":
                        monster = BackOutside(hero);
                        break;
                    case "bossfight":
                        BossFight(hero, monster);
                        break;
                    case "win":
                        Win(hero);
                        break;
                    case "loose":
                        Loose(hero);
                        break;
                    case "gameover":
                        GameOver(hero);
                        break;
                    default:
                        Console.Error.WriteLine($"You forgot to implement {hero.location}!!!!!");
                        break;
                }
            }
        }
        
        public static string Ask(string question)
        {
            string response = "";

            do
            {
                Console.Write(question);
                string input = Console.ReadLine();
                response = input.Trim().ToLower();
            } while (response == "");

            return response;
        }

        //Ask Yes or no
        public static bool AskYesOrNo(string question)
        {
            while (true)
            {
                string response = Ask(question).ToLower();
                switch (response)
                {
                    case "yes":
                    case "ok":
                        return true;
                    case "no":
                        return false;
                }
            }
        }
        //init new hero

        static void NewGame(Hero hero)
        {
            Console.Clear();
            string name = "";
            do
            {
                name = Ask("Whats your name, Adventurer? ");
            } while (!AskYesOrNo($"So, {name} it is? "));

            hero.name = name;
            hero.location = "tableroom";
        }

        static void TableRoom(Hero hero)
        {
            Console.Clear();
            hero.items.Add("wooden sword");
            Console.WriteLine("You are equipped with one wooden sword, and your task" +
                              "is to slay the monster at the end of the adventure. " + "" +
                              "In front of you is a stone table with two items on it, " + "a knife and a key." + "" +
                              "You can only pick up one of these items.");
            string response;
            do
            {
                response = Ask("Which do you wanna pickup?");
            } while (response != "key" && response != "knife" && response != "none");

            if (response != "none")
            {
                hero.items.Add(response);
                Console.WriteLine($"You picked up the {response}");
                hero.location = "corridor";
            }
            else
            {
                Console.WriteLine("wtf man why did you not pickup");
                Console.WriteLine("The dungeon master punishes you by sending you into the depths of his pit");
                hero.location = "pitfall";
            }

            
        }

        static void Corridor(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("You exit the room and find yourself standing in a dark hallway.");
            Console.WriteLine("You can either enter another room on your right ");
            Console.WriteLine("side, or continue down the hallway on your left.");

            string choice = "";

            do
            {
                choice = Ask("Left or right? ");
            } while (choice != "right" && choice != "left");

            if (choice == "right")
            {
                if (hero.items.Contains("key"))
                {
                    hero.location = "lockedroom";
                    hero.items.Remove("key");
                }
                else
                {
                    hero.location = "thirdroom";
                    Console.WriteLine("You didnt have the key :(( no entering the right for you :((");
                }
            }
            else
            {
                hero.location = "thirdroom";
            }

            Console.ReadLine();
        }

        static void LockedRoom(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("Inside the locked room " + "you find a shiny sword!");
            if (AskYesOrNo("Do you want it instead of " + "your wooden sword? "))
            {
                hero.items.Remove("wooden sword");
                hero.items.Add("shiny sword");
            }

            hero.location = "thirdroom";
        }

        static void ThirdRoom(Hero hero)
        {
            if (AskYesOrNo("As you enter the room you find a corpse, do you loot it? "))
            {
                Console.WriteLine("You picked up a silver necklace");
                if (RollD6() >= 3)
                {
                    Console.WriteLine("You got blessed boi");
                    hero.items.Add("Blessed Amulet");
                }
                else
                {
                    Console.WriteLine("You got cursed boi");
                    hero.items.Add("Cursed Amulet");
                }
            }

            Console.WriteLine("You leave the corpse and continue into an intersection which way do you go?");

            string choice = "";

            do
            {
                choice = Ask("Left or right? ");
            } while (choice != "right" && choice != "left");

            if (choice == "right")
            {
                if (hero.items.Contains("knife"))
                {
                    hero.location = "emptyroom";
                }
                else
                {
                    hero.location = "backoutside";
                }
            }
            else
            {
                hero.location = "backoutside";
            }
        }

        static void Pitfall(Hero hero)
        {
            Console.Clear();
            Monster shrok = new Monster(100, "big boi", 15, 20);
            Console.WriteLine($"As you hit the ground you awaken to see a giant ogre, his name {shrok.Name}");
            Console.WriteLine("Foight or die!");
            Console.ReadLine();
            BossFight(hero, shrok);
            
        }
        
        static void EmptyRoom(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("You enter the room to the right finding its empty");
            if (hero.items.Contains("Cursed Amulet"))
            {
                Console.WriteLine("The curse in your amulet awakens and the door behind you shuts");
                Console.WriteLine("YOURE STUCK");
                hero.location = "loose";
            }
            else
            {
                Console.WriteLine("You turn around realising the room is completely empty");
                hero.location = "backoutside";
            }

            Console.ReadLine();
        }

        static Monster BackOutside(Hero hero)
        {
            Console.Clear();
            Console.WriteLine("As you go outiside you see before you the tiniest of bois");
            Monster minotaur = new Monster(150, "mini boi", 20, 30);
            Console.WriteLine($"The feared minotaur {minotaur.Name}!");
            Console.WriteLine("Mini boi charges towards you");
            string choice = hero.Response();

            switch (choice)
            {
                case "parry":
                    Console.WriteLine($"You tried to parry successful but took damage but so did the {minotaur.Name}");
                    hero.Hurt(15);
                    minotaur.Hurt(hero.Attack());
                    break;
                case "dodge":
                    Console.WriteLine($"You dodge the charge of the {minotaur.Name} and the {minotaur.Name} crashes into the wall");
                    minotaur.Hurt(25);
                    break;
                case "jump":
                    if (hero.items.Contains("Blessed Amulet"))
                    {
                        Console.WriteLine(
                            $"The power of the amulet gives you the power of friendship and you jump way over the {minotaur.Name}");
                        Console.WriteLine($"The {minotaur.Name} crashes into the wall");
                        minotaur.Hurt(25);
                    }
                    else if (hero.items.Contains("Cursed Amulet"))
                    {
                        Console.WriteLine("The power of the cursed amulet of doom makes you unable to jump");
                        Console.WriteLine($"The {minotaur.Name} slams you into the wall");
                        hero.Hurt(25);
                        minotaur.Hurt(5);
                    }
                    else
                    {
                        Console.WriteLine(
                            $"You jump but your leg caught on his horn and you fall over behind the {minotaur.Name}");
                        hero.Hurt(10);
                        minotaur.Hurt(10);
                    }

                    break;
            }

            Console.ReadLine();
            hero.location = "bossfight";
            return minotaur;
        }

        static void BossFight(Hero hero, Monster monster)
        {
            while (hero.health > 1 && monster.Health > 1) //Checks so both parts are alive
            {
                Console.Clear();
                int rolledNumber = RollD6();

                if (rolledNumber == 1) //monster Heavy attack
                {
                    monster.HeavyAttack(hero);
                }
                else if (rolledNumber >= 2 && rolledNumber <= 5) //monster most common attack
                {
                    monster.SlashAttack(hero);
                }
                else
                {
                    monster.FlurryAttack(hero);
                }

                Console.ReadLine();
            }

            if (monster.Health > 1)
            {
                hero.location = "loose";
            }
            else if (hero.health > 1)
            {
                if (monster.Name == "big boi")
                {
                    hero.location = "thirdroom";
                }
                else
                {
                    hero.location = "win"; 
                }
                
            }

            
        }

        private static void Win(Hero hero)
        {
            Console.WriteLine($"Congratulations {hero.name} you succesfully cleared the dungeon of the bois" +
                              "and as a reward you receive nothing");
            hero.location = "gameover";
            Console.ReadLine();
        }

        private static void Loose(Hero hero)
        {
            if (hero.items.Contains("knife"))
            {
                Console.WriteLine($"Congratulations {hero.name} you succesfully dishonored your family" +
                                  "and as a reward you commit seppuku");
            }
            else
            {
                Console.WriteLine($"Congratulations {hero.name} you succesfully failed the dungeon of the bois" +
                                  "and as a reward you receive nothing");
            }

            hero.location = "gameover";
            Console.ReadLine();
        }

        private static void GameOver(Hero hero)
        {
            Console.Clear();
            if (AskYesOrNo("Do you want to continue going down the path of no return? "))
            {
                hero.items.Clear();
                hero.health = 100;
                hero.location = "newgame";
            }
            else
            {
                hero.location = "quit";
            }
        }

        public static int RollD6()
        {
            Random random = new Random();
            int roll = random.Next(1, 7);
            return roll;
        }
    }
}