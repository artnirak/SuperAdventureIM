using System.Collections.Generic;
using System.Linq;

namespace Engine
{
    public static class World
    {
        public static readonly List<Item> _items = new List<Item>();
        public static readonly List<Weapon> _weapons = new List<Weapon>();
        public static readonly List<Monster> _monsters = new List<Monster>();
        public static readonly List<Quest> _quests = new List<Quest>();
        public static readonly List<Location> _locations = new List<Location>();

        // May get rid of this since there is a VendorWants property
        public const int UNSELLABLE_ITEM_PRICE = -1;

        // Items (Weapons, Rewards, Lootables)
        public const int ITEM_ID_RUSTY_SWORD = 1;
        public const int ITEM_ID_RAT_TAIL = 2;
        public const int ITEM_ID_PIECE_OF_FUR = 3;
        public const int ITEM_ID_SNAKE_FANG = 4;
        public const int ITEM_ID_SNAKESKIN = 5;
        public const int ITEM_ID_SNAKE_VENOM_SAC = 6;
        public const int ITEM_ID_CLUB = 7;
        public const int ITEM_ID_HEALING_POTION = 8;
        public const int ITEM_ID_SPIDER_FANG = 9;
        public const int ITEM_ID_SPIDER_SILK = 10;
        public const int ITEM_ID_ADVENTURER_PASS = 11;

        // Monsters
        public const int MONSTER_ID_RAT = 1;
        public const int MONSTER_ID_SNAKE_COPPERHEAD = 2;
        public const int MONSTER_ID_SNAKE_BLACK = 3;
        public const int MONSTER_ID_GIANT_SPIDER = 4;

        // Quests
        public const int QUEST_ID_CLEAR_ALCHEMIST_GARDEN = 1;
        public const int QUEST_ID_CLEAR_FARMERS_FIELD = 2;

        // Locations
        public const int LOCATION_ID_HOME = 20;
        public const int LOCATION_ID_TOWN_SQUARE = 14;
        public const int LOCATION_ID_GUARD_POST = 15;
        public const int LOCATION_ID_ALCHEMIST_HUT = 8;
        public const int LOCATION_ID_ALCHEMISTS_GARDEN = 2;
        public const int LOCATION_ID_FARMHOUSE = 13;
        public const int LOCATION_ID_FARM_FIELD = 12;
        public const int LOCATION_ID_BRIDGE = 16;
        public const int LOCATION_ID_SPIDER_FIELD = 17;

        static World()
        {
            PopulateItems();
            PopulateMonsters();
            PopulateQuests();
            PopulateLocations();
        }

        private static void PopulateItems()
        {
            _weapons.Add(new Weapon(ITEM_ID_RUSTY_SWORD, "Espada Enferrujada", "Espadas Enferrujadas", 0, 5, 5, true, false));
            _weapons.Add(new Weapon(ITEM_ID_CLUB, "Bastão", "Bastões", 3, 10, 50, true, false));
            _items.Add(new Weapon(ITEM_ID_RUSTY_SWORD, "Espada Enferrujada", "Espadas Enferrujadas", 0, 5, 5, true, false));
            _items.Add(new Item(ITEM_ID_RAT_TAIL, "Cauda de rato", "Caudas de rato", 1, false, true));
            _items.Add(new Item(ITEM_ID_PIECE_OF_FUR, "Pêlo de rato", "Pêlos de rato", 1, false, true));
            _items.Add(new Item(ITEM_ID_SNAKE_FANG, "Presa de cobra", "Presas de cobra", 1, false, true));
            _items.Add(new Item(ITEM_ID_SNAKESKIN, "Pele de cobra", "Peles de cobra", 2, false, true));
            _items.Add(new Item(ITEM_ID_SNAKE_VENOM_SAC, "Veneno de aranha", "Venenos de aranha", 4, false, true));
            _items.Add(new Weapon(ITEM_ID_CLUB, "Bastão", "Bastões", 3, 10, 50, true, false));
            _items.Add(new HealingPotion(ITEM_ID_HEALING_POTION, "Poção de vida", "Poções de vida", 5, 3, false, false));
            _items.Add(new Item(ITEM_ID_SPIDER_FANG, "Presa de aranha", "Presas de aranha", 1, false, true));
            _items.Add(new Item(ITEM_ID_SPIDER_SILK, "Seda de aranha", "Sedas de aranha", 1, false, true));
            _items.Add(new Item(ITEM_ID_ADVENTURER_PASS, "Passe de aventureiro", "Passes de aventureiro", UNSELLABLE_ITEM_PRICE, true, false));
        }

        private static void PopulateMonsters()
        {
            Monster rat = new Monster(MONSTER_ID_RAT, "Rato", 5, 3, 10, 3, 3);
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_RAT_TAIL), 75, false));
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_PIECE_OF_FUR), 75, true));

            Monster snake_black = new Monster(MONSTER_ID_SNAKE_BLACK, "Víbora", 5, 3, 10, 3, 3);
            snake_black.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKE_FANG), 75, false));
            snake_black.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKESKIN), 75, true));

            Monster snake_copperhead = new Monster(MONSTER_ID_SNAKE_COPPERHEAD, "Cascavel", 9, 7, 15, 7, 7);
            snake_copperhead.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKE_FANG), 70, false));
            snake_copperhead.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKESKIN), 70, false));
            snake_copperhead.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKE_VENOM_SAC), 30, false));

            Monster giantSpider = new Monster(MONSTER_ID_GIANT_SPIDER, "Aranha Gigante", 20, 5, 40, 10, 10);
            giantSpider.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_FANG), 75, true));
            giantSpider.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_SILK), 25, false));

            _monsters.Add(rat);
            _monsters.Add(snake_black);
            _monsters.Add(snake_copperhead);
            _monsters.Add(giantSpider);
        }

        private static void PopulateQuests()
        {
            Quest clearAlchemistGarden =
                new Quest(
                    QUEST_ID_CLEAR_ALCHEMIST_GARDEN,
                    "Inraticida",
                    "Mata ratos no jardim do alquimista. A recompensa é uma poção de vida e 10 moedas de ouro.", 20, 10);

            clearAlchemistGarden.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_RAT_TAIL), 3));

            clearAlchemistGarden.RewardItem = ItemByID(ITEM_ID_HEALING_POTION);

            Quest clearFarmersField =
                new Quest(
                    QUEST_ID_CLEAR_FARMERS_FIELD,
                    "Exterminação",
                    "Mata cobras no campo do agricultor. A recompensa é um passe de aventureiro e 20 moedas de ouro.", 20, 20);

            clearFarmersField.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_SNAKE_FANG), 3));
            clearFarmersField.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_SNAKE_VENOM_SAC), 1));

            clearFarmersField.RewardItem = ItemByID(ITEM_ID_ADVENTURER_PASS);

            _quests.Add(clearAlchemistGarden);
            _quests.Add(clearFarmersField);
        }

        private static void PopulateLocations()
        {
            // Create each location
            Location home = new Location(LOCATION_ID_HOME, "Casa", "O teu quarto está muito desarrumado...");

            Location townSquare = new Location(LOCATION_ID_TOWN_SQUARE, "Praça da cidade", "Vês uma fonte de água.");
            Vendor hobbitWares = new Engine.Vendor("Hobbit mercante", 50);
            hobbitWares.AddItemToInventory(ItemByID(ITEM_ID_PIECE_OF_FUR), 5);
            hobbitWares.AddItemToInventory(ItemByID(ITEM_ID_RAT_TAIL), 3);
            hobbitWares.AddItemToInventory(ItemByID(ITEM_ID_CLUB), 1);
            hobbitWares.AddItemToInventory(ItemByID(ITEM_ID_RUSTY_SWORD), 1);

            townSquare.VendorWorkingHere = hobbitWares;

            Location alchemistHut = new Location(LOCATION_ID_ALCHEMIST_HUT, "Cabana do Alquimista", "Tem umas plantas estranhas nas prateleiras.");
            alchemistHut.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_ALCHEMIST_GARDEN);

            Location alchemistsGarden = new Location(LOCATION_ID_ALCHEMISTS_GARDEN, "Jardim do Alquimista", "Muitas flores e frutos crescem aqui.");
            alchemistsGarden.AddMonster(MONSTER_ID_RAT, 100);

            Location farmhouse = new Location(LOCATION_ID_FARMHOUSE, "Rancho", "Uma casa na quinta. O agricultor parece ocupado.");
            farmhouse.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_FARMERS_FIELD);

            Location farmersField = new Location(LOCATION_ID_FARM_FIELD, "Campo do Agricultor", "Linhas de vegetais crescem aqui.");
            farmersField.AddMonster(MONSTER_ID_SNAKE_BLACK, 70);
            farmersField.AddMonster(MONSTER_ID_SNAKE_COPPERHEAD, 30);

            Location guardPost = new Location(LOCATION_ID_GUARD_POST, "Posto de Guarda", "O guarda é intimidante.", ItemByID(ITEM_ID_ADVENTURER_PASS));

            Location bridge = new Location(LOCATION_ID_BRIDGE, "Ponte", "Uma ponte de pedra sobre um rio largo.");

            Location spiderField = new Location(LOCATION_ID_SPIDER_FIELD, "Floresta Obscura", "Teias de aranhas cobrem as árvores nesta floresta.");
            spiderField.AddMonster(MONSTER_ID_GIANT_SPIDER, 100);

            // Link the locations together
            home.LocationToNorth = townSquare;

            townSquare.LocationToNorth = alchemistHut;
            townSquare.LocationToSouth = home;
            townSquare.LocationToEast = guardPost;
            townSquare.LocationToWest = farmhouse;

            farmhouse.LocationToEast = townSquare;
            farmhouse.LocationToWest = farmersField;

            farmersField.LocationToEast = farmhouse;

            alchemistHut.LocationToSouth = townSquare;
            alchemistHut.LocationToNorth = alchemistsGarden;

            alchemistsGarden.LocationToSouth = alchemistHut;

            guardPost.LocationToEast = bridge;
            guardPost.LocationToWest = townSquare;

            bridge.LocationToWest = guardPost;
            bridge.LocationToEast = spiderField;

            spiderField.LocationToWest = bridge;

            // Add the locations to the static list
            _locations.Add(home);
            _locations.Add(townSquare);
            _locations.Add(guardPost);
            _locations.Add(alchemistHut);
            _locations.Add(alchemistsGarden);
            _locations.Add(farmhouse);
            _locations.Add(farmersField);
            _locations.Add(bridge);
            _locations.Add(spiderField);
        }

        public static Item ItemByID(int id)
        {
            return _items.SingleOrDefault(x => x.ID == id);
        }

        public static Weapon WeaponByID(int id)
        {
            return _weapons.SingleOrDefault(x => x.ID == id);
        }

        public static Monster MonsterByID(int id)
        {
            return _monsters.SingleOrDefault(x => x.ID == id);
        }

        public static Quest QuestByID(int id)
        {
            return _quests.SingleOrDefault(x => x.ID == id);
        }

        public static Location LocationByID(int id)
        {
            return _locations.SingleOrDefault(x => x.ID == id);
        }
    }
}