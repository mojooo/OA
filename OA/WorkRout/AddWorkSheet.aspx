<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Apply1.Master" CodeBehind="AddWorkSheet.aspx.cs" Inherits="OA.jszhichi" %>

<asp:Content ContentPlaceHolderID="Content1" runat="server">
 <table class="Tb_Blank" style="width:100%;">
        <tr>
            <td colspan="4"><h1>����֧�ֹ���</h1></td>
        </tr>
        <tr>
            <td style="width:12%">��������:<span style="color:Red;">*</span></td>
            <td style="width:50%; text-align:left;">
                 <asp:RadioButtonList ID="rblWorkSheetType" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"></asp:RadioButtonList>
             </td>
          
            <td style="width:30%" colspan="2">�������:<span style="color:Red;">*</span>
            <asp:TextBox ID="txtSheetNum" runat="server" SkinID="skinid5" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
            <table class="Tb_Common" style="text-align:center;">
            <tr>
                <td width="20%">������</td>
                <td width="30%"><asp:Label ID="lblApplyName" runat="server"></asp:Label></td>
                <td width="20%">����ʱ��<span style="color:Red;">*</span></td>
                <td width="30%">
                <asp:TextBox ID="txtApplyDate" runat="server" onclick="WdatePicker()" SkinID="skinid1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vDate" runat="server" ErrorMessage="����" ControlToValidate="txtApplyDate" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
               <td>�ͻ�����<span style="color:Red;">*</span></td>
                <td>
                <asp:DropDownList ID="ddlClient" runat="server" AutoPostBack="true" SkinID="skinid2" ></asp:DropDownList>
                </td> 
                <td>�ͻ���ϵ��<span style="color:Red;">*</span></td>
                <td><asp:DropDownList ID="ddlRelate" runat="server" AutoPostBack="true" SkinID="skinid2" ></asp:DropDownList></td>
            </tr>
            <tr>
                <td>�绰</td>
                <td><asp:Label ID="lblTelephone" runat="server"></asp:Label></td>
                <td>��ַ</td>
                <td><asp:Label ID="lblAddress" runat="server"></asp:Label></td>
            </tr>
            <tr>
               <td>����</td>
                <td><asp:Label ID="lblFax" runat="server"></asp:Label></td>
                <td>�ʱ�</td>
                <td><asp:Label ID="lblMailNo" runat="server"></asp:Label></td>
            </tr>
            <tr>
               <td>�ֻ�</td>
                <td><asp:Label ID="lblMobile" runat="server"></asp:Label></td>
                <td>Email</td>
                <td><asp:Label ID="lblEmail" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td rowspan="3">ҵ������</td>
                 <td>��ͬ����</td>
                <td><asp:DropDownList ID="ddlContract" runat="server" AutoPostBack="true" ></asp:DropDownList></td>
                <td><asp:Label ID="lblContract" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>��Ŀ����</td>
                <td><asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="true" ></asp:DropDownList></td>
                <td><asp:Label ID="lblProject" runat="server"></asp:Label></td>
              
            </tr>
            <tr>
                <td>��������</td>
                <td colspan="2"><asp:TextBox ID="txtOtherMiaoshu" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>����Ҫ��</td>
                <td colspan="3" style="text-align:left;"><asp:TextBox ID="txtTecR" runat="server" SkinID="skinid1"></asp:TextBox></td>
            </tr>
            <tr>
                <td>����Ҫ��</td>
                <td colspan="3" style="text-align:left;"><asp:TextBox ID="txtElseR" runat="server" SkinID="skinid1"></asp:TextBox></td>
            </tr>
            <tr>
                <td>���ʱ��<span style="color:Red;">*</span></td>
                <td colspan="3" style="text-align:left;">
                <asp:TextBox ID="txtShiXian" runat="server" onclick="WdatePicker()" SkinID="skinid1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="vShiXian" runat="server" ErrorMessage="����" ControlToValidate="txtShiXian"></asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                <td>
                    ��ע</td>
                <td colspan="3" style="text-align:left;"><asp:TextBox ID="txtElse" runat="server" TextMode="MultiLine"  SkinID="skinid1"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    �г������</td>
                <td><asp:TextBox ID="txtMarketView" runat="server"  SkinID="skinid1" TextMode="MultiLine"></asp:TextBox></td>
                <td>
                    ���������</td>
                <td><asp:TextBox ID="txtTechView" runat="server" SkinID="skinid1" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
        </table>
         <table class="Tb_Blank" style="width:100%;">
            <tr>
                <td style="width:15%; height:80px;">�г�������</td>
                <td style="width:35%;"><asp:Image ID="imgMarket" runat="server" ImageUrl="~/images/hechun.jpg" Visible="false" /></td>
                <td style="width:15%;">����������</td>
                <td style="width:35%;"></td>
            </tr>
        </table>
        </td>
        </tr>
    </table>
        

</asp:Content>
<asp:Content  ContentPlaceHolderID="Content2" runat="server">
 <table class="Tb_Blank" style="width:100%;">
           
             <tr>
                <td>
                     <asp:Button ID="Button1" runat="server" Text="����"   />
                </td>
               <td>
                   <asp:Button ID="Button2" runat="server" Text="����"   ValidationGroup="btn1"/>
               </td> 
           </tr>
</table>
</asp:Content>


   