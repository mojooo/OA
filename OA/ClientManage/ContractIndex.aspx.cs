﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace OA.ClientManage
{
    public partial class ContractIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strRoleName = Session["RoleName"].ToString();
            if (strRoleName == "总经理" || strRoleName == "部门经理")
            {
                Response.Redirect("Contract.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('员工角色不能查看');</script>");
            }
        }
    }
}
