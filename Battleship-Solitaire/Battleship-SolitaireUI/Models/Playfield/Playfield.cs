using System.Collections.Generic;

namespace Battleship_SolitaireUI.Models.Playfield
{
    public class Playfield : Model
    {
        private List<Field> fields;
        private List<Ship.Ship> ships;
        private bool finished;

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


        public bool Finished
        {
            get
            { 
                return finished; 
            }
            set
            { 
                finished = value;
                OnPropertyChanged(nameof(Finished));
            }
        }

    }
}