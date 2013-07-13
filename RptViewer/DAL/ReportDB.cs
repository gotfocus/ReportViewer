using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Configuration;
using System.Web.Configuration;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace RptViewer.DAL
{


    public class ReportDB
    {
        public static SqlDataReader ValidReport(string rptID)
        {
            string sqlQuery = @"SELECT ReportServer, ReportPath, ReportName
                                  FROM tblClientReportPaths
                                  WHERE ID = @rptID";

            SqlConnection sqlConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn);

            sqlCmd.Parameters.Add("@rptid", SqlDbType.Char);
            sqlCmd.Parameters["@rptid"].Value = rptID;

            sqlCmd.Connection.Open();
            return sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);

        }
    }
}