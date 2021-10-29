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
    /// Interaction logic for PlayerNameWindow.xaml
    /// </summary>
    public partial class PlayerNameWindow : Window
    {
        /// <summary>
        /// Your name
        /// </summary>
        ///  <param name="PlayerName">New name after validation</param>
        public String PlayerName { get; private set; }

        public PlayerNameWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.PlayerName = PlayerNameTextBox.Text;
                if (this.PlayerName.Length < 3)
                {
                    PlayerName = "Unknown";
                }
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Player Name was canged on {PlayerName}"));
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error",ex));
            }
            DialogResult = true;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text.Length < 3)
            {
                MessageBox.Show("You need to write at least 3 characters");
            }
            Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Player Name to low length"));
        }

        private void OK(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
