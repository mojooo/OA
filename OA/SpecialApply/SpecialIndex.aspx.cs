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

namespace OA.SpecialApply
{
    public partial class SpecialIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            int emid = Convert.ToInt32(Session["EmployeeId"]);
            int departId = Convert.ToInt32(Session["DepartId"]);
            bool ist1 = Common.IsActionNameOfMaster(emid, "�����������");
            bool ist2 = Common.IsActionNameOfMaster(emid, "�����������");

            string strRoleName = Session["RoleName"].ToString();
              

                   //��ȡ��½�û��Ľ�ɫ
                if (departId == 4 && ist1)
                {
                    Response.Redirect("SpecialApprove2.aspx");
                }
                else if (ist2)
                {
                    if (departId == 3 && strRoleName == "���ž���")
                    {
                        Response.Redirect("PreSpecialExpApprove.aspx");//�г�����������ҳ��
                    }
                    else
                    {
                        Response.Redirect("PreSpecialExpApply.aspx");//�г���Ա��
                    }
                    
                }
                else if (strRoleName == "�ܾ���" && ist1)
                {
                    Response.Redirect("SpecialApprove1.aspx");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ǰ��ɫû�в���Ȩ��');</script>");
                }
            }
          
    }
}
