using System.ComponentModel;

namespace Battleship_SolitaireUI.Enums
{
    /// <summary>
    /// The possible shiptypes, the value represents the amount of shippieces the types has
    /// </summary>
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