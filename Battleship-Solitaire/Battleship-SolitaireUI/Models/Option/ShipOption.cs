using System;
using System.Collections.Generic;
using System.Text;
using Battleship_SolitaireUI.Enums;
using Battleship_SolitaireUI.Extensions;

namespace Battleship_SolitaireUI.Models.Option
{
    /// <summary>
    /// The configuration of a ship
    /// </summary>
    public class ShipOption : Model
    {
        private int amount;
        private ShipType shipType;

        /// <summary>
        /// The shiptype of the configured ship
        /// </summary>
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

        /// <summary>
        /// Returns the name of the shiptype
        /// </summary>
        public string Name
        {
            get
            {
                return EnumHelper.GetDescriptionOfEnumValue(ShipType);
            }
        }

        /// <summary>
        /// The amount of ships
        /// </summary>
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
