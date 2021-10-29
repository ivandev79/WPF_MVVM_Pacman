using Logs;
using Pacman.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pacman.View
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : Window
    {
        public string PlayerName { get; private set; }
        #region Window const and pointers
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        #endregion
        /// <summary>
        /// Simple View with out MVVM pattern
        /// </summary>
        public MenuView()
        {
            InitializeComponent();
            PlayerName = ConfigurationManager.AppSettings["PlayerName"];
            if (PlayerName == null || PlayerName == "Unknown")
            {
                ChangePlayerName();
            }
            if (ConfigurationManager.AppSettings["AI_behavior"] == null)
            {
                OptionsWindow.SetSetting("AI_behavior", "default");
            }
            LoggerConfiguration();
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Menu open"));
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Menu -> Start Game"));
            MainWindow game = new MainWindow(PlayerName);
            game.Visibility = Visibility.Visible;
            this.Close();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Menu -> Exit Application"));
            KILL();
        }

        //Kill process, fast exit
        private void KILL()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
        //But not windows hotkey
        private void HideWindowControl(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Menu -> Options"));
            OptionsWindow view = new OptionsWindow();
            this.Visibility = Visibility.Hidden;
            if (bool.Parse(view.ShowDialog().ToString()))
            {
                this.Visibility = Visibility.Visible;
                PlayerName = ConfigurationManager.AppSettings["PlayerName"];
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Options was changed"));
            }
            else
            {
                KILL();
            }
        }

        private void ShowRecordsClick(object sender, RoutedEventArgs e)
        {
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Menu -> Records"));
            this.Visibility = Visibility.Hidden;
            new RecordsWindow().ShowDialog();
            this.Visibility = Visibility.Visible;
        }

        private void ChangePlayerName()
        {
            PlayerNameWindow player = new PlayerNameWindow();
            if (bool.Parse(player.ShowDialog().ToString()))
            {
                PlayerName = player.PlayerName;
                Task.Factory.StartNew(
              () =>
              {
                  string playerName = ConfigurationManager.AppSettings["PlayerName"];
                  if (PlayerName == null || playerName == "Unknown" && player.PlayerName != "Unknown")
                  {
                     OptionsWindow.SetSetting("PlayerName", player.PlayerName);
                  }
              }
          );
            }
            else //unknown error
            {
                KILL();
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Menu -> Info"));
            this.Visibility = Visibility.Hidden;
            if (ConfigurationManager.AppSettings["AI_behavior"]== null)
            {
                     OptionsWindow.SetSetting("AI_behavior", "default");
            }
            var tmp  = ConfigurationManager.AppSettings["AI_behavior"]?? "";
            if (tmp!="default" && tmp != "")
            {
                tmp = tmp.Substring(1);
            }
            new InfoView($"Player Name : {PlayerName} \nAI : {tmp}").ShowDialog();
            this.Visibility = Visibility.Visible;
        }

        private void LoggerConfiguration()
        {
            Logger.Frequency = 100;
            Logger.WriteWithAdding = true;
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Logger was Configurated"));
        }
    }
}
