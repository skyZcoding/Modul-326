using System.Windows.Controls;
using System.Windows.Input;
using Battleship_SolitaireUI.Commands;
using Caliburn.Micro;

namespace Battleship_SolitaireUI.ViewModels
{
    public class PlayfieldViewModel : Screen
    {
        private ICommand mGeneratePlayfield;
        private ContentControl contentControl;

        public PlayfieldViewModel()
        {
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
                if (mGeneratePlayfield == null) mGeneratePlayfield = new GeneratePlayfieldCommand();

                return mGeneratePlayfield;
            }
            set => mGeneratePlayfield = value;
        }

    }
}