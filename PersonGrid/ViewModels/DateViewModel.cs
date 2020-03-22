using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using PersonGrid.Models;
using PersonGrid.Tools;
using PersonGrid.Managers;
using PersonGrid.Properties;

namespace PersonGrid.ViewModels
{
    internal class DateViewModel : INotifyPropertyChanged 
    {
        #region Fields 
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthDate = new DateTime(2009, 9, 4);


        #region Commands
        private RelayCommand<object> _proceedCommand ;
        #endregion
        #endregion

        #region Properties
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                OnPropertyChanged();

            }
        }
        public bool IsBirthday => DateTime.Today.Month == _birthDate.Month && DateTime.Today.Day == _birthDate.Day;


        #region Commands

        public RelayCommand<object> ProceedCommand 
        {
            get
            {
                return _proceedCommand ?? (_proceedCommand = new RelayCommand<object>(ProceedExecute, ProceedCanExecute));
            }
        }
        private bool ProceedCanExecute(object obj)
        {
            return !String.IsNullOrEmpty(_firstName) &&
                   !String.IsNullOrEmpty(_lastName) &&
                   !String.IsNullOrEmpty(_email);
        }
        private async void ProceedExecute(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                Person currentPerson;
                try
                {
                    Thread.Sleep(1000);
                    currentPerson = new Person(_firstName, _lastName, _email, _birthDate);
                    StationManager.DataStorage.AddPerson(currentPerson);
                }
                catch (CreatingPersonException ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
                if (currentPerson.IsBirthday)
                {
                    MessageBox.Show($"Happy birthday,{_firstName}");
                }
                StationManager.CurrentPerson = currentPerson;
                return true;

            });
            LoaderManager.Instance.HideLoader();
            if (result)
                NavigationManager.Instance.Navigate(ModesEnum.PersonGrid);
        }


        #endregion
        #endregion


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

}
