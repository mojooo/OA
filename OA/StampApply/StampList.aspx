﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Manage.Master" CodeBehind="StampList.aspx.cs" Inherits="OA.StampApply.StampApprove3" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content1" runat="server">
<table class="Tb_Blank1" style="width:100%;">
            <tr>
                <td>
                     <table style="width:100%; text-align:center;">
                        <tr>
                            <td colspan="2"><h2>用章申请记录</h2></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height:300px; vertical-align:top;">
                                  
                                     <asp:GridView ID="gvStamp" runat="server" AutoGenerateColumns="False" DataKeyNames="StampId" AllowSorting="True" CellPadding="1" Width="100%" OnRowDataBound="gvStamp_RowDataBound" OnSorting="gvStamp_Sorting">
                                        <Columns>
                                             <asp:TemplateField HeaderText="序号">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="申请时间" SortExpression="ApplyTime"> 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApplyTime" runat="server" Text='<%#Eval("ApplyTime") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="使用日期" SortExpression="UseDate"> 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApplyDate" runat="server" Text='<%#Eval("UseDate") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="文件类型" SortExpression="StampFileType"> 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFileType" runat="server" Text='<%#Eval("StampFileTypeName") %>'></asp:Label>
                                                </ItemTemplate>
                                               <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="用章类型" SortExpression="StampType"> 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStampType" runat="server" Text='<%#Eval("StampName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="用途"> 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUse" runat="server" Text='<%#Eval("StampUse") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="备注"> 
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMemo" runat="server" Text='<%#Eval("Memo") %>'></asp:Label>
                                                </ItemTemplate>
                                              <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                        </Columns>
                                         <AlternatingRowStyle BackColor="#F2F0F0" />
                                       <HeaderStyle BackColor="#507CB2" Font-Bold="True" ForeColor="White"/>
                                       <RowStyle BackColor="#ECF5FF" ForeColor="Black" HorizontalAlign="Center" />
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
      

       