using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Battleship_SolitaireUI.Models.Playfield;
using Caliburn.Micro;
using Microsoft.Xaml.Behaviors.Input;
using Battleship_SolitaireUI.Coroutines.GeneratePlayfield;
using Battleship_SolitaireUI.Coroutines.ExportPlayfield;

namespace Battleship_SolitaireUI.ViewModels
{
    /// <summary>
    /// ViewModel for the shell view
    /// </summary>
    public class ShellViewModel : Screen
    {
        private PlayfieldViewModel _playfieldViewModel;
        private readonly OptionViewModel _optionViewModel;
        private readonly IWindowManager _windowManager;
        private readonly Playfield _playfield;

        public ShellViewModel(IWindowManager windowManager, PlayfieldViewModel playfieldViewModel, OptionViewModel optionViewModel, Playfield playfield)
        {
            _optionViewModel = optionViewModel;
            _windowManager = windowManager;
            _playfield = playfield;
        }

        /// <summary>
        /// Returns the playfield
        /// </summary>
        public Playfield Playfield
        {
            get
            {
                return _playfield;
            }
        }

        /// <summary>
        /// ViewModel for the playfield view
        /// </summary>
        public PlayfieldViewModel PlayfieldViewModel
        {
            get
            {
                return _playfieldViewModel;
            }
            set
            {
                _playfieldViewModel = value;
                NotifyOfPropertyChange(() => PlayfieldViewModel);
            }
        }

        /// <summary>
        /// Contains all task which are needed to export the playfield
        /// </summary>
        public IEnumerable<IResult> ExportPlayfield
        {
            get
            {
                yield return new SavePlayfield();
            }
        }

        /// <summary>
        /// Contains all the task which are needed to generate the playfield
        /// </summary>
        public IEnumerable<IResult> GeneratePlayfield
        {
            get
            {
                yield return new CreateFields();
                yield return new PlaceShips();
                yield return new FinalizePlayfield();
            }
        }

        /// <summary>
        /// Gets executed when the save button is clicked and tries then to export the playfield
        /// </summary>
        public void Export()
        {
            Coroutine.BeginExecute(ExportPlayfield.GetEnumerator());
        }

        /// <summary>
        /// Gets executed when the start button is clicked and tries then to generate the playfield
        /// </summary>
        public void StartGame()
        {
            Coroutine.BeginExecute(GeneratePlayfield.GetEnumerator());
        }

        /// <summary>
        /// Gets executed when the options button is clicked and tries then to open the options window
        /// </summary>
        public void OpenOptions()
        {
            _windowManager.ShowDialog(_optionViewModel);
        }
    }
}
