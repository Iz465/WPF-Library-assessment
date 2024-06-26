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
using WPF_Library_assessment.Library_Pages;
using WPF_Library_assessment.Window_stuff;

namespace WPF_Library_assessment.User_Control_Stuff
{

    public partial class headerUC : UserControl
    {
        public headerUC()
        {
            InitializeComponent();
            this.DataContext = this;
            var user = signInWn.SessionManager.CurrentUser;

            if (user is WPF_Library_assessment.Mongo_Info.Members members)
            {
                userName.Text = members.Username;
            }
        }

        public static readonly DependencyProperty HeaderTextProperty =
          DependencyProperty.Register("HeaderText", typeof(string), typeof(headerUC), new PropertyMetadata(string.Empty));

        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }


        private void userImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            UsersBooks users = new UsersBooks();
            {
                users.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                users.WindowStyle = WindowStyle.None;
            };
            users.Show();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
          
        }

     

        private void BackPageBtn_Click(object sender, RoutedEventArgs e)
        {
            WelcomePG welcomePG = new WelcomePG();
          
            switch (HeaderText)
            {
                case "":
                    MainWindow mainWindow = new MainWindow();
                    Application.Current.MainWindow = mainWindow;
                    mainWindow.Show();

                    Window.GetWindow(this).Close(); break;
                default: welcomePG.NavigateToPage(new WelcomePG());  break;
            }
         
         
        }
    }
}
