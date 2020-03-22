using System.Windows.Controls;
using PersonGrid.ViewModels;

namespace PersonGrid.Views
{
    /// <summary>
    /// Interaction logic for PersonGridView.xaml
    /// </summary>
    public partial class PersonGridView : UserControl
    {
        public PersonGridView()
        {
            InitializeComponent();
            DataContext = new EditGridViewModel();

        }
    }
}
