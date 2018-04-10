using System.ComponentModel;
using System.Linq;

namespace Engine
{
    public class Vendor : INotifyPropertyChanged
    {
        private int _gold;
        public string Name { get; set; }
        public BindingList<InventoryItem> Inventory { get; set; }

        public int VendorGold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged(nameof(VendorGold));
            }
        }

        public Vendor(string name, int gold)
        {
            Name = name;
            VendorGold = gold;
            Inventory = new BindingList<InventoryItem>();
        }

        public void AddItemToInventory(Item itemToAdd, int quantity = 1)
        {
            InventoryItem item = Inventory.SingleOrDefault(ii => ii.Details.ID == itemToAdd.ID);

            if (item == null)
            {
                // They didn't have the item, so add it to their inventory
                Inventory.Add(new InventoryItem(itemToAdd, quantity));
            }
            else
            {
                // They have the item in their inventory, so increase the quantity
                item.Quantity += quantity;
            }

            OnPropertyChanged(nameof(Inventory));
            OnPropertyChanged(nameof(VendorGold));
        }

        public void RemoveItemFromInventory(Item itemToRemove, int quantity = 1)
        {
            InventoryItem item = Inventory.SingleOrDefault(ii => ii.Details.ID == itemToRemove.ID);

            if (item == null)
            {
                // The item is not in the player's inventory, so ignore it.
                // We might want to raise an error for this situation
            }
            else
            {
                // They have the item in their inventory, so decrease the quantity
                item.Quantity -= quantity;

                // Don't allow negative quantities.
                // We might want to raise an error for this situation
                if (item.Quantity < 0)
                {
                    item.Quantity = 0;
                }

                // If the quantity is zero, remove the item from the list
                if (item.Quantity == 0)
                {
                    Inventory.Remove(item);
                }

                // Notify the UI that the inventory has changed
                OnPropertyChanged(nameof(Inventory));
                OnPropertyChanged(nameof(VendorGold));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}