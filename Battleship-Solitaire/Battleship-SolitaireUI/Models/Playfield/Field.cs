namespace Battleship_SolitaireUI.Models.Playfield
{
    public class Field : Model
    {
        private bool isRightClicked;
        private bool isLeftClicked;
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

        public bool IsRightClicked
        {
            get => isRightClicked;
            set
            {
                isRightClicked = value;
                OnPropertyChanged(nameof(IsRightClicked));
            }
        }

        public bool IsLeftClicked
        {
            get => isLeftClicked;
            set
            {
                isLeftClicked = value;
                OnPropertyChanged(nameof(IsLeftClicked));
            }
        }
    }
}