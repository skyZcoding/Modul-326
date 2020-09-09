using System;
using System.Collections.Generic;
using System.Text;
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
            new PlayfieldViewModel().GeneratePlayfieldCommand.Execute(null);
        }
    }
}
