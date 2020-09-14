using Battleship_SolitaireUI.Models.Playfield;

namespace Battleship_SolitaireUI.Models.Ship
{
    public class ShipPiece : Model
    {
        private Field field;

        public Field Field
        {
            get
            {
                return field;
            }
            set
            {
                field = value;
            }
        }
    }
}
