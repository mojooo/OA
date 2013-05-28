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
using System.IO;
namespace OA
{
    public partial class AddEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlBind();
                ddlBind1();
                ViewState["ImgUrl"] = "NewImage.jpg";
            }
        }

        //���𡢵��ΰ�
        protected void ddlBind1()
        {
            DataTable dt1 = EmployeeInfo.getEmLevel();
            ddlLevel.DataTextField = "EmLevelName";
            ddlLevel.DataValueField = "EmLevelId";
            ddlLevel.DataSource = dt1;
            DataRow dr1 = dt1.NewRow();
            dr1["EmLevelName"] = "��ѡ�񼶱�";
            dt1.Rows.InsertAt(dr1, 0);
            ddlLevel.DataBind();

            DataTable dt2 = EmployeeInfo.getEmDang();
            ddlDang.DataTextField = "EmDangName";
            ddlDang.DataValueField = "EmDangId";
            ddlDang.DataSource = dt2;
            DataRow dr2 = dt2.NewRow();
            dr2["EmDangName"] = "��ѡ�񵵴�";
            dt2.Rows.InsertAt(dr2, 0);
            ddlDang.DataBind();

        }

        protected void ddlBind()
        {
            //��ddlEducation
            DataTable dt1 = EmployeeInfo.getEducationList();
            ddlEducation.DataTextField = "EducationName";
            ddlEducation.DataValueField = "EducationId";
            ddlEducation.DataSource = dt1;
            DataRow dr1 = dt1.NewRow();
            dr1["EducationName"] = "��ѡ��ѧ��";
            dt1.Rows.InsertAt(dr1, 0);
            ddlEducation.DataBind();
            //��ddlLanguage
            DataTable dt2 = EmployeeInfo.getLanguageList();
            ddlLanguage.DataTextField = "LanguageName";
            ddlLanguage.DataValueField = "LanguageId";
            ddlLanguage.DataSource = dt2;
            DataRow dr2 = dt2.NewRow();
            dr2["LanguageName"] = "��ѡ��Ӣ��ȼ�";
            dt2.Rows.InsertAt(dr2, 0);
            ddlLanguage.DataBind();
            //��ddlPolitics
            DataTable dt3 = EmployeeInfo.getPoliticsList();
            ddlPolitics.DataTextField = "PoliticsName";
            ddlPolitics.DataValueField = "PoliticsId";
            ddlPolitics.DataSource = dt3;
            DataRow dr3 = dt3.NewRow();
            dr3["PoliticsName"] = "��ѡ��������ò";
            dt3.Rows.InsertAt(dr3, 0);
            ddlPolitics.DataBind();
            //��ddlDepart
            DataTable dt4 = EmployeeInfo.getDepartList();
            ddlDepart.DataTextField = "DepartName";
            ddlDepart.DataValueField = "DepartId";
            ddlDepart.DataSource = dt4;
            DataRow dr4 = dt4.NewRow();
            dr4["DepartName"] = "��ѡ����";
            dt4.Rows.InsertAt(dr4, 0);
            ddlDepart.DataBind();
            //��ddlPosition
            DataTable dt5 = EmployeeInfo.getPositionList();
            ddlPosition.DataTextField = "PositionName";
            ddlPosition.DataValueField = "PositionId";
            ddlPosition.DataSource = dt5;
            DataRow dr5 = dt5.NewRow();
            dr5["PositionName"] = "��ѡ��ְλ";
            dt5.Rows.InsertAt(dr5, 0);
            ddlPosition.DataBind();

        }

        protected bool IsPageValid()
        {
            if (rblSex.SelectedValue.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('�Ա��ѡ��');</script>");
                return false;
            }
            if (rblState.SelectedValue.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ְ״̬��ѡ��');</script>");
                return false;
            }
            if (ddlDepart.SelectedValue.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('���ڲ��ű�ѡ��');</script>");
                return false;
            }
            if (ddlEducation.SelectedValue.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('ѧ����ѡ��');</script>");
                return false;
            }
            if (ddlLanguage.SelectedValue.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('���ñ�ѡ��');</script>");
                return false;
            }
            if (ddlPolitics.SelectedValue.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('������ò��ѡ��');</script>");
                return false;
            }
            if (ddlPosition.SelectedValue.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('ְλ��ѡ��');</script>");
                return false;
            }

            if (ddlLevel.SelectedValue.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('�����ѡ��');</script>");
                return false;
            }
            if (ddlDang.SelectedValue.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('���α�ѡ��');</script>");
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
                    string strAddress = txtAddress.Text.Trim();
                    string strCertificate = txtCertificate.Text.Trim();
                    string strEmployeeName = txtEmployeeName.Text.Trim();
                    string strIdentyNum = txtIdentyNum.Text.Trim();
                    string strMajor = txtMajor.Text.Trim();
                    string strNativePlace = txtNativePlace.Text.Trim();
                    string strSchool = txtSchool.Text.Trim();

                    string strEmail = txtEmail.Text.Trim();
                    string strHomephone = txtHomephone.Text.Trim();
                    string strMobile = txtMobile.Text.Trim();
                    string strPhoneExt = txtPhoneExt.Text.Trim();
                    string strOfficephone = txtOfficephone.Text.Trim();

                    DateTime dtBirthday = DateTime.Parse(txtBirthday.Text.Trim());
                    DateTime dtJobTime = DateTime.Parse(txtJobTime.Text.Trim());
                    DateTime dtJoinTime = DateTime.Parse(txtJoinTime.Text.Trim());



                    ContactInfo contact = new ContactInfo();
                    contact.Email = strEmail;
                    contact.Homephone = strHomephone;
                    contact.Mobile = strMobile;
                    contact.PhoneExt = strPhoneExt;
                    contact.Officephone = strOfficephone;
                    contact.Save();

                    EmployeeInfo Em = new EmployeeInfo();

                    if (ddlDepart.SelectedValue != "")
                    {
                        Em.DepartId = Convert.ToInt32(ddlDepart.SelectedValue.ToString());
                    }
                    if (ddlEducation.SelectedValue != "")
                    {
                        Em.EducationId = Convert.ToInt32(ddlEducation.SelectedValue);
                    }
                    if (ddlLanguage.SelectedValue != "")
                    {
                        Em.LanguageId = Convert.ToInt32(ddlLanguage.SelectedValue.ToString());
                    }

                    if (ddlPosition.SelectedValue != "")
                    {
                        Em.PositionId = Convert.ToInt32(ddlPosition.SelectedValue.ToString());
                    }
                    if (rblSex.SelectedValue != "")
                    {
                        Em.Sex = Convert.ToInt32(rblSex.SelectedValue.ToString());
                    }
                    if (rbtnMarrage.SelectedValue != "")
                    {
                        int iMarrage = Convert.ToInt32(rbtnMarrage.SelectedValue.ToString());
                    }
                    if (rblState.SelectedValue != "")
                    {
                        int iState = Convert.ToInt32(rblState.SelectedValue.ToString());
                        Em.State = iState;
                        if (iState == 1)
                        {

                            Em.OffJobTime = Convert.ToDateTime(txtLeaveDate.Text.ToString());

                        }
                    }

                    if (ddlPolitics.SelectedValue != "")
                    {
                        Em.PoliticsId = Convert.ToInt32(ddlPolitics.SelectedValue.ToString());
                    }

                    Em.EmLevelId = Convert.ToInt32(ddlLevel.SelectedValue.ToString());
                    Em.EmDangId = Convert.ToInt32(ddlDang.SelectedValue.ToString());

                    Em.ContactId = contact.ContactId;
                    Em.Address = strAddress;
                    Em.Birthday = dtBirthday;
                    Em.Certificate = strCertificate;
                    Em.EmployeeName = strEmployeeName;
                    Em.IdentyNum = strIdentyNum;
                    Em.JobTime = dtJobTime;
                    Em.JoinTime = dtJoinTime;
                    Em.Major = strMajor;
                    Em.NativePlace = strNativePlace;
                    Em.School = strSchool;
                    Em.Image = "~/Photo/" + ViewState["ImgUrl"];
                    Em.Qq = txtQq.Text.ToString();
                    Em.TurnTime = txtTurntime.Text.ToString();
                    Em.Save();

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('����û��ɹ���');</script>");


                }
                catch (Exception Ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('����û�ʧ�ܣ�" + Ex.Message + "');", true);
                   
                }
            }
        }



        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Employee.aspx");
        }

        protected void btnImgUp_Click(object sender, EventArgs e)
        {
            if (UpImage.HasFile)
            {
                string strImgeName = this.UpImage.PostedFile.FileName.ToString();
                string ImgeType = strImgeName.Substring(strImgeName.LastIndexOf("."));
                if (ImgeType == ".jpg" || ImgeType == ".bmp" || ImgeType == ".gif" || ImgeType == ".jpeg" || ImgeType == ".png")
                {
                    ViewState["ImgUrl"] = txtEmployeeName.Text + ImgeType;
                    this.UpImage.PostedFile.SaveAs(Server.MapPath("~/Photo/") + ViewState["ImgUrl"]);
                    this.ImgInit.ImageUrl = "~/Photo/" + ViewState["ImgUrl"];
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('ֻ���ϴ�jpg��jpeg��png��gif��bmp��ʽͼƬ!');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ѡ��ͼƬ���ϴ�!');</script>");
            }
           
        }

        protected void rblState_SelectedIndexChanged(object sender, EventArgs e)
        {
            int istate = Convert.ToInt32(rblState.SelectedValue.ToString());
            if (istate == 1)
            {
                txtLeaveDate.Enabled = true;
                txtLeaveDate.Visible = true;
                lblLeave.Visible = true;
            }
        }
    }
}
