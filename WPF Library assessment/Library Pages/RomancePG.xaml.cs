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
 
    public partial class RomancePG : Page, MongoData.reloadPage
    {
        public RomancePG()
        {
            InitializeComponent();
            setup();
            MongoData.BookReturnNotifier.BooksReturned += resetPage;
        }
  
        public void resetPage()
        {
            romanceGrid.Children.Clear();
            romanceGrid.RowDefinitions.Clear();
            setup();
        }

        public void setup()
        {
            FantasyPG fantasyPG = new FantasyPG();
            MongoData mongoData = new MongoData();
            List<Books> drama = mongoData.Connect<Books>("Romance");
            var database = mongoData.GetMongoDatabase();
            IMongoCollection<Books> collection = database.GetCollection<Books>("Romance");
            int columnNum = 0;
            int rowNum = 0;
            fantasyPG.addInfo(drama, columnNum, rowNum, romanceGrid, collection);
        }

    }
}
