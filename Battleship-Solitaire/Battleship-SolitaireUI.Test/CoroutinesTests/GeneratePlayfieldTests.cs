using Battleship_SolitaireUI.Coroutines.GeneratePlayfield;
using Battleship_SolitaireUI.Models.Option;
using Battleship_SolitaireUI.Models.Playfield;
using Battleship_SolitaireUI.Models.Ship;
using Caliburn.Micro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship_SolitaireUI.Test.CoroutinesTests
{
    /// <summary>
    /// Test all the task for the generation of the playfield
    /// </summary>
    [TestClass]
    public class GeneratePlayfieldTests
    {
        private Option _option = new Option();
        private Playfield _playfield = new Playfield();

        /// <summary>
        /// Tests if the fields are create and placed correctly
        /// </summary>
        [TestMethod]
        public void CreateFieldsTest()
        {
            //Configure options
            _option.Columns = 15;
            _option.Rows = 15;

            new CreateFields(_playfield, _option).Execute(new CoroutineExecutionContext());

            //Checks if the right amount of fields are created
            Assert.AreEqual(225, _playfield.Fields.Count);
            //Checks if the lowest coordinate is the 0
            Assert.AreEqual(0, _playfield.Fields.Min(f => f.XCoordinate));
            //Checks if the highest coordinate is the 14
            Assert.AreEqual(14, _playfield.Fields.Max(f => f.XCoordinate));
        }

        /// <summary>
        /// Test if all ship are placed correctly
        /// </summary>
        [TestMethod]
        public void PlaceShipsTest()
        {
            new CreateFields(_playfield, _option).Execute(new CoroutineExecutionContext());
            new PlaceShips(_playfield, _option).Execute(new CoroutineExecutionContext());

            //Checks if the right amount of ships are created
            Assert.AreEqual(4, _playfield.Ships.Count);

            int amountOfShipPieces = 0;

            foreach (Ship ship in _playfield.Ships)
            {
                amountOfShipPieces += ship.ShipPieces.Count;
            }

            //Check if the right amount of shippiece are created
            Assert.AreEqual(10, amountOfShipPieces);
        }

        /// <summary>
        /// Checks if the right amount of fields are revealed
        /// </summary>
        [TestMethod]
        public void FinalizePlayfield()
        {
            new CreateFields(_playfield, _option).Execute(new CoroutineExecutionContext());
            new PlaceShips(_playfield, _option).Execute(new CoroutineExecutionContext());
            new FinalizePlayfield(_playfield, _option).Execute(new CoroutineExecutionContext());

            //Checks if the right amount of fields are revealed
            Assert.AreEqual((int)((double)_playfield.Fields.Count / 100 * 70), _playfield.Fields.Count(f => f.Status != Enums.FieldStatus.Unassigned));
        }
    }
}
