using System.ComponentModel;

namespace Battleship_SolitaireUI.Enums
{
    public enum ShipType
    {
        [Description("Einfeld")]
        OnePiece = 1,
        [Description("Zweifeld")]
        TwoPiece = 2,
        [Description("Dreifeld")]
        ThreePiece = 3
    }
}