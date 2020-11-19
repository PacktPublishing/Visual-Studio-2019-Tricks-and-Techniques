using CGHClientServer1.WPF.Service;
using CGHClientServer1.WPF.ViewModel;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace CGHClientServer1.WPF
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