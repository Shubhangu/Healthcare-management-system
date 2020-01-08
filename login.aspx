<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="_Default"
    MasterPageFile="~/Site.master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .auto-style1 {
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table align="center">
        <tr>
            <td colspan="3" class="auto-style1">Login
            </td>
        </tr>
        <tr>
            <td class="auto-style1">User Name</td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" ToolTip="Email" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox1"
                    ErrorMessage="Email is required." ToolTip="Email is required.">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">Password</td>
            <td>
                <input type="password" id="pwd" runat="server" required>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Pwd"
                    ErrorMessage="Password is required." ToolTip="Password is required.">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>

                <asp:Button ID="Button1" runat="server" CommandName="Log In" CssClass="button" Text="Log In"
                    OnClick="Button1_Click1" />
            </td>
        </tr>
    </table>
</asp:Content>
