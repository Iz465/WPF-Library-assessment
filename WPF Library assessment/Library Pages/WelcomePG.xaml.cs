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
using WPF_Library_assessment.User_Control_Stuff;
using System.IO; // File thing needs it.

namespace WPF_Library_assessment.Library_Pages
{
 

    public partial class WelcomePG : Page

    {
        public WelcomePG()
        {
              
        InitializeComponent();

            MongoData mongoData = new MongoData();
            List<Horror> horror = mongoData.Connect<Horror>("Horror");
              List<bookCardUC> cardList;

        cardList = new List<bookCardUC> { card1, card2 };



            for (int i = 0; i < cardList.Count; i++)
            {
                bookCardUC bookcard = cardList[i % cardList.Count];

                bookcard.GetType().GetProperty("Image").SetValue(bookcard, horror[i].Image);
                bookcard.GetType().GetProperty("Name").SetValue(bookcard, horror[i].Name);
                bookcard.GetType().GetProperty("Author").SetValue(bookcard, horror[i].Author);
                bookcard.GetType().GetProperty("Pages").SetValue(bookcard, horror[i].Pages);
                bookcard.GetType().GetProperty("Available").SetValue(bookcard, horror[i].Available);



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
