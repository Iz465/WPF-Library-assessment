using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace WPF_Library_assessment.Window_stuff
{   /// <summary>
    /// Interaction logic for MemberInfoWN.xaml
    /// </summary>
    /// 
    public partial class MemberInfoWN : Window
    {
        
    //    private ObservableCollection<Member> members = new ObservableCollection<Member>();

        public MemberInfoWN()
        {
            InitializeComponent();

            // Bind the members collection to the ListBox
        //    MembersListBox.ItemsSource = members;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
           
       //     Member newMember = new Member()
            {
            //    FirstName = FirstNameTextBox.Text,
          ///      LastName = LastNameTextBox.Text,
          //      Email = EmailTextBox.Text,
          //      Age = int.Parse(AgeTextBox.Text),
          //      PhoneNumber = PhoneNumberTextBox.Text,
          //      Address = AddressTextBox.Text,
          //      Username = UsernameTextBox.Text,
          //      Password = PasswordBox.Password
            };

            // Add the new member 
         //   members.Add(newMember);

            // Clear the text boxes
            ClearTextBoxes();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (MembersListBox.SelectedItem != null)
            {
                
         //       members.Remove((Member)MembersListBox.SelectedItem);
            }
            else
            {
                MessageBox.Show("Please select a member to delete.");
            }
        }

        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearTextBoxes()
        {
            
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            EmailTextBox.Text = "";
            AgeTextBox.Text = "";
            PhoneNumberTextBox.Text = "";
            AddressTextBox.Text = "";
            UsernameTextBox.Text = "";
            PasswordBox.Password = "";
        }
    }

    // Define a class to represent a member
  
}







/*
 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace WPF_Library_assessment.Window_stuff
{   /// <summary>
    /// Interaction logic for MemberInfoWN.xaml
    /// </summary>
    /// 
    public partial class MemberInfoWN : Window
    {
        
        private ObservableCollection<Member> members = new ObservableCollection<Member>();

        public MemberInfoWN()
        {
            InitializeComponent();

            // Bind the members collection to the ListBox
            MembersListBox.ItemsSource = members;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
           
            Member newMember = new Member()
            {
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                Email = EmailTextBox.Text,
                Age = int.Parse(AgeTextBox.Text),
                PhoneNumber = PhoneNumberTextBox.Text,
                Address = AddressTextBox.Text,
                Username = UsernameTextBox.Text,
                Password = PasswordBox.Password
            };

            // Add the new member 
            members.Add(newMember);

            // Clear the text boxes
            ClearTextBoxes();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (MembersListBox.SelectedItem != null)
            {
                
                members.Remove((Member)MembersListBox.SelectedItem);
            }
            else
            {
                MessageBox.Show("Please select a member to delete.");
            }
        }

        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearTextBoxes()
        {
            
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            EmailTextBox.Text = "";
            AgeTextBox.Text = "";
            PhoneNumberTextBox.Text = "";
            AddressTextBox.Text = "";
            UsernameTextBox.Text = "";
            PasswordBox.Password = "";
        }
    }

    // Define a class to represent a member
    public class Member
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}





*/