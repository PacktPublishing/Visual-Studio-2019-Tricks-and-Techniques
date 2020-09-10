using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CitySelector.Wizard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        public MainWindow()
        {
            InitializeComponent();
        }

        public event EventHandler<ConnectionStringSetEventArgs> ConnectionStringSet;

        public string ConnectionString
        {
            set
            {
                EventHandler<ConnectionStringSetEventArgs> handler = ConnectionStringSet;
                handler?.Invoke(this, new ConnectionStringSetEventArgs(value));
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtConnectionString.Text))
            {
                MessageBox.Show($"Oh, come on - at least enter something!");
            }
            else
            {
                ConnectionString = txtConnectionString.Text;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        public class ConnectionStringSetEventArgs : EventArgs
        {
            public ConnectionStringSetEventArgs(string connectionString)
            {
                ConnectionString = connectionString;
            }

            private ConnectionStringSetEventArgs()
            {
            }

            public string ConnectionString { get; set; }
        }
    }
}