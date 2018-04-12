﻿using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using mmisharp;
using Engine;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Windows;
using System.Windows.Threading;
using System.Collections.Generic;

namespace SuperAdventure
{
    public partial class SuperAdventure : Form
    {
        private const string PLAYER_DATA_FILE_NAME = "PlayerData.xml";
        private MmiCommunication mmiC;
        Dispatcher dispUI = Dispatcher.CurrentDispatcher;

        private Player _player;

        public SuperAdventure()
        {
            InitializeComponent();
            mmiC = new MmiCommunication("localhost", 8000, "User1", "GUI");
            mmiC.Message += MmiC_Message;
            mmiC.Start();

            _player = PlayerDataMapper.CreateFromDatabase();

            if (_player == null)
            {
                if (File.Exists(PLAYER_DATA_FILE_NAME))
                {
                    _player = Player.CreatePlayerFromXmlString(File.ReadAllText(PLAYER_DATA_FILE_NAME));
                }
                else
                {
                    _player = Player.CreateDefaultPlayer();
                }
            }

            lblHitPoints.DataBindings.Add("Text", _player, "CurrentHitPoints");
            lblGold.DataBindings.Add("Text", _player, "Gold");
            lblExperience.DataBindings.Add("Text", _player, "ExperiencePoints");
            lblLevel.DataBindings.Add("Text", _player, "Level");

            dgvInventory.RowHeadersVisible = false;
            dgvInventory.AutoGenerateColumns = false;

            dgvInventory.DataSource = _player.Inventory;

            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Name",
                Width = 197,
                DataPropertyName = "Description"
            });

            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Quantity",
                DataPropertyName = "Quantity"
            });

            dgvQuests.RowHeadersVisible = false;
            dgvQuests.AutoGenerateColumns = false;

            dgvQuests.DataSource = _player.Quests;

            dgvQuests.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Name",
                Width = 197,
                DataPropertyName = "Name"
            });

            dgvQuests.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Done?",
                DataPropertyName = "IsCompleted"
            });

            cboWeapons.DataSource = _player.Weapons;
            cboWeapons.DisplayMember = "Name";
            cboWeapons.ValueMember = "Id";

            if (_player.CurrentWeapon != null)
            {
                cboWeapons.SelectedItem = _player.CurrentWeapon;
            }

            cboWeapons.SelectedIndexChanged += cboWeapons_SelectedIndexChanged;

            cboPotions.DataSource = _player.Potions;
            cboPotions.DisplayMember = "Name";
            cboPotions.ValueMember = "Id";

            _player.PropertyChanged += PlayerOnPropertyChanged;
            _player.OnMessage += DisplayMessage;

            _player.MoveTo(_player.CurrentLocation);
        }

        private int JsonArray_Length(dynamic json_array)
        {
            var array = json_array.recognized;
            int x = ((IEnumerable<dynamic>)array).Cast<dynamic>().Count();
            Console.WriteLine("tamanho: "+ x.ToString());
            return x;

        }
        private void MmiC_Message(object sender, MmiEventArgs e)
        {
            
            Console.WriteLine(e.Message);
            var doc = XDocument.Parse(e.Message);
            var com = doc.Descendants("command").FirstOrDefault().Value;
            dynamic json = JsonConvert.DeserializeObject(com);

            switch ((string)json.recognized[0].ToString())
            {
                case "MUTE":
                    this.Invoke((MethodInvoker)delegate
                    {
                        if(chkbxSndDisable.Checked == true)
                        {
                            //som está ativo burro TTS
                        }
                        else
                        {
                            chkbxSndDisable.Checked = true;
                            this._player.DisableAudio = true;
                        }

                    });
                    break;
                case "UNMUTE":
                    this.Invoke((MethodInvoker)delegate
                    {
                        if(chkbxSndDisable.Checked == false)
                        {
                            //som está desativo burro TTS
                        }
                        else
                        {
                            chkbxSndDisable.Checked = false;
                            this._player.DisableAudio = false;
                        }

                    });
                    break;
                case "ATACAR":
                    this.Invoke((MethodInvoker)delegate
                    {
                        if(_player.CurrentLocation.HasAMonster)
                        {
                            if (JsonArray_Length(json) == 1 || _player.CurrentMonster.Name.ToLower() == (string)json.recognized[1].ToString().ToLower())
                            {
                                btnUseWeapon_Click(null, null);
                            }
                            else
                            {
                                //MONSTRO ERRADO! TTS
                            }
                        }
                        else
                        {
                            //NÃO HÁ MONSTRO AQUI! TTS
                        }
                    });
                    break;
                case "ABRIR":
                    this.Invoke((MethodInvoker)delegate
                    {
                        if (JsonArray_Length(json) == 2)
                        {
                            if ((string)json.recognized[1].ToString().ToLower() == "mapa")
                            {
                                btnMap_Click(null, null);
                            }
                            else if ( _player.CurrentLocation.HasAVendor && (string)json.recognized[1].ToString().ToLower() == "vendedor")
                            {
                                btnTrade_Click(null, null);
                            }
                            else
                            {
                                //NÃO É POSSÍVEL FAZER ISSO AGORA
                            }
                        }
                        else
                        {
                            //ABRIR O QUÊ ?? Não percebi.
                        }
                    });
                    break;
                case "MOVER":
                    this.Invoke((MethodInvoker)delegate
                    {
                        if (JsonArray_Length(json) == 2)
                        {
                            if ((string)json.recognized[1].ToString().ToLower() == "cima")
                            {
                                btnNorth_Click(null, null);
                            }
                            else if ((string)json.recognized[1].ToString().ToLower() == "baixo")
                            {
                                btnSouth_Click(null, null);
                            }
                            else if ((string)json.recognized[1].ToString().ToLower() == "direita")
                            {
                                btnEast_Click(null, null);
                            }
                            else if ((string)json.recognized[1].ToString().ToLower() == "esquerda")
                            {
                                btnWest_Click(null, null);
                            }
                            else
                            {
                                //MOVER PARA ONDE NÃO PERCEBI
                            }
                        }
                        else
                        {
                            //MOVER PARA ONDE? NÃO PERCEBI
                        }
                    });
                    break;
            }
           
    /*
            dispUI.Invoke(() =>
            {
                switch ((string)json.recognized[1].ToString())
                {

                }
            });
          */  
        }


        private void DisplayMessage(object sender, MessageEventArgs messageEventArgs)
        {
            rtbMessages.Text += messageEventArgs.Message + Environment.NewLine;

            if (messageEventArgs.AddExtraNewLine)
            {
                rtbMessages.Text += Environment.NewLine;
            }

            rtbMessages.SelectionStart = rtbMessages.Text.Length;
        }

        private void PlayerOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "Weapons")
            {
                Weapon previouslySelectedWeapon = _player.CurrentWeapon;

                cboWeapons.DataSource = _player.Weapons;

                if (previouslySelectedWeapon != null &&
                    _player.Weapons.Exists(w => w.ID == previouslySelectedWeapon.ID))
                {
                    cboWeapons.SelectedItem = previouslySelectedWeapon;
                }

                if (!_player.Weapons.Any())
                {
                    cboWeapons.Visible = false;
                    btnUseWeapon.Visible = false;
                }
            }

            if (propertyChangedEventArgs.PropertyName == "Potions")
            {
                cboPotions.DataSource = _player.Potions;

                if (!_player.Potions.Any())
                {
                    cboPotions.Visible = false;
                    btnUsePotion.Visible = false;
                }
            }

            if (propertyChangedEventArgs.PropertyName == "CurrentLocation")
            {
                // Show/hide available movement buttons
                btnNorth.Visible = (_player.CurrentLocation.LocationToNorth != null);
                btnEast.Visible = (_player.CurrentLocation.LocationToEast != null);
                btnSouth.Visible = (_player.CurrentLocation.LocationToSouth != null);
                btnWest.Visible = (_player.CurrentLocation.LocationToWest != null);

                // Show/hide trade button
                btnTrade.Visible = (_player.CurrentLocation.VendorWorkingHere != null);

                // Display current location name and description
                rtbLocation.Text = _player.CurrentLocation.Name + Environment.NewLine;
                rtbLocation.Text += _player.CurrentLocation.Description + Environment.NewLine;

                if (!_player.CurrentLocation.HasAMonster)
                {
                    cboWeapons.Visible = false;
                    cboPotions.Visible = false;
                    btnUseWeapon.Visible = false;
                    btnUsePotion.Visible = false;
                }
                else
                {
                    cboWeapons.Visible = _player.Weapons.Any();
                    cboPotions.Visible = _player.Potions.Any();
                    btnUseWeapon.Visible = _player.Weapons.Any();
                    btnUsePotion.Visible = _player.Potions.Any();
                }
            }
        }

        private void btnNorth_Click(object sender, EventArgs e)
        {
            _player.MoveNorth();
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            _player.MoveSouth();
        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            _player.MoveEast();
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            _player.MoveWest();
        }

        private void btnUseWeapon_Click(object sender, EventArgs e)
        {
            // Get the currently selected weapon from the cboWeapons ComboBox
            Weapon currentWeapon = (Weapon)cboWeapons.SelectedItem;

            _player.UseWeapon(currentWeapon);
        }

        private void btnUsePotion_Click(object sender, EventArgs e)
        {
            // Get the currently selected potion from the combobox
            HealingPotion potion = (HealingPotion)cboPotions.SelectedItem;

            _player.UsePotion(potion);
        }

        // On text change of Messages box, show new message and scroll to bottom.
        private void rtbMessages_TextChanged(object sender, EventArgs e)
        {
            rtbMessages.SelectionStart = rtbMessages.Text.Length;
            rtbMessages.ScrollToCaret();
        }

        private void SuperAdventure_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText(PLAYER_DATA_FILE_NAME, _player.ToXmlString());

            PlayerDataMapper.SaveToDatabase(_player);
        }

        private void cboWeapons_SelectedIndexChanged(object sender, EventArgs e)
        {
            _player.CurrentWeapon = (Weapon)cboWeapons.SelectedItem;
        }

        private void btnTrade_Click(object sender, EventArgs e)
        {
            TradingScreen tradingScreen = new TradingScreen(_player);
            tradingScreen.StartPosition = FormStartPosition.CenterParent;
            tradingScreen.ShowDialog(this);
        }

        private void btnClearRtbMessages_Click(object sender, EventArgs e)
        {
            rtbMessages.Clear();
        }

        private void chkbxSndDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxSndDisable.Checked)
            {
                _player.DisableAudio = true;
            }
            else
            {
                _player.DisableAudio = false;
            }
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            WorldMap mapScreen = new WorldMap(_player);
            mapScreen.StartPosition = FormStartPosition.CenterParent;
            mapScreen.ShowDialog(this);
        }
    }
}