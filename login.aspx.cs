using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["patientmonitoringconnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click1(object sender, EventArgs e)
    {
             
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select [Emailid],password from Registration where Emailid='" + TextBox1.Text + "' and password='" + pwd.Value + "'", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Session["Name"] = dr[0].ToString();
                
                Response.Redirect("~/User/PatientMonitoring.aspx");
                
            }
            else
            {
                string javaScript = "<script language=JavaScript>\n" + "alert('Error !!');\n" + "</script>";
                RegisterStartupScript("image1_ClickScript", javaScript);
            }
            conn.Close();



        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
   
}