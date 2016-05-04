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
using MySql.Data.MySqlClient;
using System.Data;

namespace notebookQ
{
    /// <summary>
    /// Interaction logic for register.xaml
    /// </summary>
    public partial class register : Page
    {
        public register(ulong New_userid)
        {
            InitializeComponent();

            newid = New_userid;

        }
        private ulong newid;
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            if (tbPassword.Password.Count() >= 6)
            {
                MySQL user = new MySQL();
                user.Database = "notebookqyiren";
                user.Server = "db4free.net";
                user.UserID = "notebookqyiren";
                user.Password = "19971117ramon";
                user.Table = "user";
                user.CreateMySQLConnection();
                user.Query = "INSERT INTO user (`id`, `username`,`password`) VALUES(" + newid.ToString() + ", '" + tbUsername.Text.ToString() + "','" + tbPassword.Password.ToString() + "'); ";
                long Temp = user.ExecuteMySqlCommand();



                this.NavigationService.GoBack();
            }
            else
            {
               lbpassword.Content = "Try another password!";
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

        private void Navigateback_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
