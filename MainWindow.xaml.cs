using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Diagnostics;

namespace EQApoUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool eqOnOff;
        bool init = true;
        public void buildWriteConfig()
        {
            string[] eqBuilder = new string[13];
            eqBuilder[0] = "Preamp: " + sPreamp.Value.ToString("N1") + " dB";
            eqBuilder[1] = "Equaliser: Generic";
            eqBuilder[2] = "No measurement";
            eqBuilder[3] = "Filter 1: ON PK Fc 32.0 Hz Gain " + s32hz.Value.ToString("N1") + " dB Q 1.00";
            eqBuilder[4] = "Filter 2: ON PK Fc 64.0 Hz Gain " + s64hz.Value.ToString("N1") + " dB Q 1.00";
            eqBuilder[5] = "Filter 3: ON PK Fc 125.0 Hz Gain " + s125hz.Value.ToString("N1") + " dB Q 1.00";
            eqBuilder[6] = "Filter 3: ON PK Fc 250.0 Hz Gain " + s250hz.Value.ToString("N1") + " dB Q 1.00";
            eqBuilder[7] = "Filter 3: ON PK Fc 500.0 Hz Gain " + s500hz.Value.ToString("N1") + " dB Q 1.00";
            eqBuilder[8] = "Filter 3: ON PK Fc 1000.0 Hz Gain " + s1khz.Value.ToString("N1") + " dB Q 1.00";
            eqBuilder[9] = "Filter 3: ON PK Fc 2000.0 Hz Gain " + s2khz.Value.ToString("N1") + " dB Q 1.00";
            eqBuilder[10] = "Filter 3: ON PK Fc 4000.0 Hz Gain " + s4khz.Value.ToString("N1") + " dB Q 1.00";
            eqBuilder[11] = "Filter 3: ON PK Fc 8000.0 Hz Gain " + s8khz.Value.ToString("N1") + " dB Q 1.00";
            eqBuilder[12] = "Filter 3: ON PK Fc 16000.0 Hz Gain " + s16khz.Value.ToString("N1") + " dB Q 1.00";
            if (eqOnOff == true)
            {
                File.WriteAllLines("config.txt", eqBuilder, Encoding.ASCII);
            }
            else
            {
                for (int i = 0; i < 13; i++)
                {
                    eqBuilder[i] = "#" + eqBuilder[i];
                }
                File.WriteAllLines("config.txt", eqBuilder, Encoding.ASCII);
            }
        }
        public void readSetSliders(string[] eqLines)
        {
            if (eqLines[0].Substring(0, 1) == "#")
            {
                eqOnOff = false;
                power.Text = "Off";
            }
            else
            {
                eqOnOff = true;
                power.Text = "On";
            }
            int position = 0;
            // preamp
            position = eqLines[0].IndexOf(":", 0);         
            sPreamp.Value = Double.Parse(eqLines[0].Substring(position + 2, 4));
            // 32hz
            position = eqLines[3].IndexOf("Gain ", 0);            
            s32hz.Value = Double.Parse(eqLines[3].Substring(position + 5, 4));
            // 64hz
            position = eqLines[4].IndexOf("Gain ", 0);          
            s64hz.Value = Double.Parse(eqLines[4].Substring(position + 5, 4));
            // 125hz
            position = eqLines[5].IndexOf("Gain ", 0);            
            s125hz.Value = Double.Parse(eqLines[5].Substring(position + 5, 4));
            // 250hz
            position = eqLines[6].IndexOf("Gain ", 0);            
            s250hz.Value = Double.Parse(eqLines[6].Substring(position + 5, 4));
            // 500hz
            position = eqLines[7].IndexOf("Gain ", 0);            
            s500hz.Value = Double.Parse(eqLines[7].Substring(position + 5, 4));
            // 1000hz
            position = eqLines[8].IndexOf("Gain ", 0);            
            s1khz.Value = Double.Parse(eqLines[8].Substring(position + 5, 4));
            // 2000hz
            position = eqLines[9].IndexOf("Gain ", 0);            
            s2khz.Value = Double.Parse(eqLines[9].Substring(position + 5, 4));
            // 4000hz
            position = eqLines[10].IndexOf("Gain ", 0);            
            s4khz.Value = Double.Parse(eqLines[10].Substring(position + 5, 4));
            // 8000hz
            position = eqLines[11].IndexOf("Gain ", 0);            
            s8khz.Value = Double.Parse(eqLines[11].Substring(position + 5, 4));
            // 16000hz
            position = eqLines[12].IndexOf("Gain ", 0);            
            s16khz.Value = Double.Parse(eqLines[12].Substring(position + 5, 4));
        }
        public MainWindow()
        {
            InitializeComponent();
            string[] eqLines = File.ReadAllLines(@"config.txt");
            readSetSliders(eqLines);
            init = false;
        }

        private void sliderChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (init == false)
            {
                buildWriteConfig();
            }
        }

        private void buttonOn_Click(object sender, RoutedEventArgs e)
        {
            eqOnOff = true;
            buildWriteConfig();
            power.Text = "On";
        }

        private void buttonOff_Click(object sender, RoutedEventArgs e)
        {
            eqOnOff = false;
            buildWriteConfig();
            power.Text = "Off";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
