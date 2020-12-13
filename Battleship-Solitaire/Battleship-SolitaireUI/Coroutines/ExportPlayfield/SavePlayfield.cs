using Battleship_SolitaireUI.ViewModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Battleship_SolitaireUI.Coroutines.ExportPlayfield
{
    /// <summary>
    /// Save the whole playfield as a png
    /// </summary>
    public class SavePlayfield : IResult
    {
        private readonly PlayfieldViewModel _playfieldViewModel;

        public SavePlayfield()
        {
            _playfieldViewModel = IoC.Get<PlayfieldViewModel>();
        }

        /// <summary>
        /// Gets the current playfield view and try to save it as png
        /// </summary>
        public void Execute(CoroutineExecutionContext context)
        {
            RenderTargetBitmap rtb = GetRenderedPlayfield((UserControl)_playfieldViewModel.GetView());
            string path = GetNewFileDirectory();

            SaveImage(path, rtb);

            Completed(this, new ResultCompletionEventArgs());
        }

        /// <summary>
        /// Save the rendered bitmap on the generated path
        /// </summary>
        /// <param name="path">generated path for the image</param>
        /// <param name="rtb">the playfield rendered as a bitmap</param>
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

        /// <summary>
        /// Rendering the playfield to a bitmap image
        /// </summary>
        /// <param name="playfield">the whole playfieldview</param>
        /// <returns>the rendered bitmap image</returns>
        private RenderTargetBitmap GetRenderedPlayfield(UserControl playfield)
        {
            double height = playfield.ActualHeight;
            double width = playfield.ActualWidth;

            DrawingVisual drawingVisual = new DrawingVisual();

            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                SolidColorBrush colorBrush = new SolidColorBrush();
                colorBrush.Color = Color.FromRgb(37, 37, 37);
                drawingContext.DrawRectangle(colorBrush, null,
                new Rect(new Point(), new Size(width + 10, height + 10)));
            }

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)width + 10, (int)height + 10, 96, 96, PixelFormats.Pbgra32);

            rtb.Render(drawingVisual);
            rtb.Render(playfield);

            return rtb;
        }

        /// <summary>
        /// Generates the new directory for the image
        /// </summary>
        /// <returns>the new file path for the image</returns>
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

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}
