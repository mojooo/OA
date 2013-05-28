using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CanYou.OA.BLL;

namespace OA.ApplyManage
{
    public partial class WeekIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strRoleName = Session["RoleName"].ToString();
            if (strRoleName == "Ա��")
            {
                Response.Redirect("WeekApply.aspx");
            }
            else if(strRoleName=="�ܾ���"||strRoleName=="���ž���")
            {
                Response.Redirect("WeekApprove.aspx");
            }
        }
    }
}
