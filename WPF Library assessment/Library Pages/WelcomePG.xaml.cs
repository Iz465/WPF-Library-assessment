using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WPF_Library_assessment.Mongo_Info;
using WPF_Library_assessment.User_Control_Stuff;

namespace WPF_Library_assessment.Library_Pages
{


    public partial class WelcomePG : Page

    {
        public WelcomePG()
        {
              
        InitializeComponent();

            MongoData mongoData = new MongoData();
            List<Books> horror = mongoData.Connect<Books>("Horror");
              List<bookCardUC> cardList;

        cardList = new List<bookCardUC> { card1, card2 };

            for (int i = 0; i < cardList.Count; i++)
            {
                bookCardUC bookcard = cardList[i % cardList.Count];
                bookcard.DataContext = horror[i];
     //           bookcard.DataContext = horror[i].Author;
            ///    bookcard.Image = horror[i].Image;
           //     bookcard.Available = horror[i].Available;

                //       bookcard.GetType().GetProperty("Image").SetValue(bookcard, horror[i].Image);
                //       bookcard.GetType().GetProperty("Name").SetValue(bookcard, horror[i].Name);
                //       bookcard.GetType().GetProperty("Author").SetValue(bookcard, horror[i].Author);
                //       bookcard.GetType().GetProperty("Pages").SetValue(bookcard, horror[i].Pages);
                //       bookcard.GetType().GetProperty("Available").SetValue(bookcard, horror[i].Available);
            }






        }


        private void MemberBtn_Click(object sender, RoutedEventArgs e)
        {
        
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                LibraryInfo libraryInfo = new LibraryInfo();
                mainWindow.Content = libraryInfo;
            }
        }
    }
}
