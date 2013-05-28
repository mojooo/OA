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

namespace OA.Document
{
    public partial class SendFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ddlBind();
            }
        }

        protected void ddlBind()
        {
            //��ddlDepart
            DataTable dtDepart = EmployeeInfo.getDepartList();
            ddlDepart.DataTextField = "DepartName";
            ddlDepart.DataValueField = "DepartId";
            ddlDepart.DataSource = dtDepart;
            DataRow drDepart = dtDepart.NewRow();
            drDepart["DepartName"] = "��ѡ����";
            dtDepart.Rows.InsertAt(drDepart, 0);
            ddlDepart.DataBind();
            //��ddlFileType
            DataTable dt = EmployeeInfo.getFileType();
            ddlFileType.DataTextField = "FileTypeName";
            ddlFileType.DataValueField = "FileTypeId";
            ddlFileType.DataSource = dt;
            DataRow row = dt.NewRow();
            row["FileTypeName"] = "��ѡ���ļ�����";
            dt.Rows.InsertAt(row, 0);
            ddlFileType.DataBind();
        }

        protected bool IsPageValid()
        {
            if (lbxRecv.Rows == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('�����˲�Ϊ�գ�');</script>");
                return false;
            }
            if (!UpFile.HasFile)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ѡ��Ҫ���͵��ļ���');</script>");
                return false;
            }
            if (ddlFileType.SelectedValue.ToString()=="")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ѡ���ļ����ͣ�');</script>");
                return false;
            }
           
            else
            {
                return true;
            }
        }

        protected void btnSendFile_Click(object sender, EventArgs e)
        {
            if (IsPageValid())
            {
                try
                {
                    int emid = Convert.ToInt32(Session["EmployeeId"]);
                    EmployeeInfo em = (EmployeeInfo)Session["Employee"];
                    //��ȡ�ļ���
                    string name = this.UpFile.FileName;
                    DateTime sendtime = DateTime.Now;
                    if (!CanYou.OA.BLL.FileInfo.IsFileSame(name))
                    {
                        string[] strto = new String[lbxRecv.Items.Count];
                        for (int i = 0; i < lbxRecv.Items.Count; i++)
                        {
                            CanYou.OA.BLL.FileInfo file = new CanYou.OA.BLL.FileInfo();
                            file.RecvEmployeeId = Convert.ToInt32(lbxRecv.Items[i].Value);
                            file.FileName = name;
                            file.FileTypeId = Convert.ToInt32(ddlFileType.SelectedValue.ToString());
                            file.SendEmployeeId = emid;
                            file.Memo = txtMemo.Text.ToString();
                            file.SendTime = sendtime;
                            file.IsCommon = 0;
                            file.IsDelete = 0;
                            file.IsMsg = 0;
                            file.Save();

                            MessageInfo Msg = new MessageInfo();
                            Msg.Msg = "���յ����ļ�:"+name;
                            Msg.Url = "~/ShareManage/RecvFile.aspx";
                            Msg.RecvEmployeeId = Convert.ToInt32(lbxRecv.Items[i].Value);
                            Msg.Memo = file.FileId.ToString();
                            Msg.EmployeeName = em.EmployeeName;
                            Msg.MsgType = "daiyue";
                            Msg.MsgTime = DateTime.Now.ToString("yyyy-MM-dd");
                            Msg.MsgTitle = name;
                            Msg.Save();

                            EmployeeInfo ems = new EmployeeInfo(Convert.ToInt32(lbxRecv.Items[0].Value));
                            strto[i] = ems.Qq.ToString();
                        }


                        //�ļ��ϴ���ַ��Ŀ¼������ͨ��IIS���豾������ΪFTP������  
                        //string FileSaveUri = @"ftp://192.168.11.70/www/Files/File/";
                        ////FTP�û������룬���Ǳ������û�������  
                        //string ftpUser = "test1";
                        //string ftpPassWord = "123";
                        //SendFiles(FileSaveUri, ftpUser, ftpPassWord);
                        this.UpFile.PostedFile.SaveAs(Server.MapPath("~/Files/File/" + UpFile.FileName));
                        MessageInfo.SendMailS(strto, "OA����Ϣ", "���յ����ļ�");
                      
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('���ͳɹ���');</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('���ļ����Ѵ��ڣ�����������');</script>");
                    }
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('����ʧ�ܣ�" + ex.Message + "');", true);
                }
            }
        }

       
        //protected void SendFiles(string FileSaveUri, string ftpUser, string ftpPassWord)
        //{
            
        //    Stream requestStream = null;
        //    Stream fileStream = null;
        //    FtpWebResponse uploadResponse = null;//����FtpWebResponseʵ��uploadResponse  
        //    if (UpFile.HasFile)
        //    {
        //        //��ȡ�ļ�����  
        //        int FileLength = UpFile.PostedFile.ContentLength;
        //        //�����ϴ��ļ�����ܳ���500M  
        //        if (FileLength < 512 * 1024 * 1024)
        //        {
        //            try
        //            {
        //                //��ʽ��ΪURI  
                       
        //                Uri uri = new Uri(FileSaveUri + Path.GetFileName(UpFile.PostedFile.FileName));
        //                FtpWebRequest uploadRequest = (FtpWebRequest)WebRequest.Create(uri);//����FtpWebRequestʵ��uploadRequest  
        //                uploadRequest.Method = WebRequestMethods.Ftp.UploadFile;//��FtpWebRequest��������Ϊ�ϴ��ļ�  
        //                uploadRequest.Credentials = new NetworkCredential(ftpUser, ftpPassWord);//��֤FTP�û�������  
        //                requestStream = uploadRequest.GetRequestStream();//��������ϴ�FTP����  
        //                byte[] buffer = new byte[FileLength];
        //                fileStream = UpFile.PostedFile.InputStream;//��ȡFileUpload��ȡ���ļ�������Ϊ�ϴ�FTP����  
        //                fileStream.Read(buffer, 0, FileLength);
        //                requestStream.Write(buffer, 0, FileLength);//��bufferд����  
        //                requestStream.Close();
        //                uploadResponse = (FtpWebResponse)uploadRequest.GetResponse();//����FTP��������Ӧ���ϴ����  

        //            }
        //            catch (Exception ex)
        //            {
        //                ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('�ϴ�ʧ�ܣ�" + ex.Message + "');", true);
        //                //    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('�޷��ϴ���');</script>");
        //            }
        //            finally
        //            {
        //                if (uploadResponse != null)
        //                    uploadResponse.Close();
        //                if (fileStream != null)
        //                    fileStream.Close();
        //                if (requestStream != null)
        //                    requestStream.Close();
        //            }
        //        }
        //        else
        //        {
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('�ϴ��ļ�����');</script>");
        //        }
        //    }
        //    else
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('δѡ���ļ����ļ�����Ϊ�գ�');</script>");
        //    }
        //}

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Send.aspx");
        }

        protected void ddlDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDepart.SelectedValue != "")
            {
                int DepartId = Convert.ToInt32(ddlDepart.SelectedValue.ToString());
                DataTable dt = EmployeeInfo.getEmployeeOfDepart(DepartId);
                ddlRecvEmployee.DataTextField = "EmployeeName";
                ddlRecvEmployee.DataValueField = "EmployeeId";
                DataRow row = dt.NewRow();
                row["EmployeeName"] = "��ѡ�������";
                dt.Rows.InsertAt(row, 0);
                ddlRecvEmployee.DataSource = dt;
                ddlRecvEmployee.DataBind();
               
            }
           
        }

       

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbxRecv.SelectedItem==null)
            {

                lblMsg1.Text = "��ѡ�к���ɾ��~";
            }
            else
            {
                lbxRecv.Items.Remove(new ListItem(lbxRecv.SelectedItem.Text, lbxRecv.SelectedValue));
                
            }
           
        }

        protected void ddlRecvEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isExit = false;
            for (int i = 0; i < lbxRecv.Items.Count; i++)
            {
                if (lbxRecv.Items[i].Text == ddlRecvEmployee.SelectedItem.Text)
                {
                    isExit = true;
                }
            }
            if (isExit == false && ddlRecvEmployee.SelectedItem.Text != "��ѡ�������")
            {
                lbxRecv.Items.Add(new ListItem(ddlRecvEmployee.SelectedItem.Text, ddlRecvEmployee.SelectedValue));
            }
        }

        protected void lbxRecv_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMsg1.Text = "";
        }

    }
}
