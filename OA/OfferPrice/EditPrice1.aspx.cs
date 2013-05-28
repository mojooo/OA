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
    public partial class EditPrice1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageInit();
                ddlEmDataBind();
                ddlProjectBind();
                rblDataBind();
                gvProductBind();
                ddlUnitsBind();
            }
        }

        protected void gvProductBind()
        {
            int opId = Convert.ToInt32(Request["OfferPriceId"].ToString());
            DataTable dt = OfferPriceInfo.getProductTypeList(opId);
            gvProductType.DataSource = dt;
            gvProductType.DataBind();

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

        protected void Clear()
        {
            txtName.Text = "";
            txtModel.Text = "";
            txtNum.Text = "";
            txtPrice.Text = "";
            ddlPUnit.SelectedItem.Text = "��ѡ��λ";
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
            int opid = Convert.ToInt32(Request["OfferPriceId"].ToString());
            OfferPriceInfo op = new OfferPriceInfo(opid);
            ddlProject.SelectedValue = op.MainProjectCreateId.ToString();
            ddlEmployee.SelectedValue = op.EmployeeId.ToString();
            txtFillTableDate.Text = op.FillTableDate.ToString();
            txtManagerView.Text = op.MangerView.ToString();
            txtSectionView.Text = op.SectionView.ToString();
            txtSheetNum.Text = op.SheetNum.ToString();
            txtProjectDes.Text = op.ProjectDes.ToString();
            rblType.SelectedValue = op.OfferPriceTypeId.ToString();
            lblMoneySum.Text = op.ProductMoneySum.ToString();
            if (op.MoneySum != null)
            {
                txtMoneySum.Text = op.MoneySum.ToString();
            }
            if (op.BigMoney != null)
            {
                txtBigMoney.Text = op.BigMoney.ToString();
            }

            MainProjectCreateInfo project = new MainProjectCreateInfo(Convert.ToInt32(op.MainProjectCreateId));
            lblProjectNum.Text = project.SheetNum.ToString();
            if (op.OfferFileId != null)
            {
                OfferFileInfo of = new OfferFileInfo(Convert.ToInt32(op.OfferFileId));
                lblUrl.Text = of.PhyFileName.ToString();
            }

        }

        protected void ddlEmDataBind()
        {
            EmployeeInfo em = (EmployeeInfo)Session["Employee"];
            DataTable dt = EmployeeInfo.getEmployeeOfDepart(Convert.ToInt32(em.DepartId));
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

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            int projectid = Convert.ToInt32(ddlProject.SelectedValue);
            MainProjectCreateInfo project = new MainProjectCreateInfo(projectid);
            lblProjectNum.Text = project.SheetNum.ToString();
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
                   
                    int opid = Convert.ToInt32(Request["OfferPriceId"].ToString());
                    OfferPriceInfo pc = new OfferPriceInfo(opid);
                    if (pc.IsNewCreate == 1)
                    {
                        pc.IsMain = 0;
                        pc.IsMain1 = 0;
                        pc.IsMain2 = 0;
                        pc.IsMain3 = 0;
                        OfferPriceInfo pcnew = new OfferPriceInfo();
                        pcnew.IsMain = 1;
                        pcnew.IsMain1 = 1;
                        pcnew.IsMain2 = 1;
                        pcnew.IsMain3 = 1;
                        pcnew.IsNewCreate = 0;
                        pcnew.MainOfferPriceId = pc.MainOfferPriceId;
                    

                            pcnew.SendEmployeeId = pc.SendEmployeeId;
                            pcnew.SendEmployeeName = pc.SendEmployeeName;
                            pcnew.PreIsApply = 1;
                            pcnew.PreIsOver = 1;


                        pcnew.IsApply = 0;
                        pcnew.IsApprove = 0;
                        pcnew.IsApply1 = 0;
                        pcnew.IsApply2 = 0;
                        pcnew.IsOver1 = 0;
                        pcnew.IsOver2 = 0;
                        pcnew.IsSignName = 0;

                        if (ddlProject.SelectedValue != "")
                        {
                            pcnew.MainProjectCreateId = Convert.ToInt32(ddlProject.SelectedValue);
                        }
                        if (ddlEmployee.SelectedValue != "")
                        {
                            pcnew.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
                        }

                        DateTime date = DateTime.Now;
                        string strtoday = DateTime.Now.ToString("yyyyMMdd").Substring(2);
                        pcnew.Today = strtoday;

                        if (UpFile.HasFile == true)
                        {
                            string name = this.UpFile.FileName;
                            int startindex = this.UpFile.FileName.LastIndexOf(@"\") + 1;
                            string fileName = this.UpFile.FileName.Substring(startindex);
                            string phyFileName = this.Server.MapPath(@"~\Files\" + "OfferPrice") + @"\" + fileName;
                            this.UpFile.SaveAs(phyFileName);

                            if (pcnew.OfferFileId.ToString() == "")
                            {
                                OfferFileInfo of = new OfferFileInfo();
                                of.OfferFileName = fileName;
                                of.PhyFileName = UpFile.PostedFile.FileName;//��ȡ�����ļ�·��
                                of.Save();
                                pcnew.OfferFileId = Convert.ToInt32(of.OfferFileId);
                            }
                            else
                            {
                                OfferFileInfo of = new OfferFileInfo(Convert.ToInt32(pcnew.OfferFileId));
                                of.OfferFileName = fileName;//�ļ�����
                                of.PhyFileName = UpFile.PostedFile.FileName;//��ȡ�����ļ�·��
                                of.Save();
                                pcnew.OfferFileId = Convert.ToInt32(of.OfferFileId);
                            }

                            lblUrl.Text = UpFile.PostedFile.FileName;//�����ļ�·��
                        }

                        if (UpFile.HasFile)
                        {
                           
                            this.UpFile.PostedFile.SaveAs(Server.MapPath("~/Files/OfferPrice/" + UpFile.FileName));

                        }


                        pcnew.OfferPriceTypeId = Convert.ToInt32(rblType.SelectedValue);
                        pcnew.FillTableDate = Convert.ToDateTime(txtFillTableDate.Text.ToString());
                        pcnew.SectionView = txtSectionView.Text.ToString();
                        pcnew.SheetNum = txtSheetNum.Text.ToString();
                        pcnew.ProjectDes = txtProjectDes.Text.ToString();
                        pcnew.MoneySum = txtMoneySum.Text.ToString();
                        pcnew.BigMoney = txtBigMoney.Text.ToString();
                        pcnew.ProductMoneySum = lblMoneySum.Text.ToString();
                        pcnew.Save();
                        pc.Save();

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('�����ɹ���');</script>");

                    }
                    else
                    {
                        if (ddlProject.SelectedValue != "")
                        {
                            pc.MainProjectCreateId = Convert.ToInt32(ddlProject.SelectedValue);
                        }
                        if (ddlEmployee.SelectedValue != "")
                        {
                            pc.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
                        }

                        if (UpFile.HasFile == true)
                        {
                            string name = this.UpFile.FileName;
                            int startindex = this.UpFile.FileName.LastIndexOf(@"\") + 1;
                            string fileName = this.UpFile.FileName.Substring(startindex);
                            string phyFileName = this.Server.MapPath(@"~\Files\" + "OfferPrice") + @"\" + fileName;
                            this.UpFile.SaveAs(phyFileName);

                            if (pc.OfferFileId.ToString() == "")
                            {
                                OfferFileInfo of = new OfferFileInfo();
                                of.OfferFileName = fileName;
                                of.PhyFileName = UpFile.PostedFile.FileName;//�����ļ�·��
                                of.Save();
                                pc.OfferFileId = Convert.ToInt32(of.OfferFileId);
                            }
                            else
                            {
                                OfferFileInfo of = new OfferFileInfo(Convert.ToInt32(pc.OfferFileId));
                                of.OfferFileName = fileName;//�ļ�����
                                of.PhyFileName = UpFile.PostedFile.FileName;//�����ļ�·��
                                of.Save();
                                pc.OfferFileId = Convert.ToInt32(of.OfferFileId);
                            }

                            lblUrl.Text = UpFile.PostedFile.FileName;
                        }


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

                        pc.FillTableDate = Convert.ToDateTime(txtFillTableDate.Text.ToString());
                        pc.SectionView = txtSectionView.Text.ToString();
                        pc.SheetNum = txtSheetNum.Text.ToString();
                        pc.ProjectDes = txtProjectDes.Text.ToString();
                        pc.OfferPriceTypeId = Convert.ToInt32(rblType.SelectedValue);
                        pc.MoneySum = txtMoneySum.Text.ToString();
                        pc.BigMoney = txtBigMoney.Text.ToString();
                        pc.ProductMoneySum = lblMoneySum.Text.ToString();
                        pc.Save();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('�༭�ɹ���');</script>");
                    }

                }
                catch (Exception Ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('���ʧ�ܣ�" + Ex.Message + "');", true);
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
            Response.Redirect("PriceApply.aspx");
        }

        protected void gvProductType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int productid = Convert.ToInt32(gvProductType.DataKeys[e.RowIndex].Value);
            OfferPriceInfo.DelProductType(productid);
            gvProductBind();
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
