﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Manage.Master" CodeBehind="ManagerGv.aspx.cs" Inherits="OA.ContractApply.ManagerGv" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content1" runat="server">
 <table class="Tb_Blank1" style="width:100%;">
            <tr>
                <td>
                     <table style="width:100%; text-align:center;">
                        <tr>
                            <td colspan="2"><h2>合同\协议审批列表</h2></td>
                        </tr>
                        <tr>
                            <td colspan="2" style=" vertical-align:top;">
                                 <asp:GridView ID="gvContract" runat="server" AutoGenerateColumns="false" CellPadding="1" AllowSorting="true" Width="100%" DataKeyNames="ContractApplyId" OnRowDataBound="gvContract_RowDataBound" OnSorting="gvContract_Sorting"   >
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号">
                                                 <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDetail" runat="server" style="text-decoration:none;" CommandArgument='<%#Eval("ContractApplyId") %>' CausesValidation="false" OnClick="lbtnDetail_Click" >  <%#Container.DataItemIndex+1 %></asp:LinkButton>
                                                </ItemTemplate>
                                                 <ItemStyle Width="5%" />
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="名称" SortExpression="ContractName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContractName" runat="server" Text='<%#Eval("ContractName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="状态" SortExpression="State">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblState" runat="server" ></asp:Label>
                                                </ItemTemplate>
                                                 <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="申请人" SortExpression="ApplyName">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblApplyName" runat="server" Text='<%#Eval("ApplyName") %>'></asp:Label>
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
                                                    <asp:Button ID="btnSubmit" Text="审批" runat="server" CommandArgument='<%#Eval("ContractApplyId") %>' CausesValidation="false" OnClick="btnSubmit_Click"   />
                                                </ItemTemplate>
                                                 <ItemStyle Width="10%" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle BackColor="#F2F0F0" />
                                        <HeaderStyle BackColor="#507CB2" Font-Bold="True" ForeColor="White"/>
                                        <RowStyle BackColor="#ECF5FF" ForeColor="Black" HorizontalAlign="Center"  />
                                    </asp:GridView>  
                                     <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" FirstPageText="第一页" LastPageText="最后一页" NextPageText="下一页" PrevPageText="上一页" OnPageChanged="AspNetPager1_PageChanged"   >
                                     </webdiyer:AspNetPager>
                                      
                            </td>
                        </tr>
                      </table>
                </td>
            </tr>
        </table>
</asp:Content>


