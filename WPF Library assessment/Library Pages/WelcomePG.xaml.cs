using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPF_Library_assessment.Mongo_Info;
using WPF_Library_assessment.User_Control_Stuff;
using WPF_Library_assessment.Window_stuff;

namespace WPF_Library_assessment.Library_Pages
{


    public partial class WelcomePG : Page

    {
        public WelcomePG()
        {

            InitializeComponent();
                             
        }




      

        private void NavigateToPage(Page page)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Content = page;
            }
        }


        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {


            if (sender is genreCard genreCard)

            {
                switch (genreCard.TitleGenre)
                {
                    case "Drama":
                        NavigateToPage(new DramaPG());
                        break;
                    case "mysteryImage":
                        NavigateToPage(new MysteryPG());
                        break;
                    case "historyImage":
                        NavigateToPage(new HistoryPG());
                        break;
                    case "Fantasy":
                        NavigateToPage(new FantasyPG());
                        break;
                    case "Horror":
                        NavigateToPage(new horrorPG());
                        break;
                    case "romanceImage":
                        NavigateToPage(new RomancePG());
                        break;
                    case "libraryImage":
                        NavigateToPage(new BookPG());
                        break;
                    case "memberImage":
                        NavigateToPage(new MemberPG());
                        break;
                    default:
                        break;

                }

            }
            
             
        }




    }

}
// Set the ImageSource to an image path or resource.


//MongoData mongoData = new MongoData();
//List<Books> horror = mongoData.Connect<Books>("Horror");
//List<bookCardUC> cardList;

//  cardList = new List<bookCardUC> { card1, card2 };

//    for (int i = 0; i < cardList.Count; i++)
//    {
//       bookCardUC bookcard = cardList[i % cardList.Count];
//       bookcard.test(horror[i].ImageSource);
//      bookcard.DataContext = horror[i];
//     < UserControl:bookCardUC x:Name = "card1" Grid.Column = "0" Width = "200" />
//    < UserControl:bookCardUC x:Name = "card2" Grid.Column = "1" Width = "200" />



//   bookcard.GetType().GetProperty("Image").SetValue(bookcard, horror[i].Image);
//    bookcard.GetType().GetProperty("Name").SetValue(bookcard, horror[i].Name);
//       bookcard.GetType().GetProperty("Author").SetValue(bookcard, horror[i].Author);
//       bookcard.GetType().GetProperty("Pages").SetValue(bookcard, horror[i].Pages);
//       bookcard.GetType().GetProperty("Available").SetValue(bookcard, horror[i].Available);