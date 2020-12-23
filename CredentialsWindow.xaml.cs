﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Organizer
{
    /// <summary>
    /// Interaction logic for CredentialsWindow.xaml
    /// </summary>
    public partial class CredentialsWindow : Window
    {
        public CredentialsWindow()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Login sucessful!");

            Center centerWindow = new Center();
            this.Hide();
            centerWindow.ShowDialog();

            //https://www.youtube.com/watch?v=NX8-LhgFnUU
        }

        private void xButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("REGISTER dummy");
        }
    }
}
