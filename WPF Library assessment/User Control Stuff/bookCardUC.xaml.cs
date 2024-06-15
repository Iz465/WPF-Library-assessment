using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
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
using System.Windows.Threading;
using System.Xml.Linq;
using WPF_Library_assessment.Library_Pages;
using WPF_Library_assessment.Mongo_Info;
using WPF_Library_assessment.Window_stuff;
using YourNamespace;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;
using static WPF_Library_assessment.Window_stuff.signInWn;

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
            DependencyProperty.Register("Author", typeof(string), typeof(bookCardUC));

        public string Author
        {
            get { return (string)GetValue(AuthorDetails); }
            set { DataContext = this; SetValue(AuthorDetails, value); }
        }

        public static readonly DependencyProperty PagesDetails =
            DependencyProperty.Register("Pages", typeof(int), typeof(bookCardUC));

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




        public void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var user = signInWn.SessionManager.CurrentUser;

            if (user is WPF_Library_assessment.Window_stuff.Members members)
            {
                var border = sender as Border;
                var id = (border.DataContext as Books)?.Id.ToString();

                MongoData mongoData = new MongoData();
                var database = mongoData.GetMongoDatabase();

                IMongoCollection<Books> collection = database.GetCollection<Books>("Horror");

               
                var filter = Builders<Books>.Filter.Eq("_id", ObjectId.Parse(id));
                var book = collection.Find(filter).FirstOrDefault();

                if (book.Available == "Yes")
                {

                    var update = Builders<Books>.Update
                       .Set("Available", "No")
                       .Set("Owner", members.Username);


                    collection.UpdateOne(filter, update);
                    MessageBox.Show("Book has been ordered!");
                    border.Background = new LinearGradientBrush(Colors.Red, Colors.Black, 90);


                    int timeLeft = book.Time;
                    string name = book.Genre;

                    StartTimer(timeLeft, name, book, collection);
                }
              else  if (book.Overdue == "No" && book.Available == "Yes")
                {
                   MessageBox.Show("Book is booked, you have now prebooked it");
                }
                

            }

        

          
        }

    
           
        


        



        public void StartTimer(int timeLeft, string collectionName, Books book, IMongoCollection<Books> collection)
        {

      
            
          
           // var border = sender as Border;

            TimeSpan time = TimeSpan.FromSeconds(timeLeft);
            TimerTextBlock.Visibility = Visibility.Visible;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
       //    bookCardUC bookCard = new bookCardUC();
       //     bookCard.BorderName.Background = new LinearGradientBrush(Colors.Purple, Colors.Black, 90);

        

            timer.Tick += (sender, args) =>
            {
                if (time == TimeSpan.Zero)
                {
                    timer.Stop();

                    TimerTextBlock.Visibility = Visibility.Collapsed;
                    //  border.Background = new LinearGradientBrush(Colors.Red, Colors.Black, 90);
                    var filter = Builders<Books>.Filter.Eq("_id", book.Id);
                    var update = Builders<Books>.Update
                        .Set("Overdue", "Yes");
                      collection.UpdateOne(filter, update);




                }
                else
                {

                    time = time.Add(TimeSpan.FromSeconds(-1));
                    var filter = Builders<Books>.Filter.Eq("_id", book.Id);
                    var update = Builders<Books>.Update.Set("Time", time.TotalSeconds);
                    collection.UpdateOne(filter, update);
                    TimerTextBlock.Text = time.ToString();
                }
                };
            timer.Start();
        }
     


    }
}
