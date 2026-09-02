using System.Globalization;
using Text_Rollenspiel;

namespace Rollenspiel
{
    class Program
    {
        public static void Main(string[] args)
        {
            Character player = new Character();

            Console.WriteLine("Spieler, was ist dein Name? ");
            player.Name = Console.ReadLine();

            Console.WriteLine("Wähle deine Profession:\nRitter");
            string profession = Console.ReadLine().ToLower();
            Console.Clear();

            switch (profession)
            {
                case "ritter":
                    player = SetPlayer(player, "knight");
                    Character.PrintStats(player); Console.Read();
                    Game.LoadingAnimation("");
                    Console.Clear();
                    Game.GameStart(player, "knight");
                    break;
            }

        }

        public static Character SetPlayer(Character player, string profession)
        {
            switch (profession)
            {
                case ("knight"):
                    Knight knight = new Knight();

                    player.Attack = knight.Attack;
                    player.Defense = knight.Defense;
                    player.Agility = knight.Agility;
                    player.Luck = knight.Luck;
                    player.Health = knight.Health;
                    return player;
                case ("level1Companion"):
                    player.Attack = 35;
                    player.Defense = 20;
                    player.Agility = 15;
                    player.Luck = 40;
                    player.Health = 150;
                    return player;
            }
            return player;
        }
    }
}

