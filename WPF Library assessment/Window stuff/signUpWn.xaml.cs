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
using System.Windows.Shapes;
using WPF_Library_assessment.Library_Pages;

namespace WPF_Library_assessment.Window_stuff
{
    /// <summary>
    /// Interaction logic for signUpWn.xaml
    /// </summary>
    public partial class signUpWn : Window
    {
        public signUpWn()
        {
            InitializeComponent();
        }

        private void closeSignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
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
}
