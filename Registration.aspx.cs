using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    string ip6 = "";
    save s = new save();
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        s.insert("insert into Registration([Username],[Password],[Emailid],[Contactno]) values('" + txtname.Text + "','" + txtpwd.Text + "','" + txtemail.Text + "','" + txtcontact.Text + "')");
        string javaScript = "<script language=JavaScript>\n" + "alert('Registration successful !!');\n" + "</script>";
        RegisterStartupScript("image1_ClickScript", javaScript);



    }

}

