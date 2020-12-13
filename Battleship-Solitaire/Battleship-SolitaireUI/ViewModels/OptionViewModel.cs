using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using Battleship_SolitaireUI.Models.Option;
using Battleship_SolitaireUI.Views;
using ControlzEx.Standard;
using Battleship_SolitaireUI.Models.Ship;

namespace Battleship_SolitaireUI.ViewModels
{
    public class OptionViewModel : Screen
    {
        private Option _option;
        private bool isValid;

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

        public bool IsValid
        {
            get
            {
                return isValid;
            }
        }

        public void OptionsOnChange()
        {
            int neededFields = 0;
            int actualFields = _option.Rows * _option.Columns;

            foreach (ShipOption ship in _option.Ships)
            {
                neededFields += ship.Amount * ((int)ship.ShipType * 3 + 6);
            }

            isValid = neededFields < actualFields;

            NotifyOfPropertyChange(() => IsValid);
        }
    }
}
