using System;
using System.Collections.Generic;
using System.Text;
using Battleship_SolitaireUI.Models.Ship;
using Battleship_SolitaireUI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            new PlayfieldViewModel().GeneratePlayfieldCommand.Execute(ships);
        }
    }
}
