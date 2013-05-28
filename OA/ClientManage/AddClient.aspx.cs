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

namespace OA.MarketManage
{
    public partial class AddClient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlBind();
                ddlClientTypeBind();
            }
        }

        protected void ddlBind()
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

        protected void  ddlClientTypeBind()
        {
            DataTable dt = ClientInfo.getClientTypeList();
            ddlClientType.DataSource = dt;
            ddlClientType.DataTextField = "ClientTypeName";
            ddlClientType.DataValueField = "ClientTypeId";
            DataRow row = dt.NewRow();
            row["ClientTypeName"] = "��ѡ��ͻ����";
            dt.Rows.InsertAt(row, 0);
            ddlClientType.DataBind();

        }


       

        protected bool IsPageValid()
        {
            if (ddlClientType.SelectedValue == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ѡ��ͻ����');</script>");
                return false;
            }
            if (ddlClientLevel.SelectedValue.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ѡ��ͻ��ȼ���');</script>");
                return false;
            }
          
            else
            {
                return true;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsPageValid())
            {
                try
                {

                    ClientInfo client = new ClientInfo();
                    client.Address = txtAddress.Text.ToString();
                    client.ClientName = txtClientName.Text.ToString();
                    client.MailNo = txtMailNo.Text.ToString();
                    client.Fax = txtFax.Text.ToString();
                    client.Telephone = txtTelephone.Text.ToString();
                    if (ddlClientLevel.SelectedValue != "")
                    {
                        client.ClientLevelId = Convert.ToInt32(ddlClientLevel.SelectedValue);
                    }

                    client.ClientTypeId = Convert.ToInt32(ddlClientType.SelectedValue);
                    client.SheetNum = txtClientNum.Text.ToString();
                    client.Save();
                    RelateInfo relate = new RelateInfo();
                    relate.ClientId = client.ClientId;
                    relate.Position = txtPosition.Text.ToString();
                    relate.Email = txtEmail.Text.ToString();
                    relate.Memo = txtMemo.Text.ToString();
                    relate.Mobile = txtMobile.Text.ToString();
                    relate.RelateName = txtRelateName.Text.ToString();
                    relate.Save();

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ӳɹ���');</script>");

                   // ClearForms();
                }
                catch (Exception Ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('����ʧ�ܣ�" + Ex.Message + "');", true);
                }

            }
           
        }


       

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Client.aspx");
        }

        
        protected void ddlClientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ClientTypeId = Convert.ToInt32(ddlClientType.SelectedValue);
            string preIndex = string.Empty;
            switch (ddlClientType.SelectedItem.Text)
            {
                case "�ڻ���ҵ": preIndex = "8";
                    break;
                case "������": preIndex = "7";
                    break;
                case "֤ȯ��ҵ": preIndex = "6";
                    break;
                case "HP�ͻ�": preIndex = "5";
                    break;
            }

            if (SheetNums.IsSheetNumOfClient(ClientTypeId))
            {
                txtClientNum.Text = SheetNums.SheetNumOfClient(preIndex, ClientTypeId);
            }
            else
            {
                txtClientNum.Text = preIndex + "001";
            }
        }
    }
}
