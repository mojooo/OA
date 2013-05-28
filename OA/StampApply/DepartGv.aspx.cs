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

namespace OA.StampApply
{
    public partial class StampApprove1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "State";
                ViewState["SortDir"] = "ASC";
                gvDataBind();
            }
          
        }

        protected void gvDataBind()
        {
            DataTable dt = StampInfo.getStampApproveList(Session["DepartName"].ToString());
            DataView view = dt.DefaultView;
            string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
            view.Sort = sort;
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                UI.BindCtrl(dt.DefaultView, gvStamp, AspNetPager1);
                gvStamp.Rows[0].Visible = false;
            }
            else
            {
                UI.BindCtrl(dt.DefaultView, gvStamp, AspNetPager1);
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gvDataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int StampId = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            StampInfo stamp = new StampInfo(StampId);
            StampFileTypeInfo filetype = new StampFileTypeInfo(Convert.ToInt32(stamp.StampFileTypeId));
            if (filetype.StampFileTypeName == "��˾�ļ�")
            {
                stamp.State = 3;//���ܾ�������
               
            }
            else if (filetype.StampFileTypeName == "�����ļ�")
            {
                stamp.State = 4;//ͨ��
            
            }
            stamp.Save();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('ͨ����');</script>");
            gvDataBind();
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            int StampId = Convert.ToInt32(((Button)sender).CommandArgument.ToString());
            StampInfo stamp = new StampInfo(StampId);
            stamp.State = 2;
            stamp.Save();
            gvDataBind();
            
        }

        protected void gvStamp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblState = e.Row.FindControl("lblState") as Label;
                Button btnSubmit = e.Row.FindControl("btnSubmit") as Button;
                Button btnNo = e.Row.FindControl("btnNo") as Button;
                if (!Convert.IsDBNull(gvStamp.DataKeys[e.Row.RowIndex].Value))
                {
                    StampInfo stamp = new StampInfo(Convert.ToInt32(gvStamp.DataKeys[e.Row.RowIndex].Value));
                    if (stamp.State != 1 )
                    {
                        btnSubmit.Enabled = false;
                        btnNo.Enabled = false;

                    }
                    switch (stamp.State)
                    {
                        case 0:
                            lblState.Text = "δ�ύ";
                            break;
                        case 1:
                            lblState.Text = "�������쵼����";
                            break;
                        case 2:
                            lblState.Text = "����:����";
                            break;
                        case 3:
                            lblState.Text = "���ܾ�������";
                            break;
                        case 4:
                            lblState.Text = "ͨ��";
                            break;
                        case 5:
                            lblState.Text = "�ܾ���:����";
                            break;
                        default:
                            lblState.Text = "����״̬";
                            break;
                    }
                }
                    //����ƶ���ÿ��ʱ��ɫ����Ч��   
                    e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#c1ebff'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

                    //�����������ָ����״Ϊ"С��"   
                    e.Row.Attributes["style"] = "Cursor:hand";
                
            }
        }

        protected void gvStamp_Sorting(object sender, GridViewSortEventArgs e)
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
