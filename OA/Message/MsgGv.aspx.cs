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

namespace OA.Message
{
    public partial class MsgGv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "MsgTime";
                ViewState["SortDir"] = "DESC";
                gvDataBind();
            }
        }


        protected void gvDataBind()
        {
            DataTable dt = MessageInfo.MsgGv();
            DataView view = dt.DefaultView;
            string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
            view.Sort = sort;

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                UI.BindCtrl(dt.DefaultView, gvMsg, AspNetPager1);
                gvMsg.Rows[0].Visible = false;
            }
            else
            {
                UI.BindCtrl(dt.DefaultView, gvMsg, AspNetPager1);
            }
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            try
            {
                int j = 0;
                string[] filearray = new String[11];
                filearray[0] = " ";
                for (int i = 0; i < gvMsg.Rows.Count; i++)
                {
                    CheckBox ckb = gvMsg.Rows[i].FindControl("ckbChoose") as CheckBox;
                    if (ckb.Checked == true)
                    {
                        int msgid = Convert.ToInt32(gvMsg.DataKeys[i].Value);
                        MessageInfo msg = new MessageInfo(msgid);
                        EmployeeInfo em = new EmployeeInfo(Convert.ToInt32(msg.RecvEmployeeId));
                        MessageInfo.SendMail(em.Qq, "OA�µ���Ϣ", msg.EmployeeName + "," + msg.Msg);
                    }
                }
                gvDataBind();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('���ѳɹ���');</script>");
            }
            catch (Exception Ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('������Ϣ:" + Ex.Message + "');", true);
            }
          
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gvDataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int msgid =  Convert.ToInt32(((Button)sender).CommandArgument.ToString());;
            MessageInfo msg = new MessageInfo(msgid);
            EmployeeInfo em = new EmployeeInfo(Convert.ToInt32(msg.RecvEmployeeId));
            MessageInfo.SendMail(em.Qq, "OA�µ���Ϣ", msg.EmployeeName + "," + msg.Msg);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('���ѳɹ���');</script>");
        }

        protected void gvMsg_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //����ƶ���ÿ��ʱ��ɫ����Ч��   
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#c1ebff'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
                //�����������ָ����״Ϊ"С��"   
                e.Row.Attributes["style"] = "Cursor:hand";
            }
        }

        protected void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvMsg.Rows.Count; i++)
            {
                CheckBox ckb = gvMsg.Rows[i].FindControl("ckbChoose") as CheckBox;
                if (ckbAll.Checked == true)
                {
                    ckb.Checked = true;
                }
                else
                {
                    ckb.Checked = false;
                }
            }
        }

        protected void gvMsg_Sorting(object sender, GridViewSortEventArgs e)
        {
            //�����жϵ�ǰ��������ʽ���ֶΣ����Ƿ�Ϊ��ǰ��ǰ�ı��ʽ������
            if (ViewState["SortExpression"].ToString() == e.SortExpression.ToString())
            {
                //�жϵ�ǰ������ʽ�Ƿ�Ϊ����DESC���������������ʽ��Ϊ����ASC����ԭ���ǣ���Ϊ����Ҫ��˫������
                if (ViewState["SortDir"].ToString() == "DESC")
                {
                    ViewState["SortDir"] = "ASC";
                }
                else
                {
                    ViewState["SortDir"] = "DESC";
                }
            }
            else
            {
                ViewState["SortExpression"] = e.SortExpression; //�����������ʽ��ֵ��ViewState["SortExpression"];
            }
            gvDataBind();
        }
    }
}
