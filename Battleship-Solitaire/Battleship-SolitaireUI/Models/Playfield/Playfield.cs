using System.Collections.Generic;

namespace Battleship_SolitaireUI.Models.Playfield
{
    public class Playfield : Model
    {
        private static Playfield instance;
        private static readonly object padlock = new object();
        private List<Field> fields;

        private List<Ship.Ship> ships;

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
    }
}