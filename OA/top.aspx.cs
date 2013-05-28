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
    public partial class top : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.lblDate.Text = DateTime.Now.Year.ToString() + "��" + DateTime.Now.Month.ToString() + "��" + DateTime.Now.Day.ToString() + "��      " + this.GetWeekDay();
                //��ȡ��½��
                string strLoginName = Session["userName"].ToString();
                lblMaster.Text = strLoginName.ToString();

                Notice();
            }

        }
        protected string GetWeekDay()
        {
            string Temp = "";
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    Temp = "������";
                    break;
                case DayOfWeek.Monday:
                    Temp = "����һ";
                    break;
                case DayOfWeek.Tuesday:
                    Temp = "���ڶ�";
                    break;
                case DayOfWeek.Wednesday:
                    Temp = "������";
                    break;
                case DayOfWeek.Thursday:
                    Temp = "������";
                    break;
                case DayOfWeek.Friday:
                    Temp = "������";
                    break;
                case DayOfWeek.Saturday:
                    Temp = "������";
                    break;
            }
            return Temp;
        }

        protected int getNoticeId()
        {
            int noticeid=NoticeInfo.getMaxNoticeId();
            return noticeid;
        }

        protected void Notice()
        {
            
            int noticeid = NoticeInfo.getMaxNoticeId();
            if (noticeid == 0)
            {
                lblNotice.Text = "";
            }
            else
            {
                NoticeInfo notice = new NoticeInfo(noticeid);
                lblNotice.Text = notice.NoticeTitle.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + notice.NoticeTime.ToString();
            }
           
        }
    }
}
