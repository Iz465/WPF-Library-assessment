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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Library_assessment.Window_stuff;

namespace WPF_Library_assessment.User_Control_Stuff
{

    public partial class headerUC : UserControl
    {
        public headerUC()
        {
            InitializeComponent();
            var user = signInWn.SessionManager.CurrentUser;

            if (user is WPF_Library_assessment.Window_stuff.Members members)
            {
                userName.Text = members.Username;
            }
        }


        private void userImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //     userListBox.Visibility= Visibility.Visible;
            UsersBooks users = new UsersBooks();
            {
                users.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                users.WindowStyle = WindowStyle.None;
            };
            users.Show();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = searchBox.Text;

            if (!string.IsNullOrEmpty(query))
            {
                BookInfoWN bookInfoWindow = new BookInfoWN();
                bookInfoWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                bookInfoWindow.Show();
            }
            else
            {
                MessageBox.Show("Please enter a search term.", "Search", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

    }
}
