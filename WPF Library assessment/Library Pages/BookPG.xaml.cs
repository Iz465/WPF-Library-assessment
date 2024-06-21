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
using WPF_Library_assessment.Mongo_Info;
using WPF_Library_assessment.Window_stuff;

namespace WPF_Library_assessment.Library_Pages
{

    public partial class BookPG : Page
    {
        public BookPG()
        {
            InitializeComponent();
        

            MongoData mongoData = new MongoData();
            List<Books> horrorBooks = mongoData.Connect<Books>("Horror");
            List<Books> fantasyBooks = mongoData.Connect<Books>("Fantasy");
            List<Books> DramaBooks = mongoData.Connect<Books>("Drama");
            List<Books> MysteryBooks = mongoData.Connect<Books>("Mystery");

            int row = 4;
             addInfo(horrorBooks, "Horror", "Fantasy", row); row++;
             addInfo(fantasyBooks, "Fantasy", "Drama", row); row++;
             addInfo(DramaBooks, "Drama", "Mystery", row); row++;
             addInfo(MysteryBooks, "Mystery", "", row);
        }
        public async Task addInfo(List<Books> bookType, string collectionName, string genre, int rowNum)
        {


            foreach (var book in bookType)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {


                    createRow(InfoGrid);
                    TextBlock nameText = CreateTextBlock(book.Name);
                    TextBlock authorText = CreateTextBlock(book.Author);
                    TextBlock pagesText = CreateTextBlock(book.Pages.ToString());
                    TextBlock availableText = CreateTextBlock(book.Available);
                    TextBlock overdueText = CreateTextBlock(book.Overdue);
                    TextBlock timeText = CreateTextBlock(book.Time.ToString());
                    Button updateButton = CreateButton("Update");
                    Button deleteButton = CreateButton("Delete");

                    updateButton.Tag = new Tuple<string, string, string, string, string, string>
                           (collectionName, book.Id.ToString(), book.Name, book.Author, book.Pages.ToString(), book.Available);
                    updateButton.Click += UpdateButton_Click;
                    deleteButton.Tag = new Tuple<string, string>(collectionName, book.Id.ToString());
                    deleteButton.Click += DeleteButton_Click;

                    AddElementToGrid(nameText, rowNum, 0, InfoGrid);
                    AddElementToGrid(authorText, rowNum, 1, InfoGrid);
                    AddElementToGrid(pagesText, rowNum, 2, InfoGrid);
                    AddElementToGrid(availableText, rowNum, 3, InfoGrid);
                    AddElementToGrid(overdueText, rowNum, 4, InfoGrid);
                    AddElementToGrid(timeText, rowNum, 5, InfoGrid);
                    AddElementToGrid(updateButton, rowNum, 6, InfoGrid);
                    AddElementToGrid(deleteButton, rowNum, 7, InfoGrid);

                    rowNum++;
                });
            } 

            TextBlock genreText = new TextBlock();
            genreText.Text = genre;
            genreText.FontSize = 30;
            genreText.Margin = new Thickness(0, 0, 0, 10);  
            genreText.HorizontalAlignment = HorizontalAlignment.Center;
            genreText.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetRow(genreText, rowNum);
            Grid.SetColumn(genreText, 1);
            Grid.SetColumnSpan(genreText, 6);
            InfoGrid.Children.Add(genreText);

            
        }

        public void createRow(Grid gridName)
        {

            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(40);
            gridName.RowDefinitions.Add(row);
        
        }


        public TextBlock CreateTextBlock(string text)
        {
            return new TextBlock
            {
                Text = text,
                FontSize = 16,
                Margin = new Thickness(10, 0, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom
            };
        }

        public Button CreateButton(string content)
        {
            return new Button
            {
                Content = new TextBlock { Text = content },
                FontSize = 16,
                Width = 90,
                Height = 90,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Style = (Style)FindResource("mainbutton"),

            };
        }


        public void AddElementToGrid(UIElement element, int row, int column, Grid gridName)
        {
            Grid.SetRow(element, row);
            Grid.SetColumn(element, column);
            gridName.Children.Add(element);
        }




        public void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

            Button? updatebtn = sender as Button;
            Tuple<string, string, string, string, string, string> tagData = updatebtn.Tag as Tuple<string, string, string, string, string, string>;

            string collectionName = tagData.Item1;
            string bookId = tagData.Item2;
            string name = tagData.Item3;
            string author = tagData.Item4;
            string pages = tagData.Item5;
            string available = tagData.Item6;
        //    string overdue = tagData.Item7;
         //   string time = tagData.Item8;
            UpdateWN updateWN = new UpdateWN();
            updateWN.changeBook(collectionName, bookId, name, author, pages, available);

            updateWN.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            updateWN.WindowStyle = WindowStyle.None;
            updateWN.Show();
          

        }




        public void DeleteButton_Click(object sender, RoutedEventArgs e)
        {



            Button? deleteBtn = sender as Button;



            Tuple<string, string> tagData = deleteBtn.Tag as Tuple<string, string>;
            string collectionName = tagData.Item1;
            string bookId = tagData.Item2;

            MongoData mongoData = new MongoData();
            mongoData.DeleteCollection<Books>(collectionName, bookId);


            int row = Grid.GetRow(deleteBtn);
            InfoGrid.Children.Remove(deleteBtn);


            foreach (UIElement element in InfoGrid.Children.Cast<UIElement>().ToList())
            {
                if (Grid.GetRow(element) == row)
                {
                    InfoGrid.Children.Remove(element);
                }
            }
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            NewBook newBook = new NewBook();
            newBook.WindowStartupLocation = (WindowStartupLocation.CenterScreen);
            newBook.WindowStyle = WindowStyle.None;
            newBook.Show();
     
        }
    }
}



