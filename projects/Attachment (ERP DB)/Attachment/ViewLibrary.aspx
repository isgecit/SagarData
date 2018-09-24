<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="ViewLibrary.aspx.cs" Inherits="Attachment.ViewLibrary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div><div style="float:left;width:200px"><asp:Button runat="server" ID="btnCreateLibrary" Text="New Library"  OnClick="btnCreateLibrary_Click" ForeColor="White"  BackColor="#0065ca"/></div>
     <div style="text-align:initial;font-size:16px;font-weight:bold;text-align:center;width:800px">Library Details</div></div>
    <div style="margin-left:0px;margin-top:15px">
        <asp:GridView runat="server" ID="gvData" AutoGenerateColumns="false" Width="100%" HeaderStyle-BackColor="White">
            <Columns>
                <asp:TemplateField HeaderText="Library Code">
                    <ItemTemplate>
                        <%#Eval("t_lbcd") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Library Description">
                    <ItemTemplate>
                        <%#Eval("t_desc") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Server Name">
                    <ItemTemplate>
                        <%#Eval("t_serv") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Path">
                    <ItemTemplate>
                        <%#Eval("t_path") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Is Active">
                    <ItemTemplate>
                        <%#Eval("t_acti").ToString()=="Y"?"Yes":"No" %>
                    </ItemTemplate>
                </asp:TemplateField>

                     <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lnkUpdate" Text="Edit" CommandArgument='<%#Eval("t_lbcd") %>' OnClick="lnkUpdate_Click"></asp:LinkButton>
                       <%-- <asp:LinkButton runat="server" ID="lnkDelete" Text="Delete" CommandArgument='<%#Eval("LibraryCode") %>' OnClick="lnkDelete_Click"></asp:LinkButton>--%>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
