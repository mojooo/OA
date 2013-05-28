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

namespace OA.OfferPrice
{
    public partial class PrePriceApply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "PreApplyTime";
                ViewState["SortDir"] = "DESC";
                ddlProjectBind();
                gvDataBind();
            }
        }

        protected void ddlProjectBind()
        {
            DataTable dt = MainProjectCreateInfo.getProjectList();
            ddlProject.DataSource = dt;
            ddlProject.DataTextField = "ProjectName";
            ddlProject.DataValueField = "MainProjectCreateId";
            DataRow row = dt.NewRow();
            row["ProjectName"] = "请选择项目名称";
            dt.Rows.InsertAt(row, 0);
            ddlProject.DataBind();
        }

        protected void gvDataBind()
        {

            int emid = Convert.ToInt32(Session["EmployeeId"]);
            DataTable dt = OfferPriceInfo.getPreOfferPriceListOfEmployee(emid);
           
            DataView view = dt.DefaultView;
            string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
            view.Sort = sort;

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                UI.BindCtrl(dt.DefaultView, gvPrice, AspNetPager1);
                gvPrice.Rows[0].Visible = false;
            }
            else
            {
                UI.BindCtrl(dt.DefaultView, gvPrice, AspNetPager1);
            }
           
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            int wksumId = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            OfferPriceInfo wksum = new OfferPriceInfo(wksumId);
            if (wksum.PreIsApply == 0)
            {
                wksum.PreIsApply = 1;
                DateTime date = DateTime.Now;
                wksum.PreApplyTime = date;
                int recvemid = Convert.ToInt32(Common.getEmployeeIdOfMarketManager());//市场部经理接收项目立项表
                wksum.RecvEmployeeId = recvemid;
                wksum.Save();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('申请成功！');</script>");
                gvDataBind();
               
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            int wksumId = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            Response.Redirect("EditPrice.aspx?OfferPriceId=" + wksumId);
        }

        //查看详细
        protected void btnDetail_Click(object sender, EventArgs e)
        {
            int wksumId = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString());
            Response.Redirect("Detail.aspx?OfferPriceId=" + wksumId);
        }



        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Check if this is our Blank Row being databound, if so make the row invisible
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((DataRowView)e.Row.DataItem)["OfferPriceId"].ToString() == String.Empty)
                    e.Row.Visible = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标移动到每项时颜色交替效果   
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#c1ebff'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

                //设置悬浮鼠标指针形状为"小手"   
                e.Row.Attributes["style"] = "Cursor:hand";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblState = e.Row.FindControl("lblState") as Label;
                GridView gvTemp = (GridView)sender;
                if (!Convert.IsDBNull(gvTemp.DataKeys[e.Row.RowIndex].Value))
                {
                    int PriceId = Convert.ToInt32(gvTemp.DataKeys[e.Row.RowIndex].Value);

                    OfferPriceInfo pc = new OfferPriceInfo(PriceId);
                    if (pc.IsApprove == 0)
                    {
                        lblState.Text = "市场部:待审 总经办:待审";
                    }
                    else if (pc.IsApprove == 1 && pc.IsApply2 == 0)
                    {
                        if (pc.PreIsOver == 1)
                        {
                            lblState.Text = "市场部:通过 总经办:待审";
                        }
                        else
                        {
                            lblState.Text = "市场部:修正";
                        }

                    }
                    else if (pc.IsApply2 == 1)
                    {
                        if (pc.IsOver2 == 1)
                        {
                            lblState.Text = "市场部:通过 总经办:通过";
                        }
                        else
                        {
                            lblState.Text = "市场部:通过 总经办:修正";
                        }
                    }
                }
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmployeeInfo em = (EmployeeInfo)Session["Employee"];
            string SheetNum = txtSheetNum.Text.ToString();
            if (SheetNum != "" && ddlProject.SelectedValue == "")
            {
               
                DataTable dt = OfferPriceInfo.getSheetNumOfPrice(SheetNum, em.EmployeeName.ToString());
                DataView view = dt.DefaultView;
                string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
                view.Sort = sort;

                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                    UI.BindCtrl(view, gvPrice, AspNetPager2);
                    gvPrice.Rows[0].Visible = false;
                }
                else
                {
                    UI.BindCtrl(view, gvPrice, AspNetPager2);
                }

            }
            else if (ddlProject.SelectedValue != "" && SheetNum == "")
            {

                DataTable dt=OfferPriceInfo.getProjectOfPrice(ddlProject.SelectedItem.Text, em.EmployeeName.ToString());
                DataView view = dt.DefaultView;
                string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
                view.Sort = sort;

                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                    UI.BindCtrl(view, gvPrice, AspNetPager1);
                    gvPrice.Rows[0].Visible = false;
                }
                else
                {
                    UI.BindCtrl(view, gvPrice, AspNetPager1);
                }
            }
            else if (ddlProject.SelectedValue != "" && SheetNum != "")
            {
              
                DataTable dt=OfferPriceInfo.getBothNameOfPrice(txtSheetNum.Text.ToString(), ddlProject.SelectedItem.Text.ToString(), em.EmployeeName.ToString());
                DataView view = dt.DefaultView;
                string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
                view.Sort = sort;

                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                    UI.BindCtrl(view, gvPrice, AspNetPager1);
                    gvPrice.Rows[0].Visible = false;
                }
                else
                {
                    UI.BindCtrl(view, gvPrice, AspNetPager1);
                }
            }

            else
            {
                gvDataBind();
            }
        

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrePriceApply.aspx");
        }

        protected void btnAddSheet_Click(object sender, EventArgs e)
        {
            OfferPriceInfo op = new OfferPriceInfo();
            op.Save();
            int opid = Convert.ToInt32(op.OfferPriceId);
            Response.Redirect("AddPrice.aspx?OfferPriceId=" + opid);
        }

        protected void gvPrice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblState = e.Row.FindControl("lblState") as Label;
                Button btnSubmit = e.Row.FindControl("btnSubmit") as Button;
                Button btnEdit = e.Row.FindControl("btnEdit") as Button;

                if (!Convert.IsDBNull(gvPrice.DataKeys[e.Row.RowIndex].Value))
                {

                    //绑定子GridView
                    GridView gv = e.Row.FindControl("GridView2") as GridView;
                    int emid = Convert.ToInt32(Session["EmployeeId"]);
                    int SaleId = Convert.ToInt32(gvPrice.DataKeys[e.Row.RowIndex].Value);

                    OfferPriceInfo pc = new OfferPriceInfo(SaleId);
                    int mainid = Convert.ToInt32(pc.MainOfferPriceId);
                    DataTable dt = OfferPriceInfo.getPreOfferPriceListOfEmployee2(emid, mainid);
                    if (dt.Rows.Count == 0)
                    {
                        dt.Rows.Add(dt.NewRow());
                        UI.BindCtrl(dt.DefaultView, gv, AspNetPager1);
                        gv.Rows[0].Visible = false;
                    }
                    else
                    {
                        UI.BindCtrl(dt.DefaultView, gv, AspNetPager1);
                    }


                    //button控制
                    if (pc.PreIsApply == 1)
                    {
                        btnSubmit.Enabled = false;

                        if (pc.IsNewCreate == 1)//审批未通过，修正。
                        {
                            btnEdit.Enabled = true;
                        }
                        else//默认IsNewCreate==0，已申请，编辑按钮不可用。
                        {
                            btnEdit.Enabled = false;
                        }

                    }

                    if (pc.IsApprove == 0)
                    {
                        lblState.Text = "市场部:待审 总经办:待审";
                    }
                    else if (pc.IsApprove == 1 && pc.IsApply2 == 0)
                    {
                        if (pc.PreIsOver == 1)
                        {
                            lblState.Text = "市场部:通过  总经办:待审";
                        }
                        else
                        {
                            lblState.Text = "市场部:修正";
                        }

                    }
                    else if (pc.IsApply2 == 1)
                    {
                        if (pc.IsOver2 == 1)
                        {
                            lblState.Text = "市场部:通过  总经办:通过";
                        }
                        else
                        {
                            lblState.Text = "市场部:通过  总经办:修正";
                        }
                    }
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //鼠标移动到每项时颜色交替效果   
                    e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#c1ebff'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

                    //设置悬浮鼠标指针形状为"小手"   
                    e.Row.Attributes["style"] = "Cursor:hand";
                }
            }
        }

        protected void gvPrice_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gvDataBind();
        }

    }
}
