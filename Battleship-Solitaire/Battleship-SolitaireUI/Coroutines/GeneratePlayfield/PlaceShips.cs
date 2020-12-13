using Battleship_SolitaireUI.Enums;
using Battleship_SolitaireUI.Models.Option;
using Battleship_SolitaireUI.Models.Playfield;
using Battleship_SolitaireUI.Models.Ship;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship_SolitaireUI.Coroutines.GeneratePlayfield
{
    /// <summary>
    /// Place all ships in the playfield which are configured in <see cref="Models.Option.Option"/>
    /// </summary>
    public class PlaceShips : IResult
    {
        private readonly Playfield _playfield;
        private readonly Option _option;

        public PlaceShips()
        {
            _playfield = IoC.Get<Playfield>();
            _option = IoC.Get<Option>();
        }

        /// <summary>
        /// Constructor for the unit testing
        /// </summary>
        /// <param name="playfield">fake playfield</param>
        /// <param name="option">fake option</param>
        public PlaceShips(Playfield playfield, Option option)
        {
            _playfield = playfield;
            _option = option;
        }

        /// <summary>
        /// Iterates through each configured ship and try to place them
        /// </summary>
        public void Execute(CoroutineExecutionContext context)
        {
            foreach (ShipOption ship in _option.Ships.Where(s => s.Amount >= 1))
            {
                for (int i = 0; i < ship.Amount; i++)
                {
                    PlaceShip(new Ship { ShipType = ship.ShipType });
                }
            }

            Completed(this, new ResultCompletionEventArgs());
        }

        /// <summary>
        /// Tries to place the ship in the playfield
        /// </summary>
        /// <param name="ship">the ship which is should be placed</param>
        private void PlaceShip(Ship ship)
        {
            bool placed = false;

            do
            {
                ShipAlignment shipAlignment = (ShipAlignment)new Random()
                                                                    .Next(0, 2);

                int startCoordinateX = new Random().Next(_playfield.Fields.Min(f => f.XCoordinate), _playfield.Fields.Max(f => f.XCoordinate) + 1);
                int startCoordinateY = new Random().Next(_playfield.Fields.Min(f => f.YCoordinate), _playfield.Fields.Max(f => f.YCoordinate) + 1);

                placed = true;

                ship.ShipPieces = new List<ShipPiece>();

                for (int piece = 0; piece < (int)ship.ShipType && placed; piece++)
                {
                    if (!_playfield.Fields.Any(f => f.XCoordinate == startCoordinateX && f.YCoordinate == startCoordinateY))
                    {
                        placed = false;
                    }
                    else
                    {
                        placed = PlaceShipPiece(ship, startCoordinateX, startCoordinateY);

                        if (ShipAlignment.Vertical == shipAlignment)
                        {
                            startCoordinateY++;
                        }
                        else
                        {
                            startCoordinateX++;
                        }
                    }
                }
            } while (!placed);

            _playfield.Ships.Add(ship);
        }

        /// <summary>
        /// Checks if any shippiece is around the placed piece
        /// </summary>
        /// <param name="xCoordinate">the x coordinate from the placed shippiece</param>
        /// <param name="yCoordinate">the y coordinate from the placed shippiece</param>
        /// <returns>if a shippiece is around</returns>
        private bool IsAnyShipAround(int xCoordinate, int yCoordinate)
        {
            return _playfield.Ships
                .Any(s =>
                    s.ShipPieces
                        .Any(sp => (xCoordinate - 1 == sp.Field.XCoordinate
                                && yCoordinate == sp.Field.YCoordinate)
                            || (xCoordinate + 1 == sp.Field.XCoordinate
                                && yCoordinate == sp.Field.YCoordinate)
                            || (xCoordinate == sp.Field.XCoordinate
                                && yCoordinate - 1 == sp.Field.YCoordinate)
                            || (xCoordinate == sp.Field.XCoordinate
                                && yCoordinate + 1 == sp.Field.YCoordinate)
                            || (xCoordinate + 1 == sp.Field.XCoordinate
                                && yCoordinate + 1 == sp.Field.YCoordinate)
                            || (xCoordinate - 1 == sp.Field.XCoordinate
                                && yCoordinate - 1 == sp.Field.YCoordinate)
                            || (xCoordinate - 1 == sp.Field.XCoordinate
                                && yCoordinate + 1 == sp.Field.YCoordinate)
                            || (xCoordinate + 1 == sp.Field.XCoordinate
                                && yCoordinate - 1 == sp.Field.YCoordinate)));
        }

        /// <summary>
        /// tries to place the shippiece in the playfield
        /// </summary>
        /// <param name="ship">the ship which is should be placed</param>
        /// <param name="xCoordinate">the x coordinate from the placed shippiece</param>
        /// <param name="yCoordinate">the y coordinate from the placed shippiece</param>
        /// <returns>if the placing of the shippiece was successful</returns>
        private bool PlaceShipPiece(Ship ship, int xCoordinate, int yCoordinate)
        {
            if (_playfield.Ships
                            .All(s =>
                                !Enumerable
                                    .Any<ShipPiece>(s.ShipPieces, sp =>
                                        sp.Field.XCoordinate == xCoordinate
                                            && sp.Field.YCoordinate == yCoordinate)) && !IsAnyShipAround(xCoordinate, yCoordinate))
            {
                ship.ShipPieces.Add(new ShipPiece { Field = _playfield.Fields.FirstOrDefault(f => f.XCoordinate == xCoordinate && f.YCoordinate == yCoordinate) });

                return true;
            }

            return false;
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}
