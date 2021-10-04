using System;

namespace TextAdventure
{
    public class Monster
    {
        public int Health;
        public string Name;
        public int Damage;
        public int Defence;

        public Monster(int health, string name, int damage, int defence)
        {
            Health = health;
            Name = name;
            Damage = damage;
            Defence = defence;
        }

        public void Hurt(int amount)
        {
            int damageTaken = amount - Defence;
            Health -= damageTaken;
            Console.WriteLine($"The {Name} took {damageTaken} damage and have {Health} amount left");
        }

        public void HeavyAttack(Hero hero)
        {
            Console.WriteLine($"{Name} charges a heavy attack");
            string response = hero.Response();
            if (response == "dodge")
            {
                Console.WriteLine("You sucessfully dodged");

                if (Program.AskYesOrNo("Do you want to counterAttack? "))
                {
                    if (Program.RollD6() > 1)
                    {
                        Console.WriteLine("You sucessfully hit your counterAttack");
                        this.Hurt(hero.Attack());
                    }
                    else
                    {
                        Console.WriteLine("How the frick did you miss?");
                    }
                }
                else
                {
                    Console.WriteLine("You choose to not counterattack.... idiot...");
                }
            }
            else if (response == "jump")
            {
                if (hero.items.Contains("Blessed Amulet"))
                {
                    Console.WriteLine($"You jump over the {Name}'s axe barely making it");
                }
                else
                {
                    Console.WriteLine("You jump but fail");
                    hero.Hurt(Damage);
                }
            }
            else if (response == "parry")
            {
                Console.WriteLine("You parry the Heavy attack");
                if (Program.AskYesOrNo("Do you want to counterattack? "))
                {
                    if (Program.RollD6() > 3)
                    {
                        Console.WriteLine("You sucessfully hit your counterattack");
                        this.Hurt(hero.Attack());
                        hero.Hurt((int)(Damage *0.5f));
                    }
                    else
                    {
                        Console.WriteLine("How the frick did you miss?");
                    }
                }
            }
        }

        public void SlashAttack(Hero hero)
        {
            Console.WriteLine($"{Name} slashes his axe at you");
            string response = hero.Response();
            if (response == "dodge")
            {
                if (Program.RollD6() >= 2)
                {
                    Console.WriteLine("You sucessfully dodged");

                    if (Program.AskYesOrNo("Do you want to counterAttack? "))
                    {
                        if (Program.RollD6() > 1)
                        {
                            Console.WriteLine("You sucessfully hit your counterAttack");
                            this.Hurt(hero.Attack());
                        }
                        else
                        {
                            Console.WriteLine("How the frick did you miss?");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You choose to not counter attack.... idiot...");
                    }
                }
                else
                {
                    hero.Hurt(15);
                }
            }

            else if (response == "jump")
            {
                Console.WriteLine("Why did you try to jump over his slash?????");
                Console.WriteLine("in the midst of being an idiot you also hit yourself :))");
                hero.Hurt(hero.Attack());
            }

            else if (response == "parry")
            {
                int firstRoll = Program.RollD6();
                if (firstRoll > 1)
                {
                    Console.WriteLine("You parry the Slash attack");
                    if (Program.AskYesOrNo("Do you want to counter attack? "))
                    {
                        if (firstRoll > Program.RollD6())
                        {
                            Console.WriteLine("You sucessfully hit your counterAttack");
                            this.Hurt(hero.Attack());
                        }
                        else
                        {
                            Console.WriteLine("How the frick did you miss?");
                        }
                    }
                    else
                    {
                        Console.WriteLine("You choose to not counter attack.... idiot...");
                    }
                }
                else
                {
                    hero.Hurt(Damage);
                }
            }
        }

        public void FlurryAttack(Hero hero)
        {
            Console.WriteLine($"{Name} uses flurry and its super effective");
            hero.Hurt(Damage*2);
        }
    }
}