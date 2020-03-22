using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using PersonGrid.Tools;
using PersonGrid.Managers;
using PersonGrid.Properties;

namespace PersonGrid.ViewModels
{
    public class MainWindowViewModel:ILoaderOwner
    {
        private Visibility _visibility = Visibility.Hidden;
        private bool _isEnabled = true;

        public Visibility LoaderVisibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged();
            }
        }

        public bool IsControlEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            LoaderManager.Instance.Initialize(this);
        }

        internal void StartApplication()
        {
//            NavigationManager.Instance.Navigate(StationManager.CurrentPerson != null ? ModesEnum.Proceeded : ModesEnum.DataPerson);
            NavigationManager.Instance.Navigate(ModesEnum.PersonGrid);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
