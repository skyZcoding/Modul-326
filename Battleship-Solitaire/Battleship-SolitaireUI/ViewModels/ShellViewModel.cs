using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;

namespace Battleship_SolitaireUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private PlayfieldViewModel _playfieldView;
        private readonly OptionViewModel _optionViewModel;
        private readonly IWindowManager _windowManager;

        public ShellViewModel(IWindowManager windowManager, PlayfieldViewModel playfieldViewModel, OptionViewModel optionViewModel)
        {
            _playfieldView = playfieldViewModel;
            _optionViewModel = optionViewModel;
            _windowManager = windowManager;
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

        public bool CanOpenOptions()
        {
            return true;
        }

        public void OpenOptions()
        {
            _windowManager.ShowDialog(_optionViewModel);
        }
    }
}
