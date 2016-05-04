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
using System.Windows.Shapes;
using System.Data;

using MySql.Data.MySqlClient;

namespace notebookQ
{
    /// <summary>
    /// Interaction logic for loginout.xaml
    /// </summary>
    public partial class loginout : Window
    {
        public loginout(Boolean loginin, Boolean ChangePassword, ulong user_id)
        {
            InitializeComponent();

            _loginin = loginin;
            _changepassword = ChangePassword;
            _newuserid = user_id;
            if (loginin== false)
            {
                logininPage = new login(_loginin, _changepassword, _newuserid);
                _mainFrame.NavigationService.Navigate(logininPage);
                
            }

            else  if (ChangePassword == true)
            {
                changePage = new changepassword(user_id);

                _changepassword = false;
                _mainFrame.NavigationService.Navigate(changePage);
            }
            else if (loginin == true){
                checkinfoPage = new check_INFO();
                checkinfoPage.id = user_id;
                _mainFrame.NavigationService.Navigate(checkinfoPage);

            }
           
        }
        public Boolean _loginin;
        public String username;
        private ulong _newuserid;
        private Boolean _changepassword;

        public login logininPage;
        public changepassword changePage;
        public check_INFO checkinfoPage;




    }
}
