using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Battleship_SolitaireUI.Enums;
using Battleship_SolitaireUI.Models.Option;
using Battleship_SolitaireUI.Models.Playfield;
using Battleship_SolitaireUI.Models.Ship;

namespace Battleship_SolitaireUI.Commands
{
    public class GeneratePlayfieldCommand : ICommand
    {
        private Playfield _playfield;
        private Option _option;

        public GeneratePlayfieldCommand(Playfield playfield, Option option)
        {
            _playfield = playfield;
            _option = option;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

            _playfield.Fields = new List<Field>();
            _playfield.Ships = new List<Ship>();


            List<ShipOption> ships = _option.Ships;

            int rows = _option.Rows;
            int columns = _option.Columns;

            List<Field> newFields = new List<Field>();

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    newFields.Add(new Field
                    {
                        YCoordinate = row,
                        XCoordinate = column,
                        Status = FieldStatus.Unassigned
                    });
                }
            }

            _playfield.Fields = newFields;

            foreach (ShipOption ship in ships)
            {
                PlaceShip(new Ship { ShipType = ship.ShipType}, newFields);
            }
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
                        .Any(sp => xCoordinate - 1 < sp.Field.XCoordinate
                            && xCoordinate + 1 > sp.Field.XCoordinate
                            && yCoordinate - 1 < sp.Field.YCoordinate
                            && yCoordinate + 1 > sp.Field.YCoordinate));
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

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}