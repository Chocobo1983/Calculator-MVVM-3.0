using Calculator_MVVM.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Calculator_MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region DictionaryConnections
        private void Connect(string style)
        {
            style = "styles/" + style;
            Uri uri = new Uri(style, UriKind.Relative);
            ResourceDictionary res = (ResourceDictionary)Application.LoadComponent(uri);
            if(Application.Current.Resources.MergedDictionaries.Count > 1) Application.Current.Resources.MergedDictionaries.RemoveAt(Application.Current.Resources.MergedDictionaries.Count - 1);
            Application.Current.Resources.MergedDictionaries.Add(res);
        }
        private void matrixRb_Checked(object sender, RoutedEventArgs e) => Connect("DarkGreen.xaml");
        private void blueWhiteRb_Checked(object sender, RoutedEventArgs e) => Connect("BlueWhite.xaml");
        private void redRb_Checked(object sender, RoutedEventArgs e) => Connect("Red.xaml");
        private void defaultRb_Checked(object sender, RoutedEventArgs e) => Connect("Default.xaml");

        #endregion
        #region WindowControl
        private void Window_MouseDown(object sender, MouseButtonEventArgs e) { if(e.LeftButton==MouseButtonState.Pressed) DragMove(); }
        private void Close_Click(object sender, RoutedEventArgs e) => Close();
        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }
        private void Minimize_Click(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;









        #endregion
               
    }
}
