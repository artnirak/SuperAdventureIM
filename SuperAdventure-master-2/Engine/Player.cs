using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Xml;
namespace Engine
{
    public class Player : LivingCreature
    {
        private int _gold;
        private int _experiencePoints;
        private Location _currentLocation;
        private bool _disableAudio;
        private string text = "";

        

        public event EventHandler<MessageEventArgs> OnMessage;
        
        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            private set
            {
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
                OnPropertyChanged(nameof(Level));
            }
        }

        public int Level
        {
            get { return ((ExperiencePoints / 100) + 1); }
        }

        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                OnPropertyChanged(nameof(CurrentLocation));
            }
        }

        public bool DisableAudio
        {
            get { return _disableAudio; }
            set { _disableAudio = value; }
        }

        public Weapon CurrentWeapon { get; set; }

        public BindingList<InventoryItem> Inventory { get; set; }

        public List<Weapon> Weapons
        {
            get { return Inventory.Where(x => x.Details is Weapon).Select(x => x.Details as Weapon).ToList(); }
        }

        public List<HealingPotion> Potions
        {
            get { return Inventory.Where(x => x.Details is HealingPotion).Select(x => x.Details as HealingPotion).ToList(); }
        }

        public BindingList<PlayerQuest> Quests { get; set; }

        public List<int> LocationsVisited { get; set; }

        public Monster CurrentMonster { get; set; }


        private Player(int currentHitPoints, int maximumHitPoints, int gold, int experiencePoints, bool disableAudio) : base(currentHitPoints, maximumHitPoints)
        {
            Gold = gold;
            DisableAudio = disableAudio;
            ExperiencePoints = experiencePoints;

            Inventory = new BindingList<InventoryItem>();
            Quests = new BindingList<PlayerQuest>();
            LocationsVisited = new List<int>();
        }

        public static Player CreateDefaultPlayer()
        {
            Player player = new Player(10, 10, 20, 0, false);
            player.CurrentLocation = World.LocationByID(World.LOCATION_ID_HOME);

            return player;
        }

        public static Player CreatePlayerFromXmlString(string xmlPlayerData)
        {
            try
            {
                XmlDocument playerData = new XmlDocument();

                playerData.LoadXml(xmlPlayerData);

                int currentHitPoints = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/CurrentHitPoints").InnerText);
                int maximumHitPoints = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/MaximumHitPoints").InnerText);
                int gold = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/Gold").InnerText);
                int experiencePoints = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/ExperiencePoints").InnerText);

                Player player = new Player(currentHitPoints, maximumHitPoints, gold, experiencePoints, false);

                int currentLocationID = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/CurrentLocation").InnerText);
                player.CurrentLocation = World.LocationByID(currentLocationID);

                if (playerData.SelectSingleNode("/Player/Stats/CurrentWeapon") != null)
                {
                    int currentWeaponID = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/CurrentWeapon").InnerText);
                    player.CurrentWeapon = (Weapon)World.ItemByID(currentWeaponID);
                }

                foreach (XmlNode node in playerData.SelectNodes("/Player/LocationsVisited/LocationVisited"))
                {
                    int id = Convert.ToInt32(node.Attributes["ID"].Value);

                    player.LocationsVisited.Add(id);
                }

                foreach (XmlNode node in playerData.SelectNodes("/Player/InventoryItems/InventoryItem"))
                {
                    int id = Convert.ToInt32(node.Attributes["ID"].Value);
                    int quantity = Convert.ToInt32(node.Attributes["Quantity"].Value);

                    for (int i = 0; i < quantity; i++)
                    {
                        player.AddItemToInventory(World.ItemByID(id));
                    }
                }

                foreach (XmlNode node in playerData.SelectNodes("/Player/PlayerQuests/PlayerQuest"))
                {
                    int id = Convert.ToInt32(node.Attributes["ID"].Value);
                    bool isCompleted = Convert.ToBoolean(node.Attributes["IsCompleted"].Value);

                    PlayerQuest playerQuest = new PlayerQuest(World.QuestByID(id));
                    playerQuest.IsCompleted = isCompleted;

                    player.Quests.Add(playerQuest);
                }

                return player;
            }
            catch
            {
                // If there was an error with the XML data, return a default player object
                return CreateDefaultPlayer();
            }
        }

        public static Player CreatePlayerFromDatabase(int currentHitPoints, int maximumHitPoints, int gold, int experiencePoints, int currentLocationID)
        {
            Player player = new Player(currentHitPoints, maximumHitPoints, gold, experiencePoints, false);

            //Comment the following for a bug when storing game data with an SQL database
            //player.MoveTo(World.LocationByID(currentLocationID));

            return player;
        }

        public void MoveTo(Location location, TTS tts)
        {
            if (PlayerDoesNotHaveTheRequiredItemToEnter(location))
            {
                RaiseMessage("Tens de obter " + location.ItemRequiredToEnter.Name + " primeiro para entrar neste local.");
                text = "Tens de obter " + location.ItemRequiredToEnter.Name + " primeiro para entrar neste local.";
                tts.Speak(text);
                text = "";
                return;
            }
            // The player can enter this location
            CurrentLocation = location;

            if (!LocationsVisited.Contains(CurrentLocation.ID))
            {
                LocationsVisited.Add(CurrentLocation.ID);
            }

            CompletelyHeal();

            if (location.HasAQuest)
            {
                if (PlayerDoesNotHaveThisQuest(location.QuestAvailableHere))
                {
                    GiveQuestToPlayer(location.QuestAvailableHere, tts);
                }
                else
                {
                    if (PlayerHasNotCompleted(location.QuestAvailableHere) &&
                       PlayerHasAllQuestCompletionItemsFor(location.QuestAvailableHere))
                    {
                        GivePlayerQuestRewards(location.QuestAvailableHere, tts);
                    }
                }
            }
            SetTheCurrentMonsterForTheCurrentLocation(location, tts);
        }

        public void MoveNorth(TTS tts)
        {
            if (CurrentLocation.LocationToNorth != null)
            {
                text = "Foste para Norte.";
                MoveTo(CurrentLocation.LocationToNorth, tts);
                
            }
            else
            {
                tts.Speak("Não podes ir mais para Norte.");
            }
        }

        public void MoveEast(TTS tts)
        {
            if (CurrentLocation.LocationToEast != null)
            {
                text = "Foste para Este.";
                MoveTo(CurrentLocation.LocationToEast, tts);
                
            }
            else
            {
                tts.Speak("Não podes ir mais para Este.");
            }
        }

        public void MoveSouth(TTS tts)
        {
            if (CurrentLocation.LocationToSouth != null)
            {
                text = "Foste para Sul.";
                MoveTo(CurrentLocation.LocationToSouth, tts);
                
            }
            else
            {
                tts.Speak("Não podes ir mais para Sul.");
            }
        }

        public void MoveWest(TTS tts)
        {
            if (CurrentLocation.LocationToWest != null)
            {
                text = "Foste para Oeste.";
                MoveTo(CurrentLocation.LocationToWest, tts);
                
            }
            else
            {
                tts.Speak("Não podes ir mais para Oeste.");
            }
        }

        public void UseWeapon(Weapon weapon,TTS tts)
        {
            int damage = RandomNumberGenerator.NumberBetween(weapon.MinimumDamage, weapon.MaximumDamage);
            if (damage == 0)
            {
                text="Falháste o ataque. ";
                RaiseMessage("Falhaste o ataque.");

                // Place AttackMiss sound
                PlayAudio("AttackMiss", DisableAudio);
                
            }
            else
            {
                CurrentMonster.CurrentHitPoints -= damage;
                RaiseMessage("Acertaste! Tiraste " + damage + " pontos de vida. ");
                text = "Acertáste! Tiráste " + damage + " pontos de vida. ";
                if (damage==1)
                {
                    text = "Acertáste! Tiráste " + damage + " ponto de vida. ";
                }
                
                // Place SwordHit or ClubHit sound
                
                if (CurrentWeapon.ID == World.ITEM_ID_RUSTY_SWORD)
                {
                   PlayAudio("SwordHit", DisableAudio);

                }
                else if (CurrentWeapon.ID == World.ITEM_ID_CLUB)
                { 
                    PlayAudio("ClubHit", DisableAudio);
                }
            }

            if (CurrentMonster.IsDead)
            {
                RaiseMessage("");
                RaiseMessage("Mataste o monstro!");

                // Place MonsterPain sound based on weapon used
                if (CurrentWeapon.ID == World.ITEM_ID_RUSTY_SWORD)
                {
                    PlayAudio("MonsterPainSword", DisableAudio);

                }
                else if (CurrentWeapon.ID == World.ITEM_ID_CLUB)
                {
                    PlayAudio("MonsterPainClub", DisableAudio);
                }

                LootTheCurrentMonster(tts);

                // "Move" to the current location, to refresh the current monster
                MoveTo(CurrentLocation, tts);
            }
            else
            {
                LetTheMonsterAttack(tts);
            }
            
        }

        private void LootTheCurrentMonster(TTS tts)
        {
            RaiseMessage("");
            RaiseMessage("Recebeste " + CurrentMonster.RewardExperiencePoints + " pontos de experiência.");
            
            RaiseMessage("Recebeste " + CurrentMonster.RewardGold + " moedas de ouro.");
            text = "Matáste o monstro! Recebeste " + CurrentMonster.RewardGold + " moedas de ouro " + "e " + CurrentMonster.RewardExperiencePoints + " pontos de experiência";
            AddExperiencePoints(CurrentMonster.RewardExperiencePoints);
            Gold += CurrentMonster.RewardGold;

            // Give monster's loot items to the player
            foreach (InventoryItem inventoryItem in CurrentMonster.LootItems)
            {
                AddItemToInventory(inventoryItem.Details);

                RaiseMessage(string.Format("Saqueaste: {0} {1}", inventoryItem.Quantity, inventoryItem.Description));
            }

            RaiseMessage("");

        }

        public void UsePotion(HealingPotion potion, TTS tts)
        {
            RaiseMessage("Tu bebes a " + potion.Name +".");
            text = "Bebeste a poção";
            

            HealPlayer(potion.AmountToHeal);

            RemoveItemFromInventory(potion);

            // The player used their turn to drink the potion, so let the monster attack now
            LetTheMonsterAttack(tts);
        }

        public void AddItemToInventory(Item itemToAdd, int quantity = 1)
        {
            InventoryItem existingItemInInventory = Inventory.SingleOrDefault(ii => ii.Details.ID == itemToAdd.ID);

            if (existingItemInInventory == null)
            {
                Inventory.Add(new InventoryItem(itemToAdd, quantity));
            }
            else
            {
                existingItemInInventory.Quantity += quantity;
            }

            RaiseInventoryChangedEvent(itemToAdd);
        }

        public void RemoveItemFromInventory(Item itemToRemove, int quantity = 1)
        {
            InventoryItem item = Inventory.SingleOrDefault(ii => ii.Details.ID == itemToRemove.ID && ii.Quantity >= quantity);

            if (item != null)
            {
                item.Quantity -= quantity;

                if (item.Quantity == 0)
                {
                    Inventory.Remove(item);
                }

                RaiseInventoryChangedEvent(itemToRemove);
            }
        }

        public string ToXmlString()
        {
            XmlDocument playerData = new XmlDocument();

            // Create the top-level XML node
            XmlNode player = playerData.CreateElement("Player");
            playerData.AppendChild(player);

            // Create the "Stats" child node to hold the other player statistics nodes
            XmlNode stats = playerData.CreateElement("Stats");
            player.AppendChild(stats);

            // Create the child nodes for the "Stats" node
            CreateNewChildXmlNode(playerData, stats, "CurrentHitPoints", CurrentHitPoints);
            CreateNewChildXmlNode(playerData, stats, "MaximumHitPoints", MaximumHitPoints);
            CreateNewChildXmlNode(playerData, stats, "Gold", Gold);
            CreateNewChildXmlNode(playerData, stats, "ExperiencePoints", ExperiencePoints);
            CreateNewChildXmlNode(playerData, stats, "CurrentLocation", CurrentLocation.ID);

            if (CurrentWeapon != null)
            {
                CreateNewChildXmlNode(playerData, stats, "CurrentWeapon", CurrentWeapon.ID);
            }

            // Create the "LocationsVisited" child node to hold each LocationVisited node
            XmlNode locationsVisited = playerData.CreateElement("LocationsVisited");
            player.AppendChild(locationsVisited);

            // Create an "LocationVisited" node for each item in the player's inventory
            foreach (int locationID in LocationsVisited)
            {
                XmlNode locationVisited = playerData.CreateElement("LocationVisited");

                AddXmlAttributeToNode(playerData, locationVisited, "ID", locationID);

                locationsVisited.AppendChild(locationVisited);
            }

            // Create the "InventoryItems" child node to hold each InventoryItem node
            XmlNode inventoryItems = playerData.CreateElement("InventoryItems");
            player.AppendChild(inventoryItems);

            // Create an "InventoryItem" node for each item in the player's inventory
            foreach (InventoryItem item in Inventory)
            {
                XmlNode inventoryItem = playerData.CreateElement("InventoryItem");

                AddXmlAttributeToNode(playerData, inventoryItem, "ID", item.Details.ID);
                AddXmlAttributeToNode(playerData, inventoryItem, "Quantity", item.Quantity);

                inventoryItems.AppendChild(inventoryItem);
            }

            // Create the "PlayerQuests" child node to hold each PlayerQuest node
            XmlNode playerQuests = playerData.CreateElement("PlayerQuests");
            player.AppendChild(playerQuests);

            // Create a "PlayerQuest" node for each quest the player has acquired
            foreach (PlayerQuest quest in Quests)
            {
                XmlNode playerQuest = playerData.CreateElement("PlayerQuest");

                AddXmlAttributeToNode(playerData, playerQuest, "ID", quest.Details.ID);
                AddXmlAttributeToNode(playerData, playerQuest, "IsCompleted", quest.IsCompleted);

                playerQuests.AppendChild(playerQuest);
            }

            return playerData.InnerXml; // The XML document, as a string, so we can save the data to disk
        }

        private bool HasRequiredItemToEnterThisLocation(Location location)
        {
            if (location.DoesNotHaveAnItemRequiredToEnter)
            {
                return true;
            }

            // See if the player has the required item in their inventory
            return Inventory.Any(ii => ii.Details.ID == location.ItemRequiredToEnter.ID);
        }

        private void SetTheCurrentMonsterForTheCurrentLocation(Location location,TTS tts)
        {
            // Populate the current monster with this location's monster (or null, if there is no monster here)
            CurrentMonster = location.NewInstanceOfMonsterLivingHere();
            
            if (CurrentMonster != null)
            {
                RaiseMessage("Tu vês "+ CheckMonsterGender(CurrentMonster.Name,1) +" " + CurrentMonster.Name+".");
                if(text != "")
                {
                    tts.Speak(text+" e vês " + CheckMonsterGender(CurrentMonster.Name, 1) + " " + CurrentMonster.Name + ".");
                    text = "";
                }
                else
                {
                    tts.Speak("Tu vês " + CheckMonsterGender(CurrentMonster.Name, 1) + " " + CurrentMonster.Name + ".");
                }
                
            }
            else
            {
                if(text!="")
                {
                    tts.Speak(text);
                    text = "";
                }
            }
        }

        private bool PlayerDoesNotHaveTheRequiredItemToEnter(Location location)
        {
            return !HasRequiredItemToEnterThisLocation(location);
        }

        private bool PlayerDoesNotHaveThisQuest(Quest quest)
        {
            return Quests.All(pq => pq.Details.ID != quest.ID);
        }

        private bool PlayerHasNotCompleted(Quest quest)
        {
            return Quests.Any(pq => pq.Details.ID == quest.ID && !pq.IsCompleted);
        }

        private void GiveQuestToPlayer(Quest quest, TTS tts)
        {
            RaiseMessage("Tens uma missão nova: " + quest.Name + ".");
            RaiseMessage(quest.Description);
            RaiseMessage("Para completá-la, volta com:");
            string items = "";
            foreach (QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                items += string.Format("{0} {1}", qci.Quantity,
                    qci.Quantity == 1 ? qci.Details.Name : qci.Details.NamePlural);
                items += ",";
                RaiseMessage(string.Format("{0} {1}", qci.Quantity,
                    qci.Quantity == 1 ? qci.Details.Name : qci.Details.NamePlural));
            }

            RaiseMessage("");

            Quests.Add(new PlayerQuest(quest));
            tts.Speak(text+" e tens uma missão nova: " + quest.Name + "."+ " Para completá-la, volta com: "+items);
            text = "";
            
        }

        private bool PlayerHasAllQuestCompletionItemsFor(Quest quest)
        {
            // See if the player has all the items needed to complete the quest here
            foreach (QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                // Check each item in the player's inventory, to see if they have it, and enough of it
                if (!Inventory.Any(ii => ii.Details.ID == qci.Details.ID && ii.Quantity >= qci.Quantity))
                {
                    return false;
                }
            }

            // If we got here, then the player must have all the required items, and enough of them, to complete the quest.
            return true;
        }

        private void RemoveQuestCompletionItems(Quest quest)
        {
            foreach (QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                InventoryItem item = Inventory.SingleOrDefault(ii => ii.Details.ID == qci.Details.ID);

                if (item != null)
                {
                    RemoveItemFromInventory(item.Details, qci.Quantity);
                }
            }
        }

        private void AddExperiencePoints(int experiencePointsToAdd)
        {
            ExperiencePoints += experiencePointsToAdd;
            MaximumHitPoints = (Level * 10);
        }

        private void GivePlayerQuestRewards(Quest quest, TTS tts)
        {
            RaiseMessage("");
            RaiseMessage("Completaste a missão '" + quest.Name + "'!");
            RaiseMessage("Recompensa: ");
            RaiseMessage(quest.RewardExperiencePoints + " pontos de experiência.");
            RaiseMessage(quest.RewardGold + " moedas de ouro");
            RaiseMessage(quest.RewardItem.Name, true);

            AddExperiencePoints(quest.RewardExperiencePoints);
            Gold += quest.RewardGold;

            RemoveQuestCompletionItems(quest);
            AddItemToInventory(quest.RewardItem);

            MarkPlayerQuestCompleted(quest);
            if(quest.RewardItem == null)
            {
                tts.Speak(text+" e completáste a missão '" + quest.Name + "'!" + "\n" + " Gánháste " + quest.RewardExperiencePoints + " pontos de experiência, " + quest.RewardGold + " moedas de ouro.");
            }
            else
            {
                tts.Speak(text+" e completáste a missão '" + quest.Name + "'!" + "\n" + " Gánháste " + quest.RewardExperiencePoints + " pontos de experiência, " + quest.RewardGold + " moedas de ouro e o item: "+quest.RewardItem.Name+".");
            }
            text = "";
        }

        private void MarkPlayerQuestCompleted(Quest quest)
        {
            PlayerQuest playerQuest = Quests.SingleOrDefault(pq => pq.Details.ID == quest.ID);

            if (playerQuest != null)
            {
                playerQuest.IsCompleted = true;
            }
        }

        private String CheckMonsterGender(String name, int op)
        {
            String returnval = "";
            switch (op)
            {
                case 0:
                    if (name == "Rato")
                    {
                        returnval = "o";
                    }
                    else
                    {
                        returnval = "a";
                    }
                    return returnval;
                case 1:
                    if (name == "Rato")
                    {
                        returnval= "um";
                    }
                    else
                    {
                        returnval = "uma";
                    }

                    return returnval;
            }
            return returnval;
        }

        private void LetTheMonsterAttack(TTS tts)
        {
            int damageToPlayer = RandomNumberGenerator.NumberBetween(0, CurrentMonster.MaximumDamage);

            RaiseMessage(CheckMonsterGender(CurrentMonster.Name,0).ToUpper()+ " " + CurrentMonster.Name + " tirou-te " + damageToPlayer + " pontos de vida.");
            CurrentHitPoints -= damageToPlayer;

            if (IsDead)
            {
                text = CheckMonsterGender(CurrentMonster.Name, 0).ToUpper() + " " + CurrentMonster.Name + " tirou-te " + damageToPlayer + " pontos de vida." + "E por isso Morrêste!";
                RaiseMessage(CheckMonsterGender(CurrentMonster.Name,0).ToUpper()+" " + CurrentMonster.Name + " matou-te.");

                // Place PlayerPain sound here
                PlayAudio("PlayerPain", DisableAudio);
                tts.Speak(text);
                text = "";
                MoveHome(tts);
            }
            else
            {
                tts.Speak(text + CheckMonsterGender(CurrentMonster.Name, 0).ToUpper() + " " + CurrentMonster.Name + " tirou-te " + damageToPlayer + " pontos de vida.");
                text = "";
            }

        }

        private void HealPlayer(int hitPointsToHeal)
        {
            CurrentHitPoints = Math.Min(CurrentHitPoints + hitPointsToHeal, MaximumHitPoints);
        }

        private void CompletelyHeal()
        {
            CurrentHitPoints = MaximumHitPoints;
        }

        private void MoveHome(TTS tts)
        {
            MoveTo(World.LocationByID(World.LOCATION_ID_HOME), tts);
        }

        private void CreateNewChildXmlNode(XmlDocument document, XmlNode parentNode, string elementName, object value)
        {
            XmlNode node = document.CreateElement(elementName);
            node.AppendChild(document.CreateTextNode(value.ToString()));
            parentNode.AppendChild(node);
        }

        private void AddXmlAttributeToNode(XmlDocument document, XmlNode node, string attributeName, object value)
        {
            XmlAttribute attribute = document.CreateAttribute(attributeName);
            attribute.Value = value.ToString();
            node.Attributes.Append(attribute);
        }

        private void RaiseInventoryChangedEvent(Item item)
        {
            if (item is Weapon)
            {
                OnPropertyChanged(nameof(Weapons));
            }

            if (item is HealingPotion)
            {
                OnPropertyChanged(nameof(Potions));
            }
        }

        private void RaiseMessage(string message, bool addExtraNewLine = false)
        {
            if (OnMessage != null)
            {
                OnMessage(this, new MessageEventArgs(message, addExtraNewLine));
            }
        }

        private static void PlayAudio(string soundToPlay, bool disabled)
        {
            if (!disabled)
            {
                SoundPlayer audio = null;
                Stream s = null;
                try
                {
                    s = Engine.Properties.Media.ResourceManager.GetStream(soundToPlay);
                    audio = new SoundPlayer(s);
                    audio.Play();
                } finally {
                    if( s != null && audio != null)
                    {
                        s.Dispose();
                        audio.Dispose();
                    }

                }



            }
        }
       
    }
}