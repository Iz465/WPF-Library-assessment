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
 
    public partial class WelcomePG : Page
    {
        public WelcomePG()
        {
            InitializeComponent();

            MongoData mongoData = new MongoData();
            List<Admin> admin = mongoData.Connect<Admin>("Administrator");

         
                card1.FirstName = admin[0].FirstName;
                card1.LastName = admin[0].LastName;   
                card1.Username = admin[0].Username;
                card2.FirstName = admin[1].FirstName;
                card2.LastName = admin[1].LastName;
                card2.Username = admin[1].Username;



        }

        
                              

    }
}
