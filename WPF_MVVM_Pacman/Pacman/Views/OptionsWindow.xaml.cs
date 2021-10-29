using Logs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace Pacman.Views
{
    /// <summary>
    /// This class is designed for the player’s settings, the difficulty of the game, and the behavior of ghosts.
    /// </summary>
    public partial class OptionsWindow : Window
    {
        private int _mapBlocks;
        private int _mapSize;
        public OptionsWindow()
        {
            InitializeComponent();
            if (Int32.TryParse(ConfigurationManager.AppSettings["Map_Blocks"], out _mapBlocks))
                Sliderblocks.Value = _mapBlocks;
            else
                Sliderblocks.Value = 20;

            if (Int32.TryParse(ConfigurationManager.AppSettings["Map_Size"], out _mapSize))
                SliderSize.Value = _mapSize;
            else
                SliderSize.Value = 15;
        }

        private void Window_Closing(object sender, EventArgs e)
        {
            Task.Factory.StartNew(
                () =>
                {
                    SetSetting("Map_Blocks", _mapBlocks.ToString());
                    SetSetting("Map_Size", _mapSize.ToString());
                }
            );
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, "Close Options "));
            this.DialogResult = true;
        }

        private void OK(object sender, EventArgs e)
        {
            this.DialogResult = true;
        }

        #region App Config
        public static void SetSetting(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (IsContains(key))
            {
                config.AppSettings.Settings[key].Value = value;
                config.Save(ConfigurationSaveMode.Full, true);
                ConfigurationManager.RefreshSection("appSettings");
            }
            else
            {
                config.AppSettings.Settings.Add(key, value);
                config.Save(ConfigurationSaveMode.Minimal);
            }
            Logger.Add(new Log("static", MethodBase.GetCurrentMethod().Name, $"App Config save : {key} = {value}"));
        }

        private static bool IsContains(string find)
        {
            bool flag = false;
            foreach (string key in ConfigurationManager.AppSettings.AllKeys)
            {
                if (key == find)
                { flag = true; break; }
            }
            return flag;

        }

        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
                Logger.Add(new Log("static", MethodBase.GetCurrentMethod().Name, $"App Config update : {key} = {value}"));
            }
            catch (ConfigurationErrorsException ex)
            {
                Logger.Add(new Log("static", MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
        }
        #endregion

        private void Sliderblocks_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _mapBlocks = (int)(sender as Slider).Value;
        }

        private void SliderSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _mapSize = (int)(sender as Slider).Value;
        }

        private void AI_Click(object sender, RoutedEventArgs e)
        {
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, " Options -> AI "));
            Options_AIWindow view = new Options_AIWindow();
            view.ShowDialog();
        }

        private void ChangeName_Click(object sender, RoutedEventArgs e)
        {
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, " Options -> Change Name "));
            PlayerNameWindow player = new PlayerNameWindow();
            player.ShowDialog();

            string playerName = ConfigurationManager.AppSettings["PlayerName"];
            if (playerName == null || player.PlayerName != "Unknown")
            {
                AddUpdateAppSettings("PlayerName", player.PlayerName);
            }
        }

        private void AdvancedGhostConfigurations_Click(object sender, RoutedEventArgs e)
        {
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, " Options -> Advanced Ghost Configurations "));
            AdvancedGhostOptionWindow view = new AdvancedGhostOptionWindow();
            view.ShowDialog();
            if (view.IsOptionsEnable)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Custom gost configuration saving . . . "));
                AddUpdateAppSettings("IsAdvancedGhostOptionsEnable", view.IsOptionsEnable.ToString());
                AddUpdateAppSettings("RedGhostCount", view.RedCount);
                AddUpdateAppSettings("RedGhostSpeed", view.RedSpeed);
                AddUpdateAppSettings("GreenGhostCount", view.GreenCount);
                AddUpdateAppSettings("GreenGhostSpeed", view.GreenSpeed);
                AddUpdateAppSettings("BlueGhostCount", view.BlueCount);
                AddUpdateAppSettings("BlueGhostSpeed", view.BlueSpeed);
            }
            else
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Default gost configuration( using AI specify)"));
                SetSetting("IsAdvancedGhostOptionsEnable", view.IsOptionsEnable.ToString());
            }
        }
    }
}
