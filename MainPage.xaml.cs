using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media;


// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace effective_communication_uwp
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Type defaultPage = typeof(CheckHealth);
        private LeanComms serial;

        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            serial = new LeanComms(LeanComms.MbedDevice.F411RE);
            serial.ConnectStateChanged += ConenctStateChanged;
            Fr_MainFrame.Navigate(this.defaultPage);
        }

        private void ConenctStateChanged()
        {
            if (serial.IsConnected())
            {
                ConnectState.Text = "Connected";
                ConnectState.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                ConnectStateGrid.Background = new SolidColorBrush(Windows.UI.Colors.Green);
            }
            else
            {
                ConnectState.Text = "Disconnected";
                ConnectStateGrid.Background = new SolidColorBrush(Windows.UI.Colors.Gray);
            }
        }

        private void requestNavigation(Type page)
        {
            if (Fr_MainFrame.CurrentSourcePageType != page)
                Fr_MainFrame.Navigate(page);
        }

        private void checkHealthButton_Click(object sender, RoutedEventArgs e)
        {
            requestNavigation(typeof(CheckHealth));
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {

            requestNavigation(typeof(Settings));
        }

        private void expandButton_Click(object sender, RoutedEventArgs e)
        {
            MainView.IsPaneOpen = !MainView.IsPaneOpen;
        }
    }
}
