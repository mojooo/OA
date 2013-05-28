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

namespace OA.ClientManage
{
    public partial class Client : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "SheetNum";
                ViewState["SortDir"] = "DESC";
                gvDataBind();
                ddlDataBind();
            }
        }

        protected void ddlDataBind()
        {
            DataTable dt = ClientInfo.getClientLevelList();
            ddlClientLevel.DataSource = dt;
            ddlClientLevel.DataTextField = "ClientLevelName";
            ddlClientLevel.DataValueField = "ClientLevelId";
            DataRow row = dt.NewRow();
            row["ClientLevelName"] = "��ѡ��ͻ��ȼ�";
            dt.Rows.InsertAt(row, 0);
            ddlClientLevel.DataBind();
        }

        protected void gvDataBind()
        {
            DataTable dt = ClientInfo.getClientList();
          
            DataView view = dt.DefaultView;
            string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
            view.Sort = sort;

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                UI.BindCtrl(dt.DefaultView, gvClient, AspNetPager1);
                gvClient.Rows[0].Visible = false;
            }
            else
            {
                UI.BindCtrl(dt.DefaultView, gvClient, AspNetPager1);
                
            }
        }

      

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gvDataBind();
        }

        protected void btnAddClient_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddClient.aspx");
        }

        protected void gvClient_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvClient.EditIndex = -1;
            gvDataBind();
        }

        protected void gvClient_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlClient = e.Row.FindControl("ddlClientLevel") as DropDownList;
                if (ddlClient != null)
                {
                    DataTable dt = ClientInfo.getClientLevelList();
                    ddlClient.DataSource = dt;
                    ddlClient.DataTextField = "ClientLevelName";
                    ddlClient.DataValueField = "ClientLevelId";
                    ddlClient.DataBind();
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

        protected void gvClient_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int ClientId = Convert.ToInt32(gvClient.DataKeys[e.RowIndex].Value);
            TextBox txtClientName = gvClient.Rows[e.RowIndex].FindControl("txtClientName") as TextBox;
            TextBox txtTelephone = gvClient.Rows[e.RowIndex].FindControl("txtTelephone") as TextBox;
            TextBox txtMailNo = gvClient.Rows[e.RowIndex].FindControl("txtMailNo") as TextBox;
            TextBox txtFax = gvClient.Rows[e.RowIndex].FindControl("txtFax") as TextBox;
            TextBox txtAddress = gvClient.Rows[e.RowIndex].FindControl("txtAddress") as TextBox;
            DropDownList ddlClient = gvClient.Rows[e.RowIndex].FindControl("ddlClientLevel") as DropDownList;
            try
            {
                ClientInfo client = new ClientInfo(ClientId);
                client.ClientName = txtClientName.Text.ToString();
                client.Telephone = txtTelephone.Text.ToString();
                client.MailNo = txtMailNo.Text.ToString();
                client.Fax = txtFax.Text.ToString();
                client.Address = txtAddress.Text.ToString();
                client.ClientLevelId = Convert.ToInt32(ddlClient.SelectedValue);
                client.Save();
                gvClient.EditIndex = -1;
                gvDataBind();
            }
            catch (Exception Ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('����ʧ�ܣ�" + Ex.Message + "');", true); 
            }
        
        }

        protected void gvClient_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvClient.EditIndex = e.NewEditIndex;
            gvDataBind();
        }

        protected void gvClient_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ClientId = Convert.ToInt32(gvClient.DataKeys[e.RowIndex].Value);
            ClientInfo.DelRelateOfClient(ClientId);
            ClientInfo.ClientDel(ClientId);
            gvDataBind();
        }

        protected void btnRelate_Click(object sender, EventArgs e)
        {
            int ClientId = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            Response.Redirect("AddRelate.aspx?ClientId=" + ClientId);
        }


        protected void AspNetPager2_PageChanged(object sender, EventArgs e)
        {
            
        }

        protected void gvClient_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

            Label lblclient = gvClient.Rows[e.NewSelectedIndex].FindControl("lblClientId") as Label;

            int clientid = Convert.ToInt32(lblclient.Text.ToString());
            DataTable dt = ClientInfo.getRelateOfClient(clientid);

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                UI.BindCtrl(dt.DefaultView, gvRelate, AspNetPager2);
                gvRelate.Rows[0].Visible = false;
            }
            else
            {
                UI.BindCtrl(dt.DefaultView, gvRelate, AspNetPager2);

            }
        }

        protected void gv2DataBind(int index)
        {
            Label lblclient = gvRelate.Rows[index].FindControl("lblClientId") as Label;
            int clientid = Convert.ToInt32(lblclient.Text.ToString());
            DataTable dt = ClientInfo.getRelateOfClient(clientid);

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                UI.BindCtrl(dt.DefaultView, gvRelate, AspNetPager2);
                gvRelate.Rows[0].Visible = false;
            }
            else
            {
                UI.BindCtrl(dt.DefaultView, gvRelate, AspNetPager2);

            }

        }

        protected void gvRelate_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int RelateId = Convert.ToInt32(gvRelate.DataKeys[e.RowIndex].Value);
            ClientInfo.RelateDel(RelateId);
            gv2DataBind(e.RowIndex);
        }

        protected void gvRelate_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvRelate.EditIndex = e.NewEditIndex;
            gv2DataBind(e.NewEditIndex);
        }

        protected void gvRelate_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int RelateId = Convert.ToInt32(gvRelate.DataKeys[e.RowIndex].Value);
            RelateInfo relate = new RelateInfo(RelateId);
            TextBox txtEmail = gvRelate.Rows[e.RowIndex].FindControl("txtEmail") as TextBox;
            TextBox txtRelateName=gvRelate.Rows[e.RowIndex].FindControl("txtRelateName") as TextBox;
            TextBox txtMobile=gvRelate.Rows[e.RowIndex].FindControl("txtMobile") as TextBox;
            TextBox txtMemo=gvRelate.Rows[e.RowIndex].FindControl("txtMemo") as TextBox;
            TextBox txtPosition = gvRelate.Rows[e.RowIndex].FindControl("txtPosition") as TextBox;
            try
            {
                relate.Email = txtEmail.Text.ToString();
                relate.RelateName = txtRelateName.Text.ToString();
                relate.Mobile = txtMobile.Text.ToString();
                relate.Memo = txtMemo.Text.ToString();
                relate.Position = txtPosition.Text.ToString();
                relate.Save();
                gvRelate.EditIndex = -1;
                gv2DataBind(e.RowIndex);
            }
            catch (Exception Ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('����ʧ�ܣ�" + Ex.Message + "');", true); 
            }


        }

        protected void gvRelate_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvRelate.EditIndex = -1;
            gv2DataBind(e.RowIndex);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string ClientName = txtClientName.Text.ToString();
            if (ClientName != "" && ddlClientLevel.SelectedValue == "")
            {
               
                DataTable dt = ClientInfo.getNameOfClient(ClientName);
                DataView view = dt.DefaultView;
                string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
                view.Sort = sort;

                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                    UI.BindCtrl(view, gvClient, AspNetPager1);
                    gvClient.Rows[0].Visible = false;
                }
                else
                {
                    UI.BindCtrl(view, gvClient, AspNetPager1);
                }

            }
            else if (ddlClientLevel.SelectedValue != "" && ClientName == "")
            {


                DataTable dt = ClientInfo.getLevelOfClient(Convert.ToInt32(ddlClientLevel.SelectedValue));
                DataView view = dt.DefaultView;
                string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
                view.Sort = sort;

                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                    UI.BindCtrl(view, gvClient, AspNetPager1);
                    gvClient.Rows[0].Visible = false;
                }
                else
                {
                    UI.BindCtrl(view, gvClient, AspNetPager1);
                }
            }
            else if (ddlClientLevel.SelectedValue != "" && ClientName != "")
            {
                DataTable dt = ClientInfo.getBothOfClient(Convert.ToInt32(ddlClientLevel.SelectedValue), ClientName);
                DataView view = dt.DefaultView;
                string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
                view.Sort = sort;

                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                    UI.BindCtrl(view, gvClient, AspNetPager1);
                    gvClient.Rows[0].Visible = false;
                }
                else
                {
                    UI.BindCtrl(view, gvClient, AspNetPager1);
                }
            }

            else
            {
                gvDataBind();
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            gvDataBind();
        }

        protected void gvClient_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void gvRelate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
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
}
