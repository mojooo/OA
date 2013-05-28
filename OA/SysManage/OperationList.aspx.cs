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

namespace OA.SysManage
{
    public partial class OperationList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "ProcessName";
                ViewState["SortDir"] = "DESC";
                gvDataBind();
            }
        }

        protected void gvDataBind()
        {
            DataTable dt = RoleInfo.getProcessRoleList();
            DataView view = dt.DefaultView;
            string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
            view.Sort = sort;

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                UI.BindCtrl(dt.DefaultView, gvProcess, AspNetPager1);
                gvProcess.Rows[0].Visible = false;
            }
            else
            {
                UI.BindCtrl(dt.DefaultView, gvProcess, AspNetPager1);
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gvDataBind();
        }

        protected void gvProcess_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //����ƶ���ÿ��ʱ��ɫ����Ч��   
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#c1ebff'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

                //�����������ָ����״Ϊ"С��"   
                e.Row.Attributes["style"] = "Cursor:hand";

                CheckBoxList ckblOpration = e.Row.FindControl("ckblOperation") as CheckBoxList;
                if (ckblOpration != null)
                {
                    DataTable dt = RoleInfo.getOperationList();
                    ckblOpration.DataSource = dt;
                    ckblOpration.DataTextField = "OperationName";
                    ckblOpration.DataValueField = "OperationId";
                    ckblOpration.DataBind();
                }
                
                int roleid = Convert.ToInt32(gvProcess.DataKeys[e.Row.RowIndex]["RoleId"].ToString());
                int processid = Convert.ToInt32(gvProcess.DataKeys[e.Row.RowIndex]["ProcessId"].ToString());
                InitOperation(processid, roleid, ckblOpration);
            }
        }

        //��ʼ��Ȩ��
        protected void InitOperation(int processid,int roleid, CheckBoxList ckbl)
        {
            if (RoleInfo.IsProcessRole(processid, roleid))
            {
                DataTable dt = RoleInfo.getOperation(processid, roleid);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ckbl.Items.FindByValue(dt.Rows[i]["OperationId"].ToString()).Selected = true;
                }
            }
        }

        protected void gvProcess_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void gvProcess_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProcess.EditIndex = e.NewEditIndex;
            gvDataBind();
        }

        protected void gvProcess_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int roleid = Convert.ToInt32(gvProcess.DataKeys[e.RowIndex]["RoleId"].ToString());
            int processid = Convert.ToInt32(gvProcess.DataKeys[e.RowIndex]["ProcessId"].ToString());

            CheckBoxList ckbl = gvProcess.Rows[e.RowIndex].FindControl("ckblOperation") as CheckBoxList;
            try
            {
                AddOperationRole(roleid, processid, ckbl);
                gvProcess.EditIndex = -1;
                gvDataBind();
            }
            catch (Exception Ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('����ʧ�ܣ�" + Ex.Message + "');", true);
            }
        
        }

        protected void AddOperationRole(int roleid,int processid, CheckBoxList ckl)
        {
            for (int i = 0; i < ckl.Items.Count; i++)
            {
                if (ckl.Items[i].Selected && !RoleInfo.IsOperationRole(roleid,processid,Convert.ToInt32(ckl.Items[i].Value)))
                {
                  
                        OpProRoleInfo opr = new OpProRoleInfo();
                        opr.OperationId = Convert.ToInt32(ckl.Items[i].Value);
                        opr.RoleId = roleid;
                        opr.ProcessId = processid;
                        opr.Save();
                }
                else if (!ckl.Items[i].Selected && RoleInfo.IsOperationRole(roleid, processid, Convert.ToInt32(ckl.Items[i].Value)))
                {
                    RoleInfo.DelOperation(processid, roleid, Convert.ToInt32(ckl.Items[i].Value));
                }
            }
        }

        protected void gvProcess_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProcess.EditIndex = -1;
            gvDataBind();
        }
    }
}
