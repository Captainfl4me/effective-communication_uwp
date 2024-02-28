using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Data.Json;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace effective_communication_uwp
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class CheckHealth : Page
    {
        public CheckHealth()
        {
            this.InitializeComponent();
            LeanComms.current_instance.clearNewSerialDataEvent();
            LeanComms.current_instance.NewSerialData += Serial_NewSerialData;
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
                    if (obj.ContainsKey("status"))
                    {
                        var status = obj["status"].GetObject();
                        CurrentModeBadge.Text = status["mode"].GetNumber().ToString();
                        CurrentLedValueText.Text = status["led"].GetNumber().ToString();

                        switch (status["mode"].GetNumber())
                        {
                            case 0:
                                CurrentModeText.Text = "Toggle";
                                break;
                            case 1:
                                CurrentModeText.Text = "PWM";
                                break;
                            case 2:
                                CurrentModeText.Text = "Blink";
                                break;
                        }
                    }
                } catch(Exception x)
                {
                    ErrText.Text = "ERR: " + x + " || Verify that mbed code is compile with RELEASE flag!";
                }
            }
        }

        private void refreshInfo()
        {
            LeanComms.current_instance.WriteSerial("{\"req\":0}");
        }

        private void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            refreshInfo();
        }
    }
}
