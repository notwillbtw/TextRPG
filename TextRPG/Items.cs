namespace Items
{
    internal enum ItemType
    {
        Weapon,
        Food
    }

    internal enum ItemNames
    {
        Dagger,
        Hatchet,
        Knights_Sword,

        Apple,
        Steak,
        Bread
    }

    internal class Weapon
    {
        public ItemType Type { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public int Durabibily { get; set; }

    }

    internal class Food
    {
        public ItemType Type { get; set; }
        public int HealAmount { get; set; }

        public string Name { get; set; }
    }
}


