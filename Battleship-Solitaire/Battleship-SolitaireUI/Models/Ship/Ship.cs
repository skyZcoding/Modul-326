using Battleship_SolitaireUI.Enums;
using System.Collections.Generic;

namespace Battleship_SolitaireUI.Models.Ship
{
    public class Ship : Model
    {
        private List<ShipPiece> shipPieces;
        private ShipType shipType;

        public Ship()
        {
            ShipPieces = new List<ShipPiece>();
        }

        public List<ShipPiece> ShipPieces
        {
            get
            {
                return shipPieces;
            }
            set
            {
                shipPieces = value;
                OnPropertyChanged(nameof(ShipPieces));
            }
        }


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
    }
}
