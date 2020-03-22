using System.Windows.Controls;
using PersonGrid.ViewModels;

namespace PersonGrid.Views
{
    /// <summary>
    /// Interaction logic for DataPersonView.xaml
    /// </summary>
    public partial class DataPersonView : UserControl
    {
        public DataPersonView()
        {
            InitializeComponent();
            DataContext = new DateViewModel();

        }
    }
}
