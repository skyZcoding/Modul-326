using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship_SolitaireUI.Models.Playfield
{
    public class Playfield : Model
    {
        private List<Field> fields;

        public List<Field> Fields
        {
            get
            {
                return fields;
            }
            set
            {
                fields = value;
                OnPropertyChanged(nameof(Fields));
            }
        }

    }
}
