using MongoDB.Driver;
using System;
using System.Collections;
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
using WPF_Library_assessment.Reused_Functions;
using WPF_Library_assessment.Window_stuff;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace WPF_Library_assessment.Library_Pages
{
    public partial class BookPG : Page
    {
      
        MongoData mongoData;
        editlibrary editlibrary;

        public BookPG()
        {
            InitializeComponent();
           
                mongoData = new MongoData();
           editlibrary = new editlibrary();
            List<Books> horrorBooks = mongoData.Connect<Books>("Horror");
            GenreTitle.Text = "Horror";
            addInfo(horrorBooks, "Horror", 0);


        }

        public void addInfo(List<Books> bookType, string collectionName, int rowNum)
        {
            foreach (var book in bookType)
            {
               

                createRow(FillGrid, 50);
                TextBlock nameText = CreateTextBlock(book.Name);
                TextBlock authorText = CreateTextBlock(book.Author);
                Button updateButton = CreateButton("Update"); updateButton.Click += (sender, e) => UpdateButton_Click(sender, e, book);
                Button deleteButton = CreateButton("Delete"); deleteButton.Click += (sender, e) => editlibrary.DeleteMember_Click<Books>(sender, e, collectionName, book.Id.ToString(), FillGrid);


                AddElementToGrid(nameText, rowNum, 1, FillGrid);
                AddElementToGrid(authorText, rowNum, 2, FillGrid);
                AddElementToGrid(updateButton, rowNum, 3, FillGrid);
                AddElementToGrid(deleteButton, rowNum, 4, FillGrid);

                rowNum++;
                createRow(FillGrid, 20);

                System.Windows.Shapes.Rectangle rectangle = new System.Windows.Shapes.Rectangle(); // converting it to proper one 
                rectangle.Height = 5;
                rectangle.Fill = Brushes.White;
                Grid.SetColumnSpan(rectangle, 6);
                AddElementToGrid(rectangle, rowNum, 0, FillGrid);
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
                Style = (Style)FindResource("montFontText"),
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
            //  Grid.SetColumnSpan(element, 6);
            gridName.Children.Add(element);
        }

        public void UpdateButton_Click(object sender, RoutedEventArgs e, Books book)
        {
            try
            {
                UpdateWN updateWN = new UpdateWN();
                updateWN.changeBook(book);
                updateWN.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                updateWN.WindowStyle = WindowStyle.None;
                updateWN.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



            

        public void NewBookBtn_Click(object sender, RoutedEventArgs e)
        {
            NewBook newBook = new NewBook();
            open(newBook);
        }

        public void open(Window window)
        {
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.WindowStyle = WindowStyle.None;
            window.Show();
        }

        private void GenreList(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            TextBlock textblock = sender as TextBlock;
            MongoData mongoData = new MongoData();
            FillGrid.Children.Clear();
            FillGrid.RowDefinitions.Clear();
            switch (textblock.Name)
            {
                case "HorrorList": GenreTitle.Text = "Horror"; List<Books> horrorBooks = mongoData.Connect<Books>("Horror"); addInfo(horrorBooks, "Horror", 0); break;
                case "FantasyList": GenreTitle.Text = "Fantasy"; List<Books> fantasyBooks = mongoData.Connect<Books>("Fantasy"); addInfo(fantasyBooks, "Fantasy", 0); break;
                case "ScifiList": GenreTitle.Text = "Sci-Fi"; List<Books> scifiBooks = mongoData.Connect<Books>("Sci-Fi"); addInfo(scifiBooks, "Sci-Fi", 0); break;
                case "MysteryList": GenreTitle.Text = "Mystery"; List<Books> mysteryBooks = mongoData.Connect<Books>("Mystery"); addInfo(mysteryBooks, "Mystery", 0); break;
                case "ComicList": GenreTitle.Text = "Comic"; List<Books> romanceBooks = mongoData.Connect<Books>("Romance"); addInfo(romanceBooks, "Romance", 0); break;
                case "HistoryList": GenreTitle.Text = "History"; List<Books> historyBooks = mongoData.Connect<Books>("History"); addInfo(historyBooks, "History", 0); break;

            }




        }


        private void BookSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            FilterBooks(SearchBook.Text.ToLower());
        }

        public void FilterBooks(string findBook)
        {

            MongoData mongoData = new MongoData();
            List<Books> library = new List<Books>();
            library.AddRange(mongoData.Connect<Books>("Horror"));
            library.AddRange(mongoData.Connect<Books>("Fantasy"));
            library.AddRange(mongoData.Connect<Books>("Sci-Fi"));
            library.AddRange(mongoData.Connect<Books>("Mystery"));
            library.AddRange(mongoData.Connect<Books>("Romance"));
            library.AddRange(mongoData.Connect<Books>("History"));


            List<Books> searchedBooks = library
                .Where(book => book.Name.ToLower().Contains(findBook) || book.Author.ToLower().Contains(findBook))
                .ToList();
            GenreTitle.Text = "Searched Books";
            FillGrid.Children.Clear();
            FillGrid.RowDefinitions.Clear();
            addInfo(searchedBooks, "", 0);
        }


    }
}

// book page above
