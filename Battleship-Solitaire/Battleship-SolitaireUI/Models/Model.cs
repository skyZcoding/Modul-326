using System.ComponentModel;

namespace Battleship_SolitaireUI.Models
{
    /// <summary>
    /// Class which contains the onpropertychanged for the model which inherit from this class
    /// </summary>
    public class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}