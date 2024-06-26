﻿using MongoDB.Driver;
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

                 string username = UserName.Text;
                 string password = Password.Text;

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

                SessionManager.CurrentUser = members.FirstOrDefault(m => m.Username == username && m.Password == password)
                ?? (object)admin.FirstOrDefault(a => a.Username == username && a.Password == password);

                overdueCheck();
                this.Close();

                MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null)
                {
                    try
                    {
                        WelcomePG welcomePG = new WelcomePG();
                        mainWindow.Content = welcomePG;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                  
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

        private void overdueCheck()
        {
            var user = signInWn.SessionManager.CurrentUser;
            MongoData mongoData = new MongoData();
            var database = mongoData.GetMongoDatabase();
            if (user is WPF_Library_assessment.Mongo_Info.Members members)
            {
                var collections = mongoData.getCollections();

                foreach (var book in collections)
                {
                    IMongoCollection<Books> collection = database.GetCollection<Books>(book);
              
                    var filter = Builders<Books>.Filter.Eq("Owner", members.Username); 
                    var filter1 = Builders<Books>.Filter.Eq("Overdue", "Yes"); 
                    var filter2 = Builders<Books>.Filter.And(filter,filter1); // im combining both to make sure only books bound to this user that are overdue are filtered.

                  
                    var overdueBooks = collection.Find(filter2).ToList();
                    if (overdueBooks.Count > 0)
                    {
                        MessageBox.Show($"Welcome {members.Username}. You have {overdueBooks.Count.ToString()} books overdue");
                    }
                }
            }
        }

        private void signincloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }


}

