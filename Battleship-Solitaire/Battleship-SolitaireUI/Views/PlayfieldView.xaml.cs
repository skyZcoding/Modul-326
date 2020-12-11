using Battleship_SolitaireUI.Models.Option;
using Battleship_SolitaireUI.Models.Playfield;
using Battleship_SolitaireUI.Models.Ship;
using Caliburn.Micro;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Battleship_SolitaireUI.Views
{
    /// <summary>
    /// Interaction logic for PlayfieldView.xaml
    /// </summary>
    public partial class PlayfieldView : UserControl, IHandle<bool>
    {
        private readonly Option _option;
        private readonly Playfield _playfield;

        public PlayfieldView()
        {
            InitializeComponent();

            IEventAggregator eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.Subscribe(this);

            _option = IoC.Get<Option>();
            _playfield = IoC.Get<Playfield>();
        }

        public void Handle(bool isConfigSet)
        {
            if (isConfigSet)
            {
                ClearGrid();
                InitialiseGrid();
                InitialiseButtons();
                InitialiseLabels();
            }
        }

        private void InitialiseGrid()
        {
            GridLength gridLength = new GridLength(1, GridUnitType.Star);

            for (int column = 0; column < _option.Columns + 1; column++)
            {
                PlayfieldGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = gridLength });
            }
            for (int row = 0; row < _option.Rows + 1; row++)
            {
                PlayfieldGrid.RowDefinitions.Add(new RowDefinition { Height = gridLength });
            }
        }

        private void InitialiseButtons()
        {
            int row = 1;
            int column = 1;

            Style buttonStyle = (Style)Resources["ButtonStyle"];

            foreach (Field field in _playfield.Fields)
            {
                Button newButton = new Button
                {
                    Margin = new Thickness(5),
                    Tag = field,
                    Style = buttonStyle,
                    DataContext = field
                };

                Message.SetAttach(newButton, "UpdateStatus($dataContext)");

                PlayfieldGrid.Children.Add(newButton);
                Grid.SetColumn(newButton, column);
                Grid.SetRow(newButton, row);

                UpdateColumnRow(ref column, ref row);
            }
        }

        private void UpdateColumnRow(ref int column, ref int row)
        {
            if (column == _option.Columns)
            {
                column = 1;
                row++;
            }
            else
            {
                column++;
            }
        }

        private void InitialiseLabels()
        {
            int baseRow = 0;
            int baseColumn = 0;


            for (int row = 1; row < _option.Rows + 1; row++)
            {
                int amountOfShipPieces = 0;

                foreach (Ship ship in _playfield.Ships)
                {
                    amountOfShipPieces = amountOfShipPieces + ship.ShipPieces.Count(sp => sp.Field.YCoordinate == row - 1);
                }

                Label newLabel = new Label
                {
                    Content = amountOfShipPieces,
                    Margin = new Thickness(5),
                };

                PlayfieldGrid.Children.Add(newLabel);
                Grid.SetColumn(newLabel, baseColumn);
                Grid.SetRow(newLabel, row);
            }

            for (int column = 1; column < _option.Columns + 1; column++)
            {
                int amountOfShipPieces = 0;

                foreach (Ship ship in _playfield.Ships)
                {
                    amountOfShipPieces = amountOfShipPieces + ship.ShipPieces.Count(sp => sp.Field.XCoordinate == column -1);
                }

                Label newLabel = new Label
                {
                    Content = amountOfShipPieces,
                    Margin = new Thickness(5),
                };

                PlayfieldGrid.Children.Add(newLabel);
                Grid.SetColumn(newLabel, column);
                Grid.SetRow(newLabel, baseRow);
            }
        }

        private void ClearGrid()
        {
            PlayfieldGrid.Children.Clear();
            PlayfieldGrid.ColumnDefinitions.Clear();
            PlayfieldGrid.RowDefinitions.Clear();
        }
    }
}
