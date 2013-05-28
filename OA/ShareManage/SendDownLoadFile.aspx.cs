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
    public partial class SendDownLoadFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int fileid = Int32.Parse(Request.QueryString["fileid"]);//��ȡ�ļ���ID
           
            CanYou.OA.BLL.FileInfo file = new CanYou.OA.BLL.FileInfo(fileid);
            string fileName = file.FileName;

            //��ȡ����·��
            //string DeskPath = Convert.ToString(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
            //string DeskPath = Server.MapPath("~\\Files");
            DownLoads(fileName);
        }

        protected void DownLoads(string fileName)
        {
           
           // FtpWebRequest reqFTP;
            try
            {
                string filePath = Server.MapPath("../Files/File/") + fileName;
                if (File.Exists(filePath))
                {
                    //���ַ�������ʽ�����ļ�
                    FileStream fs = new FileStream(filePath, FileMode.Open);
                    byte[] bytes = new byte[(int)fs.Length];
                    fs.Read(bytes, 0, bytes.Length);
                    fs.Close();
                    Response.ContentType = "application/octet-stream";
                    //֪ͨ����������ļ������Ǵ�
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }

                //FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
                //reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + "192.168.11.70/www/Files/File" + "/" + fileName));
                //reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                //reqFTP.UseBinary = true;
                //reqFTP.Credentials = new NetworkCredential("test1", "123");
                //FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

                //Stream ftpStream = response.GetResponseStream();
                //long cl = response.ContentLength;
                //int bufferSize = 2048;
                //int readCount;
                //byte[] buffer = new byte[bufferSize];
                //readCount = ftpStream.Read(buffer, 0, bufferSize);


                //while (readCount > 0)
                //{
                //    outputStream.Write(buffer, 0, readCount);
                //    readCount = ftpStream.Read(buffer, 0, bufferSize);
                //}


                //ftpStream.Close();
                //outputStream.Close();
                //response.Close();

                //FileStream fs = new FileStream(filePath + "\\" + fileName, FileMode.Open);
                //byte[] btFile = new byte[fs.Length];
                //fs.Read(btFile, 0, Convert.ToInt32(fs.Length));//���ļ�����������
                //fs.Close();
                //string strFileName = "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
                //this.Response.AddHeader("Content-disposition", strFileName);
                //this.Response.ContentType = "application/octet-staream";
                //this.Response.BinaryWrite(btFile);
                //string str = response.ResponseUri.ToString();
                //System.IO.File.Delete(filePath + "\\" + fileName);
                //this.Response.End();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('����ʧ�ܣ�" + ex.Message + "');", true);
            }

        }
    }
}
