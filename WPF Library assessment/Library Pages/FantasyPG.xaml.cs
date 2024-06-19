using MongoDB.Driver;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPF_Library_assessment.Mongo_Info;
using WPF_Library_assessment.User_Control_Stuff;

namespace WPF_Library_assessment.Library_Pages
{
    public partial class FantasyPG : Page
    {
        public FantasyPG()
        {
            InitializeComponent();
            setupBooks();

           
            MongoData.BookReturnNotifier.BooksReturned += resetPage;
        }

        private void setupBooks()
        {
            MongoData mongoData = new MongoData();
            List<Books> horror = mongoData.Connect<Books>("Horror");
            var database = mongoData.GetMongoDatabase();
            IMongoCollection<Books> collection = database.GetCollection<Books>("Horror");
            int columnNum = 0;
            int rowNum = 2;
            addInfo(horror, columnNum, rowNum, fantasyGrid, collection);
        }

        public void addInfo(List<Books> bookType, int num, int rowNum, Grid bookGrid, IMongoCollection<Books> collection)
        {
            foreach (var book in bookType)
            {
                bookCardUC bookcardUC = new bookCardUC();
                bookcardUC.test(book.ImageSource);
                bookcardUC.Width = 200;
                bookcardUC.Height = 350;
                if (book.Available == "No")
                {
                    bookcardUC.BorderName.Background = new LinearGradientBrush(Colors.Red, Colors.Black, 90);
                    bookcardUC.Colour(new SolidColorBrush(Colors.White));
                    if (book.PreBookOwner == string.Empty)
                    {
                       
                        bookcardUC.BorderName.Background = new LinearGradientBrush(Colors.Purple, Colors.Black, 90);
                       
                    }
                    bookcardUC.StartTimer(book.Time, book, collection);
                }
                bookcardUC.DataContext = book;
                Grid.SetRow(bookcardUC, rowNum);
                Grid.SetColumn(bookcardUC, num);
                bookGrid.Children.Add(bookcardUC);
                num++;

                if (num >= 7)
                {
                    num = 0;
                    rowNum++;
                    RowDefinition rowDefinition = new RowDefinition();
                    rowDefinition.Height = GridLength.Auto;
                    bookGrid.RowDefinitions.Add(rowDefinition);
                }
            }
        }

        private void horrorPGBtn_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            WelcomePG welcomePG = new WelcomePG();
            mainWindow.Content = welcomePG;
        }

        public void resetPage()
        {
            fantasyGrid.Children.Clear();
            fantasyGrid.RowDefinitions.Clear();
            setupBooks();
        }


       
        // Unsubscribe from the event when the page is unloaded
        public void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            MongoData.BookReturnNotifier.BooksReturned -= resetPage;
        }
    }
}
