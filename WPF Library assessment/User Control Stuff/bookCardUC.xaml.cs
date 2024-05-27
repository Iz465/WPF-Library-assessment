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
using YourNamespace;

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

        public static readonly DependencyProperty ImageDetails =
            DependencyProperty.Register("Image", typeof(string), typeof(bookCardUC));

        public string Image
        { 
            get { return (string)GetValue(ImageDetails); }
            set 
            { 
                 SetValue(ImageDetails, value); 
         ImageSource = convertImage.ConvertBase64ToImage(value);
               
             
            } 
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(BitmapImage), typeof(bookCardUC));

        public BitmapImage ImageSource
        {
            get { return (BitmapImage)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); } }




        public static readonly DependencyProperty NameDetails =
             DependencyProperty.Register("Name", typeof(string), typeof(bookCardUC));

        public string Name
        {
            get { return (string)GetValue(NameDetails); }
            set { SetValue(NameDetails, value); }
        }

        public static readonly DependencyProperty AuthorDetails = 
            DependencyProperty.Register("Author", typeof (string), typeof(bookCardUC));

        public string Author
        {
            get { return (string)GetValue(AuthorDetails); }
            set { SetValue(AuthorDetails, value); }
        }

        public static readonly DependencyProperty PagesDetails =
            DependencyProperty.Register("Pages", typeof (int), typeof (bookCardUC));

        public int Pages
        {
            get { return (int)GetValue(PagesDetails); }
            set { SetValue(PagesDetails, value); }
        }

        public static readonly DependencyProperty AvailableDetails =
          DependencyProperty.Register("Available", typeof(string), typeof(bookCardUC));

        public string Available
        {
            get { return (string)GetValue(AvailableDetails); }
            set { SetValue(AvailableDetails, value); }
        }




    }
}
