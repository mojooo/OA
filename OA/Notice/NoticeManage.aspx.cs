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

namespace OA.Notice
{
    public partial class NoticeManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "NoticeTime";
                ViewState["SortDir"] = "DESC";
                gvDataBind();
            }
        }

        protected void gvDataBind()
        {
            DataTable dt = NoticeInfo.getNoticeList();
           
            DataView view = dt.DefaultView;
            string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
            view.Sort = sort;

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                UI.BindCtrl(dt.DefaultView, gvNotice, AspNetPager1);
                gvNotice.Rows[0].Visible = false;
            }
            else
            {
                UI.BindCtrl(dt.DefaultView, gvNotice, AspNetPager1);
            }
        }

        protected  string SubContent(string content)
        {
            if (content.Length > 20)
            {
                content = content.Substring(0, 20) + "...";
            }
            if (content == "")
            {
                content = "123";
            }
            return content;
        }

        protected void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvNotice.Rows.Count; i++)
            {
                CheckBox ckb = gvNotice.Rows[i].FindControl("ckbChoose") as CheckBox;
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < gvNotice.Rows.Count; i++)
                {
                    CheckBox ckb = gvNotice.Rows[i].FindControl("ckbChoose") as CheckBox;
                    if (ckb.Checked == true)
                    {
                        int noticeid = Convert.ToInt32(gvNotice.DataKeys[i].Value);
                        NoticeInfo.DelNotice(noticeid);
                       
                    }
                }
                gvDataBind();
            }
            catch (Exception Ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('������Ϣ:" + Ex.Message + "');", true);
            }
        }

        protected void gvNotice_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gvDataBind();
        }

        protected void btnAddNotice_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddNotice.aspx");
        }
    }
}
