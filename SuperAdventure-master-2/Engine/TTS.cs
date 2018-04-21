using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Synthesis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class TTS
    {
        SpeechSynthesizer tts = null;
        static SoundPlayer player = new SoundPlayer();
        public TTS()
        {

            //create speech synthesizer
            tts = new SpeechSynthesizer();

            // Initialize a new instance of the SpeechSynthesizer.
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {

                //set voice
                tts.SelectVoiceByHints(VoiceGender.Male, VoiceAge.NotSet, 0, new System.Globalization.CultureInfo("pt-PT"));

               


                //set function to play audio after synthesis is complete
                tts.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(tts_SpeakCompleted);
                
            }
        }
        public void Speak(string text)
        {
            while (player.Stream != null)
            {
                Console.WriteLine("Waiting...");
            }
            if(text!="")
            {
                player.Stream = new System.IO.MemoryStream();
                tts.SetOutputToWaveStream(player.Stream);
                tts.SpeakAsync(text);
            }

        }


        void tts_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            if (player.Stream != null)
            {
                player.Stream.Position = 0;
                player.PlaySync();
                player.Stream = null;
            }
        }
    }
}
