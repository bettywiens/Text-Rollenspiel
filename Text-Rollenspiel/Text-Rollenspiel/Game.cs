using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
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
        public static Inventory Intro(Character player, string profession)
        {
            int talkingSpeed = 25;
            string userChoice = "";

            Inventory playerInventory = new Inventory();
            playerInventory.Weapons = new List<Weapon>();
            List<Character> players = new List<Character>();
            players.Add(player);

            Enemy deathEater = new Enemy();

            List<string> fightOrFlee = new List<string> { "f", "k" };

            Console.Clear();

            Narrator("Du betritst ein Verlies, du weißt nicht wie du hergekommen bist oder was du hier machst.");
            Narrator("Als du dich umschaust tritt ein alter gekrümmter Mann aus der Ecke, du siehst weder eine Tür noch eine andere Öffnung, du wunderst dich wo er herkommt.");            
            
            Speak("?", $"Hallo {player.Name}... ", talkingSpeed);
            Speak("", $"Du bist also {profession}...", talkingSpeed);

            Narrator("Du fragst dich woher er deinen Namen und deine Profession kennt, aber er redet einfach weiter.");

            Speak("?", "In den Verliesen ist dies allerdings egal, das wirst du noch früher oder später bemerken.", talkingSpeed);
            Speak("", $"Hier ist eine Tasche, die wirst du für deine Waffen brauchen.", talkingSpeed);

            Description($"Du hast eine Tasche erhalten ~ tippe (e), um den Inhalt der Tasche anzuzeigen: ", "neutral", "question");   

            // Neue Waffe wird zum Inventory hinzugefügt:
            playerInventory.Weapons.Add(new Weapon() { Name = "Amateur Schwert", Description = "Einfaches Anfänger Schwert", AttackDamage = player.Attack });

            userChoice = Console.ReadLine();

            if (userChoice.Equals("e"))
            {
                Inventory.ShowInventory(playerInventory); Console.ReadLine();
            }            

            Speak("?", $"Wie du siehst liegt dort ein {Inventory.ShowWeapons(0, playerInventory)} in der Tasche.", talkingSpeed);
            Speak("", "Du bist kein Amateur? In den Verließen ist das am Anfang jeder, also glaub dich nicht besser als du bist.", talkingSpeed);
            Speak("", "Deine Einführung ist vorbei, ich glaube ich sehe auch deinen ersten Feind hinter dir.", talkingSpeed);
            Speak("", "Viel Spaß...", talkingSpeed);
            
            deathEater = SetEnemy("deathEater");

            Description("Ein Aasfresser ist hinter dir aufgetaucht", "neutral", "normal");
            Narrator($"{deathEater.Description}");
            Description("Tippe (f) zum fliehen oder (k) zum kämpfen", "neutral", "question");
            
            // Nimmt Eingabe vom Spieler und Startet entweder den Kampf oder Fluchtversuch:
            userChoice = GetUserInput(fightOrFlee);
            FightOrFlee(userChoice, players, deathEater, playerInventory);

            Console.Clear();            

            Narrator($"Der Aasfresser fällt tot um und verschmilzt im Boden, so als ob er nie dagewesen ist.");
            Narrator($"Bevor du begreifen kannst was hier vor sich geht drehst du dich erschrocken um, als du bemerkst, dass der alte Mann wieder hinter dir steht.");


            Speak("?", "Hmm...", talkingSpeed);
            Speak("", $"Ich hätte dich als schwächer eingeschätzt {player.Name}", talkingSpeed);
            Speak("","Aber gut! Dann kann ich dich wohl weiter ins Verließ lassen.", talkingSpeed);

            Narrator("Bevor du protestieren kannst, schubst der alte Mann dich nach vorne und du fällst durch den Boden.");
            LoadingAnimation("");
            Console.Clear();

            return playerInventory;
        }
        public static void Level1(Character player, Inventory playerInventory)
        {
            string shouldGortJoin = "";

            Character gort = new Character();
            Program.SetPlayer(gort, "level1Companion");
            gort.Name = "Gort";
            gort.TalkingSpeed = 25;
            gort.TalkingSlow = 40;

            List<string> yesOrNo = new List<string> { "j", "n" };
            string levelName = "VERLIES LEVEL 1\n";
            Thread.Sleep(500);
            
            Speak("",levelName, 50);

            Narrator("Du bist eindeutig tiefer ins Verlies gefallen, es ist noch dunkler als zuvor und die Luft ist so dick, dass man sie zerschneiden könnte.");
            Speak("?", "Ey...", gort.TalkingSlow);
            Narrator("War da was? Du könntest schwören du hast etwas gehört.");
            Speak("?", "EY!", gort.TalkingSpeed);
            Narrator("Du fällst nach hinten und musst dich auf deinen Händen abstützen.");
            Narrator("Du schaust in die Richtung aus der das Geräusch kam und siehst etwas was dich durch die Dunkelheit mit großen Augen anguckt.");
            Speak("?", "Ich geh mal davon aus, dass du neu hier bist... Mensch", gort.TalkingSpeed);
            Speak("", "Nein, ich weiß nicht warum du hier bist... ", gort.TalkingSpeed);
            Speak("", "Ich weiß nicht mal selber warum ich hier bin...", gort.TalkingSpeed);
            Narrator("Das etwas tippt mit seinen langen Krallen auf den Boden, so als ob es darüber nachdenkt wie es selber hier gelandet ist.");
            Speak("?", "Wie dem auch sei...", gort.TalkingSlow);
            Speak("", "Du kannst mich übrigens Gort nennen", gort.TalkingSpeed);
            Speak("Gort", "Du könntest etwas Hilfe gebrauchen, möchtest du, dass ich dich auf diesem Level begleite?", gort.TalkingSpeed);
            Description("Soll Gort dich auf diesem Level begleiten (j) oder (n)?", "neutral", "question");
            
            // Nimmt (j) oder (n) input vom Spieler an und gibt es zurücK:
            shouldGortJoin = GetUserInput(yesOrNo);

            if (shouldGortJoin.Equals("j"))
            {
                Description("Gort begleitet dich auf diesem Level", "neutral", "normal");
                Description("Gort ist deiner Gruppe beigetreten!", "positive", "normal");
                LoadingAnimation("");
                Console.Clear();
                // Level 1 wird mit Gort in der Gruppe fortgeführt:
                Level1WithGort(player, gort, playerInventory);
            }
            else
            {
                Description("Gort begleitet dich nicht auf diesem Level", "neutral", "normal");
                LoadingAnimation("");
                Console.Clear();
                // Level 1 wird ohne Gort in der Gruppe fortgeführt:
                Level1WithoutGort(player, gort, playerInventory);
            }
        }
        public static void Level1WithGort(Character player, Character gort, Inventory playerInventory)
        {
            string foughtOrFled = "";
            int oldWomanTalkingSpeed = 45;
            string drinkFluid = "";
            string userChoice = "";

            // Gegner für Level 1:
            Enemy mosquito = new Enemy();
            mosquito = SetEnemy("mosquito");
            Enemy mutantWolf = new Enemy();
            mutantWolf = SetEnemy("mutantWolf");
            int enemyTalkingSpeed = 25;

            // Input-Möglichkeiten Level 1:
            List<string> yesOrNo = new List<string> { "j", "n" };
            List<string> fightOrFlee = new List<string> { "f", "k" };

            // Charaktere Level 1:
            List<Character> party = new List<Character>();
            party.Add(player);
            party.Add(gort);

            Speak("Gort", "Du bist schlau für einen Menschen...", gort.TalkingSpeed);
            Speak("", "Aber du hast richtig entschieden! Keiner kennt sich hier, wo auch immer wir sind, besser aus als Gort.", gort.TalkingSpeed);
            Speak("", "Wo wir hingehen?", gort.TalkingSlow);
            Speak("", "Dahin wo das Verlies dich haben will... Ich helfe dir nur heile dort anzukommen.", gort.TalkingSlow);

            // Wenn der Health vom Spieler unter 100 liegt, bietet Gort ihm einen Heilungstrank an:
            if (player.Health < 100)
            {
                Speak("", "Du siehst übrigens fertig aus, hier trink das, das wird deinen Zustand etwas verbessern.", gort.TalkingSpeed);
                Narrator("Gort gibt dir ein verdächtig aussehendes Fläschchen mit einer lila übel riechenden Flüssigkeit.");
                Description("Flüssigkeiten trinken? (j) oder (n)", "neutral", "question");
                drinkFluid = GetUserInput(yesOrNo);

                if (drinkFluid.Equals("j")){
                    player.Health = player.Health + 50;
                    Description($"Health + 50 = {player.Health}", "positive", "normal");
                }
                else
                {
                    Narrator("Du möchtest nicht riskieren deinen Zustand zu verschlechtern und tust nur so als ob du die Flüssigkeit trinkst.");
                }
            }

            Speak("Gort", "Wenn man vom Teufel spricht, ich hasse diese Fliege Dinger.", gort.TalkingSpeed);
            Narrator("Als du hinter dich guckst siehst du riesige fliegende Insekten auf dich zukommen, die dich an Moskitos erinnern.");                        

            Speak("Gort", "Hier nimm das!", gort.TalkingSpeed);
            Narrator("Gort gibt dir ein Messer, du nimmst es an und packst das Schwert, dass du bist jetzt bei dir getragen hast in deine Tasche.");

            // Neue Waffe wird ins Inventory hinzugefügt:
            playerInventory.Weapons.Add(new Weapon() { Name = "Nahkampf Messer", Description = "Scharfes Messer, besonders effektiv im Nahkampf", AttackDamage = 38 });
            
            Description($"Neue Waffe! Das {playerInventory.Weapons[1].Name} wurde in deine Tasche hinzugefügt ~ tippe (e), um den Inhalt der Tasche anzuzeigen", "positive", "question");

            userChoice = Console.ReadLine();

            if (userChoice.Equals("e"))
            {
                Inventory.ShowInventory(playerInventory); Console.ReadLine();
            }

            Speak("Gort", "Willst du kämpfen oder sollen wir weglaufen?", gort.TalkingSpeed);
            Description("Gort fängt schon an sich zu ducken und wartet auf deine Antwort ~ (f) oder (k)?", "neutral", "question");

            // Kampf oder Fluchtversuch wird gestartet:
            userChoice = GetUserInput(fightOrFlee);
            FightOrFlee(userChoice, party, mosquito, playerInventory);           

            Console.Clear();

            Speak("Gort", "Glück gehabt, ich hatte schon einige Auseinandersetzungen mit diesen nervigen Teilen.\nKomm wir müssen weiter.", gort.TalkingSpeed);
            Narrator("Du lässt dich von Gort durch den Gang in dem ihr euch befindet führen.");
            Narrator("Die Wände sind feucht und uneben, so als ob sie aus organischen Material bestehen, du willst sie anfassen, um deine Vermutung zu überprüfen, aber deine Hand verharrt.");
            Speak("Gort", "Das würde ich nicht anfassen...", gort.TalkingSlow);
            Speak("", "Das Verlies besteht aus allen, die je hier gefangen waren, die meisten Leute betreten nur das Verlies", gort.TalkingSlow);
            Narrator("Du musst ein Würgen zurückhalten, als du verstehst was Gort meint");
            Speak("Gort", "Der Eingang in das nächste Level wird streng bewacht.", gort.TalkingSlow);
            Speak("", "Du wirst wohl oder übel nicht an einem Kampf vorbei kommen, also sollten wir deine Ausrüstung verbessern.", gort.TalkingSlow);
            Narrator("Ihr bleibt an einer Öffnung in der Wand stehen. Dahinter in einer Art Raum, sitzt etwas was aussieht wie eine alte Frau.");
            Narrator("Ihr Körper ist mit dem Raum verschmolzen, sie ist wohl darauf angewiesen, dass man zu ihr kommt.");
            Speak("?", "Hmmm.... Gort was hast du mir da wieder gebracht", oldWomanTalkingSpeed);
            Narrator("Ihre Worte sind eher wie ein Kratzen, als wie eine Stimme");
            Speak("Gort", "Alte Frau, wir sind auf deine Dienste angewiesen. Dieser Reisende muss in das tiefere Level gelangen", gort.TalkingSpeed);
            Speak("Alte Frau", "Ahah... verstehe...", oldWomanTalkingSpeed);
            Speak("", "Ich kann dir Rüstung geben... Gute Rüstung... Dafür musst du mir aber was von deiner Beweglichkeit geben...\nWie du siehst habe ich davon nicht mehr viel...", oldWomanTalkingSpeed);
            Narrator("Du denkst nach und wegst ab ob du lieber bessere Verteidigung hättest, oder deine Beweglichkeit behalten möchtest.");
            Description("Beweglichkeit für Verteidigung eintauschen? (j) oder (n)", "neutral", "question");

            // Spieler kann entscheiden ob er etwas Beweglichkeit für Verteidigung eintauschen möchte:
            userChoice = GetUserInput(yesOrNo);

            if (userChoice.Equals("j"))
            {
                player.Agility -= 5;
                player.Defense += 50;
                
                Description($"Beweglichkeit - 5 = {player.Agility}", "negative", "normal");              
                Description($"Verteidigung + 50 = {player.Defense}", "positive", "normal");                
            }
            else
            {
                Narrator("Du traust der alten Frau nicht und lehnst es dankend ab, als du und Gort den \"Raum\" wieder verlässt spürst du ihren verurteilenden Blick im Nacken");
                Speak("Gort", "Du hättest ihr Angebot anmehmen sollen, aber naja mach was immer du für richtig hältst...", gort.TalkingSlow);
            }

            Console.Clear();
            Speak("Gort", "Dahinten ist es.", gort.TalkingSpeed);
            Narrator("Gort zeigt auf ein Tor, oder eher eine Öffnung, davor befinden sich Kreaturen die schlecht auszumachen sind.");
            Speak("Gort", "An denen kommst du nicht vorbei...", gort.TalkingSpeed);
            Speak("", "Wenn du vor dem Kampf fliehst, bleibst du für immer auf diesem Level stecken...", gort.TalkingSlow);
            Speak("", "Wie ich das weiß?", gort.TalkingSpeed);
            Speak("", "Ich glaube das ist offensichtlich", gort.TalkingSpeed);
            Narrator("Die Kreaturen kommen näher, sie sehen aus wie eine Mischung aus Wolf und Bär");
            Speak("?", "Du schon wieder, du hast einen weiteren Menschen gefunden, der es versuchen möchte?", enemyTalkingSpeed);
            Speak("Gort", "Du weißt doch dass ich weich bin für Neuankömmlinge, lass den Mensch es versuchen Hellkar", gort.TalkingSpeed);
            Speak("Hellkar", "Ach du kennst mich doch, für einen Kampf bin ich immer zu haben...", enemyTalkingSpeed);

            Description("Willst du kämpfen oder fliehen? ~ (k) oder (f)", "neutral", "question");

            userChoice = GetUserInput(fightOrFlee);

            foughtOrFled = FightOrFlee(userChoice, party, mutantWolf, playerInventory);
            

            if (foughtOrFled.Equals("k"))
            {
                Speak("Gort", "Er ist nicht wirklich tot, guck nach oben er kommt gleich wieder", gort.TalkingSpeed);
                Narrator("Du schaust gespannt nach oben und aus der \"Decke\" des Verlieses kommt Hellkar langsam zum Vorschein");
                Speak("Hellkar", "...", enemyTalkingSpeed);
                Narrator("Er fällt auf den Boden und bleibt für eine Weile sitzen, bevor er aufsteht und sich wieder gerade hinstellt");
                Speak("Hellkar", "So so...", enemyTalkingSpeed);
                Speak("", "Dann werde ich dich wohl ins nächste Level lassen müssen", enemyTalkingSpeed);
                Narrator("Du fragst dich kurz ob dies überhaupt dein Wille war, aber bevor du etwas entgegnen kannst öffnet sich der Boden unter dir");
                Speak("Gort", "War schön dich kennengelernt zu haben.", gort.TalkingSpeed);

                LoadingAnimation("");
                Console.Clear(); // Hier würde der Spieler ins nächste Level geschickt werden.
            }
            // Wenn der Spieler erfolgreich flüchtet, hat er das Spiel verloren:
            else if (foughtOrFled.Equals("f"))
            {
                Speak("Gort", "Ich hatte dir doch gesagt was das Fliehen für Folgen hat...", gort.TalkingSlow);
                Speak("", "Naja ich beschwer mich nicht, dann hab ich zumindestens Gesellschaft.", gort.TalkingSpeed);
                Speak("", "Für wie lange?", gort.TalkingSpeed);
                Speak("","Ich hab dich wohl für schlauer gehalten als du bist.", gort.TalkingSpeed);
                Narrator("Du weißt nicht wie viele Tage vergangen sind, oder ob es hier überhaupt so etwas gibt.");
                Narrator("Es ist dunkel, und die Luft ist dick, du hast das Gefühl, dass die Wände angefangen haben mit dir zu reden...");
                Narrator("Vielleicht ist das aber auch gar keine Einbildung.");
                LoadingAnimation("");
                Console.Clear();
                Narrator("Du verbringst den Rest deiner Tage in Level 1 des Verlieses und fragst dich was wohl passiert wäre wenn...");
                Narrator("Du erinnerst dich nicht, deine Erinnerungen werden mit jedem Tag weniger");
                LoadingAnimation("");
                Exit();
            }            
        }
        public static void Level1WithoutGort(Character player,Character gort, Inventory playerInventory)
        {
            string foughtOrFled = "";
            int oldWomanTalkingSpeed = 45;
            string userChoice = "";

            Enemy mosquito = new Enemy();
            mosquito = SetEnemy("mosquito");
            Enemy mutantWolf = new Enemy();
            mutantWolf = SetEnemy("mutantWolf");
            int enemyTalkingSpeed = 25;
            List<Character> party = new List<Character>();
            party.Add(player);

            List<string> yesOrNo = new List<string> { "j", "n" };
            List<string> fightOrFlee = new List<string> { "f", "k" };

            Speak("Gort", "Naja man kann einen Menschen nicht zu seinem Glück zwingen...", gort.TalkingSlow);
            Speak("Gort", "Übrigens, hinter dir- ach ne du wolltest meine Hilfe nicht.", gort.TalkingSpeed);
            Narrator("Gort verschwindet aus deiner Sicht, du bist dir nicht ganz sicher wohin er verschwunden ist");
            Narrator("Du drehst dich um und siehst wie riesige wie Moskitos aussehende Kreaturen auf dich zukommen.");
            Narrator("Du duckst dich aber es ist bereits zu spät");
            Description("Kämpfen oder fliehen? ~ (f) oder (k)", "neutral", "question");

            // Abfrage Fluchtversuch oder Kampf gegen Moskito:
            userChoice = GetUserInput(fightOrFlee);
            FightOrFlee(userChoice, party, mosquito, playerInventory);

            Narrator("Du fragst dich wie du das überleben konntest, und guckst zu wie der riesige Moskito im Boden versinkt.");
            Narrator("Du gehst entlang des Weges, der im dimmen Licht zu erkennen ist.");
            Narrator("Die Wände sind feucht und sehen weich aus, für einen kurzen Moment denkst du darüber nach sie zu berühren, aber eine Stimme hält dich davon ab");
            Speak("?","Reisender!", oldWomanTalkingSpeed);
            Narrator("Du drehst dich um in die Richtung aus der die kratzige Stimme kommt");
            Speak("?", "Ja genau, du.", oldWomanTalkingSpeed);
            Narrator("Etwas was aussieht wie eine alte Frau sitzt hinter einer Öffnung in der Wand, ein Raum? Mehr wie eine Ausscharbung");
            Speak("Alte Frau", "Hmm... Du willst bestimmt in das nächste Level stimmts?", oldWomanTalkingSpeed);
            Speak("", "In diesem Zustand wirst du allerdings nicht sehr weit kommen...", oldWomanTalkingSpeed);
            Narrator("Du schaust runter auf deine aufgescharbten Knie und zerrissene Kleidung.");
            Speak("Alte Frau", "Wie wärs damit?... Ich gebe dir Rüstung und dafür...", oldWomanTalkingSpeed);
            Speak("", "Dafür gibst du mir etwas von deiner Beweglichkeit, wie du siehst mangelt es mir daran sehr", oldWomanTalkingSpeed);
            Narrator("Bei nähere Betrachtung siehst du nicht wo die alte Frau endet und der \"Raum\" anfängt.");

            Description("Beweglichkeit für Verteidigung eintauschen? (j) oder (n)", "neutral", "question");

            // Spieler kann entscheiden ob er Beweglichkeit gegen Verteidigung eintauschen möchte:
            userChoice = GetUserInput(yesOrNo);

            if (userChoice.Equals("j"))
            {
                player.Agility -= 10; // Spieler bekommt einen schlechteren Deal, wenn er ohne Gort mit der alten Frau redet;
                player.Defense += 40;

                Description($"Beweglichkeit - 5 = {player.Agility}", "negative", "normal");
                Description($"Verteidigung + 50 = {player.Defense}", "positive", "normal");
                Narrator("Du verlässt die alte Frau, und gehst in die Richtung die sie dir gezeigt hat.");
            }
            else
            {
                Narrator("Du traust der alten Frau nicht und lehnst es dankend ab, als du den \"Raum\" wieder verlässt spürst du ihren verurteilenden Blick im Nacken");
            }

            Console.Clear();

            Narrator("Nachdem du ein paar Minuten herumirrst findest du dich vor einer noch größeren Öffnung in der Wand wieder.");
            Narrator("Man könnte es fast als Tor beschreiben, vor diesem stehen mehrere dunkle Kreaturen.");
            Narrator("Sie erinnern dich an eine Mischung aus einem Wolf und einem Bär, als du näher an sie rantrittst, tritt einer von ihnen aus der Dunkelheit hervor.");
            Speak("?", "Was haben wir denn da?", enemyTalkingSpeed);
            Speak("", "Lass mich raten, du hast keine Ahnung warum du hier bist?", enemyTalkingSpeed);
            Speak("", "Ich habe Recht? Das habe ich mir schon gedacht", enemyTalkingSpeed);
            Narrator("Er mustert dich säufzend");
            Speak("?", "Ich geh mal davon aus, dass du ins nächste Level möchstest.", enemyTalkingSpeed);
            Speak("", "Dafür musst du aber gegen mich kämpfen, bist du dazu bereit?", enemyTalkingSpeed);

            Description("Kämpfen oder fliehen? ~ (f) oder (k)", "neutral", "question");

            // Abfrage Kampf oder Fluchtversuch:
            userChoice = GetUserInput(fightOrFlee);
            foughtOrFled = FightOrFlee(userChoice, party, mutantWolf, playerInventory);

            if (foughtOrFled.Equals("k"))
            {
                Narrator("Ist er tot? Du bist dir nicht sicher was als nächstes geschieht, als ein Geräusch von der Decke kommt");
                Narrator("Du schaust gespannt nach oben und aus der \"Decke\" des Verlieses kommt der Mutanten Wolf langsam zum Vorschein");
                Speak("?", "...", enemyTalkingSpeed);
                Narrator("Er fällt auf den Boden und bleibt für eine Weile sitzen, bevor er aufsteht und sich wieder gerade hinstellt");
                Speak("?", "So so...", enemyTalkingSpeed);
                Speak("", "Dann werde ich dich wohl ins nächste Level lassen müssen", enemyTalkingSpeed);
                Narrator("Du fragst dich kurz ob dies überhaupt dein Wille war, aber bevor du etwas entgegnen kannst öffnet sich der Boden unter dir");

                LoadingAnimation("");
                Console.Clear();
            }
            // Spieler hat verloren, wenn er versucht hat zu fliehen, weiß dies aber nicht, da er Gort nicht aufgenommen hat:
            else if (foughtOrFled.Equals("f"))
            {
                Speak("?", "Wusstest du etwa nicht was Fliehen hier zu Folge hat?", enemyTalkingSpeed);
                Speak("", "Nein? Naja zu spät, wer zu diesem Zeitpunkt flieht bleibt für immer in diesem Level.", enemyTalkingSpeed);
                Speak("", "Wie der alte Gort... Hätte ich ein Gewissen würde er mir leid tun", enemyTalkingSpeed);
                LoadingAnimation("");
                Console.Clear();
                Narrator("Du weißt nicht wie viele Tage vergangen sind, oder ob es hier überhaupt so etwas gibt.");
                Narrator("Es ist dunkel, und die Luft ist dick, du hast das Gefühl, dass die Wände angefangen haben mit dir zu reden...");
                Narrator("Vielleicht ist das aber auch gar keine Einbildung.");
                LoadingAnimation("");
                Console.Clear();
                Narrator("Du verbringst den Rest deiner Tage in Level 1 des Verlieses und fragst dich was wohl passiert wäre wenn...");
                Narrator("Du erinnerst dich nicht, deine Erinnerungen werden mit jedem Tag weniger");
                LoadingAnimation("");
                Exit();
            }            
        }
        // Startet je nach gewählter Option entweder einen Kampf oder einen Fluchtversuch:
        static string FightOrFlee(string option, List<Character> players, Enemy enemy, Inventory inventory)
        {
            Character player = players[0];
            switch (option)
            {
                case "k":
                    StartFight(players, enemy, inventory);
                    return "k";
                    
                case "f":                   
                    TryFleeing(player, enemy, inventory);
                    return "f";
            }
            return "0";
        }
        // Fragt Spieler nach Kopf oder Zahl und führt einen Münzwurf durch, wenn er richtig liegt kann er fliehen, sonst nicht:
        static void TryFleeing(Character player, Enemy enemy, Inventory inventory)
        {
            List<Character> players = new List<Character>(); // Um Kampf zu starten wird die Player Gruppe benötigt
            players.Add(player);

            bool looping = true;
            string choice = "";
            double lostHealthDouble = (((100.00 - player.Luck)/100.00) * player.Health)/3; // Wenn Fliehen fehlschlägt, verliert Spieler health proportional zur Gesamt-Health
            int lostHealth = Convert.ToInt32(lostHealthDouble); 
            List<string> validCoinFlipChoices = new List<string>
            {
                "kopf", "zahl"
            };

            Console.Write("Du versuchst zu fliehen."); Console.ReadLine();
            Description("Kopf oder Zahl?", "neutral", "question");

            while (looping)
            {
                choice = Console.ReadLine().ToLower();

                if (IsInList(validCoinFlipChoices, choice)) // Guckt ob Eingabe gültig ist
                {
                    // Guckt ob richtig oder falsch geraten wurde:
                    if (CoinFlip(choice))
                    {
                        Console.Write("Du konntest fliehen"); Console.ReadLine();
                        break;
                    }
                    else
                    {
                        Console.Write("Du konntest nicht fliehen."); Console.ReadLine();
                        if (player.Agility < 15) // Wenn der Spieler nicht sehr beweglich ist, verletzt er sich bei einem fehlgeschlagenen Fluchtversuch
                        {
                            Console.Write("Du hast dir beim Fliehen das Bein verletzt."); Console.ReadLine();
                            player.Health = player.Health - lostHealth;
                            Description($"Health - {lostHealth} = {player.Health}", "negative", "normal"); Console.ReadLine();
                        }
                        StartFight(players, enemy, inventory);
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Bitte Kopf oder Zahl wählen");
                    continue;
                }
            }
        }
        // Startet ein Kampf zwischen einem Gegner und einem oder mehr Charakteren:
        static void StartFight(List<Character> players, Enemy enemy, Inventory inventory)
        {   
            int amountAttacks = enemy.Attacks.Count;
            int amountPlayers = players.Count;
            int randomAttack = 0;
            int randomPlayer = 0;
            string attack1 = "";
            int attack1Damage = 0;
            Character player;

            bool fighting = true;
            Random rnd = new Random();

            Console.Clear();

            // Es wird solange gekämpft, bis entweder der Gegner oder der Spieler tot ist:
            while (fighting)
            {
                // Es wird ein zufälliger Spieler mit einer zufälligen Attacke jede Kampfrunde angegriffen.
                randomAttack = rnd.Next(0, amountAttacks);
                randomPlayer = rnd.Next(0, amountPlayers);
                player = players[randomPlayer];
                attack1 = enemy.Attacks[randomAttack].AttackName;
                attack1Damage = enemy.Attacks[randomAttack].Damage;
                attack1Damage -= Convert.ToInt32(player.Defense * 0.5); // Gegnerschaden anhand von Verteidigung des Spielers berechnen

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($"{player.Name} Health: {player.Health}\t{enemy.Name} Health: {enemy.Health}\n"); Console.ReadLine();
                
                Description($"Der {enemy.Name} greift {player.Name} an.", "neutral", "normal");
                Description($"Er verwendet die Attacke {attack1}.", "neutral", "normal");
                player.Health = player.Health - attack1Damage;                                      // Health wird durch Schaden geringer
                CheckIfPlayerDead(player.Health);                                                   // Überprüfen ob Spieler tot ist
                Description($"{player.Name} hat {attack1Damage} Health verloren, Health: {player.Health}\n", "negative", "normal");

                Description($"{player.Name} kann angreifen! Wähle eine Waffe:\n", "neutral", "question");
                Console.WriteLine($"{Inventory.ShowWeapons(-1, inventory)}\n");                     // Zeigt Waffen, die Spieler im Inventory hat
                Weapon weapon1 = GetWeapon(inventory);                                              // Lässt Spieler Waffe auswählen
                Description($"{player.Name} attakiert {enemy.Name} mit {weapon1.Name}.", "neutral", "normal");
                enemy.Health = enemy.Health - weapon1.AttackDamage;                                 // Gegner Health nimmt mit Schaden von Spieler Waffe ab
                Description($"{enemy.Name} nimmt {weapon1.AttackDamage} Schaden.", "positive", "normal");
                Console.Clear();
                fighting = CheckIfEnemyDead(enemy.Health, enemy.Name);                              // Wenn Gegner tot ist wird der while loop gestoppt
            }

        }
        // Validiert Spieler Input, mithilfe einer Liste, die alle möglichen inputs enthält:
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
        // Überprüft ob ein string in einer Liste ist:
        static bool IsInList(List<string> list, string item)
        {
            foreach (string s in list)
            {
                if (s.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }
        // Führt einen CoinFlip durch und überpüft ob die choice mit dem Ergebnis übereinstimmt:
        static bool CoinFlip(string choice)
        {
            Random rnd = new Random();
            int randomNumber = rnd.Next(1, 3);
            int choiceToNumber = 0;

            switch (choice)
            {
                case "kopf":
                    choiceToNumber = 1;
                    break;
                case "zahl":
                    choiceToNumber = 2;
                    break;
            }

            if (randomNumber.Equals(choiceToNumber))
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
        // Überprüft ob Spieler gestorben ist, wenn ja wird das Spiel beendet:
        static void CheckIfPlayerDead(int health)
        {
            if (health <= 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Du bist gestorben!");
                Exit();
            }
        }
        // Überprüft ob der Gegner gestorben ist, wenn ja ist der Kampf gewonnen:
        static bool CheckIfEnemyDead(int health, string name)
        {
            if (health <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{name} ist gestorben!"); Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            else
            {
                return true;
            }

        }
        // Fragt die Waffe vom Spieler, während des Kampfes ab:
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
        // Legt die Eigenschaften für einen Gegner fest:
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
                case "mutantWolf":
                    enemy.Name = "Mutanten Wolf";
                    enemy.Attacks = new List<Attack>();
                    enemy.Attacks.Add(new Attack { AttackName = "Krallen Stecher", Damage = 40 });
                    enemy.Attacks.Add(new Attack { AttackName = "Nackenbiss", Damage = 50 });
                    enemy.Health = 120;
                    enemy.Description = "Riesiger auf zwei Beinen stehender Wolf, der die Kraft eines Bärs zu besitzen scheint";
                    break;
            }
            return enemy;
        }
        // Print Format, wenn Dialog gesprochen wird:
        static void Speak(string speaker, string line, int time)
        {
            // Color Choosing Möglichkeit einbauen für verschiedene Charaktere;
            if (!speaker.Equals(""))
            {
                Console.Write($"{speaker}: ");
            }

            Console.ForegroundColor = ConsoleColor.Magenta;           
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                Thread.Sleep(time);
            }
            Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
        // Print Format für beschreibende Texte:
        public static void Description(string line, string setting, string type)
        {
            if (type.Equals("normal"))
            {
                if (setting.Equals("neutral"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write($"-- {line}"); Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (setting.Equals("negative"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"-- {line}"); Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (setting.Equals("positive"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"-- {line}"); Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else if (type.Equals("question"))
            {
                if (setting.Equals("neutral"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write($"-- {line} ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (setting.Equals("negative"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"-- {line} ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (setting.Equals("positive"))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"-- {line} ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
        // Print Format für Erzählertexte:
        public static void Narrator(string line)
        {
            Console.Write($"> {line}"); Console.ReadLine();
        }
        // Beendet das Spiel:
        public static void Exit()
        {
            LoadingAnimation("Closing Game");

            Environment.Exit(0);
        }
        // Ladeanimation:
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
