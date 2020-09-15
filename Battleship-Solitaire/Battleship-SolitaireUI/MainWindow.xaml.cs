using System;
using System.Collections.Generic;
using System.IO;
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
using Battleship_SolitaireUI.Models.Option;
using Battleship_SolitaireUI.Models.Playfield;
using Battleship_SolitaireUI.Models.Ship;
using Battleship_SolitaireUI.ViewModels;

namespace Battleship_SolitaireUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int rows = 8;
        private const int columns = 10;
        private Option option;

        public MainWindow()
        {
            InitializeComponent();
            option = Option.GetInstance();
            option.Ships.Add(new Ship { ShipType = Enums.ShipType.TwoPiece });
            option.Ships.Add(new Ship { ShipType = Enums.ShipType.ThreePiece });
            option.Ships.Add(new Ship { ShipType = Enums.ShipType.TwoPiece });
            option.Ships.Add(new Ship { ShipType = Enums.ShipType.OnePiece });
            option.Ships.Add(new Ship { ShipType = Enums.ShipType.TwoPiece });
            option.Ships.Add(new Ship { ShipType = Enums.ShipType.OnePiece });

            option.Columns = 10;
            option.Rows = 8;
        }

        private void InitialiseModels()
        {
            new PlayfieldViewModel().GeneratePlayfieldCommand.Execute(null);
        }

        private void InitialiseGrid()
        {
            GridLength gridLength = new GridLength(1, GridUnitType.Star);

            for (int column = 0; column < option.Columns; column++)
            {
                ButtonsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = gridLength });
            }
            for (int row = 0; row < option.Rows; row++)
            {
                ButtonsGrid.RowDefinitions.Add(new RowDefinition { Height = gridLength });
            }
        }

        private void InitialiseButtons()
        {
            int row = 1;
            int column = 1;

            Playfield playfield = Playfield.GetInstance();
            Style buttonStyle = (Style)Resources["ButtonStyle"];

            foreach (Field field  in playfield.Fields)
            {
                Button newButton = new Button
                {
                    Margin = new Thickness(5),
                    Tag = field,
                    Style = buttonStyle,
                    DataContext = field
                };

                newButton.Click += new RoutedEventHandler(Field_Click);

                ButtonsGrid.Children.Add(newButton);
                Grid.SetColumn(newButton, column);
                Grid.SetRow(newButton, row);

                UpdateColumnRow(ref column, ref row);
            }
        }

        private void InitialiseLabels()
        {
            int baseRow = 0;
            int baseColumn = 0;

            Playfield playfield = Playfield.GetInstance();

            for (int row = 1; row < option.Rows; row++)
            {
                int amountOfShipPieces = 0;

                foreach (Ship ship in playfield.Ships)
                {
                    amountOfShipPieces = amountOfShipPieces + ship.ShipPieces.Where(sp => sp.Field.YCoordinate == row-1).Count();
                }

                Label newLabel = new Label
                {
                    Content = amountOfShipPieces,
                    Margin = new Thickness(5),
                };

                ButtonsGrid.Children.Add(newLabel);
                Grid.SetColumn(newLabel, baseColumn);
                Grid.SetRow(newLabel, row);
            }

            for (int column = 1; column < option.Columns; column++)
            {
                int amountOfShipPieces = 0;

                foreach (Ship ship in playfield.Ships)
                {
                    amountOfShipPieces = amountOfShipPieces + ship.ShipPieces.Where(sp => sp.Field.XCoordinate == column -1).Count();
                }

                Label newLabel = new Label
                {
                    Content = amountOfShipPieces,
                    Margin = new Thickness(5),
                };

                ButtonsGrid.Children.Add(newLabel);
                Grid.SetColumn(newLabel, column);
                Grid.SetRow(newLabel, baseRow);
            }
        }

        private void Field_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button) sender;
            Field clickedField = (Field) clickedButton.Tag;

            Playfield.GetInstance().Fields.FirstOrDefault(f => f == clickedField).IsClicked = true;
        }

        private void UpdateColumnRow(ref int column, ref int row)
        {
            if (column == option.Columns -1)
            {
                column = 1;
                row++;
            }
            else
            {
                column++;
            }
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)ButtonsGrid.ActualWidth, (int)ButtonsGrid.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(ButtonsGrid);

            using (FileStream stream = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/test.png", FileMode.Create))
            {
                PngBitmapEncoder png = new PngBitmapEncoder();
                png.Frames.Add(BitmapFrame.Create(rtb));
                png.Save(stream);
            }
        }

        private void OptionsButton_OnClick(object sender, RoutedEventArgs e)
        {
            OptionsWindow window = new OptionsWindow();
            window.Show();
        }

        private void ClearGrid()
        {
            ButtonsGrid.Children.Clear();
            ButtonsGrid.ColumnDefinitions.Clear();
            ButtonsGrid.RowDefinitions.Clear();
        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            ClearGrid();
            InitialiseModels();
            InitialiseGrid();
            InitialiseButtons();
            InitialiseLabels();
        }
    }
}
