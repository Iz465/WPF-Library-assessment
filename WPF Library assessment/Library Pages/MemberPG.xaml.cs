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
using WPF_Library_assessment.Window_stuff;
using Members = WPF_Library_assessment.Mongo_Info.Members;

namespace WPF_Library_assessment.Library_Pages
{
    /// <summary>
    /// Interaction logic for MemberPG.xaml
    /// </summary>
    public partial class MemberPG : Page
    {
        public MemberPG()
        {
            InitializeComponent();

            
       //         MongoData mongoData = new MongoData();
        //        List<Members> members = mongoData.Connect<Members>("Members");

         //       AddInfo(members, "Members", 3);
            
        



        }
         /*
      
       public void AddInfo(List<Members> memberList, string collectionName, int row)
        {
            BookPG bookPG = new BookPG();

            foreach ( var person in memberList)
            {
                MessageBox.Show(row.ToString());
                bookPG.createRow(MemberGrid);

                
                TextBlock FirstName = bookPG.CreateTextBlock(person.FirstName);
                TextBlock LastName = bookPG.CreateTextBlock(person.LastName);
                TextBlock Age = bookPG.CreateTextBlock(person.Age.ToString());
                TextBlock Phone = bookPG.CreateTextBlock(person.PhoneNumber.ToString());
                TextBlock Address = bookPG.CreateTextBlock(person.Address);
                TextBlock Username = bookPG.CreateTextBlock(person.Username);
                TextBlock Password = bookPG.CreateTextBlock(person.Password);
                Button UpdateBtn = bookPG.CreateButton("Update");
                Button DeleteBtn = bookPG.CreateButton("Delete");

                


                
                bookPG.AddElementToGrid(FirstName,row, 0, MemberGrid);
                bookPG.AddElementToGrid(LastName, row, 1,MemberGrid);
                bookPG.AddElementToGrid(Age, row, 2, MemberGrid);
                bookPG.AddElementToGrid(Phone, row, 3, MemberGrid);
                bookPG.AddElementToGrid(Address, row, 4, MemberGrid);
                bookPG.AddElementToGrid(Username, row, 5, MemberGrid);
                bookPG.AddElementToGrid(Password, row, 6, MemberGrid);
                bookPG.AddElementToGrid(UpdateBtn, row, 7, MemberGrid);
                bookPG.AddElementToGrid(DeleteBtn, row, 8, MemberGrid);

                row++;

            }


          
        } */



        }
}
