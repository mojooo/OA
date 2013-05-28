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
using System.Net;
using System.IO;

namespace OA.ContractApply
{
    public partial class DownLoadFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int fileid = Int32.Parse(Request.QueryString["fileid"]);//��ȡ�ļ���ID
            ContractFileInfo file = new ContractFileInfo(fileid);
            string fileName = file.ContractFileName;
            DownLoads(fileName);
        }

        protected void DownLoads(string fileName)
        {
            //FtpWebRequest reqFTP;
            try
            {
                string filePath = Server.MapPath("../Files/Contract/") + fileName;
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
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('����ʧ�ܣ�" + ex.Message + "');", true);
            }

        }


    }
}
