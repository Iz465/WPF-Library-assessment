using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Xml.Linq;
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
            GenreTitle.Text = "Horror";
            addInfo(horrorBooks, "Horror", 0); 
            // row = addInfo(fantasyBooks, "Fantasy", row); 
            //  row = addInfo(DramaBooks, "Drama", row); 
            //  row = addInfo(MysteryBooks, "Mystery", row);

        }

        public void addInfo(List<Books> bookType, string collectionName,  int rowNum)
        {
            foreach (var book in bookType)
            {
                createRow(FillGrid, 50);
                TextBlock nameText = CreateTextBlock(book.Name);
                TextBlock authorText = CreateTextBlock(book.Author);
                Button updateButton = CreateButton("Update");
                Button deleteButton = CreateButton("Delete");

                updateButton.Tag = new Tuple<string, string, string, string, string, string>
                       (collectionName, book.Id.ToString(), book.Name, book.Author, book.Pages.ToString(), book.Available);
                updateButton.Click += UpdateButton_Click;
                deleteButton.Tag = new Tuple<string, string>(collectionName, book.Id.ToString());
                deleteButton.Click += DeleteButton_Click;


                AddElementToGrid(nameText, rowNum, 1, FillGrid);
                AddElementToGrid(authorText, rowNum, 2, FillGrid);
                AddElementToGrid(updateButton, rowNum, 3, FillGrid);
                AddElementToGrid(deleteButton, rowNum, 4, FillGrid);

                rowNum++;
                createRow(FillGrid, 20);
               
                System.Windows.Shapes.Rectangle rectangle = new System.Windows.Shapes.Rectangle(); // converting it to proper one 
                rectangle.Height = 5;
                rectangle.Fill = Brushes.White;
                Grid.SetRow(rectangle, rowNum);
                Grid.SetColumn(rectangle, 0);
                Grid.SetColumnSpan(rectangle, 6);
                FillGrid.Children.Add(rectangle);
                rowNum++;

            }

    
        }

        public void createRow(Grid gridName, int height)
        {
            
            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(height);
            gridName.RowDefinitions.Add(row);
        
        }

        public TextBlock CreateTextBlock(string text)
        {
            return new TextBlock
            {
                Text = text,
                FontSize = 16,
                Style =(Style)FindResource("montFontText"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
        }

        public Button CreateButton(string content)
        {
            return new Button
            {
                Content = new TextBlock { Text = content },
                Background = Brushes.Black,
                Foreground = Brushes.White,
                FontSize = 16,
                Padding = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Style = (Style)FindResource("codebehindBtn")
            };

        }

        public void AddElementToGrid(UIElement element, int row, int column, Grid gridName)
        {
            Grid.SetRow(element, row);
            Grid.SetColumn(element, column);
        //    Grid.SetColumnSpan(element, 2);
            gridName.Children.Add(element);
        }

        public void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Button updatebtn = sender as Button;
            Tuple<string, string, string, string, string, string> tagData = updatebtn.Tag as Tuple<string, string, string, string, string, string>;

            string collectionName = tagData.Item1;
            string bookId = tagData.Item2;
            string name = tagData.Item3;
            string author = tagData.Item4;
            string pages = tagData.Item5;
            string available = tagData.Item6;

            UpdateWN updateWN = new UpdateWN();
            updateWN.changeBook(collectionName, bookId, name, author, pages, available);

            updateWN.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            updateWN.WindowStyle = WindowStyle.None;
            updateWN.Show();
        }

        public void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteBtn = sender as Button;

            Tuple<string, string> tagData = deleteBtn.Tag as Tuple<string, string>;
            string collectionName = tagData.Item1;
            string bookId = tagData.Item2;

            MongoData mongoData = new MongoData();
            mongoData.DeleteCollection<Books>(collectionName, bookId);

            int row = Grid.GetRow(deleteBtn);
            FillGrid.Children.Remove(deleteBtn);

            foreach (UIElement element in FillGrid.Children.Cast<UIElement>().ToList())
            {
                if (Grid.GetRow(element) == row)
                {
                    FillGrid.Children.Remove(element);
                }
            }
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            NewBook newBook = new NewBook();
            newBook.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            newBook.WindowStyle = WindowStyle.None;
            newBook.Show();
        }

        private void GenreList(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock textblock = sender as TextBlock;
            MongoData mongoData = new MongoData();
            FillGrid.Children.Clear();
            FillGrid.RowDefinitions.Clear();
            switch (textblock.Name)
            {
                case "HorrorList": GenreTitle.Text = "Horror";   List<Books> horrorBooks = mongoData.Connect<Books>("Horror"); addInfo(horrorBooks, "Horror", 0); break;
                case "FantasyList": GenreTitle.Text = "Fantasy"; List<Books> fantasyBooks = mongoData.Connect<Books>("Fantasy");  addInfo(fantasyBooks, "Fantasy", 0); break;
                case "DramaList": GenreTitle.Text = "Drama"; List<Books> dramaBooks = mongoData.Connect<Books>("Drama"); addInfo(dramaBooks, "Drama", 0); break;
                case "MysteryList": GenreTitle.Text = "Mystery"; List<Books> mysteryBooks = mongoData.Connect<Books>("Mystery"); addInfo(mysteryBooks, "Mystery", 0); break;
                case "RomanceList": GenreTitle.Text = "Romance"; List<Books> romanceBooks = mongoData.Connect<Books>("Romance"); addInfo(romanceBooks, "Romance", 0); break;
                case "HistoryList": GenreTitle.Text = "History"; List<Books> historyBooks = mongoData.Connect<Books>("History"); addInfo(historyBooks, "History", 0); break;

            }

        }

    
    }
}
