using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Rollenspiel;

namespace Text_Rollenspiel
{
    internal class Game
    {
        public static void GameStart(Character player, string characterProfession)
        {
            string professionName = "";

            switch (characterProfession)
            {
                case "knight":
                    professionName = "Ritter";
                    break;
            }

            Inventory inventory = Intro(player, professionName);
            Level1(player, inventory);
        }
        public static void Level1(Character player, Inventory playerInventory)
        {
            string levelName = "\nVERLIES LEVEL 1\n\n";
            Thread.Sleep(500);
            Speak(levelName, 50);
            //Character.PrintStats(player);
            //Inventory.ShowInventory(playerInventory);
            Console.Write("Du bist eindeutig tiefer ins Verlies gefallen, es ist noch dunkler als zuvor und die Luft ist so dick, dass man sie zerschneiden könnte."); Console.ReadLine();
            string dialogue1 = "Ey...";
            Console.Write("?: "); Speak(dialogue1, 30); Console.ReadLine();
            Console.Write("War da was? Du könntest schwören du hast etwas gehört."); Console.ReadLine();
            string dialogue2 = "EY!";
            Console.Write("?: "); Speak(dialogue2, 25); Console.ReadLine();
            Console.Write("Du fällst nach hinten und musst dich auf deinen Händen abstützen."); Console.ReadLine();
            Console.Write("Du schaust in die Richtung aus der das Geräusch kam und siehst etwas was dich durch die Dunkelheit mit großen Augen anguckt."); Console.ReadLine();
            string dialogue3 = "Ich geh mal davon aus, dass du neu hier bist... Mensch\n"
                + "Nein, ich weiß nicht warum du hier bist... \n"
                + "Ich weiß nicht mal selber warum ich hier bin...";
            Console.Write("?: "); Speak(dialogue3, 25); Console.ReadLine();
            Console.Write("Das etwas tippt mit seinen langen Krallen auf den Boden, so als ob es darüber nachdenkt wie es selber hier gelandet ist."); Console.ReadLine();
            string dialogue4 = "Wie dem auch sei...";
            Console.Write("?: "); Speak(dialogue4, 40);
            string dialogue5 = "Du könntest etwas Hilfe gebrauchen, möchtest du, dass ich, nenn mich einfach Gort, dich auf diesem Level begleite?\n";
            Speak(dialogue5, 30);
            Console.WriteLine("-- Soll Gort dich auf diesem Level begleiten (j) oder (n)? --");
            List<string> gortChoices = new List<string> { "j", "n" };
            string shouldGortJoin = GetUserInput(gortChoices);
            if (shouldGortJoin.Equals("j"))
            {
                Console.Write("-- Gort begleitet dich auf diesem Level --");
                LoadingAnimation("");
                Console.Clear();
                Level1WithGort(player, playerInventory);
            }
            else
            {
                Console.Write("-- Gort begleitet dich nicht auf diesem Level --");
                LoadingAnimation("");
                Console.Clear();
                Level1WithoutGort(player, playerInventory);
            }

        }
        public static void Level1WithGort(Character player, Inventory playerInventory)
        {
            List<string> yesOrNo = new List<string> { "j", "n" };
            List<string> fightOrFlee = new List<string> { "f", "k" };
            Character gort = new Character();
            Program.SetPlayer(gort, "level1Companion");
            gort.Name = "Gort";
            List<Character> party = new List<Character>();
            party.Add(player);
            party.Add(gort);
            Console.Write("Gort: "); Speak("Du bist schlau für einen Menschen...\n", 30);
            Console.Write("Gort: "); Speak("Aber du hast richtig entschieden! Keiner kennt sich hier, wo auch immer wir sind, besser aus als Gort.\n", 30);
            Console.Write("Gort: "); Speak("Wo wir hingehen?\n", 40);
            Console.Write("Gort: "); Speak("Dahin wo das Verlies dich haben will... Ich helfe dir nur heile dort anzukommen.\n", 30);
            Console.Write("Gort: "); Speak("Du siehst übrigens fertig aus, hier trink das, das wird deinen Zustand etwas verbessern\n", 30);
            Console.Write("Gort gibt dir ein verdächtig aussehendes Fläschchen mit einer lila übel riechenden Flüssigkeit"); Console.ReadLine();
            Console.WriteLine("-- Flüssigkeiten trinken? (j) oder (n)");
            string drinkFluid = GetUserInput(yesOrNo);

            if (drinkFluid.Equals("j")){
                player.Health = player.Health + 50;
                Console.Write($"-- Health + 50 = {player.Health} --"); Console.ReadLine();
            }
            else
            {
                Console.Write("Du möchtest nicht riskieren deinen Zustand zu verschlechtern und tust nur so als ob du die Flüssigkeit trinkst."); Console.ReadLine();
            }

            Console.Write("Gort: "); Speak("Wenn man vom Teufel spricht, ich hasse diese Fliege Dinger.\n", 30);
            Console.Write("Als du hinter dich guckst siehst du riesige fliegende Insekten auf dich zukommen, die dich an Moskitos erinnern."); Console.ReadLine();

            Enemy mosquito = new Enemy();
            mosquito = SetEnemy("mosquito");

            Console.Write("Gort: "); Speak("Hier nimm das\n", 25);
            Console.Write("Gort gibt dir ein Messer, du nimmst es an und packst das Schwert, dass du bist jetzt bei dir getragen hast in deine Tasche."); Console.ReadLine();

            playerInventory.Weapons.Add(new Weapon() { Name = "Nahkampf Messer", Description = "Scharfes Messer, besonders effektiv im Nahkampf", AttackDamage = 38 });          
            Console.WriteLine($"-- Neue Waffe! Das {playerInventory.Weapons[1].Name} wurde in deine Tasche hinzugefügt (tippe e, um den Inhalt der Tasche anzuzeigen)");
            string input1 = Console.ReadLine();

            if (input1.Equals("e"))
            {
                Inventory.ShowInventory(playerInventory);
            }

            Console.Write("Gort: "); Speak("\nWillst du kämpfen oder sollen wir weglaufen?\n", 20);
            Console.WriteLine("Gort fängt schon an sich zu ducken und wartet auf deine Antwort. (f) oder (k)");
            string choiceToFight = GetUserInput(fightOrFlee);

            FightOrFlee(choiceToFight, party, mosquito, playerInventory);
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("Gort: "); Speak("Glück gehabt, ich hatte schon einige Auseinandersetzungen mit diesen nervigen Teilen.\nKomm wir müssen weiter.", 35); Console.ReadLine();
            Console.Write("Du lässt dich von Gort durch den Gang in dem ihr euch befindet führen."); Console.ReadLine();
            Console.Write("Die Wände sind feucht und uneben, so als ob sie aus organischen Material bestehen, du willst sie anfassen, um deine Vermutung zu überprüfen, aber deine Hand verharrt."); Console.ReadLine();
            Console.Write("Gort: "); Speak("Das würde ich nicht anfassen...", 45); Console.ReadLine();
            Console.Write("Gort: "); Speak("Das Verlies besteht aus allen, die je hier gefangen waren, die meisten Leute betreten nur das Verlies", 40); Console.ReadLine();
            Console.Write("Du musst ein Würgen zurückhalten, als du verstehst was Gort meint"); Console.ReadLine();
            Console.Write("Gort: "); Speak("Der Eingang in das nächste Level wird streng bewacht.", 40); Console.ReadLine();
            Console.Write("Gort: "); Speak("Du wirst wohl oder übel nicht an einem Kampf vorbei kommen, also sollten wir deine Ausrüstung verbessern.", 40); Console.ReadLine();
            Console.Write("Ihr bleibt an einer Öffnung in der Wand stehen. Dahinter in einer Art Raum, sitzt etwas was aussieht wie eine alte Frau."); Console.ReadLine();
            Console.Write("Ihr Körper ist mit dem Raum verschmolzen, sie ist wohl darauf angewiesen, dass man zu ihr kommt."); Console.ReadLine();
            Console.Write("?: "); Speak("Hmmm.... Gort was hast du mir da wieder gebracht", 50); Console.ReadLine();
            Console.WriteLine("Ihre Worte sind eher wie ein Kratzen, als wie eine Stimme"); Console.ReadLine();
            Console.Write("Gort: "); Speak("Alte Frau, wir sind auf deine Dienste angewiesen. Dieser Reisende muss in das tiefere Level gelangen", 35); Console.ReadLine();
            Console.Write("Alte Frau: "); Speak("Ahah... verstehe...", 50); Console.ReadLine();
            Console.Write("Alte Frau: "); Speak("Ich kann dir Rüstung geben... Gute Rüstung... Dafür musst du mir aber was von deiner Beweglichkeit geben...\nWie du siehst habe ich davon nicht mehr viel...", 50); Console.ReadLine();
            Console.Write("Du denkst nach und wegst ab ob du lieber bessere Verteidigung hättest, oder deine Beweglichkeit behalten möchtest."); Console.ReadLine();
            Console.WriteLine("-- Beweglichkeit für Verteidigung eintasuchen? (j) oder (n)");
            string agilityForDefense = GetUserInput(yesOrNo);

            if (agilityForDefense.Equals("j"))
            {
                player.Agility -= 5;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"-- Agility - 5 = {player.Agility}");
                player.Defense += 15;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"-- Defense + 15 = {player.Defense}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine("Du traust der alten Frau nicht und lehnst es dankend ab, als du und Gort den \"Raum\" wieder verlässt spürst du ihren verurteilenden Blick im Nacken"); Console.ReadLine();
                Console.Write("Gort: "); Speak("Du hättest ihr Angebot anmehmen sollen, aber naja mach was immer du für richtig hältst...", 40);
            }


        }
        public static void Level1WithoutGort(Character player, Inventory playerInventory)
        {

        }
        static string GetUserInput(List<string> list)
        {
            bool looping = true;
            string userInput = "";

            while (looping)
            {
                userInput = Console.ReadLine();
                foreach (string option in list)
                {
                    if (option.Equals(userInput))
                    {
                        return userInput;
                    }
                }
                Console.WriteLine("Bitte eine der genannten Optionen eingeben.");
            }
            return userInput;
        }
        public static Inventory Intro(Character player, string profession)
        {
            List<Character> players = new List<Character>();
            players.Add(player);
            Console.Clear();
            Console.Write("Du betritst ein Verlies, du weißt nicht wie du hergekommen bist oder was du hier machst.\n"); Console.ReadLine();
            Console.Write("Als du dich umschaust tritt ein alter gekrümmter Mann aus der Ecke, du siehst weder eine Tür noch eine andere Öffnung, \ndu wunderst dich wo er herkommt"); Console.ReadLine();

            string dialogue1 = $"Hallo {player.Name}... "
                + $"Deine Profession is also {profession}...\n";
            Speak(dialogue1, 0);

            Console.Write("Du fragst dich woher er deinen Namen und deine Profession kennt, aber er redet einfach weiter."); Console.ReadLine();

            string dialogue2= $"In den Verliesen ist dies allerdings egal, das wirst du noch früher oder später bemerken.\n"
                + $"Hier ist eine Tasche, die wirst du für deine Waffen brauchen.\n";

            Speak(dialogue2, 0);

            Inventory playerInventory = new Inventory();

            Console.WriteLine($"-- Du hast eine Tasche erhalten (tippe e, um den Inhalt der Tasche anzuzeigen) --");

            playerInventory.Weapons = new List<Weapon>();

            playerInventory.Weapons.Add(new Weapon() { Name = "Amateur Schwert", Description = "Einfaches Anfänger Schwert", AttackDamage = 30 });

            string input1 = Console.ReadLine();

            if (input1.Equals("e"))
            {
                Inventory.ShowInventory(playerInventory);
            }

            string dialogue3 = $"Wie du siehst liegt dort ein {Inventory.ShowWeapons(0, playerInventory)} in der Tasche.\n"
                + $"Du bist kein Amateur? In den Verließen ist das am Anfang jeder, also glaub dich nicht besser als du bist.\n"
                + $"Deine Einführung ist vorbei, ich glaube ich sehe auch deinen ersten Feind hinter dir.\n"
                + $"Viel Spaß...\n"; Console.ReadLine();

            Speak(dialogue3, 0);

            Enemy deathEater = new Enemy();
            deathEater = SetEnemy("deathEater");

            Console.Write("-- Ein Aasfresser ist hinter dir aufgetaucht --"); Console.ReadLine();
            Console.Write($"{deathEater.Description}"); Console.ReadLine();
            Console.WriteLine("Tippe (f) zum fliehen oder (k) zum kämpfen");

            List<string> fightOrFlee = new List<string> { "f", "k" };
            input1 = GetUserInput(fightOrFlee);
            FightOrFlee(input1, players, deathEater, playerInventory);
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write($"Der Aasfresser fällt tot um und verschmilzt im Boden, so als ob er nie dagewesen ist."); Console.ReadLine();
            Console.Write($"Bevor du begreifen kannst was hier vor sich geht drehst du dich erschrocken um, als du bemerkst, dass der alte Mann wieder hinter dir steht." +
                $""); Console.ReadLine();

            string dialogue4 = $"Hmm..."
                + $"Ich hätte dich als schwächer eingeschätzt {player.Name}\n"
                + $"Aber gut! Dann kann ich dich wohl weiter ins Verließ lassen.\n";
            Speak(dialogue4, 0);

            Console.WriteLine("Bevor du protestieren kannst, schubst der alte Mann dich nach vorne und du fällst durch den Boden.");
            LoadingAnimation("");
            Console.Clear();
            return playerInventory;
        }
        static void FightOrFlee(string option, List<Character> players, Enemy enemy, Inventory inventory)
        {
            Character player = players[0];
            switch (option)
            {
                case "k":
                    StartFight(players, enemy, inventory);
                    break;
                case "f":
                    TryFleeing(player, enemy, inventory);
                    break;

            }
        }

        static void StartFight(List<Character> players, Enemy enemy, Inventory inventory)
        {       
            int amountAttacks = enemy.Attacks.Count;
            int amountPlayers = players.Count;
            bool fighting = true;
            Random rnd = new Random();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;

            while (fighting)
            {
                int randomAttack = rnd.Next(0, amountAttacks);
                int randomPlayer = rnd.Next(0, amountPlayers);
                Character player = players[randomPlayer];
                string attack1 = enemy.Attacks[randomAttack].AttackName;
                int attack1Damage = enemy.Attacks[randomAttack].Damage;

                Console.Write($"{player.Name} Health: {player.Health}\t{enemy.Name} Health: {enemy.Health}"); Console.ReadLine();
                Console.Write($"-- Der {enemy.Name} greift an. --"); Console.ReadLine();
                Console.Write($"-- Er verwendet die Attacke {attack1}. --"); Console.ReadLine();
                player.Health = player.Health - attack1Damage;
                CheckIfPlayerDead(player.Health);
                Console.WriteLine($"{player.Name} hat {attack1Damage} Health verloren, Health: {player.Health}"); Console.ReadLine();

                Console.WriteLine($"{player.Name} kann angreifen! Wähle deine Waffe:");
                Console.WriteLine(Inventory.ShowWeapons(-1, inventory));
                Weapon weapon1 = GetWeapon(inventory);
                Console.Write($"-- {player.Name} attakiert {enemy.Name} mit {weapon1.Name}. --"); Console.ReadLine();
                enemy.Health = enemy.Health - weapon1.AttackDamage;
                Console.Write($"-- {enemy.Name} nimmt {weapon1.AttackDamage} Schaden. --"); Console.ReadLine();
                Console.Clear();
                fighting = CheckIfEnemyDead(enemy.Health, enemy.Name);
            }
            
        }
        static void TryFleeing(Character player, Enemy enemy, Inventory inventory)
        {
            List<Character> players = new List<Character>();
            players.Add(player);
            Console.Write("Du versuchst zu fliehen."); Console.ReadLine();
            Console.WriteLine("Kopf oder Zahl?");
            string choice = Console.ReadLine().ToLower();
            if (CoinFlip(choice))
            {
                Console.Write("Du konntest fliehen"); Console.ReadLine();
            }
            else
            {
                Console.Write("Du konntest nicht fliehen."); Console.ReadLine();
                Console.Write("Du hast dir beim Fliehen das Bein verletzt."); Console.ReadLine();
                player.Health = player.Health - 10;
                Console.Write($"-10 Health: {player.Health}"); Console.ReadLine();
                StartFight(players, enemy, inventory);
            }


        }
        static bool CoinFlip(string choice)
        {
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, 3);

            if (randomNumber.Equals(1))
            {
                return true;
            }
            else if (randomNumber.Equals(2))
            {
                return false;
            }
            return false;
        }
        static Weapon GetWeapon(Inventory inventory)
        {
            bool looping = true;
            string input = "";
            Weapon weapon = inventory.Weapons[0];

            while (looping)
            {

                input = Console.ReadLine().ToLower();

                foreach (Weapon weaponOption in inventory.Weapons)
                {
                    if (input.Equals(weaponOption.Name.ToLower()))
                    {
                        return weaponOption;
                    }
                }                               
                Console.WriteLine("Bitte verfügbare Waffe eingeben");
                
            }

            return weapon;
            

        }
        static void CheckIfPlayerDead(int health)
        {
            if (health <= 0)
            {
                Console.WriteLine("Du bist gestorben!");
                Exit();
            }
        }
        static bool CheckIfEnemyDead(int health, string name)
        {
            if (health <= 0)
            {
                Console.Write($"{name} ist gestorben!"); Console.ReadLine();
                return false;
            }
            else
            {
                return true;
            }
        }


        static void Speak(string line, int time)
        {
            // Color Choosing Möglichkeit einbauen für verschiedene Charaktere;
            Console.ForegroundColor = ConsoleColor.Magenta;
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                Thread.Sleep(time);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        static Enemy SetEnemy(string enemyType)
        {
            Enemy enemy = new Enemy();

            switch (enemyType)
            {
                case "deathEater":
                    enemy.Name = "Aasfresser";
                    enemy.Attacks = new List<Attack>();
                    enemy.Attacks.Add(new Attack() { AttackName = "Augen ausstechen", Damage = 30 });
                    enemy.Attacks.Add(new Attack() { AttackName = "Federschwung Verwirrung", Damage = 20 });
                    enemy.Health = 80;
                    enemy.Description = "Ein Mittelgroßer fast schwarzer Vogel. Mit den dunklen Wänden des Verließes ist er kaum zu erkennen.";
                    break;
                case "mosquito":
                    enemy.Name = "Mutanten Moskito";
                    enemy.Attacks = new List<Attack>();
                    enemy.Attacks.Add(new Attack { AttackName = "Blut saugen", Damage = 35 });
                    enemy.Attacks.Add(new Attack { AttackName = "Schwindel Vergiftung", Damage = 30 });
                    enemy.Health = 90;
                    enemy.Description = "Riesige Moskitos, das Summen schallt von den Wänden des Verließes was es schwer macht sich zu orientieren.";
                    break;
            }
            return enemy;
        }
        public static void Exit()
        {
            LoadingAnimation("Closing Game");

            Environment.Exit(0);
        }
        public static void LoadingAnimation(string prompt)
        {
            Console.Write($"\n{prompt}");

            // Kleine Ladeanimation, damit das Programm nicht zu abrupt schließt:
            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");

                // Pausiert zwischen Ausgaben für 1/2 Sekunde:
                Thread.Sleep(500);
            }
        }
    }
}
