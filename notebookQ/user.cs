using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Threading;


namespace notebookQ
{
    public class User
    {
        #region public variable
        public String username {
            get { return _username; }
            set { _username = value; }
        }
        public ulong id {get;set;}

        
        public List<ulong> notebookid{
        get ;
            set ;

    }
        public List<ulong> notepageid
        {
            get;
            set;
        }
        public String mail
        {
            get { return _mail; }
            set { _mail = value; }
        }
        public String mailpassword
        {
            get { return _mailpassword; }
            set { _mailpassword = value; }
        }
        #endregion 

        #region private variable
        private String _username;
        private String _password;
        private String _mail;
        private String _mailpassword;
        private ulong currentnb;

        

        #endregion 

        #region public method
        public void Initialization_user()
        {
            GetUserName();
            GetPassword();
            GetNotebooksid();
        }
        public void GetUserName()
        {

            MySQL user = new MySQL();
            user.Database = "notebookqyiren";
            user.Server = "db4free.net";
            user.UserID = "notebookqyiren";
            user.Password = "19971117ramon";
            user.Table = "user";
            user.Query = "Select username from user where id =" + id.ToString();
            user.CreateMySQLConnection();
            user.CreateMySqlAdapter();
            DataTable user_id = user.GetMySQLDataSet().Tables[user.Table];
            if ((user_id != null) && (user_id.Rows.Count != 0))
            {
                _username = user_id.Rows[0]["username"].ToString();

            }
        }
        private void GetPassword()
        {
            MySQL user = new MySQL();
            user.Database = "notebookqyiren";
            user.Server = "db4free.net";
            user.UserID = "notebookqyiren";
            user.Password = "19971117ramon";
            user.Table = "user";
            user.CreateMySQLConnection();
            user.Query = "Select password from user where id =" + id.ToString();
            user.CreateMySqlAdapter();
            DataTable user_id = user.GetMySQLDataSet().Tables[user.Table];
            if ((user_id != null) && (user_id.Rows.Count != 0))
            {
                _password= user_id.Rows[0]["password"].ToString();

            }
        }
        public void GetNotebooksid()
        {
            MySQL note = new MySQL();
            note.Database = "notebookqyiren";
            note.Server = "db4free.net";
            note.UserID = "notebookqyiren";
            note.Password = "19971117ramon";
            note.Table = "user";
            note.CreateMySQLConnection();
            note.Query = "Select id from notes where type='BOOK' and userid =" + id.ToString();
            note.CreateMySqlAdapter();
            DataTable user_id = note.GetMySQLDataSet().Tables[note.Table];
            note.MySQLClose();

            MySQL note1 = new MySQL();
            note1.Database = "notebookqyiren";
            note1.Server = "db4free.net";
            note1.UserID = "notebookqyiren";
            note1.Password = "19971117ramon";
            note1.Table = "user";
            note1.CreateMySQLConnection();
            note1.Query = "Select id from notes where type='PAGE' and userid =" + id.ToString();
            note1.CreateMySqlAdapter();
            DataTable user_id1 = note1.GetMySQLDataSet().Tables[note1.Table];
            List<ulong> temp_book_id = new List<ulong>();
            List<ulong> temp_page_id = new List<ulong>();
            if ((user_id != null) && (user_id.Rows.Count != 0))
            {
                foreach (DataRow row in user_id.Rows) {
                    temp_book_id.Add((ulong)(long)row["id"]);
                        }
                notebookid = temp_book_id;
            }
            else
            {
                notebookid = temp_book_id;
            }
            if ((user_id1 != null) && (user_id1.Rows.Count != 0))
            {
                foreach (DataRow row in user_id1.Rows) {
                    temp_page_id.Add((ulong)(long)row["id"]);
                        }
                notepageid = temp_page_id;
            }
            else
            {
                notepageid = temp_page_id;
            }
        }











        public void ChangeUsername(String newusername)
        {
            _username = newusername;
        }

        public Boolean ChangePassword(String oldpassword, String newpassword)
        {
            if (oldpassword.Equals(_password)) {
                _password = newpassword;
                _changepassword(_password);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void DeleteUser(ulong userid)
        {
            if ((id==0) && (_username=="admin")){
                //SQL delete user and all of its notebook
            }
        }

        
      


        #endregion

        #region private method
        private Boolean _changepassword(String newPassword)
        {
            //SQL
            return false;
        }
       
        #endregion


    }
}
