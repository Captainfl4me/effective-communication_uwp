using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Data.Json;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace effective_communication_uwp
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        private LeanComms serial;
        public Settings()
        {
            this.InitializeComponent();
            serial = new LeanComms(LeanComms.MbedDevice.F411RE);
            serial.NewSerialData += Serial_NewSerialData;
            updateFramesVisibility();
        }

        private void updateFramesVisibility()
        {
            if (FramePWM == null || FrameToggle == null || ModeSelect == null) return;

            switch(ModeSelect.SelectedIndex)
            {
                case 0:
                    FrameToggle.Visibility = Visibility.Visible;
                    FramePWM.Visibility = Visibility.Collapsed;
                    FrameBlink.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    FrameToggle.Visibility = Visibility.Collapsed;
                    FramePWM.Visibility = Visibility.Visible;
                    FrameBlink.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    FrameToggle.Visibility = Visibility.Collapsed;
                    FramePWM.Visibility = Visibility.Collapsed;
                    FrameBlink.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void Serial_NewSerialData(NewDataArgs e)
        {
            foreach (string s in e.NewData)
            {
                string trimmedStr = s.TrimEnd('\0');
                try
                {
                    JsonObject obj = JsonObject.Parse(trimmedStr);
                    if (obj.ContainsKey("err")) 
                    {
                        ErrText.Text = "Err: " + obj["err"].GetString();
                    }
                    else
                    {
                        ErrText.Text = "";
                    }
                } catch(Exception x)
                {
                    ErrText.Text = "ERR: " + x + " || Verify that mbed code is compile with RELEASE flag!";
                }
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {

            switch (ModeSelect.SelectedIndex)
            {
                case 0:
                    if (LedState.IsOn)
                        serial.WriteSerial("{\"mode\":0,\"on\":true}");
                    else
                        serial.WriteSerial("{\"mode\":0,\"on\":false}");
                    break;
                case 1:
                    serial.WriteSerial("{\"mode\":1,\"v\":" + (PWMSlider.Value / 100f).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "}");
                    break;
                case 2:
                    try
                    {
                        string cmd = "{\"mode\":2,\"d\":" + float.Parse(DelayInput.Text, System.Globalization.CultureInfo.InvariantCulture.NumberFormat).ToString("0.00", System.Globalization.CultureInfo.InvariantCulture) + "}";
                        serial.WriteSerial(cmd);
                    }
                    catch (Exception x)
                    {
                        ErrText.Text = "ERR: " + x;
                    }
                    break;
            }
        }

        private void ModeSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateFramesVisibility();
        }
    }
}
