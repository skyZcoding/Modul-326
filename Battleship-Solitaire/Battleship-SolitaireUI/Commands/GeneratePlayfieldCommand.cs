using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Battleship_SolitaireUI.Enums;
using Battleship_SolitaireUI.Models.Option;
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
            Playfield playfield = Playfield.GetInstance();
            Option option = Option.GetInstance();

            playfield.Fields = new List<Field>();
            playfield.Ships = new List<Ship>();


            List<Ship> ships = option.Ships;

            int rows = option.Rows;
            int columns = option.Columns;

            List<Field> newFields = new List<Field>();

            for (int row = 0; row < rows-1; row++)
            {
                for (int column = 0; column < columns-1; column++)
                {
                    newFields.Add(new Field
                    {
                        YCoordinate = row,
                        XCoordinate = column,
                        IsClicked = false
                    });
                }
            }

            playfield.Fields = newFields;

            foreach (Ship ship in ships)
            {
                PlaceShip(ship, newFields);
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

                        if (ShipAlignment.HORIZONTAL == shipAlignment)
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

            Playfield playfield = Playfield.GetInstance();
            playfield.Ships.Add(ship);
        }

        private bool IsAnyShipAround(Ship ship, int xCoordinate, int yCoordinate)
        {
            Playfield playfield = Playfield.GetInstance();

            return playfield.Ships
                .Any(s => 
                    s.ShipPieces
                        .Any(sp => xCoordinate-1 < sp.Field.XCoordinate
                                                    && xCoordinate+1 > sp.Field.XCoordinate
                                                    && yCoordinate-1 < sp.Field.YCoordinate
                                                    && yCoordinate+1 > sp.Field.YCoordinate));
        }

        private bool PlaceShipPiece(Ship ship, List<Field> fields, int xCoordinate, int yCoordinate)
        {
            Playfield playfield = Playfield.GetInstance();

            if (playfield.Ships
                            .All(s => 
                                !Enumerable
                                    .Any<ShipPiece>(s.ShipPieces, sp => 
                                        sp.Field.XCoordinate == xCoordinate 
                                            && sp.Field.YCoordinate == yCoordinate)) && !IsAnyShipAround(ship, xCoordinate, yCoordinate))
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
