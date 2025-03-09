using Items;
using Newtonsoft.Json.Linq;
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
        internal static string workingDirectory = Environment.CurrentDirectory;
        internal string resourcesFilePath = Path.Combine(Directory.GetParent(workingDirectory).Parent.Parent.FullName, "Resources");

        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

        Plotline plotline = new Plotline();

        internal List<object> PlayerInventory = new();

        internal static Character Player = new();

        internal void LoadExistingCharacter()
        {
            string playerInventoryString = File.ReadAllText(Path.Combine(resourcesFilePath, "PlayerInventory.json"));

            if (String.IsNullOrWhiteSpace(playerInventoryString))
            {
                Console.WriteLine("No items found.");
            }
            else
            {
                JArray jsonArray = JArray.Parse(playerInventoryString);

                List<Weapon> weapons = new List<Weapon>();
                List<Food> foods = new List<Food>();

                foreach (var obj in jsonArray)
                {
                    string type = obj["Type"].ToString();

                    if (type == "Weapon")
                    {
                        weapons.Add(new Weapon
                        {
                            Type = ItemType.Weapon,
                            Name = obj["Name"].ToString(),
                            Damage = Convert.ToInt32(obj["Damage"]),
                            Durabibily = Convert.ToInt32(obj["Durability"])
                        });
                    }
                    else if (type == "Food")
                    {
                        foods.Add(new Food
                        {
                            Type = ItemType.Food,
                            Name = obj["Name"].ToString(),
                            HealAmount = Convert.ToInt32(obj["HealAmount"]),
                        });
                    }
                }

                PlayerInventory.AddRange(weapons);
                PlayerInventory.AddRange(foods);
            }

            

            string playerAttributesString = File.ReadAllText(Path.Combine(resourcesFilePath, "PlayerAttributes.json"));

            Player = JsonSerializer.Deserialize<Character>(playerAttributesString);
            
            foreach (Weapon fwef in PlayerInventory)
            {
                Console.WriteLine($"{fwef.Name} has {fwef.Durabibily} health and has {fwef.Damage} charisma");
            }

            Console.WriteLine($"{Player.PlayerName} has {Player.PlayerHealth} health and has {Player.CharismaStat} charisma");
            Console.ReadLine();
        }

        public void CharacterCreator()
        {
            LoadExistingCharacter();

            UI.TypeWriterConsoleWrite("what's your name stranger?");

            Player.PlayerName = Console.ReadLine();

            UI.TypeWriterConsoleWrite($"Welcome {Player.PlayerName}!");
            UI.TypeWriterConsoleWrite("choose your class:");
            UI.TypeWriterConsoleWrite("1: Assassin - a member of the Shroud, a mysterious group of killers. Hail sithis!");
            UI.TypeWriterConsoleWrite("2: Soldier - a loyal and fierce fighter. Will do anything to serve his kingdom. anything.");
            UI.TypeWriterConsoleWrite("3: Peasant - Poor but spirited");

            string characterClass = Console.ReadLine();

            string filePath = Path.Combine(resourcesFilePath, $"PlayerAttributes.json");
            string playerAttributesJson = "";

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

                    playerAttributesJson = JsonSerializer.Serialize(Player, options);

                    File.WriteAllText(filePath, playerAttributesJson);

                    AddItemToIventory(ItemType.Weapon, "Dagger");
                    AddItemToIventory(ItemType.Food, "Apple");

                    UI.TypeWriterConsoleWrite("Press enter to continue.");
                    Console.ReadLine();

                    plotline.storyMain();

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

                    playerAttributesJson = JsonSerializer.Serialize(Player, options);

                    File.WriteAllText(filePath, playerAttributesJson);

                    AddItemToIventory(ItemType.Weapon, "Knights_Sword");
                    AddItemToIventory(ItemType.Food, "Steak");

                    UI.TypeWriterConsoleWrite("Press enter to continue.");
                    Console.ReadLine();

                    plotline.storyMain();
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

                    playerAttributesJson = JsonSerializer.Serialize(Player, options);

                    File.WriteAllText(filePath, playerAttributesJson);

                    AddItemToIventory(ItemType.Weapon, "Hatchet");
                    AddItemToIventory(ItemType.Food, "Bread");

                    UI.TypeWriterConsoleWrite("Press enter to continue.");
                    Console.ReadLine();

                    plotline.storyMain();
                    break;
            }
        }

        internal void AddItemToIventory(ItemType itemType, string itemName)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string filePath = Path.Combine(resourcesFilePath, $"{itemType}.json");
            string itemLibrary = File.ReadAllText(filePath);
            string inventoryFilePath = Path.Combine(resourcesFilePath, "Resources", "playerInventory.json");
            string jsonInventoryString = "";

            switch (itemType)
            {
                case ItemType.Weapon:
                    List<Weapon> weaponList = JsonSerializer.Deserialize<List<Weapon>>(itemLibrary);

                    Weapon weapon = weaponList.Where(w => w.Name == itemName).First();

                    PlayerInventory.Add(weapon);

                    jsonInventoryString = JsonSerializer.Serialize(PlayerInventory);

                    File.WriteAllText(inventoryFilePath, jsonInventoryString);

                    break;
                case ItemType.Food:
                    List<Food> foodList = JsonSerializer.Deserialize<List<Food>>(itemLibrary);

                    Food food = foodList.Where(f => f.Name == itemName).First();

                    PlayerInventory.Add(food);

                    jsonInventoryString = JsonSerializer.Serialize(PlayerInventory);

                    File.WriteAllText(inventoryFilePath, jsonInventoryString);

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


