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

namespace OA.Score
{
    public partial class Assistantgv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "DateSpan";
                ViewState["SortDir"] = "ASC";
                gvDataBind();
            }
        }

        protected void gvDataBind()
        {
            DataSet ds = BasEmScoreInfo.getDriveds();
            DataSet ds1 = BasEmScoreInfo.getNurseds();
            ds.Merge(ds1);

            DataView view = ds.Tables[0].DefaultView;
            string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
            view.Sort = sort;

            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                UI.BindCtrl(dt.DefaultView, gvScore, AspNetPager1);
                gvScore.Rows[0].Visible = false;
            }
            else
            {
                UI.BindCtrl(dt.DefaultView, gvScore, AspNetPager1);
            }
        }

        protected void btnMyScore_Click(object sender, EventArgs e)
        {
            Response.Redirect("FrontScoregv.aspx");
        }

       

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gvDataBind();
        }

        protected void gvScore_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //����ƶ���ÿ��ʱ��ɫ����Ч��   
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#c1ebff'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");
                //�����������ָ����״Ϊ"С��"   
                e.Row.Attributes["style"] = "Cursor:hand";

               
                    LinkButton lbtName = e.Row.FindControl("lbtnName") as LinkButton;
                    Button btnSubmit = e.Row.FindControl("btnSubmit") as Button;
                    if (lbtName.Text == "�κ���")
                    {
                        int basid = Convert.ToInt32(gvScore.DataKeys[e.Row.RowIndex]["DriverScoreId"].ToString());
                        DriverScoreInfo ds = new DriverScoreInfo(basid);
                        if (ds.IsSubmit == 2)
                        {
                            btnSubmit.Enabled = false;
                        }
                    }
                    else if (lbtName.Text == "�Ų�")
                    {
                        int fid = Convert.ToInt32(gvScore.DataKeys[e.Row.RowIndex]["NurseScoreId"].ToString());
                        NurseScoreInfo ma = new NurseScoreInfo(fid);
                        if (ma.IsSubmit == 2)
                        {
                            btnSubmit.Enabled = false;
                        }

                    }
                }
        }

        protected void gvScore_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void lbtnName_Click(object sender, EventArgs e)
        {
            string[] str = ((LinkButton)sender).CommandArgument.Split(',');
            if (str[0] != "")
            {
                Response.Redirect("DriveScoreDetail.aspx?DriverScoreId=" + str[0]);
            }
            else if (str[1] != "")
            {
                Response.Redirect("NurseScoreDetail.aspx?NurseScoreId=" + str[1]);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string[] str = ((Button)sender).CommandArgument.Split(',');
            if (str[0] != "")
            {
                Response.Redirect("DriveScoreApprove.aspx?DriverScoreId=" + str[0]);
            }
            else if (str[1] != "")
            {
                Response.Redirect("NurseScoreApprove.aspx?NurseScoreId=" + str[1]);
            }
        }
    }
}
