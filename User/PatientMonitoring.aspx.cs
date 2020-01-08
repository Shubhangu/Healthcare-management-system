using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class _Default : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["patientmonitoringconnection"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private Decimal[] GetInputforshirt()
    {
        String[] arrStr = txtvalue.Text.Split(",".ToArray());
        Decimal[] arrDec = new Decimal[arrStr.Length];

        for (Int32 i = 0; i < arrStr.Length; i++)
        {
            arrDec[i] = Decimal.Parse(arrStr[i]);
        }
        return arrDec;
    }

    private Decimal[] GetInputforheartbeat()
    {
        String[] arrStr = txtheartbeat.Text.Split(",".ToArray());
        Decimal[] arrDec = new Decimal[arrStr.Length];

        for (Int32 i = 0; i < arrStr.Length; i++)
        {
            arrDec[i] = Decimal.Parse(arrStr[i]);
        }

        return arrDec;
    }
    string text = string.Empty;
    string top = string.Empty;
    string top1 = string.Empty;
    string text1 = string.Empty;
    protected void Button1_Click(object sender, EventArgs e)
    {
        //count total transcation of area for reason
        con.Open();
        SqlCommand da1 = new SqlCommand("SELECT top 15 Temperature FROM [dataset] where name='" + DropDownList1.Text + "'", con);
        SqlDataReader dt1 = da1.ExecuteReader();
        if (dt1.HasRows)
        {
            GridView1.DataSource = dt1;
            GridView1.DataBind();


        }

        con.Close();


        con.Open();
        SqlCommand da = new SqlCommand("select top 5 Temperature from [dataset] where [name]='" + DropDownList1.Text + "' ", con);
        SqlDataReader dr = da.ExecuteReader();
        while (dr.Read())
        {
            string productname = dr[0].ToString();
            top += productname + ",";
            txtvalue.Text = top.TrimEnd(',');

        }
        txtvalue.Text.TrimEnd(',');
        con.Close();

        DataTable dt = new DataTable();

        con.Open();
        SqlCommand cmd1 = new SqlCommand("select top 5 name,Temperature from [dataset] where [name]='" + DropDownList1.Text + "' ", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd1);
        sda.Fill(dt);
        con.Close();

        string[] x = new string[dt.Rows.Count];
        int[] y = new int[dt.Rows.Count];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            x[i] = dt.Rows[i][0].ToString();
            y[i] = Convert.ToInt32(dt.Rows[i][1]);
        }
        Chart1.Series[0].Points.DataBindXY(x, y);


       

        ForecastTable dt10 = TimeSeries.naive(GetInputforshirt(), 1, 6);
        GridView2.DataSource = dt10;
        GridView2.DataBind();

        ForecastTable dt11 = TimeSeries.weightedMovingAverage(GetInputforshirt(), 1, (Decimal)0.05, (Decimal)0.15, (Decimal)0.8);
        GridView3.DataSource = dt11;
        GridView3.DataBind();

        ForecastTable dt12 = TimeSeries.wellsweilderMovingaverage(GetInputforshirt(), 1, 2, 2);
        GridView6.DataSource = dt12;
        GridView6.DataBind();



        con.Open();
        SqlCommand da12 = new SqlCommand("select top 5 PulseRate from [dataset] where [name]='" + DropDownList1.Text + "' ", con);
        SqlDataReader dr1 = da12.ExecuteReader();
        while (dr1.Read())
        {
            string productname = dr1[0].ToString();
            top1 += productname + ",";
            txtheartbeat.Text = top1.TrimEnd(',');

        }
        txtheartbeat.Text.TrimEnd(',');
        con.Close();

        DataTable dt2 = new DataTable();

        con.Open();
        SqlCommand cmd2 = new SqlCommand("select top 5 name,PulseRate from [dataset] where [name]='" + DropDownList1.Text + "' ", con);
        SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
        sda2.Fill(dt2);
        con.Close();       


        ForecastTable dt102 = TimeSeries.naive(GetInputforheartbeat(), 1, 6);
        GridView4.DataSource = dt102;
        GridView4.DataBind();

        ForecastTable dt112 = TimeSeries.weightedMovingAverage(GetInputforheartbeat(), 1, (Decimal)0.05, (Decimal)0.15, (Decimal)0.8);
        GridView5.DataSource = dt112;
        GridView5.DataBind();

        foreach (GridViewRow row in GridView4.Rows)
        {
            string val = GridView3.Rows[5].Cells[2].Text;
           // Label2.Text = "Upcoming  Of State:" + ddlarea.Text + "of crime:" + DropDownList1.Text + val;
        }

    }
}