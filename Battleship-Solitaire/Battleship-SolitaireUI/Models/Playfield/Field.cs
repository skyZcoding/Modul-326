using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship_SolitaireUI.Models.Playfield
{
    public class Field : Model
    {
        private int xCoordinate;
        private int yCoordinate;
        private bool isClicked;

        public int XCoordinate
        {
            get
            {
                return xCoordinate;
            }
            set
            {
                xCoordinate = value;
                OnPropertyChanged(nameof(XCoordinate));
            }
        }

        public int YCoordinate
        {
            get
            {
                return yCoordinate;
            }
            set
            {
                yCoordinate = value;
                OnPropertyChanged(nameof(YCoordinate));
            }
        }

        public bool IsClicked
        {
            get
            {
                return isClicked;
            }
            set
            {
                isClicked = value;
                OnPropertyChanged(nameof(IsClicked));
            }
        }

    }
}
