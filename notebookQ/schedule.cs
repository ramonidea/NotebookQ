using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace notebookQ
{
    class schedule
    {
        public ulong id { get; set; }
        public List<todoitem> t { get; set; }
        private ulong _scid;
        public ulong scid { get { return _scid; } set { _scid = value; } }

        private MySQL sc = new MySQL();

        public void initialImport()
        {
            t = new List<todoitem>();
            sc.Database = "notebookqyiren";
            sc.Server = "db4free.net";
            sc.UserID = "notebookqyiren";
            sc.Password = "19971117ramon";
            sc.Table = "schedule";
            sc.CreateMySQLConnection();
            sc.Query = "Select id,time,content,place,bufunc from schedule where userid = " + id.ToString();
            sc.CreateMySqlAdapter();
            DataTable data = sc.GetMySQLDataSet().Tables["schedule"];
            foreach (DataRow row in data.Rows)
            {
                todoitem to = new todoitem();
                to.Content = row["content"].ToString();
                to.Time = (DateTime)row["time"];
                to.Place = row["place"].ToString();
                to.butfunc =(bool) row["bufunc"];
                to.UID = row["id"].ToString();
                t.Add(to);

            }

        }


        

    }

    class todoitem{
        public String Content {get;set;}
        public DateTime Time{get;set;}
        public String Place {get;set;}
        public bool butfunc { get; set; }
        public String UID { get; set; }

    }
}
