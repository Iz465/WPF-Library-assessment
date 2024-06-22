using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task setupBooks()
        {
            MongoData mongoData = new MongoData();
            var database = mongoData.GetMongoDatabase();
            IMongoCollection<Books> collection = database.GetCollection<Books>("Horror");

            var horror = await collection.Find(new BsonDocument()).ToListAsync();
            int columnNum = 0;
            int rowNum = 0;
            addInfo(horror, columnNum, rowNum, fantasyGrid, collection);
        }


        public void addInfo(List<Books> bookType, int columnNum, int rowNum, Grid bookGrid, IMongoCollection<Books> collection)
        {
            foreach (var book in bookType)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    bookCardUC bookcardUC = new bookCardUC();
                    bookcardUC.test(book.ImageSource);
                    bookcardUC.Width = 300;
                    bookcardUC.Height = 595;
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
                    Grid.SetColumn(bookcardUC, columnNum);
                    bookGrid.Children.Add(bookcardUC);
                    columnNum++;
                    if (columnNum >= 4)
                    {
                        columnNum = 0;
                        rowNum++;
                        RowDefinition rowDefinition = new RowDefinition();
                        rowDefinition.Height = GridLength.Auto;
                        bookGrid.RowDefinitions.Add(rowDefinition);
                    }
                });
            }
        }


        public void horrorPGBtn_Click(object sender, RoutedEventArgs e)
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


       
        
        public void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            MongoData.BookReturnNotifier.BooksReturned -= resetPage;
        }
    }
}


// following is for bookcard:             <TextBlock Style="{StaticResource textCardStyle}" Width="300" Height="590" Background="#80FFFFFF" Visibility="Hidden"/>