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
using WPF_Library_assessment.User_Control_Stuff;

namespace WPF_Library_assessment.Library_Pages
{

    public partial class horrorPG : Page, MongoData.reloadPage
    {
        public horrorPG()
        {
            InitializeComponent();
            setup();  
            MongoData.BookReturnNotifier.BooksReturned += resetPage;

        }

      public void resetPage()
        {
            horrorGrid.Children.Clear();
            horrorGrid.RowDefinitions.Clear();
            setup();

        }

        public void setup()
        {
            MongoData mongoData = new MongoData();
            List<Books> horror = mongoData.Connect<Books>("Horror");
            var database = mongoData.GetMongoDatabase();
            IMongoCollection<Books> collection = database.GetCollection<Books>("Horror");
            int columnNum = 0;
            int rowNum = 0;
            FantasyPG fantasyPG = new FantasyPG();
            fantasyPG.addInfo(horror, columnNum, rowNum, horrorGrid, collection);

        }
       
    }

}