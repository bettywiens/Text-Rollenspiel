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

        // Zeigt alles im Inventory (momentan nur Waffen):
        public static void ShowInventory(Inventory inventory)
        {
            Console.WriteLine($"Waffen:");
            foreach (Weapon weapon in inventory.Weapons)
            {
                Console.Write($"\n* {weapon.Name}");
            }
        }
        // Zeigt nur die Waffen (Möglichkeit entweder alle Waffen oder bestimmte Waffe zu zeigen):
        public static string ShowWeapons(int index, Inventory inventory)
        {
            string weapons = string.Empty;
            if (index < 0)
            {
                foreach (Weapon weapon in inventory.Weapons)
                {
                    weapons = weapons + $"\n* {weapon.Name}";
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
