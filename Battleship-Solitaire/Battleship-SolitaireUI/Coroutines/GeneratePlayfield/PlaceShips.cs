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
    public class PlaceShips : IResult
    {
        private readonly Playfield _playfield;
        private readonly Option _option;

        public PlaceShips()
        {
            _playfield = IoC.Get<Playfield>();
            _option = IoC.Get<Option>();
        }

        public void Execute(CoroutineExecutionContext context)
        {
            foreach (ShipOption ship in _option.Ships.Where(s => s.Amount >= 1))
            {
                for (int i = 0; i < ship.Amount; i++)
                {
                    PlaceShip(new Ship { ShipType = ship.ShipType }, _playfield.Fields);
                }
            }

            Completed(this, new ResultCompletionEventArgs());
        }

        private void PlaceShip(Ship ship, List<Field> fields)
        {
            bool placed = false;

            do
            {
                ShipAlignment shipAlignment = (ShipAlignment)new Random()
                                                                    .Next(0, 2);

                int startCoordinateX = new Random().Next(fields.Min(f => f.XCoordinate), fields.Max(f => f.XCoordinate) + 1);
                int startCoordinateY = new Random().Next(fields.Min(f => f.YCoordinate), fields.Max(f => f.YCoordinate) + 1);

                placed = true;

                ship.ShipPieces = new List<ShipPiece>();

                for (int piece = 0; piece < (int)ship.ShipType && placed; piece++)
                {
                    if (!fields.Any(f => f.XCoordinate == startCoordinateX && f.YCoordinate == startCoordinateY))
                    {
                        placed = false;
                    }
                    else
                    {
                        placed = PlaceShipPiece(ship, fields, startCoordinateX, startCoordinateY);

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

        private bool PlaceShipPiece(Ship ship, List<Field> fields, int xCoordinate, int yCoordinate)
        {
            if (_playfield.Ships
                            .All(s =>
                                !Enumerable
                                    .Any<ShipPiece>(s.ShipPieces, sp =>
                                        sp.Field.XCoordinate == xCoordinate
                                            && sp.Field.YCoordinate == yCoordinate)) && !IsAnyShipAround(xCoordinate, yCoordinate))
            {
                ship.ShipPieces.Add(new ShipPiece { Field = fields.FirstOrDefault(f => f.XCoordinate == xCoordinate && f.YCoordinate == yCoordinate) });

                return true;
            }

            return false;
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}
