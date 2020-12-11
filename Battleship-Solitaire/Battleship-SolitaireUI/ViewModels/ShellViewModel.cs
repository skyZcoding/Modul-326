using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Battleship_SolitaireUI.Commands;
using Battleship_SolitaireUI.Models.Playfield;
using Caliburn.Micro;
using Microsoft.Xaml.Behaviors.Input;

namespace Battleship_SolitaireUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private ICommand mGeneratePlayfield;
        private PlayfieldViewModel _playfieldView;
        private readonly OptionViewModel _optionViewModel;
        private readonly IWindowManager _windowManager;
        private readonly Playfield _playfield;

        public ShellViewModel(IWindowManager windowManager, PlayfieldViewModel playfieldViewModel, OptionViewModel optionViewModel, GeneratePlayfieldCommand generatePlayfieldCommand, Playfield playfield)
        {
            _playfieldView = playfieldViewModel;
            _optionViewModel = optionViewModel;
            _windowManager = windowManager;
            mGeneratePlayfield = generatePlayfieldCommand;
            _playfield = playfield;
        }

        public Playfield Playfield
        {
            get
            {
                return _playfield;
            }
        }

        public PlayfieldViewModel PlayfieldView
        {
            get
            {
                return _playfieldView;
            }
            set
            {
                _playfieldView = value;
                NotifyOfPropertyChange(() => PlayfieldView);
            }
        }
        public ICommand GeneratePlayfieldCommand
        {
            get
            {
                return mGeneratePlayfield;
            }
            set => mGeneratePlayfield = value;
        }

        public void Export(ItemsControl playfieldControl)
        {
            RenderTargetBitmap rtb = GetRenderedPlayfield((UserControl)_playfieldView.GetView());
            string path = GetNewFileDirectory();
            SaveImage(path, rtb);
        }

        private void SaveImage(string path, RenderTargetBitmap rtb)
        {
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                PngBitmapEncoder png = new PngBitmapEncoder();
                png.Frames.Add(BitmapFrame.Create(rtb));
                png.Save(stream);
            }

            MessageBox.Show("Image saved to: " + path);
        }

        private RenderTargetBitmap GetRenderedPlayfield(UserControl playfield)
        {
            double height = playfield.ActualHeight;
            double width = playfield.ActualWidth;

            DrawingVisual drawingVisual = new DrawingVisual();

            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                SolidColorBrush colorBrush = new SolidColorBrush();
                colorBrush.Color = Color.FromRgb(255, 255, 255);
                drawingContext.DrawRectangle(colorBrush, null,
                new Rect(new Point(), new Size(width + 10, height + 10)));
            }

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)width + 10, (int)height + 10, 96, 96, PixelFormats.Pbgra32);

            rtb.Render(drawingVisual);
            rtb.Render(playfield);

            return rtb;
        }

        private string GetNewFileDirectory()
        {
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

            return path + filename;
        }

        public void StartGame()
        {
            PlayfieldView.Refresh();
            mGeneratePlayfield.Execute(null);
            IEventAggregator eventAggregator = IoC.Get<IEventAggregator>();
            eventAggregator.PublishOnUIThread(true);
            PlayfieldView.Refresh();
        }

        public void OpenOptions()
        {
            _windowManager.ShowDialog(_optionViewModel);
        }
    }
}
