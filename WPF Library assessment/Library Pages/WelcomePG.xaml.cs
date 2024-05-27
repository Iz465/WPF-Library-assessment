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


          //    card1.Image = horror[0].Image;

            // Inside your WelcomePG constructor or wherever you set the image path
               string imagePath = horror[0].Image; // Assuming this is where you get the image path
            Console.WriteLine("Image Path: " + imagePath); // this will write it in the console itself
                                                           // Check if the file exists
                                                           //     if (File.Exists(imagePath))
                                                           //   {
                                                           // File exists, so it's likely a valid image path
                                                           //     card1.Image = imagePath;
                                                           //     }
                                                           //    else
                                                           //    {
                                                           // File doesn't exist, so the image path might be incorrect
                                                           //        MessageBox.Show("Image file not found at the specified path: " + imagePath);
                                                           //    }

            string knownImagePath = @"C:\Users\isakl\OneDrive\Desktop\library image folder\itHorror.jpg";

            card1.Image = knownImagePath;

            //  card1.Image = horror[0].Image;
              card1.Image = imagePath;
            card1.Name = horror[0].Name;
                card1.Author = horror[0].Author; 
                card1.Pages = horror[0].Pages;
                card1.Available = horror[0].Available;
       //     card2.Image = horror[1].Image;
            card2.Name = horror[1].Name;
                card2.Author = horror[1].Author;
                card2.Pages = horror[1].Pages;
        



        }

        
                              

    }
}
