﻿using System.Windows;
using WPF_Library_assessment.Library_Pages;
using WPF_Library_assessment.Window_stuff;

namespace WPF_Library_assessment
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

        private void signInBtn_Click(object sender, RoutedEventArgs e)
        {
            signInWn signin = new signInWn();
            signin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            signin.WindowStyle = WindowStyle.None;
            signin.Show();
            
        }

      private void signUpBtn_Click(object sender, RoutedEventArgs e)
      {
            SignUpWN signup = new SignUpWN();
            signup.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            signup.WindowStyle = WindowStyle.None;
            signup.Show();
        }

        private void headerUC_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
    

