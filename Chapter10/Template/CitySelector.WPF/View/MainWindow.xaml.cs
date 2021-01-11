using $safeprojectname$.Data;
using $safeprojectname$.Service;
using $safeprojectname$.ViewModel;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace $safeprojectname$
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(ILogger<MainWindowViewModel> logger, ICitySelectorService citySelectorService)
        {
            MainWindowViewModel vm = new MainWindowViewModel(logger, citySelectorService);
            this.DataContext = vm;

            InitializeComponent();
        }
    }
}