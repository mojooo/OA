<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Apply.Master" CodeBehind="AddMaster.aspx.cs" Inherits="OA.AddUser" %>

<asp:Content ContentPlaceHolderID="Content1" runat="server">
<table class="Tb_Blank1" style="width:80%; margin-left:10%;">
    <tr>
        <td colspan="2"><h2>���ϵͳ�û�</h2></td>
    </tr>
    <tr>
        <td colspan="2">
             <table class="Tb_Common">
             <tr>
            <td >
                <asp:Label ID="lblDepart" runat="server" Text="����"></asp:Label>
            </td>
            <td style="text-align:left;">
                <asp:DropDownList ID="ddlDepart" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepart_SelectedIndexChanged" SkinID="ddl1"></asp:DropDownList>
            </td>
           </tr>
           <tr>
            <td>
                <asp:Label ID="lblEmployeeName" runat="server" Text="����"></asp:Label><span style="color:Red;">*</span>
            </td>
            <td style="text-align:left;">
                <asp:DropDownList ID="ddlEmployee" runat="server" AutoPostBack="true" SkinID="ddl1"></asp:DropDownList>
            </td>
           </tr>
           <tr>
            <td>
                <asp:Label ID="lblUserName1" runat="server" Text="�û���"></asp:Label><span style="color:Red;">*</span>   
            </td>
            <td style="text-align:left;">
              <asp:TextBox ID="txtUserName" runat="server" SkinID="skinid3"></asp:TextBox>
              <asp:RequiredFieldValidator ID="re1" runat="server" ErrorMessage="����" ControlToValidate="txtUserName"></asp:RequiredFieldValidator>
            </td>
           </tr>
           
           <tr>
            <td >
               <asp:Label ID="lblNewPsd" runat="server" Text="����"></asp:Label><span style="color:Red;">*</span> 
            </td>
            <td style="text-align:left;">
                <asp:TextBox ID="txtNewPsd" runat="server" TextMode="Password" SkinID="skinid3"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="����" ControlToValidate="txtNewPsd"></asp:RequiredFieldValidator>
            </td>
           </tr>
           <tr>
            <td>
               <asp:Label ID="lblCkPsd" runat="server" Text="ȷ������"></asp:Label> <span style="color:Red;">*</span>
            </td>
            <td  style="text-align:left;">
                <asp:TextBox ID="txtCkPsd" runat="server" TextMode="Password" SkinID="skinid3"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="����" ControlToValidate="txtCkPsd"></asp:RequiredFieldValidator>
            </td>
           </tr>
        </table>
        </td>
    </tr>
    <tr>
        <td>   <asp:Button ID="btnAddUser" runat="server" Text="����" OnClick="btnAddUser_Click" /></td>
         <td><asp:Button ID="btnCancel" runat="server" Text="����" OnClick="btnCancel_Click" ValidationGroup="btn1" /></td>
    </tr>
</table>

     
        

</asp:Content>
       