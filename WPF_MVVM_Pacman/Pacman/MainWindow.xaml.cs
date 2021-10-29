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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Pacman.ViewModels;

namespace Pacman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //With out this i'm don't know how to interract with WPF components
            this.DataContext = new GridViewModel(Field);
            Closing += (this.DataContext as GridViewModel).OnWindowClosing;
        }

        public MainWindow(string palyerName)
        {
            InitializeComponent();
            //With out this i'm don't know how to interract with WPF components
            this.DataContext = new GridViewModel(Field, palyerName);
            Closing += (this.DataContext as GridViewModel).OnWindowClosing;
        }
    }
}
