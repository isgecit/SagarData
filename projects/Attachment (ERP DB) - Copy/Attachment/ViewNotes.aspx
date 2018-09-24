<%@ Page Title="" Language="C#" MasterPageFile="~/mstAttachment.Master" AutoEventWireup="true" CodeBehind="ViewNotes.aspx.cs" Inherits="Attachment.ViewNotes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: center; font-size: 16px; font-weight: bold">Notes</div>
    <div style="margin-left: 40px">
        <asp:Button runat="server" ID="btnNewNotes" OnClick="btnNewNotes_Click" Text="New Notes" BackColor="#95caff" /></div>
    <div style="font-size: 12px; margin-left: 40px; margin-top: 20px">
        <asp:GridView runat="server" ID="gvData" AutoGenerateColumns="false" Width="100%" HeaderStyle-BackColor="White">
            <Columns>
                <asp:TemplateField HeaderText="Note Id">
                    <ItemTemplate>
                        <%#Eval("NotesId") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Note Handle">
                    <ItemTemplate>
                        <%#Eval("NotesHandle") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Reference">
                    <ItemTemplate>
                        <%#Eval("IndexValue") %>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Created Date">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("Created_Date")).ToString("dd-MM-yyyy") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Title">
                    <ItemTemplate>
                        <%#Eval("Title") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <%#Eval("Description") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="User">
                    <ItemTemplate>
                        <%#Eval("EmployeeName") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
