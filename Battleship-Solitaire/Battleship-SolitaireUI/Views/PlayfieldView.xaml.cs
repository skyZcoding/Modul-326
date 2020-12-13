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

        /// <summary>
        /// Gets executed when the playfield gets generated and tries to create the visual playfield
        /// </summary>
        public void Handle(bool isSuccessfullyCreated)
        {
            if (isSuccessfullyCreated)
            {
                ClearGrid();
                InitialiseGrid();
                InitialiseButtons();
                InitialiseLabels();
            }
        }

        /// <summary>
        /// Creates the grid for the playfield
        /// </summary>
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

        /// <summary>
        /// Creates all buttons which represent a field
        /// </summary>
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

        /// <summary>
        /// Updates the position of the button correctly
        /// </summary>
        /// <param name="column">the column of the last button</param>
        /// <param name="row">the row of the last button</param>
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

        /// <summary>
        /// Creates the Label which tells you how many shippieces are in the row/column
        /// </summary>
        private void InitialiseLabels()
        {
            int baseRow = 0;
            int baseColumn = 0;


            for (int row = 1; row < _option.Rows + 1; row++)
            {
                PlaceLabel(row, baseColumn, false);
            }

            for (int column = 1; column < _option.Columns + 1; column++)
            {
                PlaceLabel(baseRow, column, true);
            }
        }

        /// <summary>
        /// Places the label in the grid
        /// </summary>
        /// <param name="row">the row in which the label should be placed</param>
        /// <param name="column">the column in which the label should be placed</param>
        /// <param name="isColumn">if the the labels are in the column</param>
        private void PlaceLabel(int row, int column, bool isColumn)
        {
            int amountOfShipPieces = 0;

            foreach (Ship ship in _playfield.Ships)
            {
                amountOfShipPieces = amountOfShipPieces + (isColumn ?
                    ship.ShipPieces.Count(sp => sp.Field.XCoordinate == column - 1) : ship.ShipPieces.Count(sp => sp.Field.YCoordinate == row - 1));
            }

            Label newLabel = new Label
            {
                Content = amountOfShipPieces,
                Margin = new Thickness(5),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            PlayfieldGrid.Children.Add(newLabel);
            Grid.SetColumn(newLabel, column);
            Grid.SetRow(newLabel, row);
        }

        /// <summary>
        /// Resets the whole playfield
        /// </summary>
        private void ClearGrid()
        {
            PlayfieldGrid.Children.Clear();
            PlayfieldGrid.ColumnDefinitions.Clear();
            PlayfieldGrid.RowDefinitions.Clear();
        }
    }
}
