using Battleship_SolitaireUI.Enums;
using System.Linq;

namespace Battleship_SolitaireUI.Models.Playfield
{
    public class Field : Model
    {
        private bool isRightClicked;
        private bool isLeftClicked;
        private int xCoordinate;
        private int yCoordinate;
        private FieldStatus status;
        private Playfield _playfield;

        public Field(Playfield playfield)
        {
            _playfield = playfield;
        }

        public int XCoordinate
        {
            get => xCoordinate;
            set
            {
                xCoordinate = value;
                OnPropertyChanged(nameof(XCoordinate));
            }
        }

        public int YCoordinate
        {
            get => yCoordinate;
            set
            {
                yCoordinate = value;
                OnPropertyChanged(nameof(YCoordinate));
            }
        }

        public FieldStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public bool HasShipPiece
        {
            get
            {
                return _playfield.Ships.Any(s => s.ShipPieces.Any(sp => sp.Field == this));
            }
        }

    }
}