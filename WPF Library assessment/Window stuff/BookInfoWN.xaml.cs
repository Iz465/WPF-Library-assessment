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
            List<Horror> horrorBooks = mongoData.Connect<Horror>("Horror");

            
            int rowNum = 3; //row book info will start

            // Goes through each book in collection
            foreach (var horrorBook in horrorBooks) 
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(40);
                InfoGrid.RowDefinitions.Add(row);

                TextBlock nameText = new TextBlock(); // makes a textblock with the name nameText
                nameText.Text = horrorBook.Name;
                nameText.FontSize = 16;
                nameText.Margin = new Thickness(10, 0, 0, 0);
                nameText.HorizontalAlignment = HorizontalAlignment.Center;

                TextBlock authorText = new TextBlock();
                authorText.Text = horrorBook.Author;
                authorText.FontSize = 16;
                authorText.Margin = new Thickness(10, 0, 0, 0);
                authorText.HorizontalAlignment = HorizontalAlignment.Center;

                TextBlock pagesText = new TextBlock();
                pagesText.Text = horrorBook.Pages.ToString();
                pagesText.FontSize = 16;
                pagesText.Margin = new Thickness(10, 0, 0, 0);
                pagesText.HorizontalAlignment = HorizontalAlignment.Center;

                TextBlock availableText = new TextBlock();
                availableText.Text = horrorBook.Available;
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
        }
    }
}
