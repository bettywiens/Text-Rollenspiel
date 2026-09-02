using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Rollenspiel
{
    internal class Inventory
    {
        public List<Weapon> Weapons { get; set; }


        public static void ShowInventory(Inventory inventory)
        {
            Console.WriteLine($"Waffen:");
            foreach (Weapon weapon in inventory.Weapons)
            {
                Console.Write($"\n* {weapon.Name}");
            }
        }
        public static string ShowWeapons(int index, Inventory inventory)
        {
            string weapons = string.Empty;
            if (index < 0)
            {
                foreach (Weapon weapon in inventory.Weapons)
                {
                    weapons = weapons + $"* {weapon.Name}\n";
                }
            }
            else
            {
                weapons = $"{inventory.Weapons[index].Name}";
            }
            return weapons;
        }
    }


}
