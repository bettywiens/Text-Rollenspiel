using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Rollenspiel
{
    public class Character
    {     
        public string Name { get; set; }

        public int Attack { get; set; }

        public int Defense { get; set; }

        public int Agility { get; set; }

        public int Luck { get; set; }

        public int Health { get; set; }

        public List<string> Abilities { get; set; } 

    public static void PrintStats(Character character)
    {
        Console.WriteLine($"Deine Fähigkeiten sind:\n* Health: {character.Health}\n* Attacke: {character.Attack}\n* Verteidigung: {character.Defense}\n* Beweglichkeit: {character.Agility}\n* Glück: {character.Luck}");
    }

    }
}
