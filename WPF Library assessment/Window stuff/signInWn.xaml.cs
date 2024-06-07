using System;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Library_assessment.Library_Pages;
using WPF_Library_assessment.Mongo_Info;
using WPF_Library_assessment.User_Control_Stuff;

namespace WPF_Library_assessment.Window_stuff
{
    /// <summary>
    /// Interaction logic for signInWn.xaml.cs
    /// </summary>
    public partial class signInWn : Window
    {
        public signInWn()
        {
            InitializeComponent();
        }

        private void textemail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mytextemail.Focus();
        }

        private void mytextemail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(mytextemail.Text) && mytextemail.Text.Length > 0)
            {
                mytextemail.Visibility = Visibility.Collapsed;
            }
            else
            {
                mytextemail.Visibility = Visibility.Visible;
            }
        }

        private void textpassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtpassword.Focus();
        }

        private void txtpassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtpassword.Password) && txtpassword.Password.Length > 0)
            {
                txtpassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtpassword.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(mytextemail.Text) && !string.IsNullOrEmpty(txtpassword.Password))
            {
                MessageBox.Show("Signed In successfully");
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}