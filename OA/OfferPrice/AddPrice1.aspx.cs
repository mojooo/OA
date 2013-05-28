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


namespace OA.OfferPrice
{
    public partial class AddPrice1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageInit();
                ddlEmDataBind();
                ddlProjectBind();
                ddlUnitsBind();
                rblDataBind();
                ViewState["Isbtn"] = "0";
            }
        }

        protected void rblDataBind()
        {
            DataTable dt = OfferPriceInfo.getOfferPriceType();
            rblType.DataSource = dt;
            rblType.DataTextField = "OfferPriceTypeName";
            rblType.DataValueField = "OfferPriceTypeId";
            rblType.DataBind();
        }

        protected void PageInit()
        {
            txtManagerView.ReadOnly = true;
            DateTime dt = DateTime.Now;
            txtFillTableDate.Text = dt.ToString("yyyy-MM-dd");

        }

        protected void ddlEmDataBind()
        {

            DataTable dt = EmployeeInfo.getEmployeeOfDepart(Convert.ToInt32(Session["DepartId"]));
            ddlEmployee.DataSource = dt;
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "EmployeeId";
            DataRow row = dt.NewRow();
            row["EmployeeName"] = "��ѡ������";
            dt.Rows.InsertAt(row, 0);
            ddlEmployee.DataBind();

        }

        protected void ddlProjectBind()
        {
            DataTable dt = MainProjectCreateInfo.getProjectList();
            ddlProject.DataSource = dt;
            ddlProject.DataTextField = "ProjectName";
            ddlProject.DataValueField = "MainProjectCreateId";
            DataRow row = dt.NewRow();
            row["ProjectName"] = "��ѡ����Ŀ����";
            dt.Rows.InsertAt(row, 0);
            ddlProject.DataBind();
        }

        protected void ddlUnitsBind()
        {
            DataTable dt = AssetInfo.getUnitList();
            ddlPUnit.DataSource = dt;
            ddlPUnit.DataTextField = "UnitName";
            ddlPUnit.DataValueField = "UnitId";
            DataRow row = dt.NewRow();
            row["UnitName"] = "��ѡ��λ";
            dt.Rows.InsertAt(row, 0);
            ddlPUnit.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (ViewState["Isbtn"].ToString() == "0")
            {
                int OpId = Convert.ToInt32(Request["OfferPriceId"].ToString());
                OfferPriceInfo.DelProductTypeList(OpId);
                OfferPriceInfo.DelOfferPrice(OpId);
            }
            Response.Redirect("PriceApply.aspx");
        }

        protected bool IsPageValid()
        {
            if (UpFile.HasFile == false && gvProductType.Rows.Count == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('���ϴ���������д��Ʒ��Ϣ��');</script>");
                return false;
            }
            if (rblType.SelectedValue == "")
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ѡ������ͣ�');</script>");
                return false;
            }
            if (ddlEmployee.SelectedValue.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ѡ����Ŀ�����ˣ�');</script>");
                return false;
            }
            if (ddlProject.SelectedValue.ToString() == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ѡ����Ŀ���ƣ�');</script>");
                return false;
            }
            if (imgMarket.Visible == false)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ǩ����');</script>");
                return false;
            }
            if (OfferPriceInfo.IsOfferFileSame(this.UpFile.FileName))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('���ļ����Ѵ��ڣ�����������');</script>");
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
                try
                {
                    EmployeeInfo em = (EmployeeInfo)Session["Employee"];
                    MainOfferPriceInfo mainop = new MainOfferPriceInfo();
                    mainop.Save();

                    int OpId = Convert.ToInt32(Request["OfferPriceId"].ToString());
                    OfferPriceInfo op = new OfferPriceInfo(OpId);
                    op.MainOfferPriceId = mainop.MainOfferPriceId;

                    if (ddlProject.SelectedValue != "")
                    {
                        op.MainProjectCreateId = Convert.ToInt32(ddlProject.SelectedValue);
                    }
                    if (ddlEmployee.SelectedValue != "")
                    {
                        op.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
                    }

                    op.OfferPriceTypeId = Convert.ToInt32(rblType.SelectedValue);

                    DateTime date = DateTime.Now;
                    string strtoday = DateTime.Now.ToString("yyyyMMdd").Substring(2);
                    op.Today = strtoday;
                    op.SheetNum = txtSheetNum.Text.ToString();

                    op.FillTableDate = Convert.ToDateTime(txtFillTableDate.Text.ToString());
                    op.SectionView = txtSectionView.Text.ToString();
                    op.SheetNum = txtSheetNum.Text.ToString();
                    op.ProjectDes = txtProjectDes.Text.ToString();
                    op.MoneySum = txtMoneySum.Text.ToString();
                    op.BigMoney = txtBigMoney.Text.ToString();
                    op.ProductMoneySum = lblMoneySum.Text.ToString();
                    if (UpFile.HasFile == true)
                    {
                        string name = this.UpFile.FileName;
                        int startindex = this.UpFile.FileName.LastIndexOf(@"\") + 1;
                        string fileName = this.UpFile.FileName.Substring(startindex);
                        string phyFileName = this.Server.MapPath(@"~\Files\" + "OfferPrice") + @"\" + fileName;
                        this.UpFile.SaveAs(phyFileName);

                        OfferFileInfo of = new OfferFileInfo();
                        of.OfferFileName = fileName;//�ļ���
                        of.PhyFileName = UpFile.PostedFile.FileName;//�����ļ�·��
                        of.Save();
                        op.OfferFileId = Convert.ToInt32(of.OfferFileId);
                    }

                    op.SendEmployeeName = em.EmployeeName;
                    op.SendEmployeeId = em.EmployeeId;
                    op.PreIsApply = 1;
                    op.PreIsOver = 1;

                    op.IsApply = 0;
                    op.IsApprove = 0;
                    op.IsApply1 = 0;
                    op.IsApply2 = 0;
                    op.IsOver1 = 0;
                    op.IsOver2 = 0;

                    //GridViewǶ���ж�
                    op.IsMain = 1;
                    op.IsMain1 = 1;
                    op.IsMain2 = 1;
                    op.IsMain3 = 1;
                    op.IsNewCreate = 0;
                    op.IsSignName = 0;
                    op.Save();

                    if (UpFile.HasFile)
                    {
                        ////�ļ��ϴ���ַ��Ŀ¼������ͨ��IIS���豾������ΪFTP������  
                        //string FileSaveUri = @"ftp://192.168.11.70/www/Files/OfferPrice/";
                        ////FTP�û������룬���Ǳ������û�������  
                        //string ftpUser = "test1";
                        //string ftpPassWord = "123";
                        //SendFiles(FileSaveUri, ftpUser, ftpPassWord);
                        this.UpFile.PostedFile.SaveAs(Server.MapPath("~/Files/OfferPrice/" + UpFile.FileName));

                    }

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ӳɹ���');</script>");

                    ViewState["Isbtn"] = "1";
                    txtSheetNum.Text = SheetNums.SheetNumOfOP("BJTB", strtoday);
                    gvProductInitBind();
                }
                catch (Exception Ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('���ʧ�ܣ�" + Ex.Message + "');", true);
                }
            }
        }

        protected void gvProductInitBind()
        {
            DataTable dt = new DataTable();
            gvProductType.DataSource = dt;
            gvProductType.DataBind();
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


        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProject.SelectedValue.ToString() != "")
            {
                int projectid = Convert.ToInt32(ddlProject.SelectedValue);
                MainProjectCreateInfo project = new MainProjectCreateInfo(projectid);
                lblProjectNum.Text = project.SheetNum.ToString();
            }
            else
            {
                lblProjectNum.Text = "";
            }

        }

        protected void gvProductBind()
        {
            int opId = Convert.ToInt32(Request["OfferPriceId"].ToString());
            DataTable dt = OfferPriceInfo.getProductTypeList(opId);
            gvProductType.DataSource = dt;
            gvProductType.DataBind();

        }

        protected bool isPValid()
        {
            if (ddlPUnit.SelectedItem.Text == "��ѡ��λ")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('��ѡ��λ��');</script>");
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (isPValid())
            {
                try
                {
                    int OpId = Convert.ToInt32(Request["OfferPriceId"].ToString());
                    ProductTypeInfo product = new ProductTypeInfo();
                    product.OfferPriceId = OpId;
                    product.ProductName = txtName.Text.ToString();
                    product.Model = txtModel.Text.ToString();
                    product.Num = txtNum.Text.ToString();
                    product.Price = txtPrice.Text.ToString();
                    product.Unit = ddlPUnit.SelectedItem.Text;
                    float fNum = float.Parse(txtNum.Text.ToString());
                    float fPrice = float.Parse(txtPrice.Text.ToString());
                    float fSum = fNum * fPrice;
                    product.Sums = fSum.ToString();
                    product.Save();
                    gvProductBind();
                    string str = OfferPriceInfo.GetProductMoneySum(OpId);
                    if (str == "")
                    {
                        lblMoneySum.Text = "0Ԫ";
                    }
                    else
                    {
                        lblMoneySum.Text = OfferPriceInfo.GetProductMoneySum(OpId) + "Ԫ";
                    }
                    Clear();
                }
                catch (Exception Ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('����ʧ�ܣ�" + Ex.Message + "');", true);
                }

            }

        }

        protected void Clear()
        {
            txtName.Text = "";
            txtModel.Text = "";
            txtNum.Text = "";
            txtPrice.Text = "";
            ddlPUnit.SelectedItem.Text = "��ѡ��λ";
        }

        protected void gvProductType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int productid = Convert.ToInt32(gvProductType.DataKeys[e.RowIndex].Value);
            OfferPriceInfo.DelProductType(productid);
            gvProductBind();

            int OpId = Convert.ToInt32(Request["OfferPriceId"].ToString());
            string str = OfferPriceInfo.GetProductMoneySum(OpId);
            if (str == "")
            {
                lblMoneySum.Text = "0Ԫ";
            }
            else
            {
                lblMoneySum.Text = OfferPriceInfo.GetProductMoneySum(OpId) + "Ԫ";
            }

        }

        protected void rblType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            string strtoday = DateTime.Now.ToString("yyyyMMdd").Substring(2);
            if (rblType.SelectedItem.Text == "����")
            {
                if (SheetNums.IsSheetNumOfOP(strtoday))
                {

                    txtSheetNum.Text = SheetNums.SheetNumOfOP("BJ", strtoday);
                }
                else
                {
                    txtSheetNum.Text = "BJ" + strtoday + "001";
                }
            }
            else if (rblType.SelectedItem.Text == "Ͷ��")
            {
                if (SheetNums.IsSheetNumOfOP(strtoday))
                {

                    txtSheetNum.Text = SheetNums.SheetNumOfOP("TB", strtoday);
                }
                else
                {
                    txtSheetNum.Text = "TB" + strtoday + "001";
                }
            }
        }

        protected void btnSign_Click(object sender, EventArgs e)
        {
            imgMarket.Visible = true;
            btnSign.Enabled = false;
        }
    }
}
