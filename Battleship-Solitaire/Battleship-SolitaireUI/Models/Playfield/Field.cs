using Battleship_SolitaireUI.Enums;
using System.Linq;

namespace Battleship_SolitaireUI.Models.Playfield
{
    /// <summary>
    /// Represents a field in the playfield
    /// </summary>
    public class Field : Model
    {
        private int xCoordinate;
        private int yCoordinate;
        private FieldStatus status;
        private Playfield _playfield;

        public Field(Playfield playfield)
        {
            _playfield = playfield;
        }

        /// <summary>
        /// The x coordinate which the field has in the playfield
        /// </summary>
        public int XCoordinate
        {
            get => xCoordinate;
            set
            {
                xCoordinate = value;
                OnPropertyChanged(nameof(XCoordinate));
            }
        }

        /// <summary>
        /// The y coordinate which the field has in the playfield
        /// </summary>
        public int YCoordinate
        {
            get => yCoordinate;
            set
            {
                yCoordinate = value;
                OnPropertyChanged(nameof(YCoordinate));
            }
        }

        /// <summary>
        /// The current status which the field has
        /// </summary>
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

        /// <summary>
        /// Returns if a shippiece is the field
        /// </summary>
        public bool HasShipPiece
        {
            get
            {
                return _playfield.Ships.Any(s => s.ShipPieces.Any(sp => sp.Field == this));
            }
        }

    }
}