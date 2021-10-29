using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Navigation;
using System.Configuration;
using Logs;
using System.Reflection;

namespace Pacman.Views
{
    /// <summary>
    /// Interaction logic for InfoView.xaml
    /// </summary>
    public partial class InfoView : Window
    {
        public InfoView()
        {
            InitializeComponent();
        }

        public InfoView(string name) : this()
        {
            try
            {
                IncomingString.Text = name;
                GameParam.Text = GetGameParam();
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Error", ex));
            }
        }

        private string GetGameParam()
        {
            string tmpString = $"Map Blocks = {ConfigurationManager.AppSettings["Map_Blocks"]} %\n";
            tmpString += $"Map Size = {ConfigurationManager.AppSettings["Map_Size"]} \n";
            if (bool.Parse(ConfigurationManager.AppSettings["IsAdvancedGhostOptionsEnable"]))
            {
                tmpString += $"ADVANCED GHOST PARAMS \n";
                tmpString += $"Green Ghost count = {ConfigurationManager.AppSettings["GreenGhostCount"]} \n";
                tmpString += $"Green Ghost speed = {ConfigurationManager.AppSettings["GreenGhostSpeed"]} \n";

                tmpString += $"Blue Ghost count = {ConfigurationManager.AppSettings["BlueGhostCount"]} \n";
                tmpString += $"Blue Ghost speed = {ConfigurationManager.AppSettings["BlueGhostSpeed"]} \n";

                tmpString += $"Red Ghost count = {ConfigurationManager.AppSettings["RedGhostCount"]} \n";
                tmpString += $"Red Ghost speed = {ConfigurationManager.AppSettings["RedGhostSpeed"]} ";

            }
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, " Info from AppConfig was loaded"));
            return tmpString;
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, " Info -> Going to GIT HUB"));
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void OK(object sender, RoutedEventArgs e)
        {
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Closing Info"));
            DialogResult = true;
        }
    }
}
