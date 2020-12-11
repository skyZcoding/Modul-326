using Battleship_SolitaireUI.Enums;
using System.Collections.Generic;

namespace Battleship_SolitaireUI.Models.Playfield
{
    public class Playfield : Model
    {
        private List<Field> fields;
        private List<Ship.Ship> ships;
        private PlayfieldStatus status = PlayfieldStatus.NotStarted;

        public Playfield()
        {
            Fields = new List<Field>();
            Ships = new List<Ship.Ship>();
        }

        public List<Field> Fields
        {
            get => fields;
            set
            {
                fields = value;
                OnPropertyChanged(nameof(Fields));
            }
        }

        public List<Ship.Ship> Ships
        {
            get => ships;
            set
            {
                ships = value;
                OnPropertyChanged(nameof(Ships));
            }
        }


        public PlayfieldStatus Status
        {
            get
            { 
                return status; 
            }
            set
            { 
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

    }
}