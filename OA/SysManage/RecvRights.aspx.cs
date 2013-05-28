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
    public partial class RecvRights : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //�󶨽�ɫȨ��
                rblRoleBind();
                ckblRightBind();
                //��ʼ����ɫȨ��
                int masterId = Convert.ToInt32(Request.QueryString["MasterId"].ToString());
                MasterInfo master = new MasterInfo(masterId);
                EmployeeInfo em = new EmployeeInfo(master.EmployeeId);
                lblUser.Text = master.MasterName.ToString();
                lblEmployee.Text = em.EmployeeName.ToString();
               
                InitRole(masterId);
                InitAction(masterId);

                if (master.State == 2||master.State==3)
                {
                    btnSave.Enabled = false;
                }
            }
        }

        protected void rblRoleBind()
        {
            DataTable dt = RoleInfo.getRoleList();
            rblRole.DataSource = dt;
            rblRole.DataTextField = "RoleName";
            rblRole.DataValueField = "RoleId";
            rblRole.DataBind();
        }

        protected void ckblRightBind()
        {
            DataTable dt = RoleInfo.getRightList();
            ckblRight.DataSource = dt;
            ckblRight.DataTextField = "ActionName";
            ckblRight.DataValueField = "ActionId";
            ckblRight.DataBind();
        }

        //��ʼ����ɫ
        protected void InitRole(int masterid)
        {
            if (RoleInfo.isRoleMaster(masterid))
            {
                DataTable dt = RoleInfo.GetRoleFromMaster(masterid);
                rblRole.SelectedValue = dt.Rows[0]["RoleId"].ToString();
            }
        }

        //��ʼ��Ȩ��
        protected void InitAction(int masterid)
        {
            if (RoleInfo.isActionMaster(masterid))
            {
                DataTable dt = RoleInfo.getActionFromMaster(masterid);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.ckblRight.Items.FindByValue(dt.Rows[i]["ActionId"].ToString()).Selected = true;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int roleid = Convert.ToInt32(rblRole.SelectedValue);
            int masterid = Convert.ToInt32(Request.QueryString["MasterId"].ToString());
            MasterInfo master = new MasterInfo(masterid);
            if (rblIsOver.SelectedItem.Text == "ͨ��")
            {
                master.State = 2;
                master.Save();
                try
                {
                    //��ӽ�ɫ
                    if (RoleInfo.isRoleMaster(masterid))
                    {
                        RoleInfo.UpdateRoleMasters(masterid);
                    }
                    else
                    {
                        RoleMasterInfo rm = new RoleMasterInfo();
                        rm.RoleId = roleid;
                        rm.MasterId = masterid;
                        rm.IsPass = 1;
                        rm.Save();
                    }
                    //���Ȩ��
                    if (RoleInfo.isActionMaster(masterid))
                    {
                        RoleInfo.DelActionMaster(masterid);
                        AddActionMaster(masterid);

                    }
                    else
                    {
                        AddActionMaster(masterid);
                    }
                    master.IsApply = 1;
                    master.Save();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('Ȩ�����óɹ���');</script>");
                }
                catch (Exception Ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Save", "alert('����ʧ�ܣ�" + Ex.Message + "');", true);
                }
            }
            else
            {
                master.State = 3;
                master.Save();
            }
           
        }

        protected void AddActionMaster(int masterid)
        {
            for (int i = 0; i < ckblRight.Items.Count; i++)
            {
                if (ckblRight.Items[i].Selected)
                {
                    ActionMasterInfo am = new ActionMasterInfo();
                    am.MasterId = masterid;
                    am.ActionId = Convert.ToInt32(ckblRight.Items[i].Value);
                    am.IsPass = 1;
                    am.Save();
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("RecvMaster.aspx");
        }
    }
}
