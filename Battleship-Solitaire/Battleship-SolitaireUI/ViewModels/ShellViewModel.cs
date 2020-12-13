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
    public class ShellViewModel : Screen
    {
        private PlayfieldViewModel _playfieldViewModel;
        private readonly OptionViewModel _optionViewModel;
        private readonly IWindowManager _windowManager;
        private readonly Playfield _playfield;

        public ShellViewModel(IWindowManager windowManager, PlayfieldViewModel playfieldViewModel, OptionViewModel optionViewModel, Playfield playfield)
        {
            _playfieldViewModel = playfieldViewModel;
            _optionViewModel = optionViewModel;
            _windowManager = windowManager;
            _playfield = playfield;
        }

        public Playfield Playfield
        {
            get
            {
                return _playfield;
            }
        }

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

        public IEnumerable<IResult> ExportPlayfield
        {
            get
            {
                yield return new SavePlayfield();
            }
        }

        public IEnumerable<IResult> GeneratePlayfield
        {
            get
            {
                yield return new CreateFields();
                yield return new PlaceShips();
                yield return new FinalizePlayfield();
            }
        }

        public void Export()
        {
            Coroutine.BeginExecute(ExportPlayfield.GetEnumerator());
        }

        public void StartGame()
        {
            Coroutine.BeginExecute(GeneratePlayfield.GetEnumerator());
        }

        public void OpenOptions()
        {
            _windowManager.ShowDialog(_optionViewModel);
        }
    }
}
