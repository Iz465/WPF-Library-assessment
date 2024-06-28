using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WPF_Library_assessment.Library_Pages;
using WPF_Library_assessment.Mongo_Info;
using WPF_Library_assessment.Window_stuff;
using static WPF_Library_assessment.Window_stuff.signInWn;

namespace WPF_Library_assessment.User_Control_Stuff
{
    public partial class bookCardUC : UserControl
    {
        public bookCardUC()
        {
            InitializeComponent();
        }


        public void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var user = signInWn.SessionManager.CurrentUser;

            if (user is Members members)
            {
                var border = sender as Border;
                var id = (border?.DataContext as Books)?.Id.ToString();

                MongoData mongoData = new MongoData();
                var database = mongoData.GetMongoDatabase();

                var collections = mongoData.getCollections();
                bool bookOrder = false;

                try
                {
                    foreach (var collectionName in collections)
                    {
                        if (bookOrder == true)
                            break;

                        IMongoCollection<Books> collection = database.GetCollection<Books>(collectionName);
                        var filter = Builders<Books>.Filter.Eq("_id", ObjectId.Parse(id));
                        var book = collection.Find(filter).FirstOrDefault();

                        if (book != null)
                        {
                            if (book.Available == "Yes")
                            {
                                var update = Builders<Books>.Update
                                   .Set("Available", "No")
                                   .Set("Owner", members.Username);

                                collection.UpdateOne(filter, update);
                                MessageBox.Show("Book has been ordered!");
                                border.Background = new LinearGradientBrush(Colors.Purple, Colors.Black, 90);
                                Colour(new SolidColorBrush(Colors.White));

                                int timeLeft = book.Time;
                                StartTimer(timeLeft, book, collection);
                                bookOrder = true;
                                break;
                            }

                            if (book.Overdue == "No" && book.Available == "No" && members.Username != book.Owner)
                            {
                                MessageBox.Show("Book is booked, you have now prebooked it");
                                var update = Builders<Books>.Update
                                    .Set("PreBookOwner", members.Username);
                                collection.UpdateOne(filter, update);
                                border.Background = new LinearGradientBrush(Colors.Red, Colors.Black, 90);
                                Colour(new SolidColorBrush(Colors.White));
                                bookOrder = true;
                                break;
                            }
                        }
                    }

               
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        public void Colour(Brush colour)
        {
            colour1.Foreground = colour;
            colour2.Foreground = colour;
            colour3.Foreground = colour;
            colour4.Foreground = colour;
        }

        public void StartTimer(int timeLeft, Books book, IMongoCollection<Books> collection)
        {
            TimeSpan time = TimeSpan.FromSeconds(timeLeft);
            TimerTextBlock.Visibility = Visibility.Visible;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);

            timer.Tick += (sender, args) =>
            {
                if (time == TimeSpan.Zero)
                {
                    timer.Stop();
                    TimerTextBlock.Visibility = Visibility.Collapsed;
                    var filter = Builders<Books>.Filter.Eq("_id", book.Id);
                    var update = Builders<Books>.Update.Set("Overdue", "Yes");
                    collection.UpdateOne(filter, update);
                }
                else
                {
                    time = time.Add(TimeSpan.FromSeconds(-1));
                    var filter = Builders<Books>.Filter.Eq("_id", book.Id);
                    var update = Builders<Books>.Update.Set("Time", time.TotalSeconds);
                    collection.UpdateOne(filter, update);
                    TimerTextBlock.Text = time.ToString(@"hh\:mm\:ss");
                }
            };

            TimerManager.AddTimer(timer);
            timer.Start();
        }

        public static class TimerManager
        {
            private static List<DispatcherTimer> timers = new List<DispatcherTimer>();

            public static void AddTimer(DispatcherTimer timer)
            {
                timers.Add(timer);
            }

            public static void StopAllTimers()
            {
                foreach (var timer in timers)
                {
                    timer.Stop();
                }
                timers.Clear();
            }
        }

        public void prebookConvert(Books book, IMongoCollection<Books> collection, string collectionName, int time)
        {
            var filter = Builders<Books>.Filter.Eq("_id", book.Id);
            var update = Builders<Books>.Update
                .Set("Owner", book.PreBookOwner)
                .Set("PreBookOwner", string.Empty)
                .Set("Available", "No");
            collection.UpdateOne(filter, update);
        }
    }
}
