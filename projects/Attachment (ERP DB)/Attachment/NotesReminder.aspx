<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="NotesReminder.aspx.cs" Inherits="Attachment.NotesReminder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="font: 12px">
        <asp:GridView runat="server" ID="gvData" AutoGenerateColumns="false" HeaderStyle-BackColor="White" HeaderStyle-Height="30" Width="100%" AllowPaging="true" PageSize="50" OnPageIndexChanging="gvData_PageIndexChanging" OnRowDataBound="gvData_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Reminder To" ItemStyle-Width="100">
                    <ItemTemplate>
                        <%#Eval("ReminderTo") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Date" ItemStyle-Width="100">
                    <ItemTemplate>
                        <%# Convert.ToDateTime(Eval("ReminderDateTime")).ToString("dd-MM-yyyy") %>
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

                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <%#Eval("Status").ToString()=="New"?"Pending":Eval("Status") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>
    </div>
</asp:Content>
