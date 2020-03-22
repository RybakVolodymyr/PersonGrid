using PersonGrid.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PersonGrid.Managers;
using PersonGrid.Properties;
using PersonGrid.Tools;

namespace PersonGrid.ViewModels
{
    class EditGridViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> _persons;
        private Person _selectedPerson;

        private RelayCommand<object> _deleteCommand;
        private RelayCommand<object> _addCommand;


        public ObservableCollection<Person> Persons
        {
            get => _persons;
            private set
            {
                _persons = value;
                OnPropertyChanged();
            }
        }

        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
            }
        }

        public RelayCommand<object> DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand<object>(DeleteExecute)); }
        }
        public RelayCommand<object> AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new RelayCommand<object>(AddExecute));
            }
        }
        


        private void DeleteExecute(object obj)
        {
            StationManager.DataStorage.DeletePerson(SelectedPerson);
            _persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonList);
            OnPropertyChanged (nameof(Persons));
        }

        private void AddExecute(object obj)
        {
            NavigationManager.Instance.Navigate(ModesEnum.DataPerson);

        }

        internal EditGridViewModel()
        {
            _persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonList);
        }
        


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
