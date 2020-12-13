using Battleship_SolitaireUI.Enums;
using Battleship_SolitaireUI.Extensions;
using System.Collections.Generic;

namespace Battleship_SolitaireUI.Models.Playfield
{
    /// <summary>
    /// Represents the playfield of the game
    /// </summary>
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

        /// <summary>
        /// All fields from the playfield
        /// </summary>
        public List<Field> Fields
        {
            get => fields;
            set
            {
                fields = value;
                OnPropertyChanged(nameof(Fields));
            }
        }

        /// <summary>
        /// All ships from the playfield
        /// </summary>
        public List<Ship.Ship> Ships
        {
            get => ships;
            set
            {
                ships = value;
                OnPropertyChanged(nameof(Ships));
            }
        }

        /// <summary>
        /// The current status of the playfield
        /// </summary>
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