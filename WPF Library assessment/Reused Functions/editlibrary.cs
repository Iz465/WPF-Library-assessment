using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPF_Library_assessment.Mongo_Info;

namespace WPF_Library_assessment.Reused_Functions
{
    internal class editlibrary
    {
        MongoData mongoData;
        public void DeleteMember_Click<T>(object sender, RoutedEventArgs e, string collectionName, string ID, Grid gridname) where T : class
        {
            mongoData = new MongoData();
            Button deleteBtn = sender as Button;


            mongoData.DeleteCollection<T>(collectionName, ID);
            int row = Grid.GetRow(deleteBtn);
            gridname.Children.Remove(deleteBtn);

            foreach (UIElement element in gridname.Children.Cast<UIElement>().ToList())
            {
                if (Grid.GetRow(element) == row)
                {
                    gridname.Children.Remove(element);
                }
            }

        }

        }
}
