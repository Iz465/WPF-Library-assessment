using Amazon.Runtime.Internal;
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

namespace WPF_Library_assessment.Library_Pages
{
    /// <summary>
    /// Interaction logic for LibraryInfo.xaml
    /// </summary>
    public partial class LibraryInfo : Page
    {
        public LibraryInfo()
        {
            InitializeComponent();
        }

        private void MemberInfoBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BookInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            BookInfoWN bookInfoWN= new BookInfoWN();
            bookInfoWN.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            bookInfoWN.WindowStyle = WindowStyle.None;
            bookInfoWN.Show();

       
        }

        private void ModifyBooksBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ModifyMembersBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
