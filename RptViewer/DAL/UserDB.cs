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


    public class UsersDB
    {
        public static SqlDataReader ValidUser(string UserName, string AcctID)
        {

            string sqlQuery = @"SELECT DISTINCT AA.accountname, (CC.ct_First_Name + ' ' + CC.ct_Last_Name) AS customer, U.username,
                                (select tblActAccounts.accountname 
                                    from tblActAccounts
                                    where ID = @AcctID ) AS customername
                                  FROM tblusers U
                                  JOIN tblconcontacts CC
                                  ON U.contactid = CC.ct_id
                                  JOIN tblactaccountscontacts AC
                                  ON AC.contactid = CC.ct_id
                                  JOIN tblactaccounts AA
                                  ON AA.id = AC.accountid
                                  WHERE U.GUID = CONVERT(uniqueidentifier, @UserName) AND CC.ct_accountid IN (@AcctID, 10520956)";

            SqlConnection sqlConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn);

            sqlCmd.Parameters.Add("@UserName", SqlDbType.Char);
            sqlCmd.Parameters["@UserName"].Value = UserName;
            sqlCmd.Parameters.Add("@AcctID", SqlDbType.Char);
            sqlCmd.Parameters["@AcctID"].Value = AcctID;

            sqlCmd.Connection.Open();
            return sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);

        }
    }
}