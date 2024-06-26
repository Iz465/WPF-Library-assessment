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
                             
            var user = signInWn.SessionManager.CurrentUser;
            if (user is WPF_Library_assessment.Mongo_Info.Admin admin )
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                ImageGrid.ColumnDefinitions.Add(columnDefinition);
                ImageSource memberInfoImage = new BitmapImage(new Uri("pack://application:,,,/Library Pages/members-icon.jpg"));
                ImageSource bookInfoImage = new BitmapImage(new Uri("pack://application:,,,/Library Pages/Books.jpg"));

                adminPage("Member Info",0,3, memberInfoImage );
                adminPage("Book Info", 1,3, bookInfoImage);
            }

  
        }

       


        private void adminPage(string name, int rowNum, int colNum, ImageSource image)
        {
    

            genreCard genreCard = new genreCard();
            genreCard.TitleGenre = name; 
            genreCard.Height = 400;
            genreCard.Width = 400;
            genreCard.Margin = new Thickness(0,10,5,0);
            genreCard.MouseLeftButtonDown += Image_MouseLeftButtonDown;

            Grid.SetRow(genreCard, rowNum);
            Grid.SetColumn(genreCard, colNum);
            ImageGrid.Children.Add(genreCard);
            genreCard.ImageGenre = image;
        }

      

        public void NavigateToPage(Page page)
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
                    case "Sci-Fi":
                        NavigateToPage(new DramaPG());
                        break;
                    case "Mystery":
                        NavigateToPage(new MysteryPG());
                        break;
                    case "History":
                        NavigateToPage(new HistoryPG());
                        break;
                    case "Fantasy":
                            NavigateToPage(new FantasyPG());                 
                        break;
                    case "Horror":
                        NavigateToPage(new horrorPG());
                        break;
                    case "Graphic Novel":
                        NavigateToPage(new RomancePG());
                        break;
                    case "Book Info":
                            NavigateToPage(new BookPG());
                        break;
                    case "Member Info":
                        try
                        {
                            NavigateToPage(new MemberPG());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
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