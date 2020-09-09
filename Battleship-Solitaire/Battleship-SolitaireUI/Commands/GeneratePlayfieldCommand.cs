using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
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

            List<Ship> ships = (List<Ship>) parameter;
            List<Ship> finalShips = new List<Ship>();

            int rows = 3;
            int columns = 3;

            List<Field> newFields = new List<Field>();

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    newFields.Add(new Field
                    {
                        YCoordinate = row,
                        XCoordinate = column,
                        IsClicked = false
                    });
                }
            }

            foreach (Ship ship in ships)
            {
                int counter = 0;
                do
                {

                } while (counter < (int)ship.ShipType);
            }

            playfield.Fields = newFields;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
