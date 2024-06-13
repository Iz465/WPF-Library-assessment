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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF_Library_assessment.Library_Pages;
using WPF_Library_assessment.Mongo_Info;
using WPF_Library_assessment.User_Control_Stuff;

namespace WPF_Library_assessment.Window_stuff
{

    public partial class signInWn : Window
    {
        public signInWn()
        {
            InitializeComponent();
        }


             private void submitBtn_Click(object sender, RoutedEventArgs e)
             {


                 textboxUC usernameTextBox = UserName as textboxUC;
                 textboxUC passwordTextBox = Password as textboxUC;
                 string username = usernameTextBox.Text;
                 string password = passwordTextBox.Text;

                 MongoData mongoData = new MongoData();
                 List<Members> members = mongoData.Connect<Members>("Members");
                 List<Admin> admin = mongoData.Connect<Admin>("Administrator");


                 bool muCheck = checkInfo(username, "Username", members);
                 bool mpCheck = checkInfo(password, "Password", members);
                 bool auCheck = checkInfo(username, "Username", admin);
                 bool apCheck = checkInfo(password, "Password", admin);
            //   try
            //   {
            if ((muCheck && mpCheck) || (auCheck && apCheck))
            {

                SessionManager.CurrentUser = members.FirstOrDefault(m => m.Username == username && m.Password == password);
                      //?? (object)admin.FirstOrDefault(a => a.Username == username && a.Password == password);
            
       
                this.Close();

                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null)
                {
                    WelcomePG welcomePG = new WelcomePG();
                    mainWindow.Content = welcomePG;
                }

            }
          
            else
            {
                MessageBox.Show("Incorrect Login Details");
            }

                     
             //    }
               //  catch (Exception ex) {
              //   MessageBox.Show(ex.Message);
              //   }


             }



             private bool checkInfo<T>(string info, string type, List<T> items)
             {
                 var property = typeof(T).GetProperty(type);
                 return items.Any(item => property.GetValue(item)?.ToString() == info);
             }

        public static class SessionManager
        {
            public static object CurrentUser { get; set; }
        }





    }


}

/* <Grid.RowDefinitions>
            <RowDefinition Height="100"/>

            <RowDefinition Height="70"/>
            <RowDefinition Height="70"/>
            <RowDefinition Height="70"/>
            <RowDefinition/>
        </Grid.RowDefinitions>
        <UserControl:ExitButtonUC/>
        <UserControl:textboxUC Grid.Row="1" Width="200" Height="40" PlaceHolder="Username" x:Name="UserName"/>
        <UserControl:textboxUC Grid.Row="2" Width="200" Height="40" PlaceHolder="Password" x:Name="Password"/>
        <Button Grid.Row="3" Name="submitBtn" Width="200" Height="40" Content="Submit" Click="submitBtn_Click"/>

*/



// below is bikram sign in layout 

/*  <Grid>
        <Grid.ColumnDefinitions>
           <ColumnDefinition Width="*"/>
           <ColumnDefinition Width="1.5*"/>
       </Grid.ColumnDefinitions>

       <Image Source= "images/close.png" Grid.Column="1" Panel.ZIndex="1" Style="{StaticResource imgclose}"/>

       <Border CornerRadius="10 0 0 10">
           <Border.Background>
               <LinearGradientBrush StartPoint="0,0" EndPoint="1,1">
                   <GradientStop Color="LawnGreen" Offset="0"/>
                   <GradientStop Color="LawnGreen" Offset="1"/>
               </LinearGradientBrush>
           </Border.Background>

           <Grid>
               <Canvas>
                   <Polygon Points="0,20 230,140 0,270" Fill="LightGreen"/>
                   <Polygon Points="100,400 200,300 180,470" Fill="LightGreen"/>
                   <Ellipse Margin="250 450 0 0" Width="40" Height="40" Fill="LightGreen"/>
                   <Ellipse Margin="50 400 100 40" Width="20" Height="20" Fill="LightGreen"/>
               </Canvas>
               <StackPanel VerticalAlignment="Center">
                   <TextBlock Text="Sign Up" Style="{StaticResource titleText}"/>
                   <TextBlock Text="Create new account to connect with us"  Style="{StaticResource normalText}" Opacity="1" Margin="20 30" />
                   <Button Content="Sign Up" Style="{StaticResource mybutton}"/>
               </StackPanel>
           </Grid>

       </Border>
       <Border Background="#ffffff" Grid.Column="1" CornerRadius="0 10 10 0">
           <StackPanel>
               <TextBlock Text="Sign in to Library" Style="{StaticResource titleText}" Foreground="LawnGreen"/>
               <StackPanel Orientation="Horizontal" Margin="0 20" HorizontalAlignment="Center">
                   <Button Style="{StaticResource mybuttonCircle}" >
                       <Image Source="/images/flogo.png" Width="20" Height="20" />
                   </Button>

                   <Button Style="{StaticResource mybuttonCircle}" >
                       <Image Source="/images/instalogo.png" Width="20" Height="20" />
                   </Button>


                   <Button Style="{StaticResource mybuttonCircle}" >
                       <Image Source="/images/lilogo.png" Width="20" Height="20" />
                   </Button>
               </StackPanel>


               <TextBlock Text="or use your email info :" Style="{StaticResource normalText}" Foreground="DarkGray" Margin="0 10"/>

               <Border BorderThickness="1" BorderBrush="LawnGreen" Margin="70 7" CornerRadius="5">
                   <Grid Margin="7 9">
                       <Grid.ColumnDefinitions>
                           <ColumnDefinition Width="auto"/>
                           <ColumnDefinition Width="*"/>
                       </Grid.ColumnDefinitions>

                       <Image Source="/images/emaillogo.png" Height="20"/>
                       <TextBlock x:Name="textemail"  Text="Email" Style="{StaticResource texthint}"/>
                       <TextBox x:Name="mytextemail" />
                   </Grid>

               </Border>


               <Border BorderThickness="1" BorderBrush="LawnGreen" Margin="70 7" CornerRadius="5">
                   <Grid Margin="7 9">
                       <Grid.ColumnDefinitions>
                           <ColumnDefinition Width="auto"/>
                           <ColumnDefinition Width="*"/>
                       </Grid.ColumnDefinitions>
                       <Image Source="/images/passswordlogo.png" Height="20"/>
                       <UserControl:textboxUC Width="200" Height="40" PlaceHolder="Username" x:Name="UserName"/>
                       <UserControl:textboxUC Width="200" Height="40" PlaceHolder="Password" x:Name="Password"/>
                   </Grid>
               </Border>

               <Button Name="submitBtn" Click="submitBtn_Click" Content="Sign In" Style="{StaticResource mainbutton}"/>

           </StackPanel>

       </Border>
   </Grid> 
*/