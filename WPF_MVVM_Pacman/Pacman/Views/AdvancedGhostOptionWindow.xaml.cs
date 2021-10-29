using Core;
using Logs;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AdvancedGhostOptionWindow.xaml
    /// </summary>
    public partial class AdvancedGhostOptionWindow : Window
    {
        public List<string> Speeds { get; set; }
        public bool IsOptionsEnable { get; set; }
        public string GreenSpeed { get; set; }
        public string RedSpeed { get; set; }
        public string BlueSpeed { get; set; }
        public string GreenCount { get; set; }
        public string RedCount { get; set; }
        public string BlueCount { get; set; }

        public AdvancedGhostOptionWindow()
        {
            InitializeComponent();
            SourceListInit();
        }

        private void SourceListInit()
        {
            try
            {
                string[] tmp = Enum.GetNames(typeof(Speeds));
                Speeds = tmp.ToList();
                GreenSpeedComboBox.ItemsSource = Speeds;
                GreenSpeedComboBox.SelectedIndex = 2;
                BlueSpeedComboBox.ItemsSource = Speeds;
                BlueSpeedComboBox.SelectedIndex = 2;
                RedSpeedComboBox.ItemsSource = Speeds;
                RedSpeedComboBox.SelectedIndex = 3;
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
        }

        private void PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int key = (int)e.Key;

            e.Handled = !(key >= 34 && key <= 43 ||
                          key >= 74 && key <= 83 ||
                          key == 2);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            paramsPanel.IsEnabled = IsOptionsEnable = true;
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Custom gost configuration was ENABLE"));
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            paramsPanel.IsEnabled = IsOptionsEnable = false;
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Custom gost configuration was DISABLE"));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (IsOptionsEnable)
                {
                    RedCount = RedCountTextBox.Text;
                    BlueCount = BlueCountTextBox.Text;
                    GreenCount = GreenCountTextBox.Text;
                    GreenSpeed = (string)GreenSpeedComboBox.SelectedItem;
                    RedSpeed = (string)RedSpeedComboBox.SelectedItem;
                    BlueSpeed = (string)BlueSpeedComboBox.SelectedItem;
                }
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error", ex));
            }
        }
    }
}
