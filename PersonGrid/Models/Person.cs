using System;
using System.IO;
using System.Text.RegularExpressions;
using PersonGrid.Managers;
using PersonGrid.Tools;

namespace PersonGrid.Models
{
    [Serializable]
    internal class Person
    {
        #region Fields

        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthDate;

        private readonly string[] _westernSigns =
        {
            "Capricorn", "Aquarius", "Pisces", "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra",
            "Scorpio", "Sagittarius"
        };

        private readonly string[] _chineseSigns =
            {"Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig"};

        #endregion


        #region Properties

        public string FirstName

        {
            get { return _firstName; }
            set
            {
                Regex regex = new Regex(@"^[a-zA-Z'-]+$");
                Match match = regex.Match(value);
                if (match.Success)
                {
                    _firstName = value;

                    if (StationManager.DataStorage.PersonList != null)
                        SerializationManager.Serialize(StationManager.DataStorage.PersonList,
                            FileFolderHelper.StorageFilePath);
                }
                else
                    throw new InvalidNameException(value);
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                Regex regex = new Regex(@"^[a-zA-Z'-]+$");
                Match match = regex.Match(value);
                if (match.Success)
                {
                    _lastName = value;
                    if (StationManager.DataStorage.PersonList != null)
                        SerializationManager.Serialize(StationManager.DataStorage.PersonList,
                            FileFolderHelper.StorageFilePath);
                }
                else
                    throw new InvalidNameException(value);
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                Regex regex = new Regex(@"^[a-zA-Z][a-zA-Z0-9._]*@([a-zA-Z]+[.])[a-zA-Z]+$");
                Match match = regex.Match(value);
                if (match.Success)
                {
                    _email = value;
                    if (StationManager.DataStorage.PersonList != null)
                        SerializationManager.Serialize(StationManager.DataStorage.PersonList,
                            FileFolderHelper.StorageFilePath);
                }
                else
                    throw new InvalidEmailException(value);
            }
        }

        public DateTime BirthDate
        {
            get { return _birthDate; }
            set
            {
                int v = (DateTime.Today.DayOfYear >= value.DayOfYear ? 0 : 1);
                var age = (DateTime.Today.Year - value.Year) - v;
                var diff = DateTime.Today - value;
                if (diff.Days < 0)
                {
                    throw new FutureBirthdayException(value);
                }
                else
                {
                    _birthDate = value;
                    if (StationManager.DataStorage.PersonList != null)
                        SerializationManager.Serialize(StationManager.DataStorage.PersonList,
                            FileFolderHelper.StorageFilePath);
                }

                if (age >= 135)
                {
                    throw new TooPastBirthdayException(value);
                }
                else
                {
                    _birthDate = value;
                    if (StationManager.DataStorage.PersonList != null)
                        SerializationManager.Serialize(StationManager.DataStorage.PersonList,
                            FileFolderHelper.StorageFilePath);
                }
            }
        }

        public string ChineseSign => _chineseSigns[(BirthDate.Year + 8) % 12];
        public bool IsBirthday => DateTime.Today.Month == _birthDate.Month && DateTime.Today.Day == _birthDate.Day;

        public bool IsAdult => (DateTime.Today.Year - _birthDate.Year) -
                               (DateTime.Today.DayOfYear >= _birthDate.DayOfYear ? 0 : 1) >= 18;

        public string SunSign
        {
            get
            {
                var m = _birthDate.Month;
                var d = _birthDate.Day;
                var westernSign = "";
                switch (m)
                {
                    case 1:
                        westernSign = d >= 20 ? _westernSigns[1] : _westernSigns[0];
                        break;
                    case 2:
                        westernSign = d >= 19 ? _westernSigns[2] : _westernSigns[1];
                        break;
                    case 3:
                        westernSign = d >= 21 ? _westernSigns[3] : _westernSigns[2];
                        break;
                    case 4:
                        westernSign = d >= 20 ? _westernSigns[4] : _westernSigns[3];
                        break;
                    case 5:
                        westernSign = d >= 21 ? _westernSigns[5] : _westernSigns[4];
                        break;
                    case 6:
                        westernSign = d >= 21 ? _westernSigns[6] : _westernSigns[5];
                        break;
                    case 7:
                        westernSign = d >= 23 ? _westernSigns[7] : _westernSigns[6];
                        break;
                    case 8:
                        westernSign = d >= 23 ? _westernSigns[8] : _westernSigns[7];
                        break;
                    case 9:
                        westernSign = d >= 23 ? _westernSigns[9] : _westernSigns[8];
                        break;
                    case 10:
                        westernSign = d >= 23 ? _westernSigns[10] : _westernSigns[9];
                        break;
                    case 11:
                        westernSign = d >= 22 ? _westernSigns[11] : _westernSigns[10];
                        break;
                    case 12:
                        westernSign = d >= 22 ? _westernSigns[0] : _westernSigns[11];
                        break;
                }

                return westernSign;
            }
        }

        #endregion

        #region Constructor

        public Person(string firstName, string lastName, string email, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
        }

        public Person(string firstName, string lastName, string email) : this(firstName, lastName, email,
            new DateTime(2009, 9, 4))
        {
        }

        public Person(string firstName, string lastName, DateTime birthDate) : this(firstName, lastName, " ", birthDate)
        {
        }

        #endregion
    }

    #region Exceptions

    public class CreatingPersonException : Exception
    {
        public CreatingPersonException(string message)
            : base(message)
        {
        }
    }

    public class NullPersonException : CreatingPersonException
    {
        public NullPersonException()
            : base($"Person does not exist!")
        {
        }
    }

    public class InvalidEmailException : CreatingPersonException
    {
        public InvalidEmailException(string email)
            : base($"Email {email} is not valid!")
        {
        }
    }

    public class InvalidNameException : CreatingPersonException
    {
        public InvalidNameException(string name)
            : base($"{name} is not valid value")
        {
        }
    }

    public class FutureBirthdayException : CreatingPersonException
    {
        public FutureBirthdayException(DateTime birthDate)
            : base($"{birthDate.ToShortDateString()} is in future!")
        {
        }
    }

    public class TooPastBirthdayException : CreatingPersonException
    {
        public TooPastBirthdayException(DateTime birthDate)
            : base($"{birthDate.ToShortDateString()} was more than 135 years ago!")
        {
        }
    }

    #endregion
}