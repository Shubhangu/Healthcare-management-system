using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

public partial class User_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            Chart1.DataSource = GetData("SELECT [Name],[Temperature],[Date],[Haemoglobin],[SystolicPressure],[DystolicPressure],[PulseRate] FROM [DataSet] where name='Aditya'");
            Chart1.DataBind();
        }
    }

    private static DataTable GetData(string query)
    {
        string constr = ConfigurationManager.ConnectionStrings["patientmonitoringconnection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                return dt;
            }
        }
    }
}