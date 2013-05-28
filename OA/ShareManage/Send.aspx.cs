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
using System.Net;

namespace OA.ShareManage
{
    public partial class Send : System.Web.UI.Page
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
            //��ȡ�����ļ���Ϣ��
            DataTable dt = CanYou.OA.BLL.FileInfo.getSendFile(emid);

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

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gvDataBind();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            Response.Redirect("SendFile.aspx");
        }

        //ɾ���ļ�
        public void DeleteFile(string fileName)
        {
            try
            {

                //string uri = "ftp://192.168.11.70/www/Files/File/" + fileName;
                ////����          
                //// ����uri����FtpWebRequest����
                //FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                //// ָ�����ݴ�������
                //reqFTP.UseBinary = true;
                //// ftp�û���������
                //reqFTP.Credentials = new NetworkCredential("test1", "123");
                //// Ĭ��Ϊtrue�����Ӳ��ᱻ�ر�
                //// ��һ������֮��ִ��
                //reqFTP.KeepAlive = false;
                //// ָ��ִ��ʲô����
                //reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                //FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                //response.Close();
                System.IO.File.Delete(Server.MapPath(("~/Files/File/"+ fileName)));

            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('������Ϣ:" + ex.Message + "');", true);
            }
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
            try
            {
                int j = 0;
                string[] filearray = new String[11];
                filearray[0] = " ";
                for (int i = 0; i < gvFile.Rows.Count; i++)
                {
                    CheckBox ckb = gvFile.Rows[i].FindControl("ckbChoose") as CheckBox;
                    if (ckb.Checked == true)
                    {
                        int fileid = Convert.ToInt32(gvFile.DataKeys[i].Value);
                        CanYou.OA.BLL.FileInfo file = new CanYou.OA.BLL.FileInfo(fileid);
                        if (filearray[j] != file.FileName)
                        {
                            j = j + 1;
                            filearray[j] = file.FileName;
                           
                        }
                        file.Delete();
                        if (MessageInfo.IsMessageOfFile(file.FileId.ToString()))
                        {
                            //ɾ����Ϣ
                            MessageInfo.DelMessageOfMemo(fileid.ToString());
                        }
                    }
                }
                for (int k = 1; k < j+1; k++)
                {
                    DeleteFile(filearray[k]);
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
