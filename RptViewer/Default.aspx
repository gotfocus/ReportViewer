<%@ Page Title="My Report Viewer" RUNAT="server" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="RptViewer._Default" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
   
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <h1><asp:Label id="reportName" runat="server" /></h1>
      <br />
      <h2><asp:Label id="acctName" runat="server" /></h2><asp:Label id="custName" runat="server" />
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" 
        WaitControlDisplayAfter="10000" Font-Names="Verdana" Font-Size="8pt" 
    InteractiveDeviceInfos="(Collection)" ProcessingMode="Remote" 
    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="600px" 
        Width="925px">
        <ServerReport ReportPath="/Marketing/Products Ordered By Product Family" 
            ReportServerUrl="http://223.203.188.140/reportserver" />
</rsweb:ReportViewer>
    <p>
    <!---    <ServerReport ReportPath="/OutsideClientReporting/Client Application Integration Test Report" 
            ReportServerUrl="http://340375-DB5/reportserver" /> --->
    </p>
   <!--- <p>
        You can also find <a href="http://go.microsoft.com/fwlink/?LinkID=152368&amp;clcid=0x409"
            title="MSDN ASP.NET Docs">documentation on ASP.NET at MSDN</a>.
    </p>  --->
</asp:Content>
