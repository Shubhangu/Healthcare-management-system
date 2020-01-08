<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Registration.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style1 {
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table style="width: 100%;" cellpadding="3" cellspacing="3">
        <tr>
            <td class="style1" colspan="2">
                <strong>Registration</strong>
            </td>
        </tr>
        <tr>
            <td>Name
            </td>
            <td>
                <asp:TextBox ID="txtname" runat="server"></asp:TextBox><br />
            </td>
        </tr>
        <tr>
            <td>Password
            </td>
            <td>
                <asp:TextBox ID="txtpwd" runat="server" TextMode="Password"></asp:TextBox><br />
            </td>
        </tr>
        <tr>
            <td>Email
            </td>
            <td>
                <asp:TextBox ID="txtemail" runat="server"></asp:TextBox><br />
            </td>
        </tr>
        <tr>
            <td>Contact
            </td>
            <td>
                <asp:TextBox ID="txtcontact" runat="server"></asp:TextBox><br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
            </td>
        </tr>
    </table>
</asp:Content>
