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

namespace OA.OverTime
{
    public partial class AddOverTime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageInit();
                gvDataBind();
            }
        }

        protected void PageInit()
        {
            EmployeeInfo em = (EmployeeInfo)Session["Employee"];
            lblApplyEmName.Text = em.EmployeeName.ToString();

            OverTimeInfo ot = new OverTimeInfo();
            ot.IsSubmit = "��ʼ��";
            ot.Save();
            ViewState["OverTimeId"] = ot.OverTimeId;
        }


        protected void gvDataBind()
        {
            DataTable dt = OverTimeInfo.getOtGroup(Convert.ToInt32(ViewState["OverTimeId"]));
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                UI.BindCtrl(dt.DefaultView, gvOtGroup, AspNetPager1);
                gvOtGroup.Rows[0].Visible = false;
            }
            else
            {
                UI.BindCtrl(dt.DefaultView, gvOtGroup, AspNetPager1);
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            OtGroupInfo group = new OtGroupInfo();
            group.OverTimeId = Convert.ToInt32(ViewState["OverTimeId"]);
            group.Save();
            gvDataBind();
        }

        protected void gvOverTime_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void gvOverTime_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int otid = Convert.ToInt32(gvOtGroup.DataKeys[e.RowIndex].Value);
            OverTimeInfo.DelQtGroup(otid);
            gvDataBind();
        }

        protected void gvOverTime_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvOtGroup.EditIndex = e.NewEditIndex;
            gvDataBind();
        }

        protected void gvOverTime_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int otid = Convert.ToInt32(gvOtGroup.DataKeys[e.RowIndex].Value);
            OtGroupInfo group = new OtGroupInfo(otid);
            TextBox txtGroupName = gvOtGroup.Rows[e.RowIndex].FindControl("txtGroupName") as TextBox;
            TextBox txtEmName = gvOtGroup.Rows[e.RowIndex].FindControl("txtApplyName") as TextBox;

            try
            {
                group.ApplyName = txtEmName.Text.ToString();
                group.GroupName = txtGroupName.Text.ToString();
                group.Save();
                gvOtGroup.EditIndex = -1;
                gvDataBind();

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('����ʧ�ܣ�" + ex.Message + "');", true);
            }

        }

        protected void gvOverTime_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvOtGroup.EditIndex = -1;
            gvDataBind();
        }

      

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (gvOtGroup.Rows.Count != 0)
            {
                EmployeeInfo em = (EmployeeInfo)Session["Employee"];
                OverTimeInfo ot = new OverTimeInfo(Convert.ToInt32(ViewState["OverTimeId"].ToString()));
                ot.ApplyDate = txtApplyDate.Text.ToString();
                ot.ApplyEmName = em.EmployeeName.ToString();
                ot.IsSubmit = "����";
                ot.Reason = txtReason.Text.ToString();
                ot.TimeFrom = txtTimeFrom.Text.ToString();
                ot.TimeTo = txtTimeTo.Text.ToString();
                ot.TimeSpan = txtTimeSpan.Text.ToString();
                ot.Save();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('����ɹ���');</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('�������Ӱ���Ա��¼��');</script>");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            OverTimeInfo ot = new OverTimeInfo(Convert.ToInt32(ViewState["OverTimeId"].ToString()));
            if (ot.IsSubmit.ToString() == "��ʼ��")
            {
                OverTimeInfo.DelGroupOfQt(Convert.ToInt32(ViewState["OverTimeId"].ToString()));
                OverTimeInfo.DelOverTime(Convert.ToInt32(ViewState["OverTimeId"].ToString()));
            }
            Response.Redirect("OverTimeEmgv.aspx");

        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gvDataBind();
        }
    }
}
