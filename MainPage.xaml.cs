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
using Windows.UI.ViewManagement;


// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace effective_communication_uwp
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Type defaultPage = typeof(CheckHealth);

        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.Maximized;
            Fr_MainFrame.Navigate(this.defaultPage);

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
