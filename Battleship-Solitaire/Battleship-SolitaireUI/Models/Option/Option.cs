using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship_SolitaireUI.Models.Option
{
    public class Option : Model
    {
        private int columns;
        private int rows;
        private List<ShipOption> ships;

        public Option()
        {
            foreach (var VARIABLE in Enum.GetValues())
            {
                
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
