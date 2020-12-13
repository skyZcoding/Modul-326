using Battleship_SolitaireUI.Enums;
using Battleship_SolitaireUI.Models.Option;
using Battleship_SolitaireUI.Models.Playfield;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship_SolitaireUI.Coroutines.GeneratePlayfield
{
    public class CreateFields : IResult
    {
        private readonly Playfield _playfield;
        private readonly Option _option;

        public CreateFields()
        {
            _playfield = IoC.Get<Playfield>();
            _option = IoC.Get<Option>();
        }

        public void Execute(CoroutineExecutionContext context)
        {
            _playfield.Fields = new List<Field>();

            int rows = _option.Rows;
            int columns = _option.Columns;

            List<Field> newFields = new List<Field>();

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    newFields.Add(new Field(_playfield)
                    {
                        YCoordinate = row,
                        XCoordinate = column,
                        Status = FieldStatus.Unassigned
                    });
                }
            }

            _playfield.Fields = newFields;
            Completed(this, new ResultCompletionEventArgs());
        }

        public event EventHandler<ResultCompletionEventArgs> Completed = delegate { };
    }
}
