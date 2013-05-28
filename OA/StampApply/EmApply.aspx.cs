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

namespace OA.StampApply
{
    public partial class StampApply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "ApplyTime";
                ViewState["SortDir"] = "DESC";
                gvDataBind();
            }
        }

      

        protected void gvDataBind()
        {
            DataTable dt = StampInfo.getStampApplyList(Session["EmployeeName"].ToString());
            DataView view = dt.DefaultView;
            string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
            view.Sort = sort;

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                UI.BindCtrl(dt.DefaultView, gvStamp, AspNetPager1);
                gvStamp.Rows[0].Visible = false;
            }
            else
            {
                UI.BindCtrl(dt.DefaultView, gvStamp, AspNetPager1);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            StampInfo stamp = new StampInfo();
            stamp.ApplyName = Session["EmployeeName"].ToString();
            stamp.DepartName = Session["DepartName"].ToString();
            stamp.UseDate = DateTime.Now.ToString("yyyy-MM-dd");
            stamp.StampFileTypeId =1;
            stamp.StampTypeId = 1;
            stamp.Stampuse = "";
            stamp.Memo = "";
            stamp.ApplyTime = "";
            stamp.State = 0;
            stamp.Save();
            gvDataBind();
           
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int StampId = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            StampInfo stamp = new StampInfo(StampId);
            stamp.State = 1;
            stamp.ApplyTime = DateTime.Now.ToString("yyyy-MM-dd");
            stamp.Save();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('�ύ�ɹ���');</script>");
            gvDataBind();
            
        }

        protected void gvStamp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblState = e.Row.FindControl("lblState") as Label;
                Button btnSubmit = e.Row.FindControl("btnSubmit") as Button;
                Button btnDelete = e.Row.FindControl("btnDelete") as Button;
                Button btnEdit = e.Row.FindControl("btnEdit") as Button;
                if (!Convert.IsDBNull(gvStamp.DataKeys[e.Row.RowIndex].Value))
                {
                    StampInfo stamp = new StampInfo(Convert.ToInt32(gvStamp.DataKeys[e.Row.RowIndex].Value));
                    if (stamp.State != 0)
                    {
                        btnSubmit.Enabled = false;
                        btnDelete.Enabled = false;
                        btnEdit.Enabled = false;
                    }

                    switch (stamp.State)
                    {
                        case 0:
                            lblState.Text = "δ�ύ";
                            break;
                        case 1:
                            lblState.Text = "�������쵼����";
                            break;
                        case 2:
                            lblState.Text = "����:����";
                            break;
                        case 3:
                            lblState.Text = "���ܾ�������";
                            break;
                        case 4:
                            lblState.Text = "ͨ��";
                            break;
                        case 5:
                            lblState.Text = "�ܾ���:����";
                            break;
                        default:
                            lblState.Text = "����״̬";
                            break;
                    }
                }         
                    DropDownList ddlFileType = e.Row.FindControl("ddlFileType") as DropDownList;
                    if (ddlFileType != null)
                    {
                        DataTable dt = StampInfo.getStampFileTypeList();
                        ddlFileType.DataSource = dt;
                        ddlFileType.DataTextField = "StampFileTypeName";
                        ddlFileType.DataValueField = "StampFileTypeId";
                        ddlFileType.DataBind();
                    }

                    DropDownList ddlStampType = e.Row.FindControl("ddlStampType") as DropDownList;
                    if (ddlStampType != null)
                    {
                        DataTable dt1 = StampInfo.getStampType();
                        ddlStampType.DataSource = dt1;
                        ddlStampType.DataTextField = "StampName";
                        ddlStampType.DataValueField = "StampTypeId";
                        ddlStampType.DataBind();
                    }

                    //����ƶ���ÿ��ʱ��ɫ����Ч��   
                    e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#c1ebff'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

                    //�����������ָ����״Ϊ"С��"   
                    e.Row.Attributes["style"] = "Cursor:hand";
                }
            
        }

    


        protected void gvStamp_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvStamp.EditIndex = e.NewEditIndex;
            gvDataBind();
        }

        protected void gvStamp_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int StampId = Convert.ToInt32(gvStamp.DataKeys[e.RowIndex].Value);
            TextBox txtApplyDate = gvStamp.Rows[e.RowIndex].FindControl("txtApplyDate") as TextBox;
            DropDownList ddlFileType = gvStamp.Rows[e.RowIndex].FindControl("ddlFileType") as DropDownList;
            DropDownList ddlStampType = gvStamp.Rows[e.RowIndex].FindControl("ddlStampType") as DropDownList;
            TextBox txtUse = gvStamp.Rows[e.RowIndex].FindControl("txtUse") as TextBox;
            TextBox txtMemo = gvStamp.Rows[e.RowIndex].FindControl("txtMemo") as TextBox;
            try
            {
                    StampInfo stamp = new StampInfo(StampId);
                   
                    stamp.UseDate = txtApplyDate.Text.ToString();
                    stamp.StampFileTypeId = Convert.ToInt32(ddlFileType.SelectedValue);
                    stamp.StampTypeId = Convert.ToInt32(ddlStampType.SelectedValue);
                    stamp.Stampuse = txtUse.Text.ToString();
                    stamp.Memo = txtMemo.Text.ToString();
                    stamp.Save();
                    gvStamp.EditIndex = -1;
                    gvDataBind();
            }
            catch (Exception Ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('����ʧ�ܣ�" + Ex.Message + "');", true);
            }
        }

        protected void gvStamp_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvStamp.EditIndex = -1;
            gvDataBind();
        }

        protected void gvStamp_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void gvStamp_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int StampId = Convert.ToInt32(gvStamp.DataKeys[e.RowIndex].Value);
            StampInfo.DelStamp(StampId);
            gvDataBind();
        }
    }
}
