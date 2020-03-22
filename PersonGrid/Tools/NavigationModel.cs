using System;
using PersonGrid.Views;

namespace PersonGrid.Tools 
{
    internal enum ModesEnum
    {
        DataPerson,
        PersonGrid
    }

    internal class NavigationModel
    {
        private readonly IContentWindow _contentWindow;
       
        internal NavigationModel(IContentWindow contentWindow)
        {
            _contentWindow = contentWindow;
        }

        internal void Navigate(ModesEnum mode)
        {
            switch (mode)
            {
                case ModesEnum.PersonGrid:
                    _contentWindow.ContentControl.Content = new PersonGridView();
                    break;
                case ModesEnum.DataPerson:
                    _contentWindow.ContentControl.Content = new DataPersonView();

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

    }
}