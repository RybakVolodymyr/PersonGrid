using System.Windows.Controls;
using PersonGrid.DataStorage;
using PersonGrid.Tools;
using PersonGrid.ViewModels;
using PersonGrid.Managers;

namespace PersonGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IContentWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;
            InitializeApplication ();
            mainWindowViewModel.StartApplication();

        }

        private void InitializeApplication()
        {
            StationManager.Initialize(new SerializedDataStorage());
            var navigationModel = new NavigationModel(this);
            NavigationManager.Instance.Initialize(navigationModel);
        }

        public ContentControl ContentControl
        {   
            get { return _contentControl; }
        }
    }
}
