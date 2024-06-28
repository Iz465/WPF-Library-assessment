using System;
using System.Collections.Generic;
using System.IO.Pipes;
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
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;
using Members = WPF_Library_assessment.Mongo_Info.Members;

namespace WPF_Library_assessment.Library_Pages
{
 
    public partial class MemberPG : Page
    {
        public MemberPG()
        {
            InitializeComponent();

            MemberTitle.Text = "Members";
            MongoData mongoData = new MongoData();
                BookPG bookPG = new BookPG();
                List<Members> members = mongoData.Connect<Members>("Members");
                addMember(members, "Members", 0);
            
        }       
      
       public void addMember(List<Members> memberList, string collectionName, int row)
        {
            BookPG bookPG = new BookPG();

            foreach ( var person in memberList)
            {
               
                bookPG.createRow(MembersGrid,50);
               
                TextBlock Username = bookPG.CreateTextBlock(person.Username);
                TextBlock Password = bookPG.CreateTextBlock(person.Password);
                Button UpdateBtn = bookPG.CreateButton("Update"); UpdateBtn.Click += (sender, e) => UpdateMember_Click(sender, e, person);
                Button DeleteBtn = bookPG.CreateButton("Delete"); DeleteBtn.Click += (sender, e) => DeleteMember_Click(sender, e, "Members", person.Id.ToString());
                bookPG.AddElementToGrid(Username, row, 1, MembersGrid);
                bookPG.AddElementToGrid(Password, row, 2, MembersGrid);
                bookPG.AddElementToGrid(UpdateBtn, row, 3, MembersGrid);
                bookPG.AddElementToGrid(DeleteBtn, row, 4, MembersGrid);
                row++;
                bookPG.createRow(MembersGrid, 20);

                System.Windows.Shapes.Rectangle rectangle = new System.Windows.Shapes.Rectangle(); 
                rectangle.Height = 5;
                rectangle.Fill = Brushes.White;
                Grid.SetColumnSpan(rectangle, 6);
                bookPG.AddElementToGrid(rectangle, row, 0, MembersGrid);
                row++;

            }       
        }


        private void UpdateMember_Click(object sender, RoutedEventArgs e, Members member)
        {
                UpdateMemberWN updateMemberWN = new UpdateMemberWN();
                updateMemberWN.changeMember(member);
                updateMemberWN.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                updateMemberWN.WindowStyle = WindowStyle.None;
                updateMemberWN.Show();
            
        }

        private void DeleteMember_Click(object sender, RoutedEventArgs e, string collectionName, string memberId)
        {
            Button deleteBtn = sender as Button;
            MongoData mongoData = new MongoData();
            
             mongoData.DeleteCollection<Member>(collectionName,memberId);
            int row = Grid.GetRow(deleteBtn);
            MembersGrid.Children.Remove(deleteBtn);

            foreach (UIElement element in MembersGrid.Children.Cast<UIElement>().ToList())
            {
                if (Grid.GetRow(element) == row)
                {
                    MembersGrid.Children.Remove(element);
                }
            }

        }

   



        private void NewMemberBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NewMember newMember = new NewMember();
                newMember.Closed += (s, args) => newMember = null;
                newMember.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                newMember.WindowStyle = WindowStyle.None;
                newMember.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }



        private void MemberSearchBtn_Click(object sender, RoutedEventArgs e)
    {
            FilterMembers(SearchMember.Text.ToLower());
        }

        public void FilterMembers(string findMember)
        {

            MongoData mongoData = new MongoData();
                 List<Members> members = mongoData.Connect<Members>("Members");


            List<Members> searchedMembers = members
                .Where(member => member.Username.ToLower().Contains(findMember) || member.Password.ToLower().Contains(findMember))
                .ToList();
            MemberTitle.Text = "Searched Members";
            MembersGrid.Children.Clear();
            MembersGrid.RowDefinitions.Clear();
            addMember(searchedMembers, "", 0);
        }




    }
}

