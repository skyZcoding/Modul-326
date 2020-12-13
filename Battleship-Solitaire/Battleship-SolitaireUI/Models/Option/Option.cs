using System;
using System.Collections.Generic;
using System.Text;
using Battleship_SolitaireUI.Enums;

namespace Battleship_SolitaireUI.Models.Option
{
    public class Option : Model
    {
        private int columns = 10;
        private int rows = 10;
        private List<ShipOption> ships = new List<ShipOption>()
        {
            new ShipOption() { ShipType = ShipType.OnePiece, Amount = 1},
            new ShipOption() { ShipType = ShipType.TwoPiece, Amount = 1},
            new ShipOption() { ShipType = ShipType.ThreePiece, Amount = 1},
            new ShipOption() { ShipType = ShipType.FourPiece, Amount = 1}
        };

        public List<ShipOption> Ships
        {
            get
            {
                return ships;
            }
            set
            {
                ships = value;
                OnPropertyChanged(nameof(Ships));
            }
        }


        public int Rows
        {
            get
            {
                return rows;
            }
            set
            {
                rows = value;
                OnPropertyChanged(nameof(Rows));
            }
        }

        public int Columns
        {
            get
            {
                return columns;
            }
            set
            {
                columns = value;
                OnPropertyChanged(nameof(Columns));
            }
        }

    }
}
