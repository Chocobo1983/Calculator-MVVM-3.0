﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(res);
        }
        private void matrix_rb_Checked(object sender, RoutedEventArgs e) => Connect("Matrix.Xaml");
        private void snowhite_rb_Checked(object sender, RoutedEventArgs e) => Connect("Snowhite.xaml");
        private void bloody_rb_Checked(object sender, RoutedEventArgs e) => Connect("Bloody.xaml");

        #endregion

        #region WindowControl
        private void Window_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();
        private void btn_Close_Click(object sender, RoutedEventArgs e) => Close();
        private void btn_restore_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }
        private void btn_hide_Click(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;
        #endregion
    }
}