using Battleship_SolitaireUI.Enums;
using Battleship_SolitaireUI.Models.Option;
using Battleship_SolitaireUI.Models.Playfield;
using Battleship_SolitaireUI.ViewModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship_SolitaireUI.Coroutines.GeneratePlayfield
{
    /// <summary>
    /// Finalize the playfield that it is ready to be played
    /// </summary>
    public class FinalizePlayfield : IResult
    {
        private readonly Playfield _playfield;
        private readonly Option _option;

        public FinalizePlayfield()
        {
            _playfield = IoC.Get<Playfield>();
            _option = IoC.Get<Option>();
        }

        /// <summary>
        /// Constructor for the unit testing
        /// </summary>
        /// <param name="playfield">fake playfield</param>
        /// <param name="option">fake option</param>
        public FinalizePlayfield(Playfield playfield, Option option)
        {
            _playfield = playfield;
            _option = option;
        }

        /// <summary>
        /// reveal 70 % of all fields and refresh the whole view
        /// </summary>
        public void Execute(CoroutineExecutionContext context)
        {
            // amount of fields / 2 = amount which should be shown
            int fieldCount = (int)((double)_playfield.Fields.Count / 100 * 70);
            // Random Class (Check if field status is "turned over")
            Random rnd = new Random();

            // counts amount of fields that have been made visible
            int changedFields = 0;
            while (changedFields < fieldCount)
            {
                // gen y coordinate
                int yCoord = rnd.Next(0, _option.Rows);
                // gen x coordinate
                int xCoord = rnd.Next(0, _option.Columns);

                Field randomField = _playfield.Fields.First(f => f.XCoordinate == xCoord && f.YCoordinate == yCoord);

                if (randomField.Status == FieldStatus.Unassigned)
                {
                    if (randomField.HasShipPiece)
                    {
                        randomField.Status = FieldStatus.Ship;
                    }
                    else if (!randomField.HasShipPiece)
                    {
                        randomField.Status = FieldStatus.Water;
                    }

                    changedFields++;
                }
                // else -> field has already been assigned, therefore, it is not interesting to us.
            }

            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}
