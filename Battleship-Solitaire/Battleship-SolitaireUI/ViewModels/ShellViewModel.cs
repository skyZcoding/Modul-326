using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;

namespace Battleship_SolitaireUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private PlayfieldViewModel playfieldView;

        public ShellViewModel()
        {
            playfieldView = new PlayfieldViewModel();
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
            }
        }

    }
}
