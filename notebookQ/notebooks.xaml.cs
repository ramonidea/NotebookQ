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
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading;
using System.Collections;
using System.Runtime.InteropServices;

/// notepage edit area text box multiple 

namespace notebookQ
{
    /// <summary>
    /// Interaction logic for notebooks.xaml
    /// </summary>
    public partial class notebooks : Window
    {
        Point _lastMouseDown;
        TreeViewItem draggedItem, _target;
        public notebooks()
        {
            InitializeComponent();
            this.Top = 0;
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth - this.Width;
            //InitializeAll();
            CurrentUser = new User();
            NewUserGetUserID();

            ChangePassword = false;
            loginin = false;

            userlogin();
        }
        #region private variable
        private Schedule s;

        public Boolean loginin;
        public Boolean ChangePassword;
        private loginout login;
        private User CurrentUser;
        private notepage CurrentPage;
        private Hashtable treeviewTempSave = new Hashtable();
        #endregion
        
        private void InitializeAll()
        {
            ///sync all settings
        }

        #region user


        // User login in create a showlog
        private void userlogin(){
            /// login window
             login = new loginout(loginin,ChangePassword,CurrentUser.id);
            /// window location
            login.Left = 400;
            login.Top = 400;
            try
            {
                login.Left = this.Left - (this.Left - 400) / 2;
                login.Top = this.Top - (this.Top - 400) / 2;
            }
            catch (Exception a) { login.Left = this.Left; login.Top = this.Top; }
           
             login.ShowDialog();

            ///Pass back data
        if (ChangePassword == true)
               {
                ChangePassword = login.changePage.change;
              }
            
            if ((loginin == false) &&(login.logininPage._loginin == true)){
                CurrentUser.id = login.logininPage.id;
                loginin = login.logininPage._loginin;
                rightclickChange();
                CurrentUser.username= login.logininPage.username;
                tbuser.Content = CurrentUser.username;
                login.Close();
                
                loadall();
            }
            else if (loginin == true)
            {
                try {
                    if (login.checkinfoPage.logout == true)
                    {
                        logout();

                    }
                    else {
                        login.Close();
                        loadall();
                    }
                }catch(Exception e)
                {
                    login.Close();
                    loadall();
                }
                

            }
            

        }
        
        //Log out clear everything and reopen login in window
        private void logout()
        {
            loginin = false;
            ChangePassword = false;
            CurrentUser = new User();
            tbuser.Content = "New User";
            tvNotebooks.Items.Clear();
            NewUserGetUserID();
            /// edit panel delete
            userlogin();
        }
        // Click user name button
        private void tbuser_Click(object sender, RoutedEventArgs e)
        {
           
            userlogin();
        }
        //if log in right click user context menu will change
        private void rightclickChange()
        {
            tbuser_menu.Items.Clear();
            MenuItem checkinfo = new MenuItem();
            checkinfo.Header="Check Info";
            checkinfo.Name = "tbuser_checkinfo";
            checkinfo.Click += new RoutedEventHandler(tbuser_checkinfo);
            MenuItem changePassword = new MenuItem();
            changePassword.Header = "Change Password";
            changePassword.Name = "tbuser_changepassword";
            changePassword.Click +=new RoutedEventHandler(tbuser_changepassword_click);
            tbuser_menu.Items.Add(checkinfo);
            tbuser_menu.Items.Add(changePassword);
            MenuItem logout = new MenuItem();
            logout.Header = "Log out";
            logout.Click += new RoutedEventHandler(tbuser_logout);
            Separator a = new Separator();
            tbuser_menu.Items.Add(a);
            tbuser_menu.Items.Add(logout);

        }
        //context menu check info click
        private void tbuser_checkinfo(object sender, RoutedEventArgs e)
        {
            userlogin();
        }
        // context menu change password click
        private void tbuser_changepassword_click(object sender, RoutedEventArgs e)
        {
            ChangePassword = true;
            userlogin();
        }
        //context menu not log in login in button click
        private void user_right_login_Click(object sender, RoutedEventArgs e)
        {
            userlogin();
        }
        // right click log out click
        private void tbuser_logout(object sender, RoutedEventArgs e)
        {
            logout();
            
        }


        #endregion user

        #region menu
        // Create new item
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            CreateNewNotePage();
        }
  


        #endregion menu

        #region tree
        private void loadall()
        {
            ///fresh tree view
            freshTreeview();
            tbtitle.Content = CurrentUser.username;
            tcedit.Items.Clear();

            ///close all tabs
            TabItem t = new TabItem();
            t.Uid = "add";
            t.Width = 23.733;
            t.Height = 20;
            t.HorizontalAlignment = HorizontalAlignment.Center;
            t.VerticalAlignment = VerticalAlignment.Center;
            t.PreviewMouseDown += TabItem_MouseLeftButtonDown;
            t.MouseDown += TabItem_MouseLeftButtonDown;
            t.FontSize = 20;
            t.Header = "+";
            t.Content = "";
            tcedit.Items.Add(t);
        }
        private event Action CloseRegionWindows = delegate { }; // won't have to check for null
        private void processingwindows()
        {
            Processing p = new Processing();
            p.Top = 400;
            p.Left = 400;
            CloseRegionWindows += () => p.Dispatcher.BeginInvoke(new ThreadStart(() => p.Close()));
            p.ShowDialog();


        }
        private void freshTreeview()
        {
            Thread t = new Thread(processingwindows);
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            tvNotebooks.Items.Clear();
            tvNotebooks.Items.Add(GetTreeViewItem());
            CloseRegionWindows();


        }
        // load all of tree view item



        private TreeViewItem GetTreeViewItem()
        {
            TreeViewItem ti = new TreeViewItem();
            ti.Header = GetNoteName(0);
            ti.Uid = "MA0";
            MySQL note = new MySQL();
            note.Database = "notebookqyiren";
            note.Server = "db4free.net";
            note.UserID = "notebookqyiren";
            note.Password = "19971117ramon";
            note.Table = "notes";
            note.CreateMySQLConnection();

            note.Query = "SELECT id,type,level,name,parent FROM notes where userid=" + CurrentUser.id.ToString() + " order by level ASC";
            note.CreateMySqlAdapter();
            DataTable note_tree = note.GetMySQLDataSet().Tables["notes"];
            if (note_tree.Rows == null)
            {
                ti.Uid = "nl0";
                return ti;
            }
            else
            {
                long currentlevel = 0;
                /// theading each level and add them together in the end
                /// 

                Hashtable alllevel = new Hashtable(); // key = level value = hashtable
                Hashtable lastbook = new Hashtable(); //ket id value row
                foreach (DataRow row in note_tree.Rows)
                {
                    if ((long)(int)row["level"] == currentlevel)
                    {
                        lastbook[row["id"]] = row;

                    }
                    else
                    {
                        alllevel[currentlevel] = lastbook;
                        currentlevel = (long)(int)row["level"];
                        lastbook = new Hashtable();
                        lastbook[row["id"]] = row;
                    }
                }
                alllevel[currentlevel] = lastbook;
                List<Thread> tempTreadList = new List<Thread>();
                List<Boolean> tempIsalivelist = new List<Boolean>();
                for (long i = 0; i <=  currentlevel; i++)
                {

                    Hashtable h = (Hashtable)alllevel[i];

                    CreateNote(h, i);
                //    long temp = i;
                //    Thread t = new Thread(() => CreateNote(h, temp));
                //    t.SetApartmentState(ApartmentState.STA);
                //    tempTreadList.Add(t);
                //    tempIsalivelist.Add(true);
                //    t.Start();

                //}
                //while (true)
                //{
                //    if (tempIsalivelist.All(x => x == false))
                //    {
                //        break;
                //    }
                //    tempIsalivelist = new List<Boolean>();
                //    foreach (Thread t in tempTreadList)
                //    {
                //        Boolean b = t.IsAlive;
                //        tempIsalivelist.Add(b);
                //    }
                }
               
                
                Application.Current.Dispatcher.BeginInvoke (System.Windows.Threading.DispatcherPriority.Background, new ThreadStart(delegate{
                    for (long i = currentlevel; i >= 0; i--)
                    {
                        long level = i;
                        Hashtable h = (Hashtable)treeviewTempSave[i];
                        if (level == 0)
                        {
                            foreach (TreeViewItem t in h.Values)
                            {
                                
                                    t.Selected += Page_temp_Selected;
                                    t.MouseDoubleClick += page_temp_MouseDoubleClick;
                                    t.PreviewMouseRightButtonDown += OnPreviewMouseRightButtonDown;
                                    t.MouseRightButtonDown += TreeViewItem_MouseRightButtonDown;

                                    ti.Items.Add(t);
                                
                            }
                        }
                        else
                        {
                            Hashtable hc = (Hashtable)treeviewTempSave[i - 1];
                            Hashtable hcc = (Hashtable)alllevel[i];
                            foreach (long j in h.Keys)
                            {

                                TreeViewItem child = new TreeViewItem();
                                    child = (TreeViewItem)h[j];
                                
                                long id = long.Parse(child.Uid.Substring(2));
                                long parent = (long)((DataRow)hcc[(int)id])["parent"];

                                TreeViewItem Parenttree = new TreeViewItem(); 
                                 Parenttree =  (TreeViewItem)hc[parent];
                                if (!Parenttree.Items.Contains(child)){Parenttree.Items.Add(child);}
                                
                                hc[parent] = Parenttree;
                                treeviewTempSave[i - 1] = hc;
                            }
                        }
                    }
                }
                ));
                




            }
            return ti;
        }
        private void CreateNote(Hashtable h,long level)
        {
            Hashtable treeTemp = new Hashtable(); // key = id value = treeviewitem
           foreach (DataRow row in h.Values)
            {

                if (row["type"].ToString() == "PAGE")
                {

                    TreeViewItem page_temp = new TreeViewItem();
                    page_temp.Header = row["name"];
                    page_temp.Uid = "pg" + row["id"].ToString();
                    page_temp.Header = row["name"];
                    treeTemp[Int64.Parse(row[0].ToString())] = page_temp;

                }
                else
                {
                    TreeViewItem book = new TreeViewItem();
                    book.Uid = "nb" + row["id"].ToString();
                    book.Header = row["name"];
                    treeTemp[Int64.Parse(row[0].ToString())] = book;
                }
            }
            treeviewTempSave[level] = treeTemp;
            }






        private void OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                TreeViewItem ti = (TreeViewItem)tvNotebooks.SelectedItem;
                ContextMenu menu = new ContextMenu();
                menu.Name = "Currentpage";
                ti.ContextMenu = menu;
                MenuItem rename = new MenuItem();
                rename.Click += ti_rename;
                rename.Header = "_Rename";
                MenuItem edit = new MenuItem();
                edit.Click += edit_click;
                edit.Header = "_Edit";


                MenuItem delete = new MenuItem();
                delete.Click += delete_Click;
                delete.Header = "_Delete";

                menu.Items.Add(edit);
                menu.Items.Add(rename);

                menu.Items.Add(delete);
                e.Handled = true;
            }
        }

        void delete_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem t = (TreeViewItem)tvNotebooks.SelectedItem;
            TreeViewItem ti = (TreeViewItem)t.Parent;
            ti.Items.Remove(t);
            long id = Int64.Parse(t.Uid.Substring(2));
            Thread thread = new Thread(()=> DeleteFromSQL(id));
            thread.Start();
        }
        private void DeleteFromSQL(long id)
        {
            MySQL note = new MySQL();
            note.Database = "notebookqyiren";
            note.Server = "db4free.net";
            note.UserID = "notebookqyiren";
            note.Password = "19971117ramon";
            note.Table = "notes";
            note.CreateMySQLConnection();
            note.Query = "Delete from notes where id = " + id.ToString();
            note.CreateMySqlAdapter();
            note.ExecuteMySqlCommand();
        }
        static TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);

            return source as TreeViewItem;
        }
        private void page_temp_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem ti = (TreeViewItem)tvNotebooks.SelectedItem;
            ulong pgid = (ulong)Int32.Parse(ti.Uid.Substring(2, ti.Uid.Length - 2));
            String type = ti.Uid.Substring(0, 2);
            if (type == "pg")
            {
                Thread t = new Thread(() => AddTab(pgid));
                t.SetApartmentState(ApartmentState.STA);
                t.Start();

            }


            
        }
        // Page selected create tab
        private void Page_temp_Selected(object sender, RoutedEventArgs e)
        {
           
        }
        // page right button down
        private void TreeViewItem_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
           


        }
        private void edit_click(object sender, RoutedEventArgs e)
        {
            TreeViewItem ti = (TreeViewItem)tvNotebooks.SelectedItem;
            ulong pgid = UInt64.Parse(ti.Uid.Substring(2, ti.Uid.Length - 2));

            Thread t = new Thread(() => AddTab(pgid));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }
        private void ti_rename(object sender, RoutedEventArgs e)
        {
            TreeViewItem ti = (TreeViewItem)tvNotebooks.SelectedItem;
            ulong pgid = (ulong)Int32.Parse(ti.Uid.Substring(2, ti.Uid.Length - 2));
            rename_context re = new rename_context();
            re.Width = 211;
            re.Height = 111;
            re.name = ti.Header.ToString();
            re.id = pgid;
            re.ShowDialog();
            freshTreeview();


        }


        private String GetNoteName(ulong nbid)
        {
            if (nbid == 0) { return CurrentUser.username.ToString() + "'s Notebooks"; }
            else {
                MySQL note = new MySQL();
                note.Database = "notebookqyiren";
                note.Server = "db4free.net";
                note.UserID = "notebookqyiren";
                note.Password = "19971117ramon";
                note.Table = "notes";
                note.CreateMySQLConnection();
                note.Query = "SELECT name FROM notes where id = " + nbid.ToString();
                note.CreateMySqlAdapter();
                DataTable note_tree = note.GetMySQLDataSet().Tables["notes"];
                note.MySQLClose();
                return note_tree.Rows[0][0].ToString();
            }
        }

        private void tvNotebooks_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point currentPosition = e.GetPosition(tvNotebooks);


                    if ((Math.Abs(currentPosition.X - _lastMouseDown.X) > 10.0) ||
                        (Math.Abs(currentPosition.Y - _lastMouseDown.Y) > 10.0))
                    {
                        draggedItem = (TreeViewItem)tvNotebooks.SelectedItem;
                        if (draggedItem != null)
                        {
                            DragDropEffects finalDropEffect = DragDrop.DoDragDrop(tvNotebooks, tvNotebooks.SelectedValue,
                                DragDropEffects.Move);
                            //Checking target is not null and item is dragging(moving)
                            if ((finalDropEffect == DragDropEffects.Move) && (_target != null))
                            {
                                // A Move drop was accepted
                                if (!draggedItem.Header.ToString().Equals(_target.Header.ToString()))
                                {
                                    CopyItem(draggedItem, _target);
                                    _target = null;
                                    draggedItem = null;
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void tvNotebooks_DragOver(object sender, DragEventArgs e)
        {
            try
            {

                Point currentPosition = e.GetPosition(tvNotebooks);


                if ((Math.Abs(currentPosition.X - _lastMouseDown.X) > 10.0) ||
                    (Math.Abs(currentPosition.Y - _lastMouseDown.Y) > 10.0))
                {
                    // Verify that this is a valid drop and then store the drop target
                    TreeViewItem item = GetNearestContainer(e.OriginalSource as UIElement);
                    if (CheckDropTarget(draggedItem, item))
                    {
                        e.Effects = DragDropEffects.Move;
                    }
                    else
                    {
                        e.Effects = DragDropEffects.None;
                    }
                }
                e.Handled = true;
            }
            catch (Exception)
            {
            }
        }

        private void tvNotebooks_Drop(object sender, DragEventArgs e)
        {
            try
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;

                // Verify that this is a valid drop and then store the drop target
                TreeViewItem TargetItem = GetNearestContainer(e.OriginalSource as UIElement);
                if (TargetItem != null && draggedItem != null)
                {
                    _target = TargetItem;
                    e.Effects = DragDropEffects.Move;

                }
            }
            catch (Exception)
            {
            }

        }

        private void tvNotebooks_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                _lastMouseDown = e.GetPosition(tvNotebooks);
            }
        }
        private bool CheckDropTarget(TreeViewItem _sourceItem, TreeViewItem _targetItem)
        {
            //Check whether the target item is meeting your condition
            bool _isEqual = false;
            if (!_sourceItem.Header.ToString().Equals(_targetItem.Header.ToString()))
            {
                if (_targetItem.Uid.Substring(0,2)=="nb" || _targetItem.Uid.Substring(0,2)=="MA")
                {
                    _isEqual = true;
                }
                
            }
            return _isEqual;

        }
        private void CopyItem(TreeViewItem _sourceItem, TreeViewItem _targetItem)
        {

            //Asking user wether he want to drop the dragged TreeViewItem here or not
            if (MessageBox.Show("Would you like to drop " + _sourceItem.Header.ToString() + " into " + _targetItem.Header.ToString() + "", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    //adding dragged TreeViewItem in target TreeViewItem
                    addChild(_sourceItem, _targetItem);

                    //finding Parent TreeViewItem of dragged TreeViewItem 
                    TreeViewItem ParentItem = FindVisualParent<TreeViewItem>(_sourceItem);
                    // if parent is null then remove from TreeView else remove from Parent TreeViewItem
                    if (ParentItem == null)
                    {
                        tvNotebooks.Items.Remove(_sourceItem);
                    }
                    else
                    {
                        ParentItem.Items.Remove(_sourceItem);
                    }
                }
                catch
                {

                }
            }

        }
        public void addChild(TreeViewItem _sourceItem, TreeViewItem _targetItem)
        {
            // add item in target TreeViewItem 
            TreeViewItem item1 = new TreeViewItem();
            item1.Uid = _sourceItem.Uid;
            item1.Header = _sourceItem.Header;
            item1.Selected += Page_temp_Selected;
            item1.MouseDoubleClick += page_temp_MouseDoubleClick;
            item1.PreviewMouseRightButtonDown += OnPreviewMouseRightButtonDown;
            item1.MouseRightButtonDown += TreeViewItem_MouseRightButtonDown;


            _targetItem.Items.Add(item1);
            ulong id = UInt64.Parse(_sourceItem.Uid.Substring(2));
            ulong parentid = UInt64.Parse(_targetItem.Uid.Substring(2));
            UpdateSqlName(id, parentid);
            foreach (TreeViewItem item in _sourceItem.Items)
            {
                addChild(item, item1);
            }
        }
        static TObject FindVisualParent<TObject>(UIElement child) where TObject : UIElement
        {
            if (child == null)
            {
                return null;
            }

            UIElement parent = VisualTreeHelper.GetParent(child) as UIElement;

            while (parent != null)
            {
                TObject found = parent as TObject;
                if (found != null)
                {
                    return found;
                }
                else
                {
                    parent = VisualTreeHelper.GetParent(parent) as UIElement;
                }
            }

            return null;
        }
        private TreeViewItem GetNearestContainer(UIElement element)
        {
            // Walk up the element tree to the nearest tree view item.
            TreeViewItem container = element as TreeViewItem;
            while ((container == null) && (element != null))
            {
                element = VisualTreeHelper.GetParent(element) as UIElement;
                container = element as TreeViewItem;
            }
            return container;
        }
        private void UpdateSqlName(ulong id,ulong parentid)
        {
            Thread t = new Thread(() =>
            {
                MySQL note = new MySQL();
                note.Database = "notebookqyiren";
                note.Server = "db4free.net";
                note.UserID = "notebookqyiren";
                note.Password = "19971117ramon";
                note.Table = "notes";
                note.Query = "Update notes set parent = " + parentid.ToString() + " where id = " + id.ToString();
                note.CreateMySQLConnection();
                note.CreateMySqlAdapter();
                note.ExecuteMySqlCommand();
                note.Query = "Select level from notes where id = " + parentid.ToString();
                note.CreateMySqlAdapter();
                DataTable ndata = note.GetMySQLDataSet().Tables["notes"];
                long plevel = Int64.Parse(ndata.Rows[0]["level"].ToString());


                if (parentid == 0)
                {
                    note.Query = "Update notes set level = 0 where id = " + id.ToString();
                    note.CreateMySqlAdapter();
                    note.ExecuteMySqlCommand();
                }
                else
                {
                    
                    long level = plevel + 1;
                    note.Query = "Update notes set level = " + level.ToString() + " where id = " + id.ToString();
                    note.CreateMySqlAdapter();
                    note.ExecuteMySqlCommand();
                }
            

            });
            t.Start();
            return;

        }
        #endregion tree

        #region edit_area
        private void AddTab(ulong id)
        {
            Dispatcher.Invoke(new Action(()=> { 
            Boolean f = true;
            foreach (TabItem t in tcedit.Items)
            {
                if (id.ToString() == t.Uid)
                {
                    f=false;
                }
            }
            if (f)
            {
                TabItem newtab = new TabItem();
                TextBox text = new TextBox();
                text.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                text.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                text.TextWrapping = System.Windows.TextWrapping.Wrap;
                text.AcceptsReturn = true;
                text.VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Visible;
                text.TextChanged += Text_TextChanged;
                    
                notepage page = new notepage();
                page.npid = id;
                page.Initialall();
                newtab.Header = page.name;
                text.Text = page.text;
                newtab.Content = text;
                newtab.Uid = id.ToString();



                tcedit.Items.Add(newtab);
                tcedit.SelectedItem = newtab;
                newtab.Focus();
            }
            } ));
        }

        private void Text_TextChanged(object sender, TextChangedEventArgs e)
        {
            notechangesButton.IsEnabled = true;

        }
        // Tab add new one create new one
        private void TabItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            CreateNewNotePage();

        }
        // function create new notepage
        private void CreateNewNotePage(){
            TabItem newtab = new TabItem();
            TextBox text = new TextBox();
            text.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            text.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            text.AcceptsReturn = true;
            text.VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Visible;
            text.TextChanged += Text_TextChanged;

            notepage page = new notepage();
            page.npid = (ulong)page.GetLastId(CurrentUser.id);
            page.userid = CurrentUser.id;
            page.name = "New NotePage";
            page.parent = 0;

            page.Update();

            TreeViewItem t = new TreeViewItem();
            t.Selected += Page_temp_Selected;
            t.MouseDoubleClick += page_temp_MouseDoubleClick;
            t.PreviewMouseRightButtonDown += OnPreviewMouseRightButtonDown;
            t.MouseRightButtonDown += TreeViewItem_MouseRightButtonDown;
            t.Uid = "pg" + page.npid.ToString();
            t.Header = "New NotePage";
            TreeViewItem ti = new TreeViewItem();
            ti = (TreeViewItem)tvNotebooks.Items.GetItemAt(0);
            ti.Items.Add(t);

            newtab.Header = page.name;
            text.Text = page.text;
            newtab.Content = text;
            newtab.Uid = page.npid.ToString();



            tcedit.Items.Add(newtab);
            tcedit.SelectedItem = newtab;
            newtab.Focus();
            tcedit.SelectedItem = newtab;
        }
        private void notechangesButton_Click(object sender, RoutedEventArgs e)
        { TabItem temp =(TabItem) tcedit.SelectedItem;
                 TextBox tb =(TextBox) temp.Content;
                 ulong id = UInt64.Parse(temp.Uid);
                 MySQL note = new MySQL();
                 note.Database = "notebookqyiren";
                 note.Server = "db4free.net";
                 note.UserID = "notebookqyiren";
                 note.Password = "19971117ramon";
                 note.Table = "notes";
                 note.Query = "Update notes set text='" + tb.Text +"' where id = "+id.ToString() ;
                 note.CreateMySQLConnection();
                 note.CreateMySqlAdapter();
                 note.ExecuteMySqlCommand();
           
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            s= new Schedule(CurrentUser.id);
            s.Left = this.Left - 360;
            s.Top = this.Top;
            s.Show();
        }
        

        #endregion

        #region new windows/pages


        #endregion

        #region SQl

        private void NewUserGetUserID()
        {
            MySQL user = new MySQL();
            user.Database = "notebookqyiren";
            user.Server = "db4free.net";
            user.UserID = "notebookqyiren";
            user.Password = "19971117ramon";
            user.Table = "user";
            user.CreateMySQLConnection();
            user.Query = "SELECT id FROM user ORDER BY id DESC LIMIT 0, 1;";
            user.CreateMySqlAdapter();
            DataTable userid_data = user.GetMySQLDataSet().Tables["user"];
            if ( userid_data != null){
                long temp = (long)userid_data.Rows[0]["id"];
                CurrentUser.id = (ulong)temp+1;
            }
            user.MySQLClose();
        }














        #endregion SQL

        

      


        
    }
}
