using System.Linq;

namespace Battleship_SolitaireUI.Models.Playfield
{
    public class Field : Model
    {
        private int xCoordinate;
        private int yCoordinate;
        private bool isRightClicked;
        private bool isLeftClicked;

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

        public bool IsRightClicked
        {
            get
            {
                return isRightClicked;
            }
            set
            {
                isRightClicked = value;
                OnPropertyChanged(nameof(IsRightClicked));
            }
        }
        public bool IsLeftClicked
        {
            get
            {
                return isLeftClicked;
            }
            set
            {
                isLeftClicked = value;
                OnPropertyChanged(nameof(IsLeftClicked));
            }
        }

        public bool HasShipPiece
        {
            get
            {
                return Playfield.GetInstance().Ships.Any(s => s.ShipPieces.Any(sp => sp.Field == this));
            }
        }
    }
}
