<%@ Page Title="" Language="C#" MasterPageFile="~/User/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="User_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td class="auto-style1"><strong>DataSet</strong></td>
        </tr>
        <tr>
            <td>

                <asp:Chart ID="Chart1" runat="server" Width="850px">
                    <Series>
                        <asp:Series Name="Series1" XValueMember="Name" YValueMembers="Temperature"
                            LegendText="Temperature" IsValueShownAsLabel="false" ChartArea="ChartArea1"
                            MarkerBorderColor="#DBDBDB">
                        </asp:Series>

                        <asp:Series Name="Series2" XValueMember="Name" YValueMembers="Haemoglobin"
                            LegendText="Haemoglobin" IsValueShownAsLabel="false" ChartArea="ChartArea1"
                            MarkerBorderColor="#DBDBDB">
                        </asp:Series>
                        <asp:Series Name="Series3" XValueMember="Name" YValueMembers="SystolicPressure"
                            LegendText="Systolic Pressure" IsValueShownAsLabel="false" ChartArea="ChartArea1"
                            MarkerBorderColor="#DBDBDB">
                        </asp:Series>

                         <asp:Series Name="Series4" XValueMember="Name" YValueMembers="DystolicPressure"
                            LegendText="Dystolic Pressure" IsValueShownAsLabel="false" ChartArea="ChartArea1"
                            MarkerBorderColor="#DBDBDB">
                        </asp:Series>

                         <asp:Series Name="Series5" XValueMember="Name" YValueMembers="PulseRate"
                            LegendText="Pulse Rate" IsValueShownAsLabel="false" ChartArea="ChartArea1"
                            MarkerBorderColor="#DBDBDB">
                        </asp:Series>
                    </Series>
                    <Legends>
                        <asp:Legend Title="Quarter" />
                    </Legends>
                    <Titles>
                        <asp:Title Docking="Bottom" Text="Comparission Chart" />
                    </Titles>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                
            </td>
        </tr>
    </table>
</asp:Content>

