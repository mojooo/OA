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
namespace OA.TreatApply
{
    public partial class TreatIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            int emid = Convert.ToInt32(Session["EmployeeId"]);
            bool ist1 = Common.IsActionNameOfMaster(emid, "�����д�����");
            bool ist2 = Common.IsActionNameOfMaster(emid, "�����д�����");

            string strRoleName = Session["RoleName"].ToString();
            string strDepartName = Session["DepartName"].ToString();
               //��ȡ��½�û��Ľ�ɫ
                if (strDepartName=="����"&& ist1)
                {
                    Response.Redirect("TreatApprove2.aspx");
                }
                else if (ist2)
                {
                    if (strDepartName == "�г���" && strRoleName == "���ž���")
                    {
                        Response.Redirect("PreTreatApprove.aspx"); ;//�г�����������ҳ��
                    }
                    else
                    {
                        Response.Redirect("PreTreatApply.aspx");//�г���Ա��
                    }

                  
                }
                else if (strRoleName == "�ܾ���" && ist1)
                {
                    Response.Redirect("TreatApprove1.aspx");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ǰ��ɫû�в���Ȩ��');</script>");
                }
            }
          
    }
}
