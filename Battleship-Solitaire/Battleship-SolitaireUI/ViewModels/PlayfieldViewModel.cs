using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Battleship_SolitaireUI.Commands;

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
