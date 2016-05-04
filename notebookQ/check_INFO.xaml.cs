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

namespace notebookQ
{
    /// <summary>
    /// Interaction logic for check_INFO.xaml
    /// </summary>
    public partial class check_INFO : Page
    {
        public check_INFO()
        {
            
                InitializeComponent();
            
            u = new User();
            u.id = id;
            u.Initialization_user();
            textBox.Text = u.username;
            textBlock2.Text = u.notebookid.Count().ToString();
            textBlock2_Copy.Text = u.notepageid.Count().ToString();
            logout = false;


        }
        public ulong id { get; set; }
        private User u;
        public Boolean logout ;

        

        private void button_Click(object sender, RoutedEventArgs e)
        {
            changepassword changePage = new changepassword(id);
            
            this.NavigationService.Navigate(changePage);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            logout = true;
            Window loginout1 = Window.GetWindow(this);
            if (loginout1 != null)
            {
                loginout1.Close();
            }

        }

      

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            ///update username
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine("TextChange");
        }

       
    }
}
