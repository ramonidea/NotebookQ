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
    /// Interaction logic for changepassword.xaml
    /// </summary>
    public partial class changepassword : Page
    {
        public changepassword(ulong user_id)
        {
            InitializeComponent();
            userid = user_id;
            change = false;
        }
        public ulong userid;
        public Boolean change;
        private void button_Click(object sender, RoutedEventArgs e)
        {
            label_old.Content = "";
            label.Content = "";
            label_Copy.Content = "";
            if (newpassword.Password == newpasswordagain.Password) {
                if (oldpassword.Password == GetPassword(userid)){
                    MySQL user = new MySQL();
                    user.Database = "notebookqyiren";
                    user.Server = "db4free.net";
                    user.UserID = "notebookqyiren";
                    user.Password = "19971117ramon";
                    user.Table = "user";
                    user.CreateMySQLConnection();
                    user.Query = "UPDATE user SET `password`='" + newpassword.Password.ToString() + "' WHERE `id`='" + userid.ToString() + "';";
                    long Temp = user.ExecuteMySqlCommand();
                    Window loginout1 = Window.GetWindow(this);
                    if (loginout1 != null)
                    {
                        change = false;
                        loginout1.Close();
                    }
                }
                { label_old.Content = "Old Password does not match"; }
            }
            else { label.Content = "Passwords are different";label_Copy.Content = "Passwords are different"; }
        }
       

        private String GetPassword(ulong userid)
        {
            MySQL user = new MySQL();
            user.Database = "notebookqyiren";
            user.Server = "db4free.net";
            user.UserID = "notebookqyiren";
            user.Password = "19971117ramon";
            user.Table = "user";
            user.CreateMySQLConnection();
            user.Query = "Select password from user where id = " + userid.ToString() + ";";
            user.CreateMySqlAdapter();
            DataTable user_password = user.GetMySQLDataSet().Tables[user.Table];
            if (user_password!= null)
            {
                return user_password.Rows[0][0].ToString();
            }
            else { return ""; }




        }
    }
}
