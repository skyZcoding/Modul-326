namespace Battleship_SolitaireUI.Models.Playfield
{
    public class Field : Model
    {
        private bool isClicked;
        private int xCoordinate;
        private int yCoordinate;

        public int XCoordinate
        {
            get => xCoordinate;
            set
            {
                xCoordinate = value;
                OnPropertyChanged(nameof(XCoordinate));
            }
        }

        public int YCoordinate
        {
            get => yCoordinate;
            set
            {
                yCoordinate = value;
                OnPropertyChanged(nameof(YCoordinate));
            }
        }

        public bool IsClicked
        {
            get => isClicked;
            set
            {
                isClicked = value;
                OnPropertyChanged(nameof(IsClicked));
            }
        }
    }
}