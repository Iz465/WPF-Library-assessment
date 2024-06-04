using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using WPF_Library_assessment.Mongo_Info;
using YourNamespace;

namespace WPF_Library_assessment.User_Control_Stuff
{
    
    public partial class bookCardUC : UserControl
    {
        public bookCardUC()
        {
            InitializeComponent();
        
        }

      
        public void test(string s)
        {
         //   MessageBox.Show(s);
            ImageSource = convertImage.ConvertBase64ToImage(s);
        } 

        public static readonly DependencyProperty ImageSourceProperty =
       DependencyProperty.Register("ImageSource", typeof(BitmapImage), typeof(bookCardUC));

        public BitmapImage ImageSource
        {
            get { return (BitmapImage)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }



        public static readonly DependencyProperty NameDetails =
             DependencyProperty.Register("Name", typeof(string), typeof(bookCardUC));

        public string Name
        {
            get { return (string)GetValue(NameDetails); }
            set { DataContext = this; SetValue(NameDetails, value); }
        }

        public static readonly DependencyProperty AuthorDetails = 
            DependencyProperty.Register("Author", typeof (string), typeof(bookCardUC));

        public string Author
        {
            get { return (string)GetValue(AuthorDetails); }
            set { DataContext = this; SetValue(AuthorDetails, value); }
        }

        public static readonly DependencyProperty PagesDetails =
            DependencyProperty.Register("Pages", typeof (int), typeof (bookCardUC));

        public int Pages
        {
            get { return (int)GetValue(PagesDetails); }
            set { DataContext = this; SetValue(PagesDetails, value); }
        }

        public static readonly DependencyProperty AvailableDetails =
          DependencyProperty.Register("Available", typeof(string), typeof(bookCardUC));

        public string Available
        {
            get { return (string)GetValue(AvailableDetails); }
            set { DataContext = this; SetValue(AvailableDetails, value); }
        }




        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
                var border = sender as Border;
                var id = (border.DataContext as Books)?.Id.ToString(); 
           
                    MongoData mongoData = new MongoData();
                    var database = mongoData.GetMongoDatabase();
                    IMongoCollection<Books> collection = database.GetCollection<Books>("Horror");

                    var filter = Builders<Books>.Filter.Eq("_id", ObjectId.Parse(id));

                    var update = Builders<Books>.Update
                        .Set("Available", "No");

                    collection.UpdateOne(filter, update);

                    MessageBox.Show("Book has been ordered!");
                
              
            
          
        }




    }
}
