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

namespace OA.WeekScore
{
    public partial class WeekScore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                PageInit();
                gvDataBind();
            }
        }

        protected void PageInit()
        {
             int emid = Convert.ToInt32(Session["EmployeeId"]);
            ViewState["employeeid"] =emid ;

            ViewState["SortExpression"] = "WeekScoreNum";
            ViewState["SortDir"] = "DESC";
        }

        protected void gvDataBind()
        {
            DataTable dt = DayScoreInfo.getWeekScore(Convert.ToInt32(ViewState["employeeid"]));
            DataView view = dt.DefaultView;
            string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
            view.Sort = sort;

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                UI.BindCtrl(dt.DefaultView, gvScore, AspNetPager1);
                gvScore.Rows[0].Visible = false;
            }
            else
            {
                UI.BindCtrl(dt.DefaultView, gvScore, AspNetPager1);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("DayScore.aspx?EmployeeId="+ViewState["employeeid"]);
        }

        protected void gvScore_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void gvScore_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //����ƶ���ÿ��ʱ��ɫ����Ч��   
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#c1ebff'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
                //�����������ָ����״Ϊ"С��"   
                e.Row.Attributes["style"] = "Cursor:hand";
                if (!Convert.IsDBNull(gvScore.DataKeys[e.Row.RowIndex].Value))
                {
                    int wkid = Convert.ToInt32(gvScore.DataKeys[e.Row.RowIndex].Value);
                    WeekScoreInfo wk = new WeekScoreInfo(wkid);
                    if (wk.IsSubmit != 0)
                    {
                        Button btnSubmit = e.Row.FindControl("btnSubmit") as Button;
                        Button btnEdit = e.Row.FindControl("btnEdit") as Button;
                        Button btnDelete = e.Row.FindControl("btnDelete") as Button;
                        btnSubmit.Enabled = false;
                        btnEdit.Enabled = false;
                        btnDelete.Enabled = false;
                    }
                }
            }
        }

        protected void gvScore_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int WkScoreId = Convert.ToInt32(gvScore.DataKeys[e.RowIndex].Value);
            DayScoreInfo.DelDaysOfWk(WkScoreId);
            DayScoreInfo.DelWeekScore(WkScoreId);
            gvDataBind();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gvDataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int WkScoreId = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            WeekScoreInfo wk = new WeekScoreInfo(WkScoreId);
            wk.IsSubmit = 1;
            wk.Save();
            gvDataBind();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            int WkScoreId = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            Response.Redirect("EditScore.aspx?WeekScoreId=" + WkScoreId);
        }

        protected void lblDetail_Click(object sender, EventArgs e)
        {
            int WkId = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString());
            Response.Redirect("ScoreDetail.aspx?WeekScoreId=" + WkId);
        }

        protected void btnAll_Click(object sender, EventArgs e)
        {
            Response.Redirect("AllScores.aspx");
        }
    }
}
