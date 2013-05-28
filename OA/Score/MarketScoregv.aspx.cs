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
    public partial class MarketScoregv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["SortExpression"] = "DateSpan";
                ViewState["SortDir"] = "ASC";
                gvDataBind();
            }
        }

        protected void gvDataBind()
        {
            int emid = Convert.ToInt32(Session["EmployeeId"]);
            DataTable dt = BasEmScoreInfo.getMarketScore(emid);
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

        //�鿴��ϸ
        protected void btnDetail_Click(object sender, EventArgs e)
        {
            int MsId = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString());
            Response.Redirect("MarketScoreDetail.aspx?MarketScoreId=" + MsId);
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("MarketScoreAdd.aspx");
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
                    int tesid = Convert.ToInt32(gvScore.DataKeys[e.Row.RowIndex].Value);
                    MarketScoreInfo ms = new MarketScoreInfo(tesid);
                    if (ms.IsSubmit != 0)
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
            int MsScoreId = Convert.ToInt32(gvScore.DataKeys[e.RowIndex].Value);
            BasEmScoreInfo.DelMarketScore(MsScoreId);
            gvDataBind();
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int MsScoreId = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            MarketScoreInfo ms = new MarketScoreInfo(MsScoreId);
            ms.IsSubmit = 1;
            int recvid = Convert.ToInt32(Common.getEmployeeIdOfManager());
            EmployeeInfo em = new EmployeeInfo(Convert.ToInt32(ms.EmployeeId));
            EmployeeInfo ems = new EmployeeInfo(recvid);
            MessageInfo.Msgs1(recvid, ms.MarketScoreId, "~/Score/MarketScoreApprove.aspx", em.EmployeeName + "��Ч��������", em.EmployeeName, "daiban", em.EmployeeName + "��Ч��������");
            //MessageInfo.SendMail(ems.Qq, "��Ч��������", em.EmployeeName + "��Ч��������");
            ms.Save();
            gvDataBind();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            int MsScoreId = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            Response.Redirect("MarketScoreEdit.aspx?MsScoreId=" + MsScoreId);
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gvDataBind();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManagerHelp.aspx");
        }
    }
}
