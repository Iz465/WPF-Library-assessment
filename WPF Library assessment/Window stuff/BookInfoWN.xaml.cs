using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WPF_Library_assessment.Mongo_Info;
using WPF_Library_assessment.Library_Pages;
using System.Data;

namespace WPF_Library_assessment.Window_stuff
{
    public partial class BookInfoWN : Window
    {

      
        public BookInfoWN()
        {
            InitializeComponent();
            DataContext = this;

            MongoData mongoData = new MongoData();
            List<Books> horrorBooks = mongoData.Connect<Books>("Horror");
            List<Books> booktest = mongoData.Connect<Books>("Fantasy");
               
;
           int row = addInfo(horrorBooks, "Fantasy", 3);
            row = addInfo(booktest, "Drama", row);




        }

        public int addInfo(List<Books> bookType, string genre, int rowNum) 
        {
            ; //row book info will start

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

                TextBlock authorText = new TextBlock();
                authorText.Text = book.Author;
                authorText.FontSize = 16;
                authorText.Margin = new Thickness(10, 0, 0, 0);
                authorText.HorizontalAlignment = HorizontalAlignment.Center;

                TextBlock pagesText = new TextBlock();
                pagesText.Text = book.Pages.ToString();
                pagesText.FontSize = 16;
                pagesText.Margin = new Thickness(10, 0, 0, 0);
                pagesText.HorizontalAlignment = HorizontalAlignment.Center;

                TextBlock availableText = new TextBlock();
                availableText.Text = book.Available;
                availableText.FontSize = 16;
                availableText.Margin = new Thickness(10, 0, 0, 0);
                availableText.HorizontalAlignment = HorizontalAlignment.Center;


                Grid.SetRow(nameText, rowNum);
                Grid.SetColumn(nameText, 0);

                Grid.SetRow(authorText, rowNum);
                Grid.SetColumn(authorText, 1);

                Grid.SetRow(pagesText, rowNum);
                Grid.SetColumn(pagesText, 2);

                Grid.SetRow(availableText, rowNum);
                Grid.SetColumn(availableText, 3);

                InfoGrid.Children.Add(nameText);
                InfoGrid.Children.Add(authorText);
                InfoGrid.Children.Add(pagesText);
                InfoGrid.Children.Add(availableText);

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
            genreText.HorizontalAlignment= HorizontalAlignment.Center;
            genreText.VerticalAlignment= VerticalAlignment.Center;
            
            Grid.SetRow(genreText,rowNum);
            Grid.SetColumn(genreText, 1);
            Grid.SetColumnSpan(genreText, 2);
            InfoGrid.Children.Add(genreText);

            return rowNum;
        }

    }
}
