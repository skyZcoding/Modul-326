using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Battleship_SolitaireUI.Enums;
using Battleship_SolitaireUI.Models.Playfield;
using Battleship_SolitaireUI.Models.Ship;

namespace Battleship_SolitaireUI.Commands
{
    public class GeneratePlayfieldCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var playfield = Playfield.GetInstance();

            var ships = (List<Ship>) parameter;

            var rows = 3;
            var columns = 3;

            var newFields = new List<Field>();

            for (var row = 0; row < rows; row++)
            for (var column = 0; column < columns; column++)
                newFields.Add(new Field
                {
                    YCoordinate = row,
                    XCoordinate = column,
                    IsClicked = false
                });

            playfield.Fields = newFields;

            foreach (var ship in ships) PlaceShip(ship, newFields);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }


        private void PlaceShip(Ship ship, List<Field> fields)
        {
            var placed = false;

            do
            {
                var shipAlignment = (ShipAlignment) new Random()
                    .Next(0, 2);

                var startCoordinateX =
                    new Random().Next(fields.Min(f => f.XCoordinate), fields.Max(f => f.XCoordinate) + 1);
                var startCoordinateY =
                    new Random().Next(fields.Min(f => f.YCoordinate), fields.Max(f => f.YCoordinate) + 1);

                placed = true;

                ship.ShipPieces = new List<ShipPiece>();

                for (var piece = 0; piece < (int) ship.ShipType && placed; piece++)
                    if (!fields.Any(f => f.XCoordinate == startCoordinateX && f.YCoordinate == startCoordinateY))
                    {
                        placed = false;
                    }
                    else
                    {
                        placed = PlaceShipPiece(ship, fields, startCoordinateX, startCoordinateY);

                        if (ShipAlignment.HORIZONTAL == shipAlignment)
                            startCoordinateY++;
                        else
                            startCoordinateX++;
                    }
            } while (!placed);

            var playfield = Playfield.GetInstance();
            playfield.Ships.Add(ship);
        }

        private bool PlaceShipPiece(Ship ship, List<Field> fields, int xCoordinate, int yCoordinate)
        {
            var playfield = Playfield.GetInstance();

            if (playfield.Ships.All(s =>
                !s.ShipPieces.Any(sp => sp.Field.XCoordinate == xCoordinate && sp.Field.YCoordinate == yCoordinate)))
            {
                ship.ShipPieces.Add(new ShipPiece
                    {Field = fields.FirstOrDefault(f => f.XCoordinate == xCoordinate && f.YCoordinate == yCoordinate)});

                return true;
            }

            return false;
        }
    }
}