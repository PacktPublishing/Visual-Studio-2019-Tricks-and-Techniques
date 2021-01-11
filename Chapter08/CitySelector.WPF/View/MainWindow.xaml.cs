using CitySelector.WPF.Data;
using CitySelector.WPF.Service;
using CitySelector.WPF.ViewModel;
using Microsoft.Extensions.Logging;
using System.Windows;

namespace CitySelector.WPF
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