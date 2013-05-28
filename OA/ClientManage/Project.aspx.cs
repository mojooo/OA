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

namespace OA.ClientManage
{
    public partial class Project : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "SheetNum";
                ViewState["SortDir"] = "DESC";
                gvDataBind();
                ddlDataBind();
            }
        }

        protected void ddlDataBind()
        {
            DataTable dt = ProjectCreateInfo.getProjectLevel();
            ddlProjectLevel.DataSource = dt;
            ddlProjectLevel.DataTextField = "ProjectLevelName";
            ddlProjectLevel.DataValueField = "ProjectLevelId";
            DataRow row = dt.NewRow();
            row["ProjectLevelName"] = "��ѡ����Ŀ����";
            dt.Rows.InsertAt(row, 0);
            ddlProjectLevel.DataBind();
        }

        protected void gvDataBind()
        {
            DataTable dt = MainProjectCreateInfo.getProjectList();

            DataView view = dt.DefaultView;
            string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
            view.Sort = sort;

            if (dt.Rows.Count == 0)
            {
                dt.Rows.Add(dt.NewRow());
                UI.BindCtrl(dt.DefaultView, gvProject, AspNetPager1);
                gvProject.Rows[0].Visible = false;
            }
            else
            {
                UI.BindCtrl(dt.DefaultView, gvProject, AspNetPager1);
            }
        }

       
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string ProjectName = txtProjectName.Text.ToString();
            if (ProjectName != "" && ddlProjectLevel.SelectedValue == "")
            {
               
                DataTable dt = MainProjectCreateInfo.getNameOfProject(ProjectName);
                DataView view = dt.DefaultView;
                string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
                view.Sort = sort;

                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                    UI.BindCtrl(view, gvProject, AspNetPager1);
                    gvProject.Rows[0].Visible = false;
                }
                else
                {
                    UI.BindCtrl(view, gvProject, AspNetPager1);
                }

            }
            else if (ddlProjectLevel.SelectedValue != "" && ProjectName == "")
            {
                DataTable dt = MainProjectCreateInfo.getLevelOfProject(ddlProjectLevel.SelectedItem.Text);
                DataView view = dt.DefaultView;
                string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
                view.Sort = sort;

                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                    UI.BindCtrl(view, gvProject, AspNetPager1);
                    gvProject.Rows[0].Visible = false;
                }
                else
                {
                    UI.BindCtrl(view, gvProject, AspNetPager1);
                }
            }
            else if (ddlProjectLevel.SelectedValue != "" && ProjectName != "")
            {
                DataTable dt = MainProjectCreateInfo.getBothOfProject(ProjectName, ddlProjectLevel.SelectedItem.Text);
                DataView view = dt.DefaultView;
                string sort = (string)ViewState["SortExpression"] + " " + (string)ViewState["SortDir"];
                view.Sort = sort;

                if (dt.Rows.Count == 0)
                {
                    dt.Rows.Add(dt.NewRow());
                    UI.BindCtrl(view, gvProject, AspNetPager1);
                    gvProject.Rows[0].Visible = false;
                }
                else
                {
                    UI.BindCtrl(view, gvProject, AspNetPager1);
                }
            }

            else
            {
                gvDataBind();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Project.aspx");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddProject.aspx");
        }

        protected void gvProject_Sorting(object sender, GridViewSortEventArgs e)
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

        protected void gvProject_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //����ƶ���ÿ��ʱ��ɫ����Ч��   
                e.Row.Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor='#c1ebff'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=e");

                //�����������ָ����״Ϊ"С��"   
                e.Row.Attributes["style"] = "Cursor:hand";


                if (!Convert.IsDBNull(gvProject.DataKeys[e.Row.RowIndex].Value))
                {
                    int projectid = Convert.ToInt32(gvProject.DataKeys[e.Row.RowIndex].Value);
                    MainProjectCreateInfo project = new MainProjectCreateInfo(projectid);

                    DropDownList ddlProjectLevel = e.Row.FindControl("ddlProjectLevel") as DropDownList;

                    if (ddlProjectLevel != null)
                    {
                        DataTable dt = ProjectCreateInfo.getProjectLevel();
                        ddlProjectLevel.DataSource = dt;
                        ddlProjectLevel.DataTextField = "ProjectLevelName";
                        ddlProjectLevel.DataValueField = "ProjectLevelId";
                        ddlProjectLevel.DataBind();
                        ddlProjectLevel.SelectedValue = project.ProjectLevelId.ToString();
                    }

                    DropDownList ddlProjectType = e.Row.FindControl("ddlProjectType") as DropDownList;

                    if (ddlProjectType != null)
                    {
                        DataTable dt = ProjectCreateInfo.getProjectType();
                        ddlProjectType.DataSource = dt;
                        ddlProjectType.DataTextField = "ProjectTypeName";
                        ddlProjectType.DataValueField = "ProjectTypeId";
                        ddlProjectType.DataBind();
                        ddlProjectType.SelectedValue = project.ProjectTypeId.ToString();
                    }

                    DropDownList ddlClient = e.Row.FindControl("ddlClient") as DropDownList;
                    if (ddlClient != null)
                    {
                        DataTable dt = ClientInfo.getClientList();
                        ddlClient.DataSource = dt;
                        ddlClient.DataTextField = "ClientName";
                        ddlClient.DataValueField = "ClientValue";
                        ddlClient.DataBind();
                        ddlClient.SelectedValue = project.ClientId.ToString();
                    }

                }
            }
        }

        protected void gvProject_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int projectId = Convert.ToInt32(gvProject.DataKeys[e.RowIndex].Value);
            MainProjectCreateInfo.DelProject(projectId);
            gvDataBind();
        }

        protected void gvProject_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvProject.EditIndex = -1;
            gvDataBind();
        }

        protected void gvProject_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProject.EditIndex = e.NewEditIndex;
            gvDataBind();
        }

        protected void gvProject_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int projectid = Convert.ToInt32(gvProject.DataKeys[e.RowIndex].Value);
            TextBox txtProjectName = gvProject.Rows[e.RowIndex].FindControl("txtProjectName") as TextBox;
            TextBox txtPreMoney = gvProject.Rows[e.RowIndex].FindControl("txtPreMoney") as TextBox;
            TextBox txtPreProfit = gvProject.Rows[e.RowIndex].FindControl("txtPreProfit") as TextBox;
            DropDownList ddlProjectLevel = gvProject.Rows[e.RowIndex].FindControl("ddlProjectLevel") as DropDownList;
            DropDownList ddlProjectType = gvProject.Rows[e.RowIndex].FindControl("ddlProjectType") as DropDownList;
            DropDownList ddlClient = gvProject.Rows[e.RowIndex].FindControl("ddlClient") as DropDownList;
            try
            {
                MainProjectCreateInfo project = new MainProjectCreateInfo(projectid);
                project.ProjectName = txtProjectName.Text.ToString();
                project.PreMoney = txtPreMoney.Text.ToString();
                project.PreProfit = txtPreProfit.Text.ToString();
                project.ProjectLevelId = Convert.ToInt32(ddlProjectLevel.SelectedValue);
                project.ProjectTypeId = Convert.ToInt32(ddlProjectType.SelectedValue);
                project.ClientId = Convert.ToInt32(ddlClient.SelectedValue);
                project.Save();
                gvProject.EditIndex = -1;
                gvDataBind();
            }
            catch (Exception Ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('����ʧ�ܣ�" + Ex.Message + "');", true);
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            gvDataBind();
        }
    }
}
