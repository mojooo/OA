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

namespace OA.Score
{
    public partial class ScoreIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            EmployeeInfo em = (EmployeeInfo)Session["Employee"];
            string strDepartName = Session["DepartName"].ToString();
            string strRoleName = Session["RoleName"].ToString();
            //��ȡ��½�û��Ľ�ɫ
            switch (strDepartName)
            {
                case "������":
                    if (strRoleName == "���ž���")
                        Response.Redirect("TechMaScoreGv.aspx");
                    else
                        Response.Redirect("TechEmScoregv.aspx");
                    break;
                case "�ܾ���":
                    if (strRoleName == "�ܾ���")
                        Response.Redirect("ManagerGv.aspx");
                    break;
                case "�г���":
                    if (em.EmployeeName == "����")
                        Response.Redirect("BASEmScoregv.aspx");
                    else if (em.EmployeeName == "�δ�")
                        Response.Redirect("MarketScoregv.aspx");
                    break;
                case "�ۺϲ�":
                    if (em.EmployeeName == "����")
                        Response.Redirect("FrontScoregv.aspx");
                    else if (em.EmployeeName == "����")
                        Response.Redirect("AsistantScoregv.aspx");
                    else if (em.EmployeeName == "�κ���")
                        Response.Redirect("DriveScoregv.aspx");
                    else if (em.EmployeeName == "�Ų�")
                        Response.Redirect("NurseScoregv.aspx");
                    break;
                default:
                    Response.Redirect("tab.aspx");
                    break;


            }
        }
    }
}
