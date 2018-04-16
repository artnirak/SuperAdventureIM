using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Engine;
using mmisharp;
using System;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

namespace SuperAdventure
{
    public partial class WorldMap : Form
    {
        readonly Assembly _thisAssembly = Assembly.GetExecutingAssembly();
        private Player _player;
        //private MmiCommunication mmiC;

        public WorldMap(Player player)
        {
            InitializeComponent();
            /*
            mmiC = new MmiCommunication("localhost", 8001, "User2", "GUI2");
            mmiC.Message += MmiC_Message;
            Console.WriteLine("faço isto quantas vezes? ????");
           */
            this._player = player;
            /* string[] resources = _thisAssembly.GetManifestResourceNames();
             * string toDisplay = string.Join(System.Environment.NewLine, resources);
             * MessageBox.Show(toDisplay);
             */

            SetImage(pic_0_0, player.LocationsVisited.Contains(0) ? "" : "FogLocation");
            SetImage(pic_0_1, player.LocationsVisited.Contains(1) ? "" : "FogLocation");
            SetImage(pic_0_2, player.LocationsVisited.Contains(2) ? "HerbalistsGarden" : "FogLocation");
            SetImage(pic_0_3, player.LocationsVisited.Contains(3) ? "" : "FogLocation");
            SetImage(pic_0_4, player.LocationsVisited.Contains(4) ? "" : "FogLocation");
            SetImage(pic_0_5, player.LocationsVisited.Contains(5) ? "" : "FogLocation");
            SetImage(pic_1_0, player.LocationsVisited.Contains(6) ? "" : "FogLocation");
            SetImage(pic_1_1, player.LocationsVisited.Contains(7) ? "" : "FogLocation");
            SetImage(pic_1_2, player.LocationsVisited.Contains(8) ? "HerbalistsHut" : "FogLocation");
            SetImage(pic_1_3, player.LocationsVisited.Contains(9) ? "" : "FogLocation");
            SetImage(pic_1_4, player.LocationsVisited.Contains(10) ? "" : "FogLocation");
            SetImage(pic_1_5, player.LocationsVisited.Contains(11) ? "" : "FogLocation");
            SetImage(pic_2_0, player.LocationsVisited.Contains(12) ? "FarmFields" : "FogLocation");
            SetImage(pic_2_1, player.LocationsVisited.Contains(13) ? "Farmhouse" : "FogLocation");
            SetImage(pic_2_2, player.LocationsVisited.Contains(14) ? "TownSquare" : "FogLocation");
            SetImage(pic_2_3, player.LocationsVisited.Contains(15) ? "TownGate" : "FogLocation");
            SetImage(pic_2_4, player.LocationsVisited.Contains(16) ? "Bridge" : "FogLocation");
            SetImage(pic_2_5, player.LocationsVisited.Contains(17) ? "SpiderForest" : "FogLocation");
            SetImage(pic_3_0, player.LocationsVisited.Contains(18) ? "" : "FogLocation");
            SetImage(pic_3_1, player.LocationsVisited.Contains(19) ? "" : "FogLocation");
            SetImage(pic_3_2, player.LocationsVisited.Contains(20) ? "Home" : "FogLocation");
            SetImage(pic_3_3, player.LocationsVisited.Contains(21) ? "" : "FogLocation");
            SetImage(pic_3_4, player.LocationsVisited.Contains(22) ? "" : "FogLocation");
            SetImage(pic_3_5, player.LocationsVisited.Contains(23) ? "" : "FogLocation");
        }
        /*
        private void StartComms(MmiCommunication mmiC)
        {
            Console.WriteLine("Estou aqui. e está a correr ? ----> " + mmiC.IsRunning.ToString());
            mmiC.Start();
            Console.WriteLine(mmiC.Uid);
            Console.WriteLine("Estou aqui. e está a correr ? ----> " + mmiC.IsRunning.ToString());
        }


        private int JsonArray_Length(dynamic json_array)
        {
            var array = json_array.recognized;
            int x = ((IEnumerable<dynamic>)array).Cast<dynamic>().Count();
            Console.WriteLine("tamanho: " + x.ToString());
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
 
                case "FECHAR":
                    Invoke((MethodInvoker)delegate
                    {
                        if (JsonArray_Length(json) == 2)
                        {
                            if ((string)json.recognized[1].ToString().ToLower() == "mapa")
                            {
                                mmiC.Stop();
                                this.Close();
                            }
                            else
                            {
                                //TTS
                            }
                        }
                        else
                        {
                            //TTS
                        }
                    });
                    break;
                    
            }

        }*/

        private void SetImage(PictureBox pictureBox, string imageName)
    {
        using (Stream resourceStream =
        _thisAssembly.GetManifestResourceStream(
             //_thisAssembly.GetName().Name + ".Resources." + imageName + ".png"))
             "SuperAdventure.Resources." + imageName + ".png"))
        {
            if (resourceStream != null)
            {
                pictureBox.Image = new Bitmap(resourceStream);
            }
        }
    }

        private void WorldMap_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void WorldMap_Load(object sender, EventArgs e)
        {
            //StartComms(mmiC);
        }
    }
}
