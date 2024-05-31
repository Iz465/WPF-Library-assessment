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
using System.Windows.Shapes;
using WPF_Library_assessment.User_Control_Stuff;

namespace WPF_Library_assessment.Window_stuff
{

    public partial class UpdateWN : Window
    {
        public UpdateWN()
        {
            InitializeComponent();
        }


        public void changeBook(string collection, string id, string name, string author, string pages, string available)
        {
      
            TextBlock nameBook = new TextBlock();
            TextBlock authorBook = new TextBlock();
            TextBlock pagesBook = new TextBlock();
            TextBlock availableBook = new TextBlock();  

        //    nameBook.Text = name;
        //    nameBook.HorizontalAlignment= HorizontalAlignment.Center;
        //    nameBook.VerticalAlignment= VerticalAlignment.Center;
         //   authorBook.Text = author;
         //   authorBook.HorizontalAlignment= HorizontalAlignment.Center;
         //   pagesBook.Text = pages;
          //  availableBook.Text = available;


            textboxUC nameUC = new textboxUC();
            textboxUC authorUC = new textboxUC();
            textboxUC pagesUC = new textboxUC();
            textboxUC availableUC = new textboxUC();
            
            nameUC.PlaceHolder = name; nameUC.Width = 200; nameUC.Height = 60; nameUC.HorizontalAlignment= HorizontalAlignment.Center;
            authorUC.PlaceHolder = author; authorUC.Width = 200; authorUC.Height = 60; authorUC.HorizontalAlignment= HorizontalAlignment.Center;
            pagesUC.PlaceHolder = pages; pagesUC.Width = 200; pagesUC.Height = 60; pagesUC.HorizontalAlignment= HorizontalAlignment.Center;
            availableUC.PlaceHolder = available; availableUC.Width = 200; availableUC.Height = 60; availableUC.HorizontalAlignment= HorizontalAlignment.Center;

            Grid.SetRow(nameUC,1);
            Grid.SetRow(authorUC,2);
            Grid.SetRow(pagesUC,3);
            Grid.SetRow(availableUC,4);

            updateGrid.Children.Add(nameUC);
            updateGrid.Children.Add(authorUC);
            updateGrid.Children.Add(pagesUC);
            updateGrid.Children.Add(availableUC);

        }

    }

   

}
