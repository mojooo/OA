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
using CanYouLib.ExcelLib;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace OA
{
    public partial class Asset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "DepartName";
                ViewState["SortDir"] = "ASC";
                IsRole();
                gvDataBind();
            }
        }

        protected void gvDataBind()
        {
            DataTable dt = AssetInfo.getAssetListVW();
           
            DataView view = dt.DefaultView;
            string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
            view.Sort = sort;
           
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                UI.BindCtrl(dt.DefaultView, gvAsset, AspNetPager1);
                gvAsset.Rows[0].Visible = false;
            }
            else
            {
                UI.BindCtrl(dt.DefaultView, gvAsset, AspNetPager1);
            }
           
        }


        protected void IsRole()
        {
            string strDepartName = Session["DepartName"].ToString();

            string strRoleName = Session["RoleName"].ToString();
            if ((strRoleName == "�ܾ���") || (strRoleName == "���ž���" && strDepartName=="�ۺϲ�"))
            {
                btnAdd.Visible = true;
            }
            else
            {
                btnAdd.Visible = false;
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddAsset.aspx");
        }

   
        protected void gvAsset_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int assetid=Convert.ToInt32(gvAsset.DataKeys[e.RowIndex].Value);
            AssetInfo.DelAsset(assetid);
            gvDataBind();
        }

        protected void gvAsset_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int Assetid = Convert.ToInt32(gvAsset.DataKeys[e.RowIndex].Value);
            TextBox txtAssetName = gvAsset.Rows[e.RowIndex].FindControl("txtAssetName") as TextBox;
            TextBox txtAmount = gvAsset.Rows[e.RowIndex].FindControl("txtAmount") as TextBox;
            TextBox txtPrice = gvAsset.Rows[e.RowIndex].FindControl("txtPrice") as TextBox;
            TextBox txtType = gvAsset.Rows[e.RowIndex].FindControl("txtType") as TextBox;
            TextBox txtMemo = gvAsset.Rows[e.RowIndex].FindControl("txtMemo") as TextBox;
            DropDownList ddlUnit = gvAsset.Rows[e.RowIndex].FindControl("ddlUnit") as DropDownList;
            DropDownList ddlDepart = gvAsset.Rows[e.RowIndex].FindControl("ddlDepart") as DropDownList;
            DropDownList ddlSite = gvAsset.Rows[e.RowIndex].FindControl("ddlSite") as DropDownList;
            try
            {
                AssetInfo asset = new AssetInfo(Assetid);
                asset.AssetName = txtAssetName.Text.ToString();
                asset.Amount = txtAmount.Text.ToString();
                asset.Price = txtPrice.Text.ToString();
                asset.Type = txtType.Text.ToString();
                asset.Memo = txtMemo.Text.ToString();
                asset.UnitName = ddlUnit.SelectedItem.Text.ToString();
                asset.DepartName = ddlDepart.SelectedItem.Text.ToString();
                asset.SiteName = ddlSite.SelectedItem.Text.ToString();
                asset.Save();

                gvAsset.EditIndex = -1;
                gvDataBind();
            }
            catch(Exception Ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('����ʧ�ܣ�" + Ex.Message + "');", true); 
            }
        }

        protected void gvAsset_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAsset.EditIndex = -1;
            gvDataBind();
        }

        protected void gvAsset_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string strRoleName = Session["RoleName"].ToString();
                string strDepartName = Session["DepartName"].ToString();

                if ((strRoleName == "�ܾ���") || (strRoleName == "���ž���" && strDepartName == "�ۺϲ�"))
                {
                    e.Row.Cells[9].Visible = true;
                    e.Row.Cells[10].Visible = true;
                    gvAsset.Columns[9].Visible = true;
                    gvAsset.Columns[10].Visible = true;

                }
                else
                {
                    e.Row.Cells[9].Visible = false;
                    e.Row.Cells[10].Visible = false;
                    gvAsset.Columns[9].Visible = false;
                    gvAsset.Columns[10].Visible = false;

                }
            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //����ƶ���ÿ��ʱ��ɫ����Ч��   
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#c1ebff'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

                //�����������ָ����״Ϊ"С��"   
                e.Row.Attributes["style"] = "Cursor:hand";
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gvDataBind();
        }

        protected void gvAsset_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAsset.EditIndex = e.NewEditIndex;
            gvDataBind();
        }

        void Export(bool p_IsTemplate)
        {
            try
            {
                DataTable dt = AssetInfo.getExAssetList();
              
                DataTable dtTemp = dt.Copy();


                if (dt.Rows.Count > 0)
                {

                    //��ȡ·��
                    string strPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 1);
                    strPath = strPath.Replace("/", "\\");
                    DateTime time1 = DateTime.Now;
                    string temlpate = strPath + "\\template\\������������.xls";
                    strPath = strPath + "\\excel";
                    List<ExcelColumInfo> ExcelColumns = new List<ExcelColumInfo>();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        ExcelColumInfo colinfo = new ExcelColumInfo();
                        colinfo.HeadName = dc.ColumnName;
                        colinfo.TableColumnName = dc.ColumnName;
                        colinfo.Width = 20;
                        colinfo.DataType = DataType.Text;
                        if (dc.ColumnName == "MCDate" || dc.ColumnName == "WriteDate")
                            colinfo.DataType = DataType.DateTime;//ShareddedRate
                        else if (dc.ColumnName == "Processes" || dc.ColumnName == "Reject" || dc.ColumnName == "RejectRate" || dc.ColumnName == "Sharedded" || dc.ColumnName == "ShareddedRate" || dc.ColumnName == "RejShareddedRateect" || dc.ColumnName == "RunHour")
                            colinfo.DataType = DataType.Number;
                        ExcelColumns.Add(colinfo);
                    }

                    ExcelHelp eh = new ExcelHelp();
                    byte[] byteResult;
                    //�������ݵ�byte�ֽ�����
                    if (!p_IsTemplate)
                        byteResult = eh.ExportData(dt, ExcelColumns, "�칫��Ʒ����", strPath, 5000);
                    else
                        byteResult = eh.ExportData(dt, ExcelColumns, "�칫��Ʒ����", strPath, 5000, temlpate);


                    DateTime time2 = DateTime.Now;
                    TimeSpan ts1 = new TimeSpan(time1.Ticks);
                    TimeSpan ts2 = new TimeSpan(time2.Ticks);
                    TimeSpan ts = ts1.Subtract(ts2).Duration();
                    string dateDiff = "����1�����ݱ���" + dt.Rows.Count.ToString() + "����¼����ʱ" + ts.Days.ToString() + "��"
                    + ts.Hours.ToString() + "Сʱ"
                    + ts.Minutes.ToString() + "����"
                    + ts.Seconds.ToString() + "��";
                    //lblOledb.Text = dateDiff;
                    //�ͻ��������ļ�
                    Response.Clear();
                    //Response.Buffer = true;
                    Response.Charset = "GB2312";
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    //  ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ���  
                    Response.AddHeader("Content-Disposition", "attachment; filename = " + Server.UrlEncode("�칫��Ʒ����.xls"));
                    //  ���ͷ��Ϣ��ָ���ļ���С����������ܹ���ʾ���ؽ���  
                    Response.AddHeader("Content-Length", byteResult.Length.ToString());
                    //  ָ�����ص���һ�����ܱ��ͻ��˶�ȡ���������뱻����  
                    Response.ContentType = "application/ms-excel";
                    //  ���ļ������͵��ͻ���  
                    //Response.WriteFile(file.FullName);
                    Response.BinaryWrite(byteResult);
                    Response.Flush();
                    //  ֹͣҳ���ִ��
                    //GC.Collect();
                    //Response.End();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('����Ϊ�յ���ʧ�ܣ�');</script>");
                }
            }
            catch (Exception Ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('��������ʧ�ܣ�" + Ex.Message + "');", true); 
                
            }
        }


        protected void btnOutExecel_Click(object sender, EventArgs e)
        {
            Export(false);
        }

        protected void btnInExecel_Click(object sender, EventArgs e)
        {
            if (myFile.PostedFile.FileName != "")
            {
                //�ϴ��ļ��ľ���·��
                string sFile = myFile.PostedFile.FileName;
                //��ȡ�ļ�ȫ��
                sFile = sFile.Substring(sFile.LastIndexOf("\\") + 1);
                //��ȡ��׺��
                sFile = sFile.Substring(sFile.LastIndexOf("."));
                if (sFile.ToLower() != ".xls")
                {
                    Page.RegisterStartupScript("", "<script>alert('�ļ���ʽ����ȷ')</script>");
                }
                //Ϊ�˷�ֹ�������������Ϊ�ļ���������ʱ�������
                string datatime = System.DateTime.Now.ToString("yyyMMddHHmmssffff");
                //�ϴ����ļ�������
                sFile = datatime + sFile;
                //AppDomain.CurrentDomain.BaseDirectory.ToString() ��ȡ����Ŀ�ĸ�Ŀ¼
                //sPath ��ȡ�ϴ����·��
                string sPath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "ExcelFiles\\" + sFile;
                //�ϴ��ļ�
                myFile.PostedFile.SaveAs(sPath);

                string connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sPath + ";Extended Properties=Excel 8.0;";
                OleDbConnection conn = new OleDbConnection(connStr);
                if (conn.State.ToString() == "Closed")
                {
                    conn.Open();
                }
                OleDbDataAdapter oda = new OleDbDataAdapter("select * from [Sheet1$]", conn);
                DataSet ds = new DataSet();
                oda.Fill(ds);
                conn.Close();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string strAssetName = ds.Tables[0].Rows[i][0].ToString();
                    string strAmount = ds.Tables[0].Rows[i][1].ToString();
                    string strUnitName = ds.Tables[0].Rows[i][2].ToString();
                    string strDepartName = ds.Tables[0].Rows[i][3].ToString();
                    string strSiteName = ds.Tables[0].Rows[i][4].ToString();
                    string strPrice = ds.Tables[0].Rows[i][5].ToString();
                    string strType = ds.Tables[0].Rows[i][6].ToString();
                    string strMemo = ds.Tables[0].Rows[i][7].ToString();
                    try
                    {
                        AssetInfo asset = new AssetInfo();
                        asset.AssetName = strAssetName.ToString();
                        asset.Amount = strAmount.ToString();
                        asset.UnitName = strUnitName.ToString();
                        asset.DepartName = strDepartName.ToString();
                        asset.SiteName = strSiteName.ToString();
                        asset.Price = strPrice.ToString();
                        asset.Type = strType.ToString();
                        asset.Memo = strMemo.ToString();
                        asset.Save();
                    }
                    catch (Exception ex)
                    {
                        Response.Write("����ʧ��!���ڣ�" + ex.Message);
                    }
                }
                Response.Write("<SCRIPT>alert('�����ѳɹ����뵽���ݿ⣡');</SCRIPT>");
                gvDataBind();
                File.Delete(sPath);
            }
            else
            {
                Response.Write("<SCRIPT>alert('��ѡ��Excel�ļ���');</SCRIPT>");
            }
           
        }

        protected void gvAsset_Sorting(object sender, GridViewSortEventArgs e)
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

      
  }
}
