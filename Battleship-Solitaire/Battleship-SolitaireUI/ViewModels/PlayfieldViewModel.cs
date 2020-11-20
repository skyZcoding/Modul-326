﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using Battleship_SolitaireUI.Commands;
using Battleship_SolitaireUI.Enums;
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

            _playfield.Finished = CheckForWin();
        }

        private bool CheckForWin()
        {
 
            bool win = true;

            foreach (Field field in _playfield.Fields)
            {
                if (!((field.Status == FieldStatus.Ship && field.HasShipPiece)
                    || (field.Status != FieldStatus.Ship && !field.HasShipPiece)))
                {
                    win = false;
                    break;
                }
            }

            return win;
        }
    }
}