using System;
using System.Collections.Generic;
using System.Text;
using Battleship_SolitaireUI.Enums;

namespace Battleship_SolitaireUI.Models.Option
{
    public class ShipOption : Model
    {
        private int amount;
        private ShipType shipType;

        public ShipType ShipType
        {
            get
            {
                return shipType;
            }
            set
            {
                shipType = value;
                OnPropertyChanged(nameof(ShipType));
            }
        }

        public string Name
        {
            get
            {
                return Enum.GetName(, ShipType);
            }
        }

        public int Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

    }
}
