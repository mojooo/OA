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

namespace OA.OverTime
{
    public partial class OverTimeMagv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "ApplyDate";
                ViewState["SortDir"] = "DESC";
                gvDataBind();
            }
        }

        protected void gvDataBind()
        {
            EmployeeInfo em = (EmployeeInfo)Session["Employee"];
            DataTable dt = OverTimeInfo.getAllApproveOvertime();
            DataView view = dt.DefaultView;
            string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
            view.Sort = sort;

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                UI.BindCtrl(dt.DefaultView, gvOverTime, AspNetPager1);
                gvOverTime.Rows[0].Visible = false;
            }
            else
            {
                UI.BindCtrl(dt.DefaultView, gvOverTime, AspNetPager1);
            }
        }

        protected void gvOverTime_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //����ƶ���ÿ��ʱ��ɫ����Ч��   
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#c1ebff'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
                //�����������ָ����״Ϊ"С��"   

                if (!Convert.IsDBNull(gvOverTime.DataKeys[e.Row.RowIndex].Value))
                {
                    int wkid = Convert.ToInt32(gvOverTime.DataKeys[e.Row.RowIndex].Value);
                    OverTimeInfo ot=new OverTimeInfo(wkid);
                    if (ot.IsSubmit == "δͨ��" || ot.IsSubmit == "ͨ��")
                    {
                        Button btnSubmit = e.Row.FindControl("btnSubmit") as Button;
                        btnSubmit.Enabled = false;
                    }
                }
            }
        }

        protected void gvOverTime_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int wksumId = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            Response.Redirect("OverTimeApprove.aspx?OverTimeId=" + wksumId);
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gvDataBind();
        }

        protected void lblDetail_Click(object sender, EventArgs e)
        {
            int WkId = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString());
            Response.Redirect("OverTimeDetail.aspx?OverTimeId=" + WkId);
        }
    }
}
