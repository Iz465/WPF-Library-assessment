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

namespace WPF_Library_assessment.Library_Pages
{

    public partial class FantasyPG : Page
    {
        public FantasyPG()
        {
            InitializeComponent();

            MongoData mongoData = new MongoData();
            List<Books> horror = mongoData.Connect<Books>("Horror");
            int columnNum = 0;
            int rowNum = 2;
            addInfo(horror, columnNum, rowNum, fantasyGrid);


        }


        public void addInfo(List<Books> bookType, int num, int rowNum, Grid bookGrid)
        {


            foreach (var book in bookType)
            {

                bookCardUC bookcardUC = new bookCardUC();

                bookcardUC.test(book.ImageSource);
                bookcardUC.Width = 200;
                bookcardUC.Height = 350;
                bookcardUC.DataContext = book;
                Grid.SetRow(bookcardUC, rowNum);
                Grid.SetColumn(bookcardUC, num);

                bookGrid.Children.Add(bookcardUC);
                num++;

                if (num >= 7)
                {
                    num = 0;
                    rowNum++;
                    RowDefinition rowDefinition = new RowDefinition();
                    rowDefinition.Height = GridLength.Auto;
                    bookGrid.RowDefinitions.Add(rowDefinition);

                }




            }


        }

        private void horrorPGBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            WelcomePG welcomePG = new WelcomePG();
            mainWindow.Content = welcomePG;
        }
    }



}


/*  <UserControl:bookCardUC x:Name="card1" Width="200"/>
            <UserControl:bookCardUC x:Name="card2" Grid.Column="1" Width="200"/>
            <UserControl:bookCardUC x:Name="card3" Grid.Column="2" Width="200"/>
            <UserControl:bookCardUC x:Name="card4" Grid.Column="3" Width="200"/>
            <UserControl:bookCardUC x:Name="card5" Grid.Column="4" Width="200"/>
            <UserControl:bookCardUC x:Name="card6" Grid.Column="5" Width="200"/>
            <UserControl:bookCardUC x:Name="card7" Grid.Column="6" Width="200"/>
*/




/* 
   cardList = new List<bookCardUC> { card1, card2};

        try
        {
            for (int i = 0; i < cardList.Count; i++)
            {
                bookCardUC bookcard = cardList[i % cardList.Count];
                bookcard.test(horror[i].ImageSource);
                bookcard.DataContext = horror[i];
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }



 */
