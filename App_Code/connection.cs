using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public class connection
{
    SqlConnection conn;

    string get_connect = System.Configuration.ConfigurationManager.ConnectionStrings["gpmce"].ConnectionString;

    public connection()
    {
        conn = new SqlConnection(get_connect);
    }

    public void open_connection()
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
    }

    public void close_connection()
    {
        if (conn.State == ConnectionState.Open)
        {
            conn.Close();
        }
    }

    public SqlConnection con_pass()
    {
        return conn;
    }
}