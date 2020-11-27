using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using Battleship_SolitaireUI.Models.Option;
using Battleship_SolitaireUI.Views;

namespace Battleship_SolitaireUI.ViewModels
{
    public class OptionViewModel : Screen
    {
        private Option _option = new Option();

        public OptionViewModel(Option option)
        {
            _option = option;
        }

        public Option Option
        {
            get
            {
                return _option;
            }
            set
            {
                _option = value;
                NotifyOfPropertyChange(() => Option);
            }
        }
    }
}
