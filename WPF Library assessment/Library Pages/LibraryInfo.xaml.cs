using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Library_assessment.Window_stuff;

namespace WPF_Library_assessment.Library_Pages
{

    public partial class LibraryInfo : Page
    {
        public LibraryInfo()
        {
            InitializeComponent();
        }


        private void MemberInfo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MemberInfoWN memberInfo = new MemberInfoWN();
            memberInfo.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            memberInfo.WindowStyle = WindowStyle.None;
            memberInfo.Show();
        }

        private void BookInfo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BookInfoWN bookInfoWN = new BookInfoWN();
            bookInfoWN.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            bookInfoWN.WindowStyle = WindowStyle.None;
            bookInfoWN.Show();
        }
    }
}
