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
namespace OA
{
    public partial class AddAsset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlDataBind();
            }
        }

        protected void ddlDataBind()
        {
            DataTable dtUnit = AssetInfo.getUnitList();
            DataTable dtDepart = EmployeeInfo.getDepartList();
            DataTable dtSite = AssetInfo.getSiteList();

            ddlUnit.DataTextField = "UnitName";
            ddlUnit.DataValueField = "UnitId";
            ddlUnit.DataSource = dtUnit;
            DataRow drUnit = dtUnit.NewRow();
            drUnit["UnitName"] = "��ѡ�������λ";
            dtUnit.Rows.InsertAt(drUnit,0);
            ddlUnit.DataBind();

            ddlDepart.DataTextField = "DepartName";
            ddlDepart.DataValueField = "DepartId";
            ddlDepart.DataSource = dtDepart;
            DataRow drDepart = dtDepart.NewRow();
            drDepart["DepartName"] = "��ѡ��ʹ�ò���";
            dtDepart.Rows.InsertAt(drDepart, 0);
            ddlDepart.DataBind();

            ddlSite.DataTextField = "SiteName";
            ddlSite.DataValueField = "SiteId";
            ddlSite.DataSource = dtSite;
            DataRow drSite = dtSite.NewRow();
            drSite["SiteName"] = "��ѡ���ŵص�";
            dtSite.Rows.InsertAt(drSite, 0);
            ddlSite.DataBind();

        }

        protected void ClearForms()
        {
            foreach (Control ct in this.FindControl("form1").Controls)
            {
              

                if (ct is TextBox)
                {
                    TextBox tb = ct as TextBox;
                    tb.Text = "";
                }
                if (ct is DropDownList)
                {
                    DropDownList ddl = ct as DropDownList;
                    ddl.SelectedIndex = 0;
                }
            }
        }

        protected bool IsPageValid()
        {
            if(ddlDepart.SelectedValue.ToString()=="")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('ʹ�ò��ű��');</script>");
                return false;
            }
            if (ddlSite.SelectedValue.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ŵص���');</script>");
                return false;
            }
            if (ddlUnit.SelectedValue.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('������λ���');</script>");
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void btnAddAsset_Click(object sender, EventArgs e)
        {
            if (IsPageValid())
            {
                try
                {
                    AssetInfo asset = new AssetInfo();
                    asset.UnitName = ddlUnit.SelectedItem.Text.ToString();
                    asset.DepartName = ddlDepart.SelectedItem.Text.ToString();
                    asset.SiteName = ddlSite.SelectedItem.Text.ToString();
                    asset.AssetName = txtAssetName.Text.ToString();
                    asset.Amount = txtAmount.Text.ToString();
                    asset.Price = txtPrice.Text.ToString();
                    asset.Memo = txtMemo.Text.ToString();
                    asset.Type = txtType.Text.ToString();
                    asset.Save();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ӳɹ���');</script>");
                }
                catch (Exception Ex)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('���ʧ�ܣ�');</script>");

                }
            }
           
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Asset.aspx");
        }
    }
}
