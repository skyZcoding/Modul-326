using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;

namespace Battleship_SolitaireUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private PlayfieldViewModel playfieldView;
        private readonly IWindowManager _windowManager;

        public ShellViewModel(IWindowManager windowManager)
        {
            playfieldView = new PlayfieldViewModel();
            _windowManager = windowManager;
        }

        public PlayfieldViewModel PlayfieldView
        {
            get
            {
                return playfieldView;
            }
            set
            {
                playfieldView = value;
                NotifyOfPropertyChange(() => PlayfieldView);
                Options();
            }
        }

        private void Options()
        {
            _windowManager.ShowDialog(new OptionViewModel());
        }
    }
}
