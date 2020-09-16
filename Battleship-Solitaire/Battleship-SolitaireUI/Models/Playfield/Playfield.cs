using System.Collections.Generic;

namespace Battleship_SolitaireUI.Models.Playfield
{
    public class Playfield : Model
    {
        private static Playfield instance;
        private static readonly object padlock = new object();

        private List<Ship.Ship> ships;
        private List<Field> fields;

        public static Playfield GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Playfield();
                    instance.Ships = new List<Ship.Ship>();
                    instance.Fields = new List<Field>();
                }

                return instance;
            }
        }

        public List<Field> Fields
        {
            get
            {
                return fields;
            }
            set
            {
                fields = value;
                OnPropertyChanged(nameof(Fields));
            }
        }

        public List<Ship.Ship> Ships
        {
            get
            {
                return ships;
            }
            set
            {
                ships = value;
                OnPropertyChanged(nameof(Ships));
            }
        }

    }
}
