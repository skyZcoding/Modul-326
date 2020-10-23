using System.Windows.Controls;
using System.Windows.Input;
using Battleship_SolitaireUI.Commands;
using Battleship_SolitaireUI.Models.Playfield;
using Caliburn.Micro;

namespace Battleship_SolitaireUI.ViewModels
{
    public class PlayfieldViewModel : Screen
    {
        private ICommand mGeneratePlayfield;
        private ContentControl contentControl;
        private readonly Playfield _playfield;

        public PlayfieldViewModel(Playfield playfield)
        {
            _playfield = playfield;
        }

        public ContentControl ContentControl
        {
            get
            {
                return contentControl;
            }
            set
            {
                contentControl = value;
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