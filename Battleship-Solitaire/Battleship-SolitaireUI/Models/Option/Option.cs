using System;
using System.Collections.Generic;
using System.Text;
using Battleship_SolitaireUI.Enums;

namespace Battleship_SolitaireUI.Models.Option
{
    public class Option : Model
    {
        private int columns;
        private int rows;
        private List<ShipOption> ships;

        public Option()
        {
            Ships = new List<ShipOption>();

            foreach (int value in Enum.GetValues(typeof(ShipType)))
            {
               Ships.Add(new ShipOption{ShipType = (ShipType)value}); 
            }
        }

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
