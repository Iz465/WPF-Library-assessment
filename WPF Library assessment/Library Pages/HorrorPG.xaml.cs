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

    public partial class HorrorPG : Page
    {
        public HorrorPG()
        {
            InitializeComponent();

            MongoData mongoData = new MongoData();
            List<Books> horror = mongoData.Connect<Books>("Horror");
            List<bookCardUC> cardList;
            int columnNum = 0;
            int rowNum = 2;
            FantasyPG fantasyPG = new FantasyPG();
            fantasyPG.addInfo(horror, columnNum, rowNum, horrorGrid);

        }

        private void horrorPGBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            WelcomePG welcomePG = new WelcomePG();
            mainWindow.Content = welcomePG;
        }

       
    }

}
