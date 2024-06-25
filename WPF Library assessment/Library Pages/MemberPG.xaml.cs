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
                //     UpdateBtn.Tag = new Tuple<string, string, string, string, string, string>
                //      (collectionName, person.Id.ToString(), person.FirstName, person.LastName, book.Pages.ToString(), book.Available);
                //    UpdateBtn.Click += UpdateButton_Click;
                //    DeleteBtn.Tag = new Tuple<string, string>(collectionName, book.Id.ToString());
                //    DeleteBtn.Click += DeleteButton_Click;
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

        private void DeleteMember_Click(object sender, RoutedEventArgs e,string collectionName, string memberId)
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
            BookPG bookPG = new BookPG();
            NewMember newMember = new NewMember();
            bookPG.open(newMember);
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

/*   <Page.Resources>
        <Style TargetType="TextBlock" x:Key="SmallHeaderTextStyle">
            <Setter Property="FontSize" Value="28"/>
            <Setter Property="FontWeight" Value="Bold"/>
            <Setter Property="HorizontalAlignment" Value="Center"/>
            <Setter Property="VerticalAlignment" Value="Top"/>
            <Setter Property="Margin" Value="10"/>
            <Setter Property="Foreground" Value="LawnGreen"/>
        </Style>
        <Style TargetType="TextBlock" x:Key="BookInfoLabelStyle">
            <Setter Property="FontSize" Value="16"/>
            <Setter Property="Margin" Value="5"/>
            <Setter Property="VerticalAlignment" Value="Center"/>
            <Setter Property="Foreground" Value="LawnGreen"/>
        </Style>
        <Style TargetType="TextBox" x:Key="BookInfoTextBoxStyle">
            <Setter Property="FontSize" Value="16"/>
            <Setter Property="Margin" Value="9"/>
            <Setter Property="VerticalAlignment" Value="Center"/>
            <Setter Property="Width" Value="300"/>
            <Setter Property="HorizontalAlignment" Value="Left"/>
            <Setter Property="Padding" Value="8"/>
            <Setter Property="Background" Value="White"/>
            <Setter Property="BorderBrush" Value="#D3D3D3"/>
            <Setter Property="BorderThickness" Value="1"/>
            <Setter Property="Foreground" Value="LawnGreen"/>
        </Style>
        <Style TargetType="Button" x:Key="ActionButtonStyle">
            <Setter Property="FontSize" Value="14"/>
            <Setter Property="FontWeight" Value="Bold"/>
            <Setter Property="Width" Value="100"/>
            <Setter Property="Height" Value="30"/>
            <Setter Property="Margin" Value="8"/>
            <Setter Property="HorizontalAlignment" Value="Center"/>
            <Setter Property="Background" Value="DarkOliveGreen"/>
            <Setter Property="Foreground" Value="LawnGreen"/>
        </Style>
            
        <ImageBrush x:Key="PageBackgroundBrush" ImageSource="/images/mback.jpg" Stretch="UniformToFill"/>
        
    </Page.Resources>

    <Grid x:Name="MemberGrid" Margin="10" Opacity="0.9" VerticalAlignment="Center">

        <Grid.Background>
            <ImageBrush ImageSource="..\images\mback.jpg"/>
        </Grid.Background>





        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="Auto"/>
            <ColumnDefinition Width="Auto"/>
            <ColumnDefinition Width="Auto"/>
        </Grid.ColumnDefinitions>

        <!-- Small Header -->
        <TextBlock Text="Member Info" Style="{StaticResource SmallHeaderTextStyle}" Grid.Row="0" Grid.Column="3" Grid.ColumnSpan="3" HorizontalAlignment="Center"/>

        <!-- First Name -->
        <TextBlock Text="First Name:" Style="{StaticResource BookInfoLabelStyle}" Grid.Row="1" Grid.Column="0"/>
        <TextBox x:Name="FirstNameTextBox" Style="{StaticResource BookInfoTextBoxStyle}" Grid.Row="1" Grid.Column="1" Grid.ColumnSpan="2"/>

        <!-- Last Name -->
        <TextBlock Text="Last Name:" Style="{StaticResource BookInfoLabelStyle}" Grid.Row="2" Grid.Column="0"/>
        <TextBox x:Name="LastNameTextBox" Style="{StaticResource BookInfoTextBoxStyle}" Grid.Row="2" Grid.Column="1" Grid.ColumnSpan="2"/>

        <!-- Age -->
        <TextBlock Text="Age:" Style="{StaticResource BookInfoLabelStyle}" Grid.Row="3" Grid.Column="0"/>
        <TextBox x:Name="AgeTextBox" Style="{StaticResource BookInfoTextBoxStyle}" Grid.Row="3" Grid.Column="1" Grid.ColumnSpan="2"/>

        <!-- Phone Number -->
        <TextBlock Text="Phone Number:" Style="{StaticResource BookInfoLabelStyle}" Grid.Row="4" Grid.Column="0"/>
        <TextBox x:Name="PhoneNumberTextBox" Style="{StaticResource BookInfoTextBoxStyle}" Grid.Row="4" Grid.Column="1" Grid.ColumnSpan="2"/>

        <!-- Address -->
        <TextBlock Text="Address:" Style="{StaticResource BookInfoLabelStyle}" Grid.Row="5" Grid.Column="0"/>
        <TextBox x:Name="AddressTextBox" Style="{StaticResource BookInfoTextBoxStyle}" Grid.Row="5" Grid.Column="1" Grid.ColumnSpan="2"/>

        <!-- Username -->
        <TextBlock Text="Username:" Style="{StaticResource BookInfoLabelStyle}" Grid.Row="6" Grid.Column="0"/>
        <TextBox x:Name="UsernameTextBox" Style="{StaticResource BookInfoTextBoxStyle}" Grid.Row="6" Grid.Column="1" Grid.ColumnSpan="2"/>

        <!-- Password -->
        <TextBlock Text="Password:" Style="{StaticResource BookInfoLabelStyle}" Grid.Row="7" Grid.Column="0"/>
        <TextBox x:Name="PasswordTextBox" Style="{StaticResource BookInfoTextBoxStyle}" Grid.Row="7" Grid.Column="1" Grid.ColumnSpan="2"/>

        <!-- Action Buttons -->
        <Button Content="Update" Style="{StaticResource ActionButtonStyle}" Grid.Row="9" Grid.Column="1"/>
        <Button Content="Delete" Style="{StaticResource ActionButtonStyle}" Grid.Row="9" Grid.Column="2"/>
    </Grid> */