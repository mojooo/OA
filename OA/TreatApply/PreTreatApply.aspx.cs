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
    public partial class PreTreatApply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "PreApplyTime";
                ViewState["SortDir"] = "DESC";
                gvDataBind();
            }
        }

        protected void gvDataBind()
        {
            int emid = Convert.ToInt32(Session["EmployeeId"]);
            DataTable dt = BusinessExpInfo.getBusinessExpListOfPreEmployee(emid);
            
            DataView view = dt.DefaultView;
            string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
            view.Sort = sort;

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                UI.BindCtrl(dt.DefaultView, gvBusiness, AspNetPager1);
                gvBusiness.Rows[0].Visible = false;
            }
            else
            {
                UI.BindCtrl(dt.DefaultView, gvBusiness, AspNetPager1);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int wksumId = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            BusinessExpInfo wksum = new BusinessExpInfo(wksumId);
           
            if (wksum.PreIsApply == 0)
            {
                

                wksum.PreIsApply = 1;
                DateTime date = DateTime.Now;
                wksum.PreApplyTime = date;
                int recvemid = Convert.ToInt32(Common.getEmployeeIdOfMarketManager());//�г������������Ŀ�����
                wksum.RecvEmployeeId = recvemid;
                wksum.Save();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('����ɹ���');</script>");
                gvDataBind();
                
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            int wksumId = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            Response.Redirect("EditTreat.aspx?BusinessExpId=" + wksumId);
        }

        //�鿴��ϸ
        protected void btnDetail_Click(object sender, EventArgs e)
        {
            int wksumId = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString());
            Response.Redirect("Detail.aspx?BusinessExpId=" + wksumId);
        }

        protected void btnAddSheet_Click(object sender, EventArgs e)
        {
            BusinessExpInfo bExp = new BusinessExpInfo();
            bExp.Save();
            int bExpId = Convert.ToInt32(bExp.BusinessExpId);
            Response.Redirect("AddTreat.aspx?BusinessExpId=" + bExpId);
        }

        protected void gvBusiness_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblState = e.Row.FindControl("lblState") as Label;
                Button btnSubmit = e.Row.FindControl("btnSubmit") as Button;
                Button btnEdit = e.Row.FindControl("btnEdit") as Button;

                if (!Convert.IsDBNull(gvBusiness.DataKeys[e.Row.RowIndex].Value))
                {

                    //����GridView
                    GridView gv = e.Row.FindControl("GridView2") as GridView;
                    int emid = Convert.ToInt32(Session["EmployeeId"]);
                    int SaleId = Convert.ToInt32(gvBusiness.DataKeys[e.Row.RowIndex].Value);

                    BusinessExpInfo pc = new BusinessExpInfo(SaleId);
                    int mainid = Convert.ToInt32(pc.MainBusinessExpId);

                    DataTable dt=BusinessExpInfo.getBusinessExpListOfPreEmployee2(emid, mainid);
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
                    if (pc.PreIsApply == 1)
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

                    if (pc.IsApprove == 0)
                    {
                        lblState.Text = "�г���:���� �ܾ���:����";
                    }
                    else if (pc.IsApprove == 1 && pc.IsApply1 == 0)
                    {
                        if (pc.PreIsOver == 1)
                        {
                            lblState.Text = "�г���:ͨ�� �ܾ���:����";
                        }
                        else
                        {
                            lblState.Text = "�г���:����";
                        }

                    }
                    else if (pc.IsApply1 == 1 && pc.IsApply2 == 0)
                    {
                        if (pc.IsOver1 == 1)
                        {
                            lblState.Text = "�г���:ͨ�� �ܾ���:ͨ��";
                        }
                        else
                        {
                            lblState.Text = "�г���:ͨ�� �ܾ���:����";
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

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Check if this is our Blank Row being databound, if so make the row invisible
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((DataRowView)e.Row.DataItem)["BusinessExpId"].ToString() == String.Empty)
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
                    int SaleId = Convert.ToInt32(gvTemp.DataKeys[e.Row.RowIndex].Value);
                    BusinessExpInfo pc = new BusinessExpInfo(SaleId);

                    if (pc.IsApprove == 0)
                    {
                        lblState.Text = "�г���:���� �ܾ���:����";
                    }
                    else if (pc.IsApprove == 1 && pc.IsApply1 == 0)
                    {
                        if (pc.PreIsOver == 1)
                        {
                            lblState.Text = "�г���:ͨ�� �ܾ���:����";
                        }
                        else
                        {
                            lblState.Text = "�г���:����";
                        }

                    }
                    else if (pc.IsApply1 == 1 && pc.IsApply2 == 0)
                    {
                        if (pc.IsOver1 == 1)
                        {
                            lblState.Text = "�г���:ͨ�� �ܾ���:ͨ��";
                        }
                        else
                        {
                            lblState.Text = "�г���:ͨ�� �ܾ���:����";
                        }
                    }
                    
                }
            }
        }


        protected void gvBusiness_Sorting(object sender, GridViewSortEventArgs e)
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
    }
}
