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
    public partial class UploadDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlBind();
                ddl2Bind();
            }
        }

        protected void ddlBind()
        {
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

        protected void ddl2Bind()
        {
            DataTable dt = CanYou.OA.BLL.FileInfo.FilePermission();
            ddlFilePermit.DataTextField = "FilePermissionName";
            ddlFilePermit.DataValueField = "FilePermissionId";
            ddlFilePermit.DataSource = dt;
            DataRow row = dt.NewRow();
            row["FilePermissionName"] = "��ѡ���ļ�Ȩ��";
            dt.Rows.InsertAt(row, 0);
            ddlFilePermit.DataBind();
        }
        protected bool IsPageValid()
        {
            if (!UpFile.HasFile)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ѡ��Ҫ�ϴ����ĵ���');</script>");
                return false;
            }
            if (ddlFileType.SelectedValue.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ѡ���ĵ����ͣ�');</script>");
                return false;
            }
            if (ddlFilePermit.SelectedValue.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ѡ���ĵ�Ȩ�ޣ�');</script>");
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
                    if (!CanYou.OA.BLL.FileInfo.IsDocumentSame(name))
                    {
                        CanYou.OA.BLL.FileInfo file = new CanYou.OA.BLL.FileInfo();
                        file.FileName = name;
                        file.FileTypeId = Convert.ToInt32(ddlFileType.SelectedValue.ToString());
                        file.SendEmployeeId = emid;
                        file.Memo = txtMemo.Text.ToString();
                        file.SendTime = sendtime;
                        file.IsCommon = 1;
                        file.FilePermissionId=Convert.ToInt32(ddlFilePermit.SelectedValue);
                        file.Save();

                        if (ddlFilePermit.SelectedItem.Text.ToString() == "����")
                        {
                                DataTable EmTb = MessageInfo.getEmployeeId(emid);
                                string[] strto = new String[EmTb.Rows.Count];
                                for (int i = 0; i < EmTb.Rows.Count; i++)
                                {
                                    MessageInfo.Msgs1(Convert.ToInt32(EmTb.Rows[i]["EmployeeId"]), file.FileId, "~/ShareManage/DownloadDocument.aspx", "���й����ĵ�:"+name, em.EmployeeName,"daiyue",name);
                                    EmployeeInfo ems = new EmployeeInfo(Convert.ToInt32(EmTb.Rows[i]["EmployeeId"]));
                                    strto[i] = ems.Qq.ToString();
                                }
                                //MessageInfo.SendMailS(strto, "OA�µ���Ϣ", "���й����ĵ�:"+name);

                            DataTable dt=MessageInfo.getEmployeeId1();
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                FileEmployeeInfo fileem = new FileEmployeeInfo();
                                fileem.FileId = file.FileId;
                                fileem.EmployeeId = Convert.ToInt32(dt.Rows[j]["EmployeeId"]);
                                fileem.IsMsg = 0;
                                fileem.IsDelete = 0;
                                fileem.Save();
                            }
                        }
                        else
                        {
                            DataTable EmTb = MessageInfo.getEmployeeIdOf(emid, ddlFilePermit.SelectedItem.Text.ToString());
                            string[] strto = new String[EmTb.Rows.Count];
                            for (int i = 0; i < EmTb.Rows.Count; i++)
                            {
                                MessageInfo.Msgs1(Convert.ToInt32(EmTb.Rows[i]["EmployeeId"]), file.FileId, "~/ShareManage/DownloadDocument.aspx", "���й����ĵ�:" + name, em.EmployeeName, "daiyue", name);
                                EmployeeInfo ems = new EmployeeInfo(Convert.ToInt32(EmTb.Rows[i]["EmployeeId"]));
                                strto[i] = ems.Qq.ToString();
                            }
                            MessageInfo.SendMailS(strto, "OA�µ���Ϣ", "���й����ĵ�:"+name);

                            DataTable dt = MessageInfo.getEmployeeIdOf1(ddlFilePermit.SelectedItem.Text.ToString());
                            {
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    FileEmployeeInfo fileem = new FileEmployeeInfo();
                                    fileem.FileId = file.FileId;
                                    fileem.EmployeeId = Convert.ToInt32(dt.Rows[j]["EmployeeId"]);
                                    fileem.IsMsg = 0;
                                    fileem.IsDelete = 0;
                                    fileem.Save();
                                }
                               
                            }
                        }
                        //�ļ��ϴ���ַ��Ŀ¼������ͨ��IIS���豾������ΪFTP������  
                        //string FileSaveUri = @"ftp://192.168.11.70/www/Files/Documents/";
                        ////FTP�û������룬���Ǳ������û�������  
                        //string ftpUser = "test1";
                        //string ftpPassWord = "123";
                        //SendFiles(FileSaveUri, ftpUser, ftpPassWord);
                        this.UpFile.PostedFile.SaveAs(Server.MapPath("~/Files/Documents/" + UpFile.FileName));
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('�ϴ��ɹ���');</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('���ĵ����Ѵ��ڣ�����������');</script>");
                    }

                   
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('�ϴ�ʧ�ܣ�" + ex.Message + "');", true);
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
            Response.Redirect("Upload.aspx");
        }
    }
}
