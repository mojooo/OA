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
    public partial class TreatApprove1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "SheetNum";
                ViewState["SortDir"] = "DESC";
                gvDataBind();
            }
        }

        protected void gvDataBind()
        {

            int emid = Convert.ToInt32(Session["EmployeeId"]);

            DataTable dt = BusinessExpInfo.getBusinessExpListOfApprove1(emid);

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

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gvDataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int wksumId = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            Response.Redirect("TApp1.aspx?BusinessExpId=" + wksumId);
        }

        //�鿴��ϸ
        protected void btnDetail_Click(object sender, EventArgs e)
        {
            int wksumId = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString());
            Response.Redirect("Detail.aspx?BusinessExpId=" + wksumId);
        }

        protected void gvBusiness_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //����ƶ���ÿ��ʱ��ɫ����Ч��   
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#c1ebff'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

                //�����������ָ����״Ϊ"С��"   
                e.Row.Attributes["style"] = "Cursor:hand";

                //�����ˡ�����ʱ��
                Label lblState = e.Row.FindControl("lblState") as Label;
                Button btnSubmit = e.Row.FindControl("btnSubmit") as Button;
                Label lblApplyName = e.Row.FindControl("lblApplyName") as Label;
                Label lblApplyTime = e.Row.FindControl("lblApplyTime") as Label;

                if (!Convert.IsDBNull(gvBusiness.DataKeys[e.Row.RowIndex].Value))
                {
                    int SaleId = Convert.ToInt32(gvBusiness.DataKeys[e.Row.RowIndex].Value);
                    BusinessExpInfo pc = new BusinessExpInfo(SaleId);

                    if (pc.SendEmployeeName == "")
                    {
                        lblApplyName.Text = pc.PreEmployeeName.ToString();//�г���Ա��
                        lblApplyTime.Text = pc.PreApplyTime.ToString();
                    }
                    else if (pc.PreEmployeeName == "")
                    {
                        lblApplyName.Text = pc.SendEmployeeName.ToString();//�г�������
                        lblApplyTime.Text = pc.ApplyTime.ToString();
                    }

                    //����GridView
                    GridView gv = e.Row.FindControl("GridView2") as GridView;

                    int emid = Convert.ToInt32(Session["EmployeeId"]);
                    int mainid = Convert.ToInt32(pc.MainBusinessExpId);
                    DataTable dt = BusinessExpInfo.getBusinessExpListOfApproves1(emid, mainid);

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
                    if (pc.IsApply1 == 1)
                    {
                        btnSubmit.Enabled = false;
                    }

                    //����״̬
                    if (pc.IsApply1 == 0)
                    {
                        lblState.Text = "����";
                    }
                    else if (pc.IsApply1 == 1 && pc.IsApply2 == 0)
                    {
                        if (pc.IsOver1 == 0)
                        {
                            lblState.Text = "����";
                        }
                        else
                        {
                            lblState.Text = "ͨ��";
                        }
                    }
                   

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

                    Label lblApplyName = e.Row.FindControl("lblApplyName") as Label;
                    Label lblApplyTime = e.Row.FindControl("lblApplyTime") as Label;
                    if (pc.SendEmployeeName == "")
                    {
                        lblApplyName.Text = pc.PreEmployeeName.ToString();//�г���Ա��
                        lblApplyTime.Text = pc.PreApplyTime.ToString();
                    }
                    else if (pc.PreEmployeeName == "")
                    {
                        lblApplyName.Text = pc.SendEmployeeName.ToString();//�г�������
                        lblApplyTime.Text = pc.ApplyTime.ToString();
                    }


                    //����״̬
                    if (pc.IsApply1 == 0)
                    {
                        lblState.Text = "����";
                    }
                    else if (pc.IsApply1 == 1 && pc.IsApply2 == 0)
                    {
                        if (pc.IsOver1 == 0)
                        {
                            lblState.Text = "����";
                        }
                        else
                        {
                            lblState.Text = "ͨ�� ";
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

       

    }
}
