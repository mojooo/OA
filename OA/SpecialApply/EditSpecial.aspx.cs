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



namespace OA.SpecialApply
{
    public partial class EditSpecial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ddlProjectBind();
                ddlStepBind();
                PageInit();
                ViewState["BackUrl"] = Request.UrlReferrer.ToString();
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

        protected void ddlStepBind()
        {
            DataTable dt = SpecialExpInfo.getProjectStepList();
            ddlProjectStep.DataSource = dt;
            ddlProjectStep.DataTextField = "ProjectStepName";
            ddlProjectStep.DataValueField = "ProjectStepId";
            DataRow row = dt.NewRow();
            row["ProjectStepName"] = "��ѡ����Ŀ�׶�";
            dt.Rows.InsertAt(row, 0);
            ddlProjectStep.DataBind();
        }

        protected void PageInit()
        {


            EmployeeInfo em = (EmployeeInfo)Session["Employee"];
            lblApplyPeople.Text = em.EmployeeName;

            int id = Convert.ToInt32(Request["SpecialExpId"].ToString());
            SpecialExpInfo sp = new SpecialExpInfo(id);

            txtApplyMoney.Text = sp.ApplyMoney.ToString();
            txtApplyDate.Text = sp.ApplyDate.ToString();
            txtReason.Text = sp.ApplyReason.ToString();

            if (sp.MainProjectCreateId.ToString() !="")
            {
                ddlProject.SelectedValue = sp.MainProjectCreateId.ToString();
            }
            
            if (sp.ProjectStepId.ToString() != "")
            {
                ddlProjectStep.SelectedValue = sp.ProjectStepId.ToString();
            }
            
            MainProjectCreateInfo project = new MainProjectCreateInfo(Convert.ToInt32(sp.MainProjectCreateId));
            lblMoneyNum.Text = project.PreMoney.ToString();
            txtSheetNum.Text = sp.SheetNum.ToString();
            string strRoleName = Session["RoleName"].ToString();
            PositionInfo position = new PositionInfo(Convert.ToInt32(em.PositionId));
            if (strRoleName == "Ա��" || position.PositionName == "�ۺ�����")
            {
                btnSign.Enabled = false;
                btnSign.Visible = false;
            }
        }


        protected bool IsPageValid()
        {
            string strRoleName = Session["RoleName"].ToString();
            if (ddlProject.SelectedValue.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ѡ����Ŀ���ƣ�');</script>");
                return false;
            }
            if (ddlProjectStep.SelectedValue.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ѡ����Ŀ�׶Σ�');</script>");
                return false;
            }
            if (strRoleName == "���ž���" && imgMarket.Visible == false)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ǩ����');</script>");
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsPageValid())
            {
                try
                {
                  
                    int id = Convert.ToInt32(Request["SpecialExpId"].ToString());
                    SpecialExpInfo sp = new SpecialExpInfo(id);
                    if (sp.IsNewCreate == 1)
                    {
                        sp.IsMain = 0;
                        sp.IsMain1 = 0;
                        sp.IsMain2 = 0;
                        sp.IsMain3 = 0;
                        SpecialExpInfo spnew = new SpecialExpInfo();
                        spnew.IsNewCreate = 0;
                        spnew.IsMain = 1;
                        spnew.IsMain1 = 1;
                        spnew.IsMain2 = 1;
                        spnew.IsMain3 = 1;
                        spnew.MainSpecialExpId = sp.MainSpecialExpId;
                        string strRoleName = Session["RoleName"].ToString();
                        if (strRoleName == "���ž���")
                        {
                            spnew.SendEmployeeId = sp.SendEmployeeId;
                            spnew.SendEmployeeName = sp.SendEmployeeName;
                            spnew.PreIsApply = 1;
                            spnew.PreIsOver = 1;
                            spnew.SignName = 1;
                        }
                        else
                        {
                            spnew.PreEmployeeId = sp.PreEmployeeId;
                            spnew.PreEmployeeName = sp.PreEmployeeName;
                            spnew.PreIsApply = 0;
                            spnew.PreIsOver = 0;
                            spnew.SignName = 0;
                        }

                        spnew.IsApply = 0;
                        spnew.IsApprove = 0;
                        spnew.IsApply1 = 0;
                        spnew.IsApply2 = 0;
                        spnew.IsOver1 = 0;
                        spnew.IsOver2 = 0;

                        DateTime date = DateTime.Now;
                        string strtoday = DateTime.Now.ToString("yyyyMMdd").Substring(2);
                        spnew.Today = strtoday;
                        spnew.SheetNum = txtSheetNum.Text.ToString();

                        spnew.ApplyDate = Convert.ToDateTime(txtApplyDate.Text.ToString());
                        spnew.ApplyPeople = lblApplyPeople.Text.ToString();
                        spnew.ApplyReason = txtReason.Text.ToString();
                        spnew.ApplyMoney = txtApplyMoney.Text.ToString();
                        if (ddlProject.SelectedValue != "")
                        {
                            spnew.MainProjectCreateId = Convert.ToInt32(ddlProject.SelectedValue);
                        }
                        if (ddlProjectStep.SelectedValue != "")
                        {
                            spnew.ProjectStepId = Convert.ToInt32(ddlProjectStep.SelectedValue);
                        }

                        spnew.Save();
                        sp.Save();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('�����ɹ���');</script>");
                    }

                    else
                    {
                        sp.ApplyDate = Convert.ToDateTime(txtApplyDate.Text.ToString());
                        sp.ApplyPeople = lblApplyPeople.Text.ToString();
                        sp.ApplyReason = txtReason.Text.ToString();
                        sp.ApplyMoney = txtApplyMoney.Text.ToString();

                        if (ddlProject.SelectedValue != "")
                        {
                            sp.MainProjectCreateId = Convert.ToInt32(ddlProject.SelectedValue);
                        }
                        if (ddlProjectStep.SelectedValue != "")
                        {
                            sp.ProjectStepId = Convert.ToInt32(ddlProjectStep.SelectedValue);
                        }
                        sp.Save();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('�༭�ɹ���');</script>");
                    }

                   
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ӳɹ���');</script>");
                }
                catch (Exception Ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('���ʧ�ܣ�" + Ex.Message + "');", true);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(ViewState["BackUrl"].ToString());
        }

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProject.SelectedItem.Text == "��ѡ����Ŀ����")
            {
                lblMoneyNum.Text = "";
            }
            else
            {
                MainProjectCreateInfo project = new MainProjectCreateInfo(Convert.ToInt32(ddlProject.SelectedValue));
                lblMoneyNum.Text = project.PreMoney.ToString();
            }
        }

       
        protected void btnSign_Click(object sender, EventArgs e)
        {
            imgMarket.Visible = true;
            btnSign.Enabled = false;
        }
    }
}
