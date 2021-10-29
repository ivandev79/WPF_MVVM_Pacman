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

namespace Pacman.Views
{
    /// <summary>
    /// Interaction logic for DieRestartWindow.xaml
    /// </summary>
    public partial class DieRestartWindow : Window
    {
        public DieRestartWindow()
        {
            InitializeComponent();
        }

        public DieRestartWindow(int score):this()
        {
            Points.Text = score.ToString();
        }

        public DieRestartWindow(int score , string caption) : this()
        {
            Points.Text = score.ToString();
            this.Title = caption;
        }

        private void RestartClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
