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
  
    public partial class HistoryPG : Page, MongoData.reloadPage
    {
        public HistoryPG()
        {
            InitializeComponent();
            setup();
            MongoData.BookReturnNotifier.BooksReturned += resetPage;
        }
    
        public void resetPage()
      
        {
            historyGrid.Children.Clear();
            historyGrid.RowDefinitions.Clear();
            setup();
        }

        private void setup()
        {
            FantasyPG fantasyPG = new FantasyPG();
            MongoData mongoData = new MongoData();
            List<Books> drama = mongoData.Connect<Books>("History");
            var database = mongoData.GetMongoDatabase();
            IMongoCollection<Books> collection = database.GetCollection<Books>("History");
            int columnNum = 0;
            int rowNum = 0;
            fantasyPG.addInfo(drama, columnNum, rowNum, historyGrid, collection);
        }

    }

}
