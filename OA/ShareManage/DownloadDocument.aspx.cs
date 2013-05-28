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
namespace OA.ShareManage
{
    public partial class DownloadDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "SendTime";
                ViewState["SortDir"] = "DESC";
                gvDataBind();
            }
        }

        protected void gvDataBind()
        {
            int emid = Convert.ToInt32(Session["EmployeeId"]);
            int departId = Convert.ToInt32(DayInfo.GetDepartIdOfEmployee(emid));
            DepartInfo depart = new DepartInfo(departId);
            DataTable dt = FileInfo.getDocument(depart.DepartName,emid);

            DataView view = dt.DefaultView;
            string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
            view.Sort = sort;
            
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                UI.BindCtrl(dt.DefaultView, gvFile, AspNetPager1);
                gvFile.Rows[0].Visible = false;
            }
            else
            {
                UI.BindCtrl(dt.DefaultView, gvFile, AspNetPager1);
            }
        }

        protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            this.AspNetPager1.CurrentPageIndex = e.NewPageIndex;
            gvDataBind();
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gvDataBind();
        }

        protected void gvFile_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvFile.Rows.Count; i++)
            {
                CheckBox ckb = gvFile.Rows[i].FindControl("ckbChoose") as CheckBox;
                if (ckbAll.Checked == true)
                {
                    ckb.Checked = true;
                }
                else
                {
                    ckb.Checked = false;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int emid = Convert.ToInt32(Session["EmployeeId"]);
            try
            {
                for (int i = 0; i < gvFile.Rows.Count; i++)
                {
                    CheckBox ckb = gvFile.Rows[i].FindControl("ckbChoose") as CheckBox;
                    if (ckb.Checked == true)
                    {
                        int fileid = Convert.ToInt32(gvFile.DataKeys[i].Value);
                       
                        FileInfo.DelDocumentOf(fileid, emid);
                        if (MessageInfo.IsMessageOfFile(fileid.ToString()))
                        {
                            MessageInfo.DelMsgOfBoth(fileid.ToString(), emid);
                        }
                    }
                }
                gvDataBind();
            }
            catch (Exception Ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('������Ϣ:" + Ex.Message + "');", true);
            }
            
        }
    }
}
