using Battleship_SolitaireUI.Models.Option;
using System.Windows;

namespace Battleship_SolitaireUI
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
            DataContext = Option.GetInstance();
        }
    }
}
