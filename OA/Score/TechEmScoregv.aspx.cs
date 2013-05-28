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
    public partial class TechEmScoregv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "DateSpan";
                ViewState["SortDir"] = "ASC";
                gvDataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("TechEmScoreAdd.aspx");
        }

        protected void gvDataBind()
        {
            int emid = Convert.ToInt32(Session["EmployeeId"]);
            DataTable dt = TechEmScoreInfo.getTechEmScoreGv(emid);
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


        //查看详细
        protected void btnDetail_Click(object sender, EventArgs e)
        {
            int TechEmId = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString());
            Response.Redirect("TechEmScoreDetail.aspx?TechEmScoreId=" + TechEmId);
        }

        protected void gvScore_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标移动到每项时颜色交替效果   
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#c1ebff'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
                //设置悬浮鼠标指针形状为"小手"   
                e.Row.Attributes["style"] = "Cursor:hand";
                if (!Convert.IsDBNull(gvScore.DataKeys[e.Row.RowIndex].Value))
                {
                    int tesid = Convert.ToInt32(gvScore.DataKeys[e.Row.RowIndex].Value);
                    TechEmScoreInfo tes = new TechEmScoreInfo(tesid);
                    if (tes.IsSubmit!=0)
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
            int TechEmScoreId = Convert.ToInt32(gvScore.DataKeys[e.RowIndex].Value);
            TechEmScoreInfo.DelTechEmScore(TechEmScoreId);
            gvDataBind();
        }

        protected void gvScore_Sorting(object sender, GridViewSortEventArgs e)
        {
            //首先判断当前的排序表达式（字段），是否为当前当前的表达式。。。
            if (ViewState["SortExpression"].ToString() == e.SortExpression.ToString())
            {
                //判断当前的排序方式是否为倒序（DESC），如果是则将排序方式设为升序（ASC），原因是：因为我们要做双向排序！
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
                ViewState["SortExpression"] = e.SortExpression; //将触发到表达式赋值给ViewState["SortExpression"];
            }
            gvDataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int TechEmScoreId = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            TechEmScoreInfo tes = new TechEmScoreInfo(TechEmScoreId);
            tes.IsSubmit = 1;

            int recvid = Convert.ToInt32(Common.getEmployeeIdOfTechManager());
            EmployeeInfo em = new EmployeeInfo(Convert.ToInt32(tes.EmployeeId));
            EmployeeInfo ems = new EmployeeInfo(recvid);
            MessageInfo.Msgs1(recvid, tes.TechEmScoreId, "~/Score/TechEmApprove.aspx", em.EmployeeName + "绩效考核申请", em.EmployeeName, "daiban", em.EmployeeName + "绩效考核申请");
            MessageInfo.SendMail(ems.Qq, "绩效考核申请", em.EmployeeName + "绩效考核申请");
            tes.Save();
            gvDataBind();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gvDataBind();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            int TechEmScoreId = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            Response.Redirect("TechEmScoreEdit.aspx?TechEmScoreId="+TechEmScoreId);
        }
    }
}
