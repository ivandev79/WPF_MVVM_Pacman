using System;
using System.CodeDom.Compiler;
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
    /// Interaction logic for Options_AIWindow.xaml
    /// </summary>
    public partial class Options_AIWindow : Window
    {
        private List<string> _plugins;

        public List<string> Plugins
        {
            get { return _plugins; }
            private set { _plugins = value; }
        }

        public Options_AIWindow()
        {
            InitializeComponent();
            _plugins = new List<string>();
        }

        private void default_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox) == defaultCheckbox)
            {
                Task.Factory.StartNew(
                () =>
                {
                        OptionsWindow.SetSetting("AI_behavior", "default");
                }
                );
                if (pluginCheckbox != null)
                {
                    pluginCheckbox.IsChecked = false;
                    PluginsList.IsEnabled = false;
                }
            }
            else
            {
                InitListBoxSourse();
            }
        }

        private void InitListBoxSourse()
        {
            PluginsList.IsEnabled = true;
            defaultCheckbox.IsChecked = false;
            var dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Plugins\\";
            if (Directory.Exists(dir))
            {
                PluginsList.ItemsSource = null;
                PluginsList.Items.Clear();
                Plugins = new List<string>();
                foreach (string fileName in Directory.GetFiles(dir))
                {
                    if (fileName.Contains(".dll"))
                    {
                        
                        if (fileName.Contains("Core") || fileName.Contains("Prism") ||  fileName.Contains("Logs")  || fileName.Contains("ValueTuple")) { continue; }
                        Type[] file = Assembly.LoadFrom(fileName).GetTypes();
                        //var tempName = fileName.Substring(fileName.LastIndexOf('\\') + 1);
                        foreach (var item in file)
                        {
                            var tempName = item.ToString();
                            if (tempName.Contains("SimpleAI"))
                            {
                                tempName += " (default)";
                            }
                            Plugins.Add(tempName);
                        }
                    }
                }
                PluginsList.ItemsSource = Plugins;
            }
            else
            {
                MessageBox.Show("Plugins folder not found", "Worning", MessageBoxButton.OK);
            }
        }

        private void UnChecked(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox) == defaultCheckbox)
            {
                if (pluginCheckbox != null)
                {
                    pluginCheckbox.IsChecked = true;
                    PluginsList.IsEnabled = true;
                }
            }
            else
            {
                PluginsList.IsEnabled = false;
                defaultCheckbox.IsChecked = true;
            }
        }

        private void PluginsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var list = (ListBox)sender;

            object item = list.SelectedItem;
            int itemIndex = list.SelectedIndex;
            Task.Factory.StartNew(
             () =>
             {
                 OptionsWindow.SetSetting("AI_behavior", $"{itemIndex}{ item}");
             }
             );

            DialogResult = true;
        }
    }
}
