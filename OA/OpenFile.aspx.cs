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
using System.IO;
using System.Net;

namespace OA
{
    public partial class OpenFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fileName = "help.chm";

            //��ȡ����·��
            //string DeskPath = Convert.ToString(System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
            string DeskPath = Server.MapPath("~\\Files\\File");
            DownLoads(DeskPath, fileName);

        }

        protected void DownLoads(string filePath, string fileName)
        {
            FtpWebRequest reqFTP;
            try
            {
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + "192.168.11.70/www/Files/Help" + "/" + fileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = false;
                reqFTP.Credentials = new NetworkCredential("test1", "123");
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);


                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);


                }


                ftpStream.Close();
                outputStream.Close();
                response.Close();

                FileStream fs = new FileStream(filePath + "\\" + fileName, FileMode.Open);
                byte[] btFile = new byte[fs.Length];
                fs.Read(btFile, 0, Convert.ToInt32(fs.Length));//���ļ�����������
                fs.Close();
                string strFileName = "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
                this.Response.AddHeader("Content-disposition", strFileName);
                this.Response.ContentType = "application/octet-staream";
                this.Response.BinaryWrite(btFile);
                string str = response.ResponseUri.ToString();
                System.IO.File.Delete(filePath + "\\" + fileName);
                this.Response.End();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('����ʧ�ܣ�" + ex.Message + "');", true);
            }

        }
    }
}
