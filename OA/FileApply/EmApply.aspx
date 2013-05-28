﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Manage.Master" CodeBehind="EmApply.aspx.cs" Inherits="OA.FileApply.EmApply" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content1" runat="server">
 <table class="Tb_Blank1" style="width:100%;">
            <tr>
                <td>
                     <table style="width:100%; text-align:center;">
                        <tr>
                            <td colspan="2"><h2>文件申请列表</h2></td>
                        </tr>
                        
                        <tr>
                        <td colspan="2">
                            <table class="Tb_Blank1">
                                <tr>   
                                    <td  style="text-align:right;"><asp:Button ID="btnAdd" runat="server" Text="新增" OnClick="btnAdd_Click"  /></td>
                                </tr>
                            </table>
                        </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height:300px; vertical-align:top;">
                                 <asp:GridView ID="gvFileApply" runat="server" AutoGenerateColumns="false" CellPadding="1" AllowSorting="true" Width="100%" DataKeyNames="FileApplyId"  OnRowDataBound="gvFileApply_RowDataBound" OnRowDeleting="gvFileApply_RowDeleting"  OnSorting="gvFileApply_Sorting" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                 <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDetail" runat="server" style="text-decoration:none;" CommandArgument='<%#Eval("FileApplyId") %>' CausesValidation="false" OnClick="lbtnDetail_Click"   >  <%#Container.DataItemIndex+1 %></asp:LinkButton>
                                                </ItemTemplate>
                                                 <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="文件名" SortExpression="FilesName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFilesName" runat="server" Text='<%#Eval("FilesName") %>'></asp:Label>
                                                </ItemTemplate>
                                                 <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="类型" SortExpression="FileType">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFileType" runat="server" Text='<%#Eval("FileType") %>'></asp:Label>
                                                </ItemTemplate>
                                                 <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="状态" SortExpression="State">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblState" runat="server" ></asp:Label>
                                                </ItemTemplate>
                                                 <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="申请时间" SortExpression="ApplyTime">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApplyTime" runat="server" Text='<%#Eval("ApplyTime") %>'></asp:Label>
                                                </ItemTemplate>
                                                 <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnSubmit" Text="申请" runat="server" CommandArgument='<%#Eval("FileApplyId") %>' CausesValidation="false" OnClientClick="return confirm('确认要申请吗？');" OnClick="btnSubmit_Click" />
                                                </ItemTemplate>
                                                 <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEdit" Text="编辑" runat="server"  CommandArgument='<%#Eval("FileApplyId") %>' CausesValidation="false" OnClick="btnEdit_Click" />
                                                </ItemTemplate>
                                                 <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('确认要删除吗？');"
                                                        Text="删除" CommandArgument='<%#Eval("FileApplyId") %>'></asp:Button>
                                                </ItemTemplate>
                                                 <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#F2F0F0" />
                                        <HeaderStyle BackColor="#507CB2" Font-Bold="True" ForeColor="White"/>
                                        <RowStyle BackColor="#ECF5FF" ForeColor="Black" HorizontalAlign="Center"  />
                                    </asp:GridView>  
                                     <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" FirstPageText="第一页" LastPageText="最后一页" NextPageText="下一页" PrevPageText="上一页" OnPageChanged="AspNetPager1_PageChanged">
                                     </webdiyer:AspNetPager>
                                      
                            </td>
                        </tr>
                      </table>
                </td>
            </tr>
        </table>
</asp:Content>
