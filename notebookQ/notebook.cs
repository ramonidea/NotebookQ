using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace notebookQ
{
    public class notebook
    {
        #region private variable
        
        private List<ulong> _nblist;
        private ulong _userid;
        private String _name;
        #endregion
        #region public variable
        public ulong nbid { get; set; }
        public String name { get { return _name; } set { _name = value; } }
        public ulong parent { get; set; }
        public ulong userid { get { return _userid; } set { _userid = value; } }
        public List<ulong> nblist;
        
        #endregion

        #region private method
        private void _syncnblist()
        {
            //SQL sync nblist;
        }
        private void _deleteChild(ulong id)
        {
            //SQL delete child
        }
        private void _moveChild(ulong pid, ulong cid)
        {
            //SQL change parent
        }
        #endregion
        #region public method
        public void AddNoteChild(ulong npid){
            //SQl create new notepage and receive new id  set parent as nbid

            _syncnblist();
        }
        public void DeleteNoteChild(ulong npid)
        {
            _deleteChild(npid);
            _syncnblist();
        }
        public void DeleteNotebook()
        {
            foreach (ulong i in nblist)
            {
                _deleteChild(i);
            }
            _deleteChild(nbid);
        }
        public void ChangeNbName(String NewName)
        {
            _name = NewName;
            //SQL  change name
        }
        public void ChangeParent(ulong NewParent)
        {
            _moveChild(parent, NewParent);
            parent = NewParent;
            
        }
        #endregion

        public override string ToString()
        {
            return name;
        }

    }
}
