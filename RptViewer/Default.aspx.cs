using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using Microsoft.Reporting.WebForms;
using RptViewer.DAL;

namespace RptViewer
{


    public partial class _Default : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.Label custName;
        protected System.Web.UI.WebControls.Label acctName;
    //    protected HtmlGenericControl PageTitle = new HtmlGenericControl();

        protected void Page_Init(object sender, EventArgs e)
        {
            string customername = string.Empty;
            string UserName = string.Empty;
            string AcctID = string.Empty;
            Boolean showRpt = true;

            if (Request.QueryString["ctl"] == null)
            {
               Response.Redirect("error.aspx");
            }
            else
            {
                string text = Request.QueryString["ctl"];

                //string[] ctls = new String[2];
                //  ctls = text.Split('*');

                string[] ctls = text.Split(new char[] { '-' }, 2);
                AcctID = ctls[0];
                UserName = ctls[1];

                SqlDataReader drUser = UsersDB.ValidUser(UserName, AcctID);
                if (drUser.Read())
                {
                    acctName.Text = drUser["accountname"].ToString();
                    if (acctName.Text == string.Empty)
                    {
 //                       Response.Redirect("error.aspx");
                        throw new Exception("account name issue");
                    }

                    custName.Text = drUser["customer"].ToString();
                    if (custName.Text == string.Empty)
                    {
 //                       Response.Redirect("error.aspx");
                        throw new Exception("customer issue");
                    }
 //                   UserName = drUser["UserName"].ToString();

                    customername = drUser["customername"].ToString();
                }

            }

           // Uri ReportServer;
            string ReportPath = string.Empty;

            if (Request.QueryString["rpt"] == null)
            {
                ReportViewer1.Visible = false;
                showRpt = false;
            }
            else
            {
                string rptid = Request.QueryString["rpt"];

                SqlDataReader drReport = ReportDB.ValidReport(rptid);
                if (drReport.Read())
                {
                   // ReportServer = drReport["ReportServer"];
                    ReportPath = drReport["ReportPath"].ToString();
                    reportName.Text = drReport["ReportName"].ToString() + ' ' + customername;
                    Page.Title = drReport["ReportName"].ToString();
                }
                else    
                {
                    throw new Exception("report path issue");
//                    Response.Redirect("error.aspx");
                }
            }

            ReportViewer1.ServerReport.DisplayName = reportName.Text;


           ReportViewer1.ServerReport.ReportServerCredentials =
                new MyReportServerCredentials();

           if (showRpt)
           {
               SetReportParameters(ReportPath, UserName, AcctID);
           }
        }


        public void SetReportParameters(string ReportPath, string UserName, string AcctID)
        {
//            Color c = ColorTranslator.FromHtml("DDF1FF");

            Color myColor = Color.FromArgb(221, 241, 255);


            ReportViewer1.BackColor = myColor;
            //ReportViewer1.SplitterBackColor = System.Drawing.Color.LightBlue;
            //ReportViewer1.ToolBarItemHoverBackColor = System.Drawing.Color.Yellow;


            
            // Set Processing Mode

            
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;

    //    ReportViewer1.ServerReport.ReportServerUrl = ReportServer;


        ReportViewer1.ServerReport.ReportPath = ReportPath;
        
        List<ReportParameter> paramList = new List<ReportParameter>();


        paramList.Add(new ReportParameter("AcctID", AcctID));
        paramList.Add(new ReportParameter("UserName", UserName));

        ReportViewer1.ServerReport.SetParameters(paramList);

    // Process and render the report

    // reportViewer1.RefreshReport();

        }
    }

    [Serializable]
    public sealed class MyReportServerCredentials :
        IReportServerCredentials
    {
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                // Use the default Windows user.  Credentials will be
                // provided by the NetworkCredentials property.
                return null;
            }
        }

        public ICredentials NetworkCredentials
        {
            get
            {
                // Read the user information from the Web.config file.  
                // By reading the information on demand instead of 
                // storing it, the credentials will not be stored in 
                // session, reducing the vulnerable surface area to the
                // Web.config file, which can be secured with an ACL.

                // User name
                string userName =
                    ConfigurationManager.AppSettings
                        ["MyReportViewerUser"];

                if (string.IsNullOrEmpty(userName))
                    throw new Exception(
                        "Missing user name from web.config file");

                // Password
                string password =
                    ConfigurationManager.AppSettings
                        ["MyReportViewerPassword"];

                if (string.IsNullOrEmpty(password))
                    throw new Exception(
                        "Missing password from web.config file");

                // Domain
                string domain =
                    ConfigurationManager.AppSettings
                        ["MyReportViewerDomain"];

                if (string.IsNullOrEmpty(domain))
                    throw new Exception(
                        "Missing domain from web.config file");

                return new NetworkCredential(userName, password, domain);
            }
        }

        public bool GetFormsCredentials(out Cookie authCookie,
                    out string userName, out string password,
                    out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;

            // Not using form credentials
            return false;
        }
    }


    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }


}
