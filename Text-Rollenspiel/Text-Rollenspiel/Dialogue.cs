using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Rollenspiel
{
    internal class Dialogue
    {
        public static void OpeningDialogueKnight(Character character)
        {
            string dialogue1 = "";
            TypeLine(dialogue1);
            Console.WriteLine("\n(1) bist du zur Seite gesprungen und bist ihm ausgewichen."); // Fast Attack erlangen
            Console.WriteLine("\n(2) hast du dein Schild zur Verteidigung hoch gehalten"); // Defence Stance erlangen
            int ability = int.Parse(Console.ReadLine());

            switch (ability)
            {
                case 0:
                    character.Abilities.Add("fast attack");
                    break;
                
            }
            
            
            //character.Abilities.Add(ability); // Füge Fähigkeit hinzu

            Console.WriteLine("Es stellte sich heraus, dass du ein hervorragender Kämpfer bist, eine wertvolle Resource für das Königreich.");
            Console.WriteLine("Leider... Leider stellte sich heraus");
        }

        static void TypeLine (string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                Thread.Sleep(20);
            }
        }
    }
}
