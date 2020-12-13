using System.Collections.Generic;
using Battleship_SolitaireUI.Enums;

namespace Battleship_SolitaireUI.Models.Ship
{
    /// <summary>
    /// Represents a ship in the playfield
    /// </summary>
    public class Ship : Model
    {
        private List<ShipPiece> shipPieces;
        private ShipType shipType;

        public Ship()
        {
            ShipPieces = new List<ShipPiece>();
        }


        /// <summary>
        /// All pieces from the ship
        /// </summary>
        public List<ShipPiece> ShipPieces
        {
            get => shipPieces;
            set
            {
                shipPieces = value;
                OnPropertyChanged(nameof(ShipPieces));
            }
        }

        /// <summary>
        /// The type of the ship
        /// </summary>
        public ShipType ShipType
        {
            get => shipType;
            set
            {
                shipType = value;
                OnPropertyChanged(nameof(ShipType));
            }
        }
    }
}