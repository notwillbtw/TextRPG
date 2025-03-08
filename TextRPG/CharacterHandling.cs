using Items;
using System.Text.Json;

namespace TextRPG
{
    internal class StatHandling
    {
        internal static Random random = new Random();

        internal static int statCheck(StatTypes statType)
        {

            int statCheck = 0;

            switch (statType)
            {
                case StatTypes.strength:
                    statCheck = random.Next(0, 20) + CharacterHandling.Player.StrengthStat;
                    break;
                case StatTypes.dexterity:
                    statCheck = random.Next(0, 20) + CharacterHandling.Player.DexStat;
                    break;
                case StatTypes.charisma:
                    statCheck = random.Next(0, 20) + CharacterHandling.Player.CharismaStat;
                    break;
                case StatTypes.stealth:
                    statCheck = random.Next(0, 20) + CharacterHandling.Player.StealthStat;
                    break;
                case StatTypes.arcane:
                    statCheck = random.Next(0, 20) + CharacterHandling.Player.ArcaneStat;
                    break;
            }

            return statCheck;
        }

        internal enum StatTypes
        {
            strength,
            dexterity,
            charisma,
            stealth,
            arcane

        }
    }

    internal class CharacterHandling
    {
        Plotline plotline = new Plotline();

        internal List<object> PlayerInventory = new();

        internal static Character Player = new();

        internal void CharacterCreator()
        {
            UI.TypeWriterConsoleWrite("what's your name stranger?");

            Player.PlayerName = Console.ReadLine();

            UI.TypeWriterConsoleWrite($"Welcome {Player.PlayerName}!");
            UI.TypeWriterConsoleWrite("choose your class:");
            UI.TypeWriterConsoleWrite("1: Assassin - a member of the Shroud, a mysterious group of killers. Hail sithis!");
            UI.TypeWriterConsoleWrite("2: Soldier - a loyal and fierce fighter. Will do anything to serve his kingdom. anything.");
            UI.TypeWriterConsoleWrite("3: Peasant - Poor but spirited");

            string characterClass = Console.ReadLine();

            switch (characterClass)
            {
                case "1":
                    UI.TypeWriterConsoleWrite("Welcome, assassin.");
                    Player.PlayerHealth = 70;
                    Player.PlayerRole = Role.assassin;
                    Player.StrengthStat = 3;
                    Player.DexStat = 6;
                    Player.StealthStat = 9;
                    Player.CharismaStat = 6;
                    Player.ArcaneStat = 0;

                    AddItemToIventory(ItemType.weapon, "Dagger");
                    AddItemToIventory(ItemType.food, "Apple");

                    UI.TypeWriterConsoleWrite("Press enter to continue.");
                    Console.ReadLine();

                    plotline.peasantPlotline();

                    break;
                case "2":
                    UI.TypeWriterConsoleWrite("Welcome, Soldier.");
                    Player.PlayerHealth = 100;
                    Player.PlayerRole = Role.soldier;
                    Player.StrengthStat = 9;
                    Player.DexStat = 6;
                    Player.StealthStat = 3;
                    Player.CharismaStat = 4;
                    Player.ArcaneStat = 1;

                    AddItemToIventory(ItemType.weapon, "Knights_Sword");
                    AddItemToIventory(ItemType.food, "Steak");

                    UI.TypeWriterConsoleWrite("Press enter to continue.");
                    Console.ReadLine();

                    plotline.peasantPlotline();
                    break;
                case "3":
                    UI.TypeWriterConsoleWrite("Welcome, Peasant.");
                    Player.PlayerHealth = 50;
                    Player.PlayerRole = Role.peasant;
                    Player.StrengthStat = 4;
                    Player.DexStat = 4;
                    Player.StealthStat = 3;
                    Player.CharismaStat = 9;
                    Player.ArcaneStat = -1;

                    AddItemToIventory(ItemType.weapon, "Hatchet");
                    AddItemToIventory(ItemType.food, "Bread");

                    UI.TypeWriterConsoleWrite("Press enter to continue.");
                    Console.ReadLine();

                    plotline.peasantPlotline();
                    break;
            }
        }

        private void AddItemToIventory(ItemType itemType, string itemName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", $"{itemType}.json");
            string inventoryFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "playerInventory.json");
            string itemLibrary = File.ReadAllText(filePath);

            switch (itemType)
            {
                case ItemType.weapon:
                    List<Weapon> weaponList = JsonSerializer.Deserialize<List<Weapon>>(itemLibrary);

                    Weapon weapon = weaponList.Where(w => w.Name == itemName).First();

                    PlayerInventory.Add(weapon);

                    string jsonWeaponString = JsonSerializer.Serialize(weapon);

                    File.WriteAllText(inventoryFilePath, jsonWeaponString);

                    break;
                case ItemType.food:
                    List<Food> foodList = JsonSerializer.Deserialize<List<Food>>(itemLibrary);

                    Food food = foodList.Where(f => f.Name == itemName).First();

                    string jsonFoodString = JsonSerializer.Serialize(food);

                    File.WriteAllText(inventoryFilePath, jsonFoodString);

                    break;
            }



        }


    }

    enum Role
    {
        unassigned,
        assassin,
        peasant,
        soldier
    }

    class Character
    {
        public string PlayerName { get; set; }

        public Role PlayerRole { get; set; }

        public int PlayerHealth { get; set; }


        public int StrengthStat { get; set; }

        public int DexStat { get; set; }

        public int CharismaStat { get; set; }

        public int StealthStat { get; set; }

        public int ArcaneStat { get; set; }
    }

}


