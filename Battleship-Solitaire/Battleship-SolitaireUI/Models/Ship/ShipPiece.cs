using Battleship_SolitaireUI.Models.Playfield;

namespace Battleship_SolitaireUI.Models.Ship
{
    public class ShipPiece : Model
    {
        /// <summary>
        /// The field on which the shippiece is
        /// </summary>
        public Field Field { get; set; }
    }
}