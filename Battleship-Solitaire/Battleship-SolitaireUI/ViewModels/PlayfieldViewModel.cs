using Battleship_SolitaireUI.Commands;
using System.Windows.Input;

namespace Battleship_SolitaireUI.ViewModels
{
    public class PlayfieldViewModel
    {
        private ICommand mGeneratePlayfield;

        public ICommand GeneratePlayfieldCommand
        {
            get
            {
                if (mGeneratePlayfield == null)
                {
                    mGeneratePlayfield = new GeneratePlayfieldCommand();
                }

                return mGeneratePlayfield;
            }
            set
            {
                mGeneratePlayfield = value;
            }
        }

    }
}
