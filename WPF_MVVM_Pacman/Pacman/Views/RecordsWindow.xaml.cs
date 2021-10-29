using DataLayer;
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
    /// Show top 10 players
    /// </summary>
    public partial class RecordsWindow : Window
    {
        /// <summary>
        /// List items for binding
        /// </summary>
        public List<Score> Items { get; set; }

        public RecordsWindow()
        {
            try
            {
                Items = ScoreData.GetTopResult();
                InitializeComponent();
                ScoreListBox.ItemsSource = Items;
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Records loaded"));
            }
            catch (Exception ex)
            {
                Logger.Add(new Log(this.GetType().Name, MethodBase.GetCurrentMethod().Name, $"Error",ex));
            }
        }
    }
}
