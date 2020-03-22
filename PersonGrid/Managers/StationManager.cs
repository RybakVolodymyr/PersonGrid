using System;
using System.Windows;
using PersonGrid.DataStorage;
using PersonGrid.Models;

namespace PersonGrid.Managers
{
    internal static class StationManager
    {
        private static IDataStorage _dataStorage;
        internal static Person CurrentPerson { get; set; }

        internal static IDataStorage DataStorage
        {
            get { return _dataStorage; }
        }

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }


        internal static void CloseApp()
        {
            MessageBox.Show("ShutDown");
            Environment.Exit(1);
        }

       
    }
}
