
using System;
using System.Collections.Generic;

namespace TextAdventure
{
    public class Hero
    {
        public string name = "";
        public int health = 100;
        public List<string> items = new List<string>();
        public string location = "newgame";
        public int defence = 0;
        public void Hurt(int amount)
        {
            if (items.Contains("Blessed Amulet"))
            {
                defence = 5;
            }
            else if (items.Contains("Cursed Amulet"))
            {
                defence = -5;
            }
            {
                
            }
            int damageTaken = amount - defence;
            health -= damageTaken;
            Console.WriteLine($"you took {damageTaken} damage and have {health} amount left");


        }

        public int Attack()
        {
            foreach (string item in items)
            {
                if (item == "wooden sword")
                {
                    return 1445;
                }
                else if (item == "shiny sword")
                {
                    return 60;
                }
               
            }

            return 5;
        }

        public string Response()
        {
            string choice = "";
            do
            {
                choice = Program.Ask("What you do? Parry, Dodge, Jump? ");
            } while (choice != "parry" && choice != "dodge" && choice != "jump");

            

            return choice;
        }
    }
    
    
}