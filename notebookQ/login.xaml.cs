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
using System.Data;

namespace notebookQ
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Page
    {
        public login(Boolean loginin, Boolean ChangePassword, ulong newuser_id)
        {
            InitializeComponent();
            _loginin = loginin;
            _newuserid = newuser_id;
            label_username.Content = "";
            label_password.Content = "";
            
            database = "notebookqyiren";
            sqlusername = "notebookqyiren";
            server = "db4free.net";
            sqlpassword = "19971117ramon";
            userTable = "user";
            NoteTable = "notes";
        }

        public ulong id { get; set; }
        public Boolean _loginin;
        public String username;
        private ulong _newuserid;

        private String sqlusername;
        private String database;
        private String server;
        private String sqlpassword;
        private String userTable;
        private String NoteTable;



        private void loginin_function()
        {

            if (tbUsername.Text == "") { label_username.Content = "Please Input Username"; }
            else if (tbPassword.Password ==""){ label_username.Content = ""; label_password.Content = "Please Input Password"; }
            else{
                label_password.Content = "";
                label_username.Content = "";
                MySQL user = new MySQL();
                user.Database = database;
                user.Server = server;
                user.UserID = sqlusername;
                user.Password = sqlpassword;
                user.Table = userTable;
                user.CreateMySQLConnection();
                user.Query = "Select id,username,password from user where username = '" + tbUsername.Text.ToString() + "';";
                user.CreateMySqlAdapter();
                DataTable user_password = user.GetMySQLDataSet().Tables[user.Table];
                if ((user_password.Rows.Count != 0) || user_password == null)
                {
                    foreach (DataRow row in user_password.Rows)
                    {
                        if (row[1].ToString() == tbUsername.Text)
                        {
                            if (row["password"].ToString() == tbPassword.Password)
                            {
                                _loginin = true;
                                long temp = (long)row["id"];
                                id = (ulong)temp;
                                username = row[1].ToString();
                                _loginin = true;
                                label_username.Content = "Login in Successfully";
                                Window loginout1 = Window.GetWindow(this);
                                if (loginout1 != null)
                                {
                                    loginout1.Close();

                                }

                            }
                            else
                            {
                                label_password.Content = "Wrong Password";
                                id = _newuserid;
                            }

                        }
                        else
                        {
                            label_username.Content = "User Does not Exist.";
                            id = _newuserid;
                        }
                    }
                }
                else
                {
                    label_username.Content = "User Does not Exist.";
                    id = _newuserid;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loginin_function();
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            /// Register  another page

            register r = new register(_newuserid);
            this.NavigationService.Navigate(r);
            id = _newuserid;

        }

       

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if(Keyboard.IsKeyDown(Key.Enter))
            {
                loginin_function();
            }
        }

      
    

     
   
        private void button_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            VisiblePassword.Visibility = System.Windows.Visibility.Visible;
            tbPassword.Visibility = System.Windows.Visibility.Hidden;
            VisiblePassword.Content = tbPassword.Password.ToString();
        }

        private void button_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            VisiblePassword.Visibility = System.Windows.Visibility.Hidden;
            tbPassword.Visibility = System.Windows.Visibility.Visible;
            VisiblePassword.Content = "";
        }

        private void tbUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                loginin_function();
            }
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                loginin_function();
            }
        }

        private void cbLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                loginin_function();
            }
        }

        private void btloginin_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                loginin_function();
            }
        }
    }


    /// Keep login in   save  
}
