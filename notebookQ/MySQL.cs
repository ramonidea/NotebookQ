
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
//        Author: Stephen C. Allen
// Creation Date: July 01, 2008
//       Version: 3.0
//     Copyright: 2008
//          Name: clsSQL.vb 
//      Assembly: MyLibrary
//Root Namespace: SCA.Data

using MySql.Data.MySqlClient;


public class MySQL
{
    #region "Fields"
    //Non-Property Fields    
    private string _connectString = null;
    private MySqlConnection _MySQLconn = new MySqlConnection();
    private MySqlConnection _MsSQLconn = new MySqlConnection();
    private bool _error = false;
    private string _Error_Description;
    private MySqlDataAdapter _myDataAdapter = new MySqlDataAdapter();
    private DataSet _ds = new DataSet();

    private MySqlCommand _myCommand = new MySqlCommand();
    //Property Fields
    private string _database = null;
    private string _password = null;
    private string _server = null;
    private string _query = null;
    private string _table = null;
    #endregion
    private string _userid = null;

    #region "Properties"

    public MySqlDataAdapter MyAdapter
    {
        get { return _myDataAdapter; }
        set { _myDataAdapter = value; }
    }

    public MySqlConnection MyConnection
    {
        get { return _MySQLconn; }
        set { _MySQLconn = value; }
    }

    /// <summary>
    /// Gets/Sets the database name.
    /// </summary>
    /// <value>Database name as string.</value>
    /// <returns>Database name as string</returns>
    /// <remarks>This property is used when creating a connection to the database (CreateMsSQLConnection/CreateMySQLConnection)</remarks>
    public string Database
    {
        get { return _database; }
        set { _database = value; }
    }


    /// <summary>
    /// Gets/Sets the database password.
    /// </summary>
    /// <value>Password as string</value>
    /// <returns>Password as string</returns>
    /// <remarks>This property is used when creating a connection to the database {CreateMsSQLConnection/CreateMySQLConnection}</remarks>
    public string Password
    {
        get { return _password; }
        set { _password = value; }
    }

    /// <summary>
    /// Gets/Sets a query to the database
    /// </summary>
    /// <value>SQL Query as string</value>
    /// <returns>SQL Query as string</returns>
    /// <remarks>This must be set in order to {CreateMsSQLAdapter/CreateMySQLAdapter}</remarks>
    public string Query
    {
        get { return _query; }
        set { _query = value; }
    }

    /// <summary>
    /// Gets/Sets (Server/Host Name) for database 
    /// </summary>
    /// <value>Server name as string</value>
    /// <returns>Server name as string</returns>
    /// <remarks>This property is used when creating a connection to the database (CreateMsSQLConnection/CreateMySQLConnection)</remarks>
    public string Server
    {
        get { return _server; }
        set { _server = value; }
    }

    /// <summary>
    /// Gets/Sets table name
    /// </summary>
    /// <value>Table name as string</value>
    /// <returns>Table name as string</returns>
    /// <remarks>This must be set in order to {CreateMsSQLAdapter/CreateMySQLAdapter}</remarks>
    public string Table
    {
        get { return _table; }
        set { _table = value; }
    }

    /// <summary>
    /// Gets/Sets (UserID/UserName) for database.
    /// </summary>
    /// <value>username as string</value>
    /// <returns>username as string</returns>
    /// <remarks>This property is used when creating a connection to the database (CreateMsSQLConnection/CreateMySQLConnection)</remarks>
    public string UserID
    {
        get { return _userid; }
        set { _userid = value; }
    }
    /// <summary>
    /// Returns the error description as string.  First check HasError to determine if there is an error.
    /// </summary>
    /// <value></value>
    /// <returns>String</returns>
    /// <remarks></remarks>
    public string GetErrorDecription
    {
        get { return _Error_Description; }
    }

    #endregion


    #region "Methods"

    /// <summary>
    /// Create a connection to the MySQL server
    /// Server, UserID, Password and Database must be set before calling this method
    /// </summary>
    /// <remarks>Check HasError and GetErrorDescription to determine if there was errors and a description of the error is any.</remarks>
    public void CreateMySQLConnection()
    {
        if ((_server != null) & (_userid != null) & (_password != null) & (_database != null))
        {
            _connectString = "server=" + _server + "; Port=3306; user id=" + _userid + "; password=" + _password + "; database=" + _database + "; pooling=false; CharSet=utf8mb4;";
        }
        try
        {
            _MySQLconn = new MySqlConnection(_connectString);
        }
        catch (Exception ex)
        {
            _error = true;
            _Error_Description = ex.Message;
        }
    }

    public void CreateMySQLConnection(string ConnectionString)
    {
        string[] f = ConnectionString.Split(';');
        _server = f[0].Split('=')[1];
        _database = f[1].Split('=')[1];
        _userid = f[2].Split('=')[1];
        _password = f[3].Split('=')[1];
        if ((_server != null) & (_userid != null) & (_password != null) & (_database != null))
        {
            _connectString = ConnectionString;
        }
        try
        {
            _MySQLconn = new MySqlConnection(_connectString);
        }
        catch (Exception ex)
        {
            _error = true;
            _Error_Description = ex.Message;
        }
    }





    /// <summary>
    /// Creates a MySQL Adapter: 
    /// Query, Table and CreateConnection must be done first.
    /// </summary>
    /// <remarks>Check HasError and GetErrorDescription to determine if there was errors and a description of the error.</remarks>
    public void CreateMySqlAdapter()
    {
        try
        {
            _myDataAdapter = new MySqlDataAdapter(_query, _MySQLconn);
        }
        catch (Exception ex)
        {
            _error = true;
            _Error_Description = ex.Message;
        }
    }




    /// <summary>
    /// Creates a dataset using the Table Property
    /// CreateMySqlAdapter must be done set first
    /// </summary>
    /// <returns>A System.Data DataSet with Tables(0) equal to the Table Property</returns>
    /// <remarks>Check HasError and GetErrorDescription to determine if there was errors and a description of the error.</remarks>
    public DataSet GetMySQLDataSet()
    {
        try
        {
            _myDataAdapter.Fill(_ds, _table);
        }
        catch (Exception ex)
        {
            _error = true;
            _Error_Description = "**" + ex.Message + " -- " + _table;
        }
        return _ds;
    }



    /// <summary>
    /// Creates a dataset using the supplied table string
    /// CreateMySqlAdapter must be done set first
    /// </summary>
    /// <returns>A System.Data DataSet with Tables(0) equal to the Table Property</returns>
    /// <remarks>Check HasError and GetErrorDescription to determine if there was errors and a description of the error.</remarks>
    public DataSet GetMySQLDataSet(string table)
    {
        try
        {
            _myDataAdapter.Fill(_ds, table);
        }
        catch (Exception ex)
        {
            _error = true;
            _Error_Description = ex.Message;
        }
        return _ds;
    }




    /// <summary>
    /// Returns true if there has been an error otherwise false
    /// </summary>
    /// <returns>Boolean</returns>
    /// <remarks>If set to true then check GeetErrorDescription for a descripton of the error</remarks>
    public bool HasError()
    {
        return _error;
    }

    /// <summary>
    /// Executes a MySQL non-Select query like "INSERT INTO table (field1, field2, field3 ...) values ('','',''....)"
    /// "UPADTE table SET field1='', field2='' WHERE field5=''"
    /// </summary>
    /// <param name="CommandText"></param>
    /// <remarks>Check HasError and GetErrorDescription to determine if there was errors and a description of the error.</remarks>
    public long ExecuteMySqlCommand(string CommandText)
    {
        MySqlCommand cmd = new MySqlCommand();
        long id = new long();

        cmd.Connection = _MySQLconn;
        cmd.CommandText = CommandText;

        try
        {
            _MySQLconn.Open();
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            id = Convert.ToInt64(cmd.ExecuteScalar());
            _MySQLconn.Close();
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex.Message)
            _error = true;
            _Error_Description = ex.Message;
            //RaiseEvent MySqlError(ex.Message)
        }
        return id;
    }

    /// <summary>
    /// Executes a MySQL non-Select query like "INSERT INTO table (field1, field2, field3 ...) values ('','',''....)"
    /// "UPADTE table SET field1='', field2='' WHERE field5=''"  Using the Query Property
    /// </summary>
    /// <remarks>Check HasError and GetErrorDescription to determine if there was errors and a description of the error.</remarks>
    public long ExecuteMySqlCommand()
    {
        MySqlCommand cmd = new MySqlCommand();
        long id = new long();

        cmd.Connection = _MySQLconn;
        cmd.CommandText = this.Query;

        try
        {
            _MySQLconn.Open();
            cmd.ExecuteNonQuery();
            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            id = Convert.ToInt64(cmd.ExecuteScalar());
            _MySQLconn.Close();
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex.Message)
            _error = true;
            _Error_Description = ex.Message;
            //RaiseEvent MySqlError(ex.Message)
        }
        return id;
    }








    /// <summary>
    /// Creates an MySQL table using the supplied TableName and TableDefinition
    /// </summary>
    /// <param name="TableName"></param>
    /// <param name="TableDefinition"></param>
    /// <remarks>Check HasError and GetErrorDescription to determine if there was errors and a description of the error.</remarks>
    public void CreateMySQLTable(string TableName, string TableDefinition)
    {
        string query = "CREATE TABLE " + TableName + " " + TableDefinition;
        this.ExecuteMySqlCommand();
    }





    /// <summary>
    /// Deletes a MySQL table from active database using the supplied TableName
    /// </summary>
    /// <param name="TableName"></param>
    /// <remarks>Check HasError and GetErrorDescription to determine if there was errors and a description of the error.</remarks>
    public void DeleteMySQLTable(string TableName)
    {
        Query = "DROP TABLE " + TableName;
        this.ExecuteMySqlCommand(Query);
    }

    /// <summary>
    /// Deletes a MySQL table from active database using the supplied Table Property as the table name
    /// </summary>
    /// <remarks>Check HasError and GetErrorDescription to determine if there was errors and a description of the error.</remarks>
    public void DeleteMySQLTable()
    {
        Query = "DROP TABLE IF EXISTS " + this.Table;
        this.ExecuteMySqlCommand(Query);
    }



    public bool myTableExist(string TableName)
	{
		
		Query = "SELECT * FROM INFORMATION_SCHEMA.TABLES";
		Table = "Tables";
		CreateMySqlAdapter();
		_ds = GetMySQLDataSet();
		if (_ds.Tables["Tables"].Rows.Count > 0) {
			foreach ( DataRow dr in _ds.Tables["Tables"].Rows) {
				if (dr["TABLE_NAME"].ToString() == TableName) {
					return true;
				}
			}
		}
		return false;
	}



    public Array myListOfTables()
	{
		
		ArrayList al = new ArrayList();

		Query = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE Table_Schema='" + Database + "'";
		Table = "Tables";
		CreateMySqlAdapter();
		_ds = GetMySQLDataSet();
		if (_ds.Tables["Tables"].Rows.Count > 0) {
			foreach (DataRow dr in _ds.Tables["Tables"].Rows) {
				al.Add(dr["Table_Name"].ToString());
			}
		}
		return al.ToArray();
	}








    public string GetMySQLDate(System.DateTime MyDate)
    {
        string month = MyDate.Month.ToString().PadLeft(2, '0');
        string day = MyDate.Day.ToString().PadLeft(2, '0');
        string year = MyDate.Year.ToString();

        return year + "-" + month + "-" + day;
    }

    public void MySQLClose()
    {
        _MySQLconn.Close();
    }




    #endregion
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
