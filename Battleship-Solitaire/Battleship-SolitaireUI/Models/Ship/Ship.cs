using System.Collections.Generic;
using Battleship_SolitaireUI.Enums;

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
            get => shipPieces;
            set
            {
                shipPieces = value;
                OnPropertyChanged(nameof(ShipPieces));
            }
        }


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