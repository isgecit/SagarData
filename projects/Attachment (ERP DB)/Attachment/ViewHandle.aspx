<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="ViewHandle.aspx.cs" Inherits="Attachment.ViewHandle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div style="float: left; width: 200px">
            <asp:Button runat="server" ID="btnCreate" Text="New Attachment Handle" OnClick="btnCreate_Click" ForeColor="White" BackColor="#0065ca" /></div>
        <div style="text-align: initial; font-size: 16px; font-weight: bold; text-align: center; width: 500px">Attachment Handle </div>
    </div>
    <div style="margin-left: 0px; margin-top: 15px;font-size:12px">
        <asp:GridView runat="server" ID="gvData" AutoGenerateColumns="false" Width="100%" HeaderStyle-BackColor="White">
            <Columns>
                <asp:TemplateField HeaderText="Attachment Handle">
                    <ItemTemplate>
                        <%#Eval("t_hndl") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Access Index">
                    <ItemTemplate>
                        <%#Eval("t_indx") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="DataBase ID">
                    <ItemTemplate>
                        <%#Eval("t_dbid") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Table Name">
                    <ItemTemplate>
                        <%#Eval("t_tabl") %>
                    </ItemTemplate>
                </asp:TemplateField>

               <asp:TemplateField HeaderText="Table Description">
                <ItemTemplate>
                        <%#Eval("t_tdes") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Remarks">
                    <ItemTemplate>
                        <%#Eval("t_rema") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lnkUpdate" Text="Edit" CommandArgument='<%#Eval("t_hndl") %>' OnClick="lnkUpdate_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
