using Battleship_SolitaireUI.Models.Option;
using Battleship_SolitaireUI.Models.Playfield;
using Battleship_SolitaireUI.Models.Ship;
using Battleship_SolitaireUI.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.CodeDom;
using MahApps.Metro.Controls;
using ControlzEx.Theming;

namespace Battleship_SolitaireUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private const int rows = 8;
        private const int columns = 10;
        private Option option;

        public MainWindow()
        {
            InitializeComponent();
            option = Option.GetInstance();
            //option.Ships.Add(new Ship { ShipType = Enums.ShipType.TwoPiece });
            //option.Ships.Add(new Ship { ShipType = Enums.ShipType.ThreePiece });
            //option.Ships.Add(new Ship { ShipType = Enums.ShipType.TwoPiece });
            //option.Ships.Add(new Ship { ShipType = Enums.ShipType.OnePiece });
            //option.Ships.Add(new Ship { ShipType = Enums.ShipType.TwoPiece });
            option.Ships.Add(new Ship { ShipType = Enums.ShipType.OnePiece });

            option.Columns = 10;
            option.Rows = 8;

            //Change Window Theme to Dark and cyan coloured Titlebar
            ThemeManager.Current.ChangeTheme(this, "Dark.Cyan");
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

            foreach (Field field in playfield.Fields)
            {
                Button newButton = new Button
                {
                    Margin = new Thickness(5),
                    Tag = field,
                    Style = buttonStyle,
                    DataContext = field
                };

                newButton.PreviewMouseDown += new MouseButtonEventHandler(Field_Click);

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
                    amountOfShipPieces = amountOfShipPieces + ship.ShipPieces.Where(sp => sp.Field.YCoordinate == row - 1).Count();
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
                    amountOfShipPieces = amountOfShipPieces + ship.ShipPieces.Where(sp => sp.Field.XCoordinate == column - 1).Count();
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

        private void Field_Click(object sender, MouseButtonEventArgs e)
        {
            Button clickedButton = (Button)sender;
            Field clickedField = (Field)clickedButton.Tag;

            if (e.ChangedButton == MouseButton.Right)
            {
                clickedField.IsRightClicked = true;
                clickedField.IsLeftClicked = false;
            }
            else if (e.ChangedButton == MouseButton.Left)
            {
                clickedField.IsRightClicked = false;
                clickedField.IsLeftClicked = true;
            }

            if (CheckForWin())
            {
                FinishGame();

                MessageBox.Show("Won");
            }
        }

        /// <summary>
        /// Disables all the buttons when the game is finished
        /// </summary>
        private void FinishGame()
        {
            for (int i = 0; i < ButtonsGrid.Children.Count; i++)
            {
                if (ButtonsGrid.Children[i] is Button)
                {
                    Button button = (Button)ButtonsGrid.Children[i];
                    button.IsEnabled = false;
                }
            }
        }

        /// <summary>
        /// Check if the game is finished after every button click
        /// </summary>
        /// <returns></returns>
        private bool CheckForWin()
        {
            Playfield playfield = Playfield.GetInstance();
            bool win = true;

            foreach (Field field in playfield.Fields)
            {
                if (!((field.IsLeftClicked && field.HasShipPiece)
                    || (field.IsRightClicked && !field.HasShipPiece)))
                {
                    win = false;
                    break;
                }
            }

            return win;
        }

        private void UpdateColumnRow(ref int column, ref int row)
        {
            if (column == option.Columns - 1)
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
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                SolidColorBrush colorBrush = new SolidColorBrush();
                colorBrush.Color = Color.FromRgb(255, 255, 255);
                drawingContext.DrawRectangle(colorBrush, null,
                new Rect(new Point(), new Size((int)ButtonsGrid.ActualWidth + 10, (int)ButtonsGrid.ActualHeight + 10)));
            }

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)ButtonsGrid.ActualWidth + 10, (int)ButtonsGrid.ActualHeight + 10, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(drawingVisual);
            rtb.Render(ButtonsGrid);

            string name = "battleship-solitaire";
            string extension = ".png";
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";
            

            // create directory if it doesn't yet exist
            if (!Directory.Exists(path + name + "\\"))
            {
                Directory.CreateDirectory(path + name + "\\");
            }

            // change path to include the folder
            path += name + "\\";

            string filename = name + extension;

            // check if file name is available
            for (int iter = 1; File.Exists(path + filename); iter++)
            {
                filename = name + iter.ToString() + extension;
            }

            using (FileStream stream = new FileStream(path + filename, FileMode.Create))
            {
                PngBitmapEncoder png = new PngBitmapEncoder();
                png.Frames.Add(BitmapFrame.Create(rtb));
                png.Save(stream);
            }
            MessageBox.Show("Image saved to: " + path + filename);
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
            SaveButton.IsEnabled = true;
        }
    }
}
