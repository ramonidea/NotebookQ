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

namespace notebookQ
{
    /// <summary>
    /// Interaction logic for rename_context.xaml
    /// </summary>
    public partial class rename_context : Window
    {
        public rename_context()
        {
            InitializeComponent();
            InitialName();
        }
        public String name { get; set; }
        public ulong id { get; set; }

        private void InitialName()
        {
            tbbox.Text = name;
            tbbox.TextChanged+=tbbox_TextChanged;
        }

        private void tbbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bu.IsEnabled = true;

        }

        private void bu_Click(object sender, RoutedEventArgs e)
        {
            name = tbbox.Text;
            MySQL note = new MySQL();
            note.Database = "notebookqyiren";
            note.Server = "db4free.net";
            note.UserID = "notebookqyiren";
            note.Password = "19971117ramon";
            note.Table = "notes";
            try
            {
                note.CreateMySQLConnection();
                note.Query = "UPDATE notes SET `name`='" + name.ToString() + "' WHERE `id`='" + id.ToString() + "';";
                note.CreateMySqlAdapter();
                note.ExecuteMySqlCommand();
                this.Close();
            }
            catch (Exception)
            {
                //nothing;
            }
        }
        
    }
}
