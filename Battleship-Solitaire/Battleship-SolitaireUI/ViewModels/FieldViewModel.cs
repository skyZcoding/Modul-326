using Battleship_SolitaireUI.Models.Option;
using Battleship_SolitaireUI.Models.Playfield;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Battleship_SolitaireUI.ViewModels
{
    public class FieldViewModel
    {
        private readonly Playfield _playfield;
        private readonly int _row;

        public FieldViewModel(int row, Playfield playfield)
        {
            _playfield = playfield;
            _row = row;
        }


        public int AmountOfFields
        {
            get
            {
                return Fields.Count();
            }
        }

        public List<Field> Fields
        {
            get
            {
                return _playfield.Fields.Where(f => f.YCoordinate == _row).ToList();
            }
        }

    }
}
