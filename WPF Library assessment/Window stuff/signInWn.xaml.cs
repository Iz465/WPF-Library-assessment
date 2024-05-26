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


        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            textboxUC usernameTextBox = UserName as textboxUC;
            textboxUC passwordTextBox = Password as textboxUC;
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            MongoData mongoData = new MongoData();
            List<Members> members = mongoData.Connect<Members>("Members");
            List<Admin> admin = mongoData.Connect<Admin>("Administrator");


            bool muCheck = checkInfo(username, "Username", members);
            bool mpCheck = checkInfo(password, "Password", members);
            bool auCheck = checkInfo(username, "Username", admin);
            bool apCheck = checkInfo(password, "Password", admin);

            if ((muCheck && mpCheck) || (auCheck && apCheck))
            {
                this.Close();

                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null)
                {
                    WelcomePG welcomePG = new WelcomePG();
                    mainWindow.Content = welcomePG;
                }
             
            }
            else
            {
                MessageBox.Show("Incorrect Login Details");
            }

            }


        private bool checkInfo<T>(string info, string type, List<T> items)
        {
            var property = typeof(T).GetProperty(type);
            return items.Any(item => property.GetValue(item)?.ToString() == info);
        }


        private void closeSignInBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

      
        }

    
}
