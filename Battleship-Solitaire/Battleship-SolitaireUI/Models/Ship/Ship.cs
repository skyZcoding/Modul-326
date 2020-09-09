using System.Collections.Generic;
using System.Windows.Documents;

namespace Battleship_SolitaireUI.Models.Ship
{
    public class Ship : Model
    {
        private List<ShipPiece> shipPieces;

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

    }
}
