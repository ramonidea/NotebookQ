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
using System.Threading;

namespace notebookQ
{
    /// <summary>
    /// Interaction logic for Schedule.xaml
    /// </summary>
    public partial class Schedule : Window
    {
        public Schedule(ulong userid)
        {

            InitializeComponent();
            id = userid;
            selectfromSQL();
           
        }
        public ulong id { get; set; }
        private schedule s;




        private void selectfromSQL()
        {
            s = new schedule();
            s.id = id;
            s.initialImport();
            List<todoitem> t = s.t;
            slist.ItemsSource = t;
           


        }
     
        private void update_Click(object sender, RoutedEventArgs e)
        {
            long aid = 0;
                MySQL sc = new MySQL();
                sc.Database = "notebookqyiren";
                sc.Server = "db4free.net";
                sc.UserID = "notebookqyiren";
                sc.Password = "19971117ramon";
                sc.Table = "schedule";
                sc.CreateMySQLConnection();
            sc.Query = "SELECT id FROM schedule ORDER BY id DESC LIMIT 0, 1;";
            sc.CreateMySqlAdapter();
            DataTable userid_data = sc.GetMySQLDataSet().Tables["schedule"];
            if (userid_data != null)
            {
                long temp = long.Parse(userid_data.Rows[0]["id"].ToString());
                aid = (long)temp + 1;
            }
            DateTime a = DateTime.Today;
            DateTime b = DateTime.Today;
            if( Dates.SelectedDate!= null) { a = (DateTime)Dates.SelectedDate; }else {  a = DateTime.Today; }
            if (Times.SelectedTime != null) {  b = (DateTime)Times.SelectedTime; } else {  b = DateTime.Today; }
            

            string formatForMySql = a.ToString("yyyy-MM-dd")+" "+b.ToString("HH:mm:ss");
            sc.Query = "Insert into schedule (id,userid,time,content,place,bufunc) value("+ aid.ToString()+","+ id.ToString() + ",'" +  formatForMySql+ "','" + content.Text.ToString() + "','" + place.Text.ToString() + "',0)";
                sc.CreateMySqlAdapter();
                sc.ExecuteMySqlCommand();
                selectfromSQL();
           
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            todoitem u = (todoitem)slist.SelectedItem;
            long uid = Int64.Parse(u.UID);
            deletesql(uid);
            selectfromSQL();
        }
    
       
        private void updateSQL(long uid)
        {
            MySQL sc = new MySQL();
            sc.Database = "notebookqyiren";
            sc.Server = "db4free.net";
            sc.UserID = "notebookqyiren";
            sc.Password = "19971117ramon";
            sc.Table = "schedule";
            sc.CreateMySQLConnection();

            sc.Query = "update schedule set bufunc = 1  where id =" + uid.ToString();

               sc.CreateMySqlAdapter();
            sc.ExecuteMySqlCommand();
        }

        private void deletesql(long uid)
        {
            MySQL sc = new MySQL();
            sc.Database = "notebookqyiren";
            sc.Server = "db4free.net";
            sc.UserID = "notebookqyiren";
            sc.Password = "19971117ramon";
            sc.Table = "schedule";
            sc.CreateMySQLConnection();

            sc.Query = "delete from schedule where id =" + uid.ToString();
            sc.CreateMySqlAdapter();
            sc.ExecuteMySqlCommand();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

       

       
        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            todoitem u = (todoitem)slist.SelectedItem;
            long uid = Int64.Parse(u.UID);
            Thread t = new Thread(()=>updateSQL(uid ));
            t.Start();

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            todoitem u = (todoitem)slist.SelectedItem;
            long uid = Int64.Parse(u.UID);
            Thread t = new Thread(()=>uncheck(uid));
            t.Start();
        }
        private void uncheck(long uid)
        {
            MySQL sc = new MySQL();
            sc.Database = "notebookqyiren";
            sc.Server = "db4free.net";
            sc.UserID = "notebookqyiren";
            sc.Password = "19971117ramon";
            sc.Table = "schedule";
            sc.CreateMySQLConnection();

            sc.Query = "update schedule set bufunc = 0  where id =" + uid.ToString();

            sc.CreateMySqlAdapter();
            sc.ExecuteMySqlCommand();
        }

        private void listsp_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var cb = sender as StackPanel;
            var item = cb.DataContext;
            slist.SelectedItem = item;


        }
    }
}
