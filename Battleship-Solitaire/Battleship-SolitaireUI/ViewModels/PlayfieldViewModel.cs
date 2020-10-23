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
        private ICommand mGeneratePlayfield;
        private ContentControl contentControl;
        private readonly Playfield _playfield;
        private readonly Option _option;

        public PlayfieldViewModel(Playfield playfield, Option option)
        {
            _playfield = playfield;
            _option = option;
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

        public ICommand GeneratePlayfieldCommand
        {
            get
            {
                if (mGeneratePlayfield == null) mGeneratePlayfield = new GeneratePlayfieldCommand(_playfield);

                return mGeneratePlayfield;
            }
            set => mGeneratePlayfield = value;
        }

    }
}