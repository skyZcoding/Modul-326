using Battleship_SolitaireUI.Models.Option;
using ControlzEx.Theming;
using MahApps.Metro.Controls;
using System.Windows;

namespace Battleship_SolitaireUI
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow 
    {
        public OptionsWindow()
        {
            InitializeComponent();
            DataContext = Option.GetInstance();

            //Change Window Theme to Dark and cyan coloured Titlebar
            ThemeManager.Current.ChangeTheme(this, "Dark.Cyan");
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
