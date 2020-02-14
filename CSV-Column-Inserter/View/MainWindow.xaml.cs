using CSV_Column_Inserter.ViewModel;
using System.Windows;

namespace CSV_Column_Inserter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class for the Main View.  Also sets the DataContext property for data binding.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
