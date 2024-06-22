using MongoDB.Driver;
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

namespace WPF_Library_assessment.Library_Pages
{
    /// <summary>
    /// Interaction logic for MysteryPG.xaml
    /// </summary>
    public partial class MysteryPG : Page
    {
        public MysteryPG()
        {
            InitializeComponent();
            FantasyPG fantasyPG = new FantasyPG();
            MongoData mongoData = new MongoData();
            List<Books> drama = mongoData.Connect<Books>("Mystery");
            var database = mongoData.GetMongoDatabase();
            IMongoCollection<Books> collection = database.GetCollection<Books>("Mystery");
            int columnNum = 0;
            int rowNum = 0;
            fantasyPG.addInfo(drama, columnNum, rowNum, mysteryGrid, collection);
        }
        private void horrorPGBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            WelcomePG welcomePG = new WelcomePG();
            mainWindow.Content = welcomePG;
        }
    }
}
