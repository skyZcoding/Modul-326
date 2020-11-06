using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using Battleship_SolitaireUI.Commands;
using Battleship_SolitaireUI.Models.Option;
using Battleship_SolitaireUI.Models.Playfield;
using Caliburn.Micro;

namespace Battleship_SolitaireUI.ViewModels
{
    public class PlayfieldViewModel : Screen
    {
        private readonly Playfield _playfield;
        private readonly Option _option;

        public PlayfieldViewModel(Playfield playfield, Option option)
        {
            _playfield = playfield;
            _option = option;
        }

        public List<FieldViewModel> Fields
        {
            get
            {
                List<FieldViewModel> fields = new List<FieldViewModel>();

                for (int row = 0; row < _option.Rows; row++)
                {
                    fields.Add(new FieldViewModel(row, _playfield));
                }

                return fields;
            }
        }

        public Playfield Playfield
        {
            get
            {
                return _playfield;
            }
        }

        public Option Option
        {
            get
            {
                return _option;
            }
        }

    }
}