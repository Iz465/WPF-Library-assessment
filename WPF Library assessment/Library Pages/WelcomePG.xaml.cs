using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPF_Library_assessment.Mongo_Info;
using WPF_Library_assessment.User_Control_Stuff;

namespace WPF_Library_assessment.Library_Pages
{


    public partial class WelcomePG : Page

    {
        public WelcomePG()
        {

            InitializeComponent();


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
        }





        private void dramaImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

            DramaPG dramaPG = new DramaPG();
            mainWindow.Content = dramaPG;

        }



        private void mysteryImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            MysteryPG mysteryPG = new MysteryPG();
            mainWindow.Content = mysteryPG;

        }

        private void historyImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
        private void fantasyImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            FantasyPG fantasyPG = new FantasyPG();
            mainWindow.Content = fantasyPG;

        }
        private void horrorImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            horrorPG horrorPG = new horrorPG();
            mainWindow.Content = horrorPG;

        }
        private void romanceImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            MysteryPG mysteryPG = new MysteryPG();
            mainWindow.Content = mysteryPG;

        }



        private void libraryImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                BookPG bookPG = new BookPG();
                mainWindow.Content = bookPG;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
         

        }

        private void memberImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            MemberPG memberPG = new MemberPG();
            mainWindow.Content= memberPG;
        }




    }

}
