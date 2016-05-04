using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace notebookQ
{
    public class notepage
    {

        #region private variable
        private String _name;
        private String _textaddress;
        
        #endregion

        #region public variable
        public ulong npid { get; set; }
        public ulong parent { get; set; }
        public String textaddress { get { return _textaddress; } set { _textaddress = value; } }
        public String text { get; set; }
        public String name { get { return _name; } set { _name = value; } }
        public ulong userid { get; set; }
        #endregion

        #region private method
        private void _syncText()
        {
            //SQL
        }
        private void readText()
        {
            //connect to text
        }

        #endregion
        #region public method
        public void changeParent(ulong ubid)
        {
            parent = ubid;
            //SQL change;
        }
        public void changeName(String NewName)
        {
            _name = NewName;
            //SQL change;
        }
        public long GetLastId(ulong user_id)
        {

            userid = user_id;
            String sqlusername = "notebookqyiren";
            String database = "notebookqyiren";
            String server = "db4free.net";
            String sqlpassword = "19971117ramon";
            String userTable = "user";
            String NoteTable = "notes";
            MySQL note = new MySQL();
            note.Database = database;
            note.Server = server;
            note.UserID = sqlusername;
            note.Password = sqlpassword;
            note.Table = NoteTable;
            note.CreateMySQLConnection();
            note.Query = "SELECT id FROM notes ORDER BY id DESC LIMIT 0, 1;";
            note.CreateMySqlAdapter();
            DataTable note_id = note.GetMySQLDataSet().Tables[note.Table];
            if (note_id!=null)
            {
                long temp = (long.Parse((((int)note_id.Rows[0].ItemArray[0]) + 1).ToString()));
                note = new MySQL();
                note.Database = database;
                note.Server = server;
                note.UserID = sqlusername;
                note.Password = sqlpassword;
                note.Table = NoteTable;
                note.CreateMySQLConnection();
              


                note.Query = "INSERT into notes values(" + temp.ToString() + ",'','',0,0,'',0);";
                note.CreateMySqlAdapter();
                note.ExecuteMySqlCommand();
                return temp;
            }
            else
            {
                return 1;
            }
        }

        public void Update()
        {
            String sqlusername = "notebookqyiren";
            String database = "notebookqyiren";
            String server = "db4free.net";
            String sqlpassword = "19971117ramon";
            String NoteTable = "notes";
            MySQL note = new MySQL();
            note.Database = database;
            note.Server = server;
            note.UserID = sqlusername;
            note.Password = sqlpassword;
            note.Table = NoteTable;
            note.CreateMySQLConnection();
            note.Query = "UPDATE notes SET type='PAGE',userid = "+userid.ToString()+",name = '" + name + "',parent = " + parent.ToString() + ",text='' where id = " + npid.ToString() ;
            note.CreateMySqlAdapter();
             note.ExecuteMySqlCommand();

        }
        #endregion

        public void Initialall()
        {
            String database = "notebookqyiren";
            String sqlusername = "notebookqyiren";
           String server = "db4free.net";
          String  sqlpassword = "19971117ramon";
           String userTable = "user";
          String  NoteTable = "notes";
            MySQL note = new MySQL();
            note.Database = database;
            note.Server = server;
            note.UserID = sqlusername;
            note.Password = sqlpassword;
            note.Table = NoteTable;
            note.CreateMySQLConnection();
            note.Query = "Select name,text,parent,userid from notes where id =" + npid.ToString();
            note.CreateMySqlAdapter();
            DataTable note_id = note.GetMySQLDataSet().Tables[note.Table];

            if ((note_id != null) && (note_id.Rows.Count != 0))
            {
                _name = note_id.Rows[0]["name"].ToString();
                parent =(ulong)(long) note_id.Rows[0]["parent"];
                text = note_id.Rows[0]["text"].ToString();

                

            }


        }

        public override String ToString()
        {
            return _name;
        }

    }
}
