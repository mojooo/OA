﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Manage.Master"  CodeBehind="Upload.aspx.cs" Inherits="OA.ShareManage.Upload" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content1" runat="server">
 <table class="Tb_Blank1" style="width:100%;">
            <tr>
                <td>
                     <table style="width:100%; text-align:center;">
                        <tr>
                            <td colspan="2"><h2>上传文档列表</h2></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table class="Tb_Blank1" style="width:100%">
                                    <tr>
                                        <td style="width:90%; text-align:left;">
                                        <asp:CheckBox ID="ckbAll" runat="server" Text="全部" AutoPostBack="true" OnCheckedChanged="ckbAll_CheckedChanged" />
                                        <asp:Button ID="btnDelete" runat="server" Text="删除" OnClick="btnDelete_Click" OnClientClick="return confirm('确认要删除吗？');" />
                                         </td>
                                        <td style="width:10%;"><asp:Button ID="btnSend" runat="server" Text="上传文档" OnClick="btnSend_Click"  /></td>
                                    </tr>
                                </table>
                               
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height:300px; vertical-align:top;">
                                  <asp:GridView ID="gvFile" runat="server" AutoGenerateColumns="False" AllowSorting="true" DataKeyNames="FileId" CellPadding="3" Width="100%" EmptyDataText="没有数据"  OnSorting="gvFile_Sorting" >
                                   <Columns>
                                   <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ckbChoose" runat="server" />
                                    </ItemTemplate>
                                      <ItemStyle Width="5%" />
                                   </asp:TemplateField>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="文档名" SortExpression="FileName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileName" runat="server" Text='<%#Eval("FileName") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="文档类型" SortExpression="FileTypeName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileType" runat="server" Text='<%#Eval("FileTypeName") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="文档权限" SortExpression="FilePermissionName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepart" runat="server" Text='<%#Eval("FilePermissionName") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注" SortExpression="Memo">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMemo" runat="server" Text='<%#Eval("Memo") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="上传时间" SortExpression="SendTime">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTime" runat="server" Text='<%#Eval("SendTime") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="下载">
                                        <ItemTemplate>
                                            <a href="DownloadDocuments.aspx?fileid=<%#Eval("FileId") %>" target="I2" style="text-decoration:none;">查看</a>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle BackColor="#F2F0F0" />
                               <HeaderStyle BackColor="#507CB2" Font-Bold="True" ForeColor="White"/>
                               <RowStyle BackColor="#ECF5FF" ForeColor="Black" />
                             </asp:GridView>
                             <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" FirstPageText="第一页" LastPageText="最后一页" NextPageText="下一页" PrevPageText="上一页" OnPageChanged="AspNetPager1_PageChanged" >
                            </webdiyer:AspNetPager>
                       
                            </td>
                        </tr>
                      </table>
                </td>
            </tr>
        </table>
</asp:Content>
      

                 

