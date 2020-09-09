using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship_SolitaireUI.Models.Playfield
{
    public class Field : Model
    {
        private int xCoordinate;

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

        private int yCoordinate;

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

        private bool isClicked;

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
