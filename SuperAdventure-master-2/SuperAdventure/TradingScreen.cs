using System;
using System.Linq;
using System.Windows.Forms;
using Engine;

namespace SuperAdventure
{
    public partial class TradingScreen : Form
    {
        private Player _currentPlayer;

        public TradingScreen(Player player)
        {
            _currentPlayer = player;

            InitializeComponent();

            lblVendorGold.DataBindings.Add("Text", _currentPlayer.CurrentLocation.VendorWorkingHere, "VendorGold");

            // Style, to display numeric column values
            DataGridViewCellStyle rightAlignedCellStyle = new DataGridViewCellStyle();
            rightAlignedCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Populate the datagrid for the player's inventory
            dgvMyItems.RowHeadersVisible = false;
            dgvMyItems.AutoGenerateColumns = false;

            // This hidden column holds the item ID, so we know which item to sell
            dgvMyItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ItemID",
                Visible = false
            });

            dgvMyItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Qtd",
                Width = 30,
                DefaultCellStyle = rightAlignedCellStyle,
                DataPropertyName = "Quantity"
            });

            dgvMyItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Item",
                Width = 160,
                DataPropertyName = "Description"
            });

            dgvMyItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Preço",
                Width = 45,
                DefaultCellStyle = rightAlignedCellStyle,
                DataPropertyName = "Price"
            });

            dgvMyItems.Columns.Add(new DataGridViewButtonColumn
            {
                Text = "Vender 1",
                UseColumnTextForButtonValue = true,
                Width = 70,
                DataPropertyName = "ItemID"
            });

            // Bind the player's inventory to the datagridview 
            dgvMyItems.DataSource = _currentPlayer.Inventory;

            // When the user clicks on a row, call this function
            dgvMyItems.CellClick += dgvMyItems_CellClick;


            // Populate the datagrid for the vendor's inventory
            dgvVendorItems.RowHeadersVisible = false;
            dgvVendorItems.AutoGenerateColumns = false;

            // This hidden column holds the item ID, so we know which item to sell
            dgvVendorItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ItemID",
                Visible = false
            });

            dgvVendorItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Qtd",
                Width = 30,
                DefaultCellStyle = rightAlignedCellStyle,
                DataPropertyName = "Quantity"
            });

            dgvVendorItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Item",
                Width = 160,
                DataPropertyName = "Description"
            });

            dgvVendorItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Preço",
                Width = 45,
                DefaultCellStyle = rightAlignedCellStyle,
                DataPropertyName = "Price"
            });

            dgvVendorItems.Columns.Add(new DataGridViewButtonColumn
            {
                Text = "Comprar 1",
                UseColumnTextForButtonValue = true,
                Width = 80,
                DataPropertyName = "ItemID"
            });

            // Bind the vendor's inventory to the datagridview 
            dgvVendorItems.DataSource = _currentPlayer.CurrentLocation.VendorWorkingHere.Inventory;

            // When the user clicks on a row, call this function
            dgvVendorItems.CellClick += dgvVendorItems_CellClick;
        }

        private void dgvMyItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // The first column of a datagridview has a ColumnIndex = 0
            // This is known as a "zero-based" array/collection/list.
            // You start counting with 0.
            //
            // The 5th column (ColumnIndex = 4) is the column with the button.
            // So, if the player clicked the button column, we will sell an item from that row.
            if (e.ColumnIndex == 4)
            {
                // This gets the ID value of the item, from the hidden 1st column
                // Remember, ColumnIndex = 0, for the first column
                var itemID = dgvMyItems.Rows[e.RowIndex].Cells[0].Value;

                // Get the Item object for the selected item row
                Item itemBeingSold = World.ItemByID(Convert.ToInt32(itemID));

                // Check if the vendor wants an item in player's inventory
                if (!itemBeingSold.VendorWants)
                {
                    MessageBox.Show("Vendor doesn't want " + itemBeingSold.Name);

                }
                // May get rid of this since there is a VendorWants property
                // Check if item is unsellable
                else if (itemBeingSold.Price == World.UNSELLABLE_ITEM_PRICE)
                {
                    MessageBox.Show("You cannot sell the " + itemBeingSold.Name);

                }
                // Check if Vendor has enough gold when buying a player's item
                else if (_currentPlayer.CurrentLocation.VendorWorkingHere.VendorGold >= itemBeingSold.Price)
                {
                    // Remove one of these items from the player's inventory and add to Vendor's inventory
                    _currentPlayer.RemoveItemFromInventory(itemBeingSold);
                    _currentPlayer.CurrentLocation.VendorWorkingHere.AddItemToInventory(itemBeingSold);


                    // Give the player the gold for the item being sold and remove gold from vendor
                    _currentPlayer.Gold += itemBeingSold.Price;
                    _currentPlayer.CurrentLocation.VendorWorkingHere.VendorGold -= itemBeingSold.Price;

                }
                else
                {
                    MessageBox.Show("Vendor does not have enough gold to buy " + itemBeingSold.Name);

                }
            }
        }

        public void VoiceBuy(int itemID, TTS tts, string text = "")
        {
            Item itemBeingBought = World.ItemByID(Convert.ToInt32(itemID));

            InventoryItem inv_item = _currentPlayer.CurrentLocation.VendorWorkingHere.Inventory.SingleOrDefault(ii => ii.Details.ID == itemBeingBought.ID);
            if (inv_item != null)
            {
                if ((itemBeingBought.CanOnlyHaveOne) && (_currentPlayer.Inventory.SingleOrDefault(ii => ii.Details.ID == itemBeingBought.ID)) != null)
                {
                    tts.Speak("Só podes ter um único item: " + itemBeingBought.Name);
                }
                // Check if the player has enough gold to buy the item
                else if (_currentPlayer.Gold >= itemBeingBought.Price)
                {
                    // Add one of the items to the player's inventory and remove from vendor's inventory
                    _currentPlayer.AddItemToInventory(itemBeingBought);
                    _currentPlayer.CurrentLocation.VendorWorkingHere.RemoveItemFromInventory(itemBeingBought);

                    // Remove the gold to pay for the item and add to vendor's gold
                    _currentPlayer.Gold -= itemBeingBought.Price;
                    _currentPlayer.CurrentLocation.VendorWorkingHere.VendorGold += itemBeingBought.Price;
                    tts.Speak(text + " Efectuáste a compra com sucesso.");
                }
                else
                {
                    tts.Speak("Não tens ouro suficiente para comprar isso.");
                }
            }
            else
            {
                tts.Speak("O vendedor não tem esse item!");
            }

        }

        public void VoiceSell(int itemID, TTS tts, string text = "")
        {
            // The first column of a datagridview has a ColumnIndex = 0
            // This is known as a "zero-based" array/collection/list.
            // You start counting with 0.
            //
            // The 5th column (ColumnIndex = 4) is the column with the button.
            // So, if the player clicked the button column, we will sell an item from that row.
            // This gets the ID value of the item, from the hidden 1st column
            // Remember, ColumnIndex = 0, for the first column

            // Get the Item object for the selected item row
            Item itemBeingSold = World.ItemByID(Convert.ToInt32(itemID));
            InventoryItem inv_item = _currentPlayer.Inventory.SingleOrDefault(ii => ii.Details.ID == itemBeingSold.ID);
            if (inv_item != null)
            {
                // Check if the vendor wants an item in player's inventory
                if (!itemBeingSold.VendorWants)
                {
                    tts.Speak("O senhor vendedor não tem interesse nesse item.");
                }
                // May get rid of this since there is a VendorWants property
                // Check if item is unsellable
                else if (itemBeingSold.Price == World.UNSELLABLE_ITEM_PRICE)
                {
                    tts.Speak("Esse item não pode ser vendido.");
                }
                // Check if Vendor has enough gold when buying a player's item
                else if (_currentPlayer.CurrentLocation.VendorWorkingHere.VendorGold >= itemBeingSold.Price)
                {
                    // Remove one of these items from the player's inventory and add to Vendor's inventory
                    _currentPlayer.RemoveItemFromInventory(itemBeingSold);
                    _currentPlayer.CurrentLocation.VendorWorkingHere.AddItemToInventory(itemBeingSold);


                    // Give the player the gold for the item being sold and remove gold from vendor
                    _currentPlayer.Gold += itemBeingSold.Price;
                    _currentPlayer.CurrentLocation.VendorWorkingHere.VendorGold -= itemBeingSold.Price;
                    tts.Speak(text + " Efectuáste a venda com sucesso.");
                }
                else
                {
                    tts.Speak("O vendedor não tem ouro suficiente para te comprar esse item.");
                }
            }
            else
            {
                tts.Speak("Não tens esse item.");
            }

        }

        private void dgvVendorItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // The 5th column (ColumnIndex = 4) has the "Buy 1" button.
            if (e.ColumnIndex == 4)
            {
                // This gets the ID value of the item, from the hidden 1st column
                var itemID = dgvVendorItems.Rows[e.RowIndex].Cells[0].Value;

                // Get the Item object for the selected item row
                Item itemBeingBought = World.ItemByID(Convert.ToInt32(itemID));

                // Check if item is one that the player can only have one and if it is in the player inventory
                if ((itemBeingBought.CanOnlyHaveOne) && (_currentPlayer.Inventory.SingleOrDefault(ii => ii.Details.ID == itemBeingBought.ID)) != null)
                {
                    MessageBox.Show("You can only have one " + itemBeingBought.Name);
                }
                // Check if the player has enough gold to buy the item
                else if (_currentPlayer.Gold >= itemBeingBought.Price)
                {
                    // Add one of the items to the player's inventory and remove from vendor's inventory
                    _currentPlayer.AddItemToInventory(itemBeingBought);
                    _currentPlayer.CurrentLocation.VendorWorkingHere.RemoveItemFromInventory(itemBeingBought);

                    // Remove the gold to pay for the item and add to vendor's gold
                    _currentPlayer.Gold -= itemBeingBought.Price;
                    _currentPlayer.CurrentLocation.VendorWorkingHere.VendorGold += itemBeingBought.Price;
                }
                else
                {
                    MessageBox.Show("You do not have enough gold to buy the " + itemBeingBought.Name);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
