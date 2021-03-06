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

namespace OA.Notice
{
    public partial class AddNotice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected bool IsPageValid()
        {
            string strTitle = Request.Form["txtTitle"].ToString();
            string strContent = Request.Form["txtContent"].ToString();
            string strSignName = Request.Form["txtSignName"].ToString();
            string strNoticeTime = Request.Form["txtNoticeTime"].ToString();
            if (strTitle == "" || strContent == "" || strSignName == "" || strNoticeTime == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('请填写完整信息！');</script>");
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
                string strTitle = Request.Form["txtTitle"].ToString();
                string str = Request.Form["txtContent"].ToString();
                string strSignName = Request.Form["txtSignName"].ToString();
                string strNoticeTime = Request.Form["txtNoticeTime"].ToString();

                EmployeeInfo em = (EmployeeInfo)Session["Employee"];
                NoticeInfo notice = new NoticeInfo();
                notice.EmployeeName = em.EmployeeName;
                notice.NoticeTitle = strTitle;
                notice.NoticeContent = str;
                notice.SignName = strSignName;
                notice.NoticeTime = strNoticeTime;
                notice.Save();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('发布公告成功！');</script>");

                DataTable EmTb = MessageInfo.getEmployeeId(em.EmployeeId);
                string[] strto = new String[EmTb.Rows.Count];
                for (int i = 0; i < EmTb.Rows.Count; i++)
                {
                    EmployeeInfo ems = new EmployeeInfo(Convert.ToInt32(EmTb.Rows[i]["EmployeeId"]));
                    strto[i] = ems.Qq.ToString();
                }

                MessageInfo.SendMailS(strto, "OA新的消息", "OA有新的公告");
            }
                
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("NoticeManage.aspx");
        }
    }
}
