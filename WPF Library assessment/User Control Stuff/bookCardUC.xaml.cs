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

namespace WPF_Library_assessment.User_Control_Stuff
{
    /// <summary>
    /// Interaction logic for bookCardUC.xaml
    /// </summary>
    public partial class bookCardUC : UserControl
    {
        public bookCardUC()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly DependencyProperty FirstNameProperty =
             DependencyProperty.Register("FirstName", typeof(string), typeof(bookCardUC));

        public string FirstName
        {
            get { return (string)GetValue(FirstNameProperty); }
            set { SetValue(FirstNameProperty, value); }
        }

        public static readonly DependencyProperty LastNameProperty = 
            DependencyProperty.Register("LastName", typeof (string), typeof(bookCardUC));

        public string LastName
        {
            get { return (string)GetValue(LastNameProperty); }
            set { SetValue(LastNameProperty, value); }
        }

        public static readonly DependencyProperty UsernameProperty =
            DependencyProperty.Register("Username", typeof (string), typeof (bookCardUC));

        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }
        


    }
}
