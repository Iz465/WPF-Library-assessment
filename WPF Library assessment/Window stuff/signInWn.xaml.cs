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
  
    public partial class signInWn : Window
    {
        public signInWn()
        {
            InitializeComponent();
        }


        private void closeSignInBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {

         
            textboxUC usernameTextBox = UserName as textboxUC;
            textboxUC passwordTextBox = Password as textboxUC;
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            bool uCheck = checkInfo(username, "Username");
            bool pCheck = checkInfo(password, "Password");

            if (!uCheck || !pCheck)
            {
                MessageBox.Show("Incorrect Login Details");
            }

            else
            {
                this.Close();

                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null)
                {
                    WelcomePG welcomePG = new WelcomePG();
                    mainWindow.Content = welcomePG;
                }
            }
        }

        private bool checkInfo(string info, string type)
        {
       
            MongoData mongoData = new MongoData();
            mongoData.MembersConnect();
            var property = typeof(Members).GetProperty(type);
            bool check = mongoData.members.Any(members => property.GetValue(members).ToString() == info);
         
            return check;
         
        }
    }
}
