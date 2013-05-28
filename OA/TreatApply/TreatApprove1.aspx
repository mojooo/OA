﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Manage.Master" CodeBehind="TreatApprove1.aspx.cs" Inherits="OA.TreatApply.TreatApprove1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ContentPlaceHolderID="Content1" runat="server">
<table class="Tb_Blank1" style="width:100%;">
            <tr>
                <td>
                     <table style="width:99%; text-align:center;">
                        <tr>
                            <td colspan="2"><h2>业务费用审批</h2></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height:300px; vertical-align:top;">
                               <asp:GridView ID="gvBusiness" runat="server" AutoGenerateColumns="false" AllowSorting="true" DataKeyNames="BusinessExpId" CellPadding="1" Width="100%" OnSorting="gvBusiness_Sorting" OnRowDataBound="gvBusiness_RowDataBound">
                                    <Columns>
                                       <asp:TemplateField >
                                            <ItemTemplate>
                                                <a href="javascript:expandcollapse('div<%# Eval("BusinessExpId") %>','one')">
                                                    <img id="imgdiv<%# Eval("BusinessExpID") %>" alt="Click to show/hide Orders for Customer <%# Eval("BusinessExpId") %>"   border="0" src="../images/plus.gif" />
                                                </a>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="表单编号" SortExpression="SheetNum">
                                             <ItemTemplate>
                                                <asp:LinkButton ID="lbtnDetail" runat="server" style="text-decoration:none;" Text='<%#Eval("SheetNum") %>' CommandArgument='<%#Eval("BusinessExpId") %>' CausesValidation="false" OnClick="btnDetail_Click"  ></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                         
                                          <asp:TemplateField HeaderText="申请人">
                                            <ItemTemplate>
                                                <asp:Label ID="lblApplyName" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="申请时间">
                                            <ItemTemplate>
                                                <asp:Label ID="lblApplyTime" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="审批时间">
                                            <ItemTemplate>
                                                <asp:Label ID="lblApproveTime" runat="server" Text='<%#Eval("Apply1Time") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="审批状态">
                                            <ItemTemplate>
                                                <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnSubmit" Text="审批" runat="server"   CommandArgument='<%#Eval("BusinessExpId") %>' CausesValidation="false" OnClick="btnSubmit_Click" />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField >
                                            <ItemTemplate>
                                                 <tr>
                                                    <td colspan="100%">
                                                         <div id="div<%# Eval("BusinessExpID") %>" style="display:none;position:relative;left:0%;OVERFLOW: auto;WIDTH:100%" >
                                                            <asp:GridView ID="GridView2" Width=100%  AutoGenerateColumns=false  runat="server" ShowHeader="false" DataKeyNames="BusinessExpId"  OnRowDataBound = "GridView2_RowDataBound">
                                                                <Columns>
                                                                   <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="5%" />
                                                                   </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="序号">
                                                                        <ItemTemplate>
                                                                            <%#Container.DataItemIndex+1 %>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="5%" />
                                                                    </asp:TemplateField>
                                                                   <asp:TemplateField HeaderText="表单编号" SortExpression="SheetNum">
                                                                         <ItemTemplate>
                                                                            <asp:LinkButton ID="lbtnDetail" runat="server" style="text-decoration:none;" Text='<%#Eval("SheetNum") %>' CommandArgument='<%#Eval("BusinessExpId") %>' CausesValidation="false" OnClick="btnDetail_Click"  ></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                 
                                                                    <asp:TemplateField HeaderText="申请人">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblApplyName" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="申请时间">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblApplyTime" runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="15%" />
                                                                    </asp:TemplateField>
                                                                      <asp:TemplateField HeaderText="审批时间">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblApproveTime" runat="server" Text='<%#Eval("Apply1Time") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="15%" />
                                                                        </asp:TemplateField>
                                                                         <asp:TemplateField HeaderText="审批状态">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="30%" />
                                                                        </asp:TemplateField>
                                                                         <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                </ItemTemplate>
                                                                                 <ItemStyle Width="10%" />
                                                                   </asp:TemplateField>
                                                                </Columns>
                                                                <AlternatingRowStyle BackColor="#F2F0F0" />
                                                                <HeaderStyle BackColor="#507CB2" Font-Bold="True" ForeColor="White" />
                                                                 <RowStyle BackColor="#ECF5FF" ForeColor="Black" HorizontalAlign="Center" />
                                                           </asp:GridView>
                                                           </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <ItemStyle Width="1px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <AlternatingRowStyle BackColor="#F2F0F0" />
                                   <HeaderStyle BackColor="#507CB2" Font-Bold="True" ForeColor="White"/>
                                   <RowStyle BackColor="#ECF5FF" ForeColor="Black" HorizontalAlign="Center" />
                                </asp:GridView>
                                 <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="True" FirstPageText="第一页" LastPageText="最后一页" NextPageText="下一页" PrevPageText="上一页" OnPageChanged="AspNetPager1_PageChanged">
                               </webdiyer:AspNetPager>
                                <webdiyer:AspNetPager ID="AspNetPager2" runat="server" AlwaysShow="True" FirstPageText="第一页" LastPageText="最后一页" NextPageText="下一页" PrevPageText="上一页" NumericButtonCount="30" PageSize="30" Visible="false" >
                                </webdiyer:AspNetPager>
                            </td>
                        </tr>
                      </table>
                </td>
            </tr>
        </table>
</asp:Content>
    