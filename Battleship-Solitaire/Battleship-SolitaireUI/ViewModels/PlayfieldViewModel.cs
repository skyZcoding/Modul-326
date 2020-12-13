using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Battleship_SolitaireUI.Enums;
using Battleship_SolitaireUI.Models.Option;
using Battleship_SolitaireUI.Models.Playfield;
using Caliburn.Micro;
using System.Linq;

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

        public void UpdateStatus(Field field)
        {
            int newFieldId = (int)field.Status + 1;

            if (newFieldId <= (int)Enum.GetValues(typeof(FieldStatus)).Cast<FieldStatus>().Max())
            {
                field.Status = (FieldStatus)(newFieldId);
            }
            else
            {
                field.Status = (FieldStatus)0;
            }

            _playfield.Status = CheckForWin();

        }

        private PlayfieldStatus CheckForWin()
        {
            bool win = _playfield.Fields.Any(f => !((f.Status == FieldStatus.Ship && f.HasShipPiece) ||
                                            (f.Status == FieldStatus.Water && !f.HasShipPiece) &&
                                            f.Status != FieldStatus.Unassigned));

            bool allFieldsSet = !_playfield.Fields.Any(f => f.Status == FieldStatus.Unassigned);

            PlayfieldStatus status = PlayfieldStatus.InProgress;

            if(allFieldsSet && !win)
            {
                status = PlayfieldStatus.Lost;
            }
            else if(allFieldsSet && win)
            {
                status = PlayfieldStatus.Won;
            }

            return status;
        }
    }
}