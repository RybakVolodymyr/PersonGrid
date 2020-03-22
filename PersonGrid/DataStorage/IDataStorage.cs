using System.Collections.Generic;
using PersonGrid.Models;

namespace PersonGrid.DataStorage
{
     internal interface IDataStorage
    {
        bool PersonExists(string login);

        void AddPerson(Person person);
        void DeletePerson(Person person);
        List<Person> PersonList { get; }
    }
}
