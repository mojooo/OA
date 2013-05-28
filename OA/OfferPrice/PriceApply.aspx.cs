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
    public partial class PriceApply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "ApplyTime";
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
            row["ProjectName"] = "��ѡ����Ŀ����";
            dt.Rows.InsertAt(row, 0);
            ddlProject.DataBind();
        }

        protected void gvDataBind()
        {
            int emid = Convert.ToInt32(Session["EmployeeId"]);
            DataTable dt = OfferPriceInfo.getOfferPriceListOfEmployee(emid);

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

        protected void btnAddSale_Click(object sender, EventArgs e)
        {
            OfferPriceInfo op = new OfferPriceInfo();
            op.Save();
            int opid = Convert.ToInt32(op.OfferPriceId);
            Response.Redirect("AddPrice1.aspx?OfferPriceId=" + opid);
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gvDataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int wksumId = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            OfferPriceInfo wksum = new OfferPriceInfo(wksumId);

            wksum.IsApply = 1;
            DateTime date = DateTime.Now;
            wksum.ApplyTime = date;

            //int recvemid = Convert.ToInt32(Common.getEmployeeIdOfFinaceManager());//ֻ�в��񲿾����ܽ�����Ŀ�����
            int recvemid = Convert.ToInt32(Common.getEmployeeIdOfManager());//�ܾ������
            wksum.Recv2EmployeeId = recvemid;
            wksum.Save();
            gvDataBind();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('����ɹ���');</script>");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            int wksumId = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            Response.Redirect("EditPrice1.aspx?OfferPriceId=" + wksumId);
        }

        //�鿴��ϸ
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
                //����ƶ���ÿ��ʱ��ɫ����Ч��   
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#c1ebff'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

                //�����������ָ����״Ϊ"С��"   
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


                    if (pc.IsApply2 == 0)
                    {
                        lblState.Text = "�ܾ���:����";
                    }
                  
                    else if (pc.IsApply2 == 1)
                    {
                        if (pc.IsOver2 == 1)
                        {
                            lblState.Text = "�ܾ���:ͨ��";
                        }
                        else
                        {
                            lblState.Text = "�ܾ���:����";
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

                DataTable dt = OfferPriceInfo.getSheetNumOfPrice1(SheetNum, em.EmployeeName.ToString());
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
            else if (ddlProject.SelectedValue != "" && SheetNum == "")
            {

                DataTable dt = OfferPriceInfo.getProjectOfPrice1(ddlProject.SelectedItem.Text, em.EmployeeName.ToString());
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

                DataTable dt = OfferPriceInfo.getBothNameOfPrice1(txtSheetNum.Text.ToString(), ddlProject.SelectedItem.Text.ToString(), em.EmployeeName.ToString());
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
            Response.Redirect("PriceApply.aspx");
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
                    int emid = Convert.ToInt32(Session["EmployeeId"]);
                    //����GridView
                    GridView gv = e.Row.FindControl("GridView2") as GridView;
                    int SaleId = Convert.ToInt32(gvPrice.DataKeys[e.Row.RowIndex].Value);

                    OfferPriceInfo pc = new OfferPriceInfo(SaleId);
                    int mainid = Convert.ToInt32(pc.MainOfferPriceId);
                    DataTable dt = OfferPriceInfo.getOfferPriceListOfEmployee2(emid, mainid);
                    if (dt.Rows.Count == 0)
                    {
                        dt.Rows.Add(dt.NewRow());
                        UI.BindCtrl(dt.DefaultView, gv, AspNetPager2);
                        gv.Rows[0].Visible = false;
                    }
                    else
                    {
                        UI.BindCtrl(dt.DefaultView, gv, AspNetPager2);
                    }

                    //button����
                    if (pc.IsApply == 1)
                    {
                        btnSubmit.Enabled = false;

                        if (pc.IsNewCreate == 1)//����δͨ����������
                        {
                            btnEdit.Enabled = true;
                        }
                        else//Ĭ��IsNewCreate==0�������룬�༭��ť�����á�
                        {
                            btnEdit.Enabled = false;
                        }

                    }


                    if (pc.IsApply2 == 0)
                    {
                        lblState.Text = "�ܾ���:����";
                    }
                    
                    else if (pc.IsApply2 == 1)
                    {
                        if (pc.IsOver2 == 1)
                        {
                            lblState.Text = "�ܾ���:ͨ��";
                        }
                        else
                        {
                            lblState.Text = "�ܾ���:����";
                        }
                    }
                }

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //����ƶ���ÿ��ʱ��ɫ����Ч��   
                    e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#c1ebff'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

                    //�����������ָ����״Ϊ"С��"   
                    e.Row.Attributes["style"] = "Cursor:hand";
                }
            }
        }

        protected void gvPrice_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PrePriceApprove.aspx");
        }
    }
}
