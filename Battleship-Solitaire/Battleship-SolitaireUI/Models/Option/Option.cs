using System;
using System.Collections.Generic;
using System.Text;
using Battleship_SolitaireUI.Models.Playfield;

namespace Battleship_SolitaireUI.Models.Option
{
    public class Option
    {
        private static Option instance;
        private static readonly object padlock = new object();

        public static Option GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new Option();
                    instance.Ships = new List<Ship.Ship>();
                }

                return instance;
            }
        }

        public List<Ship.Ship> Ships { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
    }
}
