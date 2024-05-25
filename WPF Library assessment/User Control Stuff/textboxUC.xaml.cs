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
    
    public partial class textboxUC : UserControl
    {
        public textboxUC()
        {
            InitializeComponent();
        }

        public string Text
        { get { return txtInput.Text; }
            set { txtInput.Text = value; } 
        }

          

        private string placeHolder;

        public string PlaceHolder
        {
            get { return placeHolder; }
            set { placeHolder = value;
                tbPlaceHolder.Text = placeHolder;
            }
        }



        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Clear();
            txtInput.Focus();
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtInput.Text)) // checks if their is text in it. if their is noe this will happen
            {
                tbPlaceHolder.Visibility = Visibility.Visible; // makes placeholder text seen as no user input text is in,
            }
            else
            {
                tbPlaceHolder.Visibility = Visibility.Hidden; // makes it hidden as soon as user types in it.
            }
        }
    }
}
