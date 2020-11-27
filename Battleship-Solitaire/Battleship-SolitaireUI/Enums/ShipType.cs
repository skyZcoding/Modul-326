using System.ComponentModel;

namespace Battleship_SolitaireUI.Enums
{
    public enum ShipType
    {
        [Description("Submarines")]
        OnePiece = 1,
        [Description("Destroyer")]
        TwoPiece = 2,
        [Description("Cruiser")]
        ThreePiece = 3,
        [Description("Battleship")]
        FourPiece = 4
    }
}