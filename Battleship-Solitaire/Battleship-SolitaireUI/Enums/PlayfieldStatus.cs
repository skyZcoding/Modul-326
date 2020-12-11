using System.ComponentModel;

namespace Battleship_SolitaireUI.Enums
{
    public enum PlayfieldStatus
    {
        [Description("Not Started")]
        NotStarted,
        [Description("In Progress")]
        InProgress,
        [Description("Won")]
        Won,
        [Description("Finished")]
        Finished
    }
}