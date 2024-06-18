using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WPF_Library_assessment.Mongo_Info;
using WPF_Library_assessment.Library_Pages;
using System.Data;
using System.Windows.Media;
using System.Linq;

namespace WPF_Library_assessment.Window_stuff
{
    public partial class BookInfoWN : Window
    {


        public BookInfoWN()
        {
            InitializeComponent();
            DataContext = this;


            LoadBookData();
        }


        private void LoadBookData()
        { 

         


            MongoData mongoData = new MongoData();
            List<Books> horrorBooks = mongoData.Connect<Books>("Horror");
            List<Books> fantasyBooks = mongoData.Connect<Books>("Fantasy");
            List<Books> DramaBooks = mongoData.Connect<Books>("Drama");
            List<Books> MysteryBooks = mongoData.Connect<Books>("Mystery");


            InfoGrid.Children.Clear();
            InfoGrid.RowDefinitions.Clear();


            int row = 0;
            row = addInfo(horrorBooks,"Horror", "Fantasy", row); row++;
            row = addInfo(fantasyBooks,"Fantasy", "Drama", row); row++;
            row = addInfo(DramaBooks,"Drama", "Mystery", row); row++;
            row = addInfo(MysteryBooks,"Mystery", "", row);




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

                Button updateButton = new Button();
                TextBlock updateText = new TextBlock();
                updateText.Text = "Update";
                updateButton.Content = updateText;
                updateButton.FontSize= 16;
                updateButton.Width = 70;
                updateButton.Height = 30;
                updateButton.HorizontalAlignment = HorizontalAlignment.Center;
                updateButton.VerticalAlignment = VerticalAlignment.Center;
                updateButton.Style = (Style)FindResource("RoundedButtonStyle");
                updateButton.Tag = new Tuple<string, string, string, string, string, string>
                       (collectionName, book.Id.ToString(), book.Name, book.Author, book.Pages.ToString(), book.Available);
                updateButton.Click += UpdateButton_Click;

                Button deleteButton = new Button();
                TextBlock buttonText = new TextBlock();
                buttonText.Text = "Delete";         
                deleteButton.Content = buttonText;
                deleteButton.FontSize = 16;
                deleteButton.Width = 70;
                deleteButton.Height = 30;
                deleteButton.HorizontalAlignment = HorizontalAlignment.Center;
                deleteButton.VerticalAlignment = VerticalAlignment.Center;
                deleteButton.Style = (Style)FindResource("RoundedButtonStyle");
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


                Grid.SetRow(updateButton, rowNum);
                Grid.SetColumn(updateButton, 4);

                Grid.SetRow(deleteButton, rowNum);
                Grid.SetColumn(deleteButton, 5);

                InfoGrid.Children.Add(nameText);
                InfoGrid.Children.Add(authorText);
                InfoGrid.Children.Add(pagesText);
                InfoGrid.Children.Add(availableText);
                InfoGrid.Children.Add(updateButton);
                InfoGrid.Children.Add(deleteButton);

                rowNum++;
            }
            RowDefinition Genrerow = new RowDefinition();
            Genrerow.Height = new GridLength(40);
            InfoGrid.RowDefinitions.Add(Genrerow);
            TextBlock genreText = new TextBlock();
            genreText.Text = genre;
            genreText.FontSize = 30;
            genreText.Margin = new Thickness(0, 0, 0, 10);
            genreText.Padding = new Thickness(0, 0, 0, 10);
            genreText.HorizontalAlignment = HorizontalAlignment.Center;
            genreText.VerticalAlignment = VerticalAlignment.Center;

            Grid.SetRow(genreText, rowNum);
            Grid.SetColumn(genreText, 1);
            Grid.SetColumnSpan(genreText, 3);
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
                UpdateWN updateWN = new UpdateWN();
       //         updateWN.changeBook(collectionName, bookId, name, author, pages, available);

                updateWN.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                updateWN.WindowStyle = WindowStyle.None;
                updateWN.Show();
                this.Close();  

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



        public void HandleSearchQuery(string searchQuery)
        {
            MongoData mongoData = new MongoData();
            List<Books> searchResults = mongoData.SearchBooks<Books>("LoadBooksData",searchQuery);

            // Clear existing UI
            InfoGrid.Children.Clear();
            InfoGrid.RowDefinitions.Clear();

            int rowNum = 0;
            foreach (var book in searchResults)
            {
                rowNum = addInfo(new List<Books> { book }, "", "", rowNum);
            }
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewBook newBook = new NewBook();
            newBook.WindowStartupLocation =(WindowStartupLocation.CenterScreen);
            newBook.WindowStyle = WindowStyle.None;
            newBook.Show();
            this.Close();

        }
    }
}
