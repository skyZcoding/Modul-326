using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Battleship_SolitaireUI.Commands;
using Caliburn.Micro;

namespace Battleship_SolitaireUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private ICommand mGeneratePlayfield;
        private PlayfieldViewModel _playfieldView;
        private readonly OptionViewModel _optionViewModel;
        private readonly IWindowManager _windowManager;

        public ShellViewModel(IWindowManager windowManager, PlayfieldViewModel playfieldViewModel, OptionViewModel optionViewModel, GeneratePlayfieldCommand generatePlayfieldCommand)
        {
            _playfieldView = playfieldViewModel;
            _optionViewModel = optionViewModel;
            _windowManager = windowManager;
            mGeneratePlayfield = generatePlayfieldCommand;
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

        public void StartGame()
        {
            PlayfieldView.Refresh();
            mGeneratePlayfield.Execute(null);
            PlayfieldView.Refresh();
        }

        public void OpenOptions()
        {
            _windowManager.ShowDialog(_optionViewModel);
        }
    }
}
