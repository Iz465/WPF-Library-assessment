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
            DataContext = this;

            MongoData mongoData = new MongoData();
            List<Books> horrorBooks = mongoData.Connect<Books>("Horror");
            List<Books> fantasyBooks = mongoData.Connect<Books>("Fantasy");
            List<Books> DramaBooks = mongoData.Connect<Books>("Drama");
            List<Books> MysteryBooks = mongoData.Connect<Books>("Mystery");


            int row = addInfo(horrorBooks, "Horror", "Fantasy", 4); row++;
            row = addInfo(fantasyBooks, "Fantasy", "Drama", row); row++;
            row = addInfo(DramaBooks, "Drama", "Mystery", row); row++;
            row = addInfo(MysteryBooks, "Mystery", "", row);
        }
        public int addInfo(List<Books> bookType, string collectionName, string genre, int rowNum)
        {


            foreach (var book in bookType)       // Goes through each book in collection
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(40);
                InfoGrid.RowDefinitions.Add(row);



                TextBlock nameText = new TextBlock(); // makes a textblock with the name nameText
                nameText.Text = book.Name;
                nameText.FontSize = 16;
                nameText.Margin = new Thickness(10, 0, 0, 0);
                nameText.HorizontalAlignment = HorizontalAlignment.Center;
                nameText.VerticalAlignment = VerticalAlignment.Center;

                TextBlock authorText = new TextBlock();
                authorText.Text = book.Author;
                authorText.FontSize = 16;
                authorText.Margin = new Thickness(10, 0, 0, 0);
                authorText.HorizontalAlignment = HorizontalAlignment.Center;
                authorText.VerticalAlignment = VerticalAlignment.Center;

                TextBlock pagesText = new TextBlock();
                pagesText.Text = book.Pages.ToString();
                pagesText.FontSize = 16;
                pagesText.Margin = new Thickness(10, 0, 0, 0);
                pagesText.HorizontalAlignment = HorizontalAlignment.Center;
                pagesText.VerticalAlignment = VerticalAlignment.Center;

                TextBlock availableText = new TextBlock();
                availableText.Text = book.Available;
                availableText.FontSize = 16;
                availableText.Margin = new Thickness(10, 0, 0, 0);
                availableText.HorizontalAlignment = HorizontalAlignment.Center;
                availableText.VerticalAlignment = VerticalAlignment.Center;

                TextBlock overdueText = new TextBlock();
                overdueText.Text = book.Overdue;
                overdueText.FontSize = 16;
                overdueText.Margin = new Thickness(10, 0, 0, 0);
                overdueText.HorizontalAlignment = HorizontalAlignment.Center;
                overdueText.VerticalAlignment = VerticalAlignment.Center;

                TextBlock timeText = new TextBlock();
                timeText.Text = book.Time.ToString();
                timeText.FontSize = 16;
                timeText.Margin = new Thickness(10, 0, 0, 0);
                timeText.HorizontalAlignment = HorizontalAlignment.Center;
                timeText.VerticalAlignment = VerticalAlignment.Center;


                Button updateButton = new Button();
                TextBlock updateText = new TextBlock();
                updateText.Text = "Update";
                updateButton.Content = updateText;
                updateButton.FontSize = 16;
                updateButton.Width = 90;
                updateButton.Height = 90;
                updateButton.HorizontalAlignment = HorizontalAlignment.Center;
                updateButton.VerticalAlignment = VerticalAlignment.Center;
                updateButton.Style = (Style)FindResource("mainbutton");
                updateButton.Tag = new Tuple<string, string, string, string, string, string>
                       (collectionName, book.Id.ToString(), book.Name, book.Author, book.Pages.ToString(), book.Available);
                updateButton.Click += UpdateButton_Click;

                Button deleteButton = new Button();
                TextBlock buttonText = new TextBlock();
                buttonText.Text = "Delete";
                deleteButton.Content = buttonText;
                deleteButton.FontSize = 16;
                deleteButton.Width = 90;
                deleteButton.Height = 90;
                deleteButton.HorizontalAlignment = HorizontalAlignment.Center;
                deleteButton.VerticalAlignment = VerticalAlignment.Center;
                deleteButton.Style = (Style)FindResource("mainbutton");
                deleteButton.Tag = new Tuple<string, string>(collectionName, book.Id.ToString());
                deleteButton.Click += DeleteButton_Click;



                Grid.SetRow(nameText, rowNum);
                Grid.SetColumn(nameText, 0);

                Grid.SetRow(authorText, rowNum);
                Grid.SetColumn(authorText, 1);

                Grid.SetRow(pagesText, rowNum);
                Grid.SetColumn(pagesText, 2);

                Grid.SetRow(availableText, rowNum);
                Grid.SetColumn(availableText, 3);

                Grid.SetRow(overdueText, rowNum);
                Grid.SetColumn(overdueText, 4);

                Grid.SetRow(timeText, rowNum);
                Grid.SetColumn(timeText, 5);

                Grid.SetRow(updateButton, rowNum);
                Grid.SetColumn(updateButton, 6);

                Grid.SetRow(deleteButton, rowNum);
                Grid.SetColumn(deleteButton, 7);

                InfoGrid.Children.Add(nameText);
                InfoGrid.Children.Add(authorText);
                InfoGrid.Children.Add(pagesText);
                InfoGrid.Children.Add(availableText);
                InfoGrid.Children.Add(overdueText);
                InfoGrid.Children.Add(timeText);
                InfoGrid.Children.Add(updateButton);
                InfoGrid.Children.Add(deleteButton);


                rowNum++;
            }
       //     RowDefinition Genrerow = new RowDefinition();
       //     Genrerow.Height = new GridLength(40);
        //    InfoGrid.RowDefinitions.Add(Genrerow);
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

            return rowNum;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
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




        private void DeleteButton_Click(object sender, RoutedEventArgs e)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewBook newBook = new NewBook();
            newBook.WindowStartupLocation = (WindowStartupLocation.CenterScreen);
            newBook.WindowStyle = WindowStyle.None;
            newBook.Show();
     

        }
    }
}



/*
 
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WPF_Library_assessment.Mongo_Info;
using WPF_Library_assessment.Window_stuff;

namespace WPF_Library_assessment.Library_Pages
{
    public partial class BookPG : Page
    {
        public BookPG()
        {
            InitializeComponent();
            DataContext = this;

            MongoData mongoData = new MongoData();
            List<Books> horrorBooks = mongoData.Connect<Books>("Horror");
            List<Books> fantasyBooks = mongoData.Connect<Books>("Fantasy");
            List<Books> dramaBooks = mongoData.Connect<Books>("Drama");
            List<Books> mysteryBooks = mongoData.Connect<Books>("Mystery");

            
                int row = 4; 
                row = addInfo(horrorBooks, "Horror", "Fantasy", row); 
                row = addInfo(fantasyBooks, "Fantasy", "Drama", row);
                row = addInfo(dramaBooks, "Drama", "Fantasy", row);
                row = addInfo(mysteryBooks, "Mystery", "", row); 
            
          
        }
    
        public int addInfo(List<Books> bookType, string collectionName, string genre, int rowNum)
        {
            foreach (var book in bookType) // Goes through each book in collection
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(40);
                InfoGrid.RowDefinitions.Add(row);

                TextBlock nameText = CreateTextBlock(book.Name);
                TextBlock authorText = CreateTextBlock(book.Author);
                TextBlock pagesText = CreateTextBlock(book.Pages.ToString());
                TextBlock availableText = CreateTextBlock(book.Available);
                TextBlock overdueText = CreateTextBlock(book.Overdue);
                TextBlock timeText = CreateTextBlock(book.Time.ToString());

                Button updateButton = CreateButton("Update", new Tuple<string, string, string, string, string, string>(collectionName, book.Id.ToString(), book.Name, book.Author, book.Pages.ToString(), book.Available), UpdateButton_Click);
                Button deleteButton = CreateButton("Delete", new Tuple<string, string>(collectionName, book.Id.ToString()), DeleteButton_Click);

                AddElementToGrid(nameText, rowNum, 0);
                AddElementToGrid(authorText, rowNum, 1);
                AddElementToGrid(pagesText, rowNum, 2);
                AddElementToGrid(availableText, rowNum, 3);
                AddElementToGrid(overdueText, rowNum, 4);
                AddElementToGrid(timeText, rowNum, 5);
                AddElementToGrid(updateButton, rowNum, 6);
                AddElementToGrid(deleteButton, rowNum, 7);

                rowNum++;
            }
            
        //    RowDefinition Genrerow = new RowDefinition();
      //      Genrerow.Height = new GridLength(70);
       //    InfoGrid.RowDefinitions.Add(Genrerow);
            TextBlock genreText = new TextBlock();
            genreText.Text = genre;
            genreText.Height = 100;
            genreText.Width = 200;
            genreText.FontSize = 30;
            genreText.Margin = new Thickness(100, 0, 0, 0);
           // genreText.Padding = new Thickness(0, 0, 0, 20);
            genreText.HorizontalAlignment = HorizontalAlignment.Center;
    //      genreText.VerticalAlignment= VerticalAlignment.Center;


            Grid.SetRow(genreText, rowNum);
            Grid.SetColumn(genreText, 0);
            Grid.SetColumnSpan(genreText, 8);
            InfoGrid.Children.Add(genreText); 
            return rowNum;
        }

        private TextBlock CreateTextBlock(string text)
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

        private Button CreateButton(string content, object tag, RoutedEventHandler clickHandler)
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
                Tag = tag
            }.Apply(btn => btn.Click += clickHandler);
        }

        private void AddElementToGrid(UIElement element, int row, int column)
        {
            Grid.SetRow(element, row);
            Grid.SetColumn(element, column);
            InfoGrid.Children.Add(element);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Button updateBtn = sender as Button;
            var tagData = (Tuple<string, string, string, string, string, string,string>)updateBtn.Tag;
               
            string collectionName = tagData.Item1;
            string bookId = tagData.Item2;  
            string name = tagData.Item3;
            string author = tagData.Item4;
            string pages = tagData.Item5;
            string available = tagData.Item6;
           // string overdue = tagData.Item7;
         //   int time = tagData.Item8;

            UpdateWN updateWN = new UpdateWN();
            updateWN.changeBook(collectionName, bookId, name, author, pages, available);
            updateWN.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            updateWN.WindowStyle = WindowStyle.None;
            updateWN.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button deleteBtn = sender as Button;
            var tagData = (Tuple<string, string>)deleteBtn.Tag;
            string collectionName = tagData.Item1;
            string bookId = tagData.Item2;

            MongoData mongoData = new MongoData();
            mongoData.DeleteCollection<Books>(collectionName, bookId);

            int row = Grid.GetRow(deleteBtn);
            var elementsToRemove = InfoGrid.Children.Cast<UIElement>().Where(el => Grid.GetRow(el) == row).ToList();
            foreach (var element in elementsToRemove)
            {
                InfoGrid.Children.Remove(element);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewBook newBook = new NewBook
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                WindowStyle = WindowStyle.None
            };
            newBook.Show();
        }
    }

    public static class Extensions
    {
        public static T Apply<T>(this T self, Action<T> action)
        {
            action(self);
            return self;
        }
    }
}






  

/* 
    RowDefinition Genrerow = new RowDefinition();
            Genrerow.Height = new GridLength(40);
            InfoGrid.RowDefinitions.Add(Genrerow);
            TextBlock genreText = new TextBlock();
            genreText.Text = genre;
            genreText.FontSize = 30;
            genreText.Margin = new Thickness(0, 0, 0, 20);
            genreText.Padding = new Thickness(0, 0, 0, 10);
            genreText.HorizontalAlignment = HorizontalAlignment.Center;
      //      genreText.VerticalAlignment= VerticalAlignment.Center;
           

            Grid.SetRow(genreText, rowNum);
            Grid.SetColumn(genreText, 0);
            Grid.SetColumnSpan(genreText, 8);
            InfoGrid.Children.Add(genreText);

 
  */


//  public int addInfo(List<Books> bookType, string collectionName, string genre, int rowNum)


