<%@ Page Title="" Language="C#" MasterPageFile="~/User/Site.master" AutoEventWireup="true" CodeFile="DataSet.aspx.cs" Inherits="User_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .auto-style1 {
            text-align: center;
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <table style="width: 100%;">
        <tr>
            <td class="auto-style1"><strong>DataSet</strong></td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="100%" Style="text-align: center">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Temperature" HeaderText="Temperature" SortExpression="Temperature" />

                        <asp:BoundField DataField="Haemoglobin" HeaderText="Haemoglobin" SortExpression="Haemoglobin" />
                        <asp:BoundField DataField="SystolicPressure" HeaderText="Systolic Pressure" SortExpression="SystolicPressure" />
                        <asp:BoundField DataField="DystolicPressure" HeaderText="Dystolic Pressure" SortExpression="DystolicPressure" />
                        <asp:BoundField DataField="PulseRate" HeaderText="Pulse Rate" SortExpression="PulseRate" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:patientmonitoring %>" SelectCommand="SELECT * FROM [DataSet]"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

