using Battleship_SolitaireUI.Models.Ship;
using Battleship_SolitaireUI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Battleship_SolitaireUI.Test.TestCommands
{
    [TestClass]
    public class GenerateFieldCommandTest
    {
        [TestMethod]
        public void Execute()
        {
            List<Ship> ships = new List<Ship>();
            ships.Add(new Ship { ShipType = Enums.ShipType.TwoPiece });
            ships.Add(new Ship { ShipType = Enums.ShipType.TwoPiece });
            ships.Add(new Ship { ShipType = Enums.ShipType.ThreePiece });
            ships.Add(new Ship { ShipType = Enums.ShipType.OnePiece });

            new PlayfieldViewModel().GeneratePlayfieldCommand.Execute(ships);
        }
    }
}
