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

namespace OA.ContractApply
{
    public partial class ContractApplyIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strRoleName = Session["RoleName"].ToString();
            if (strRoleName == "员工")
            {
                Response.Redirect("EmApply.aspx");
            }
            else if (strRoleName == "部门经理")
            {
                Response.Redirect("DepartApply.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'>alert('总经理无需申请');</script>");
            }
        }
    }
}
