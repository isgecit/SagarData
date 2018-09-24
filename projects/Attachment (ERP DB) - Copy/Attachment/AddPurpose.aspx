<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="AddPurpose.aspx.cs" Inherits="Attachment.AddPurpose" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table tr {
            margin-top: 20px;
        }
    </style>

    <script>
        function myFunction() {
            var txt;
            var r = confirm("Are you sure to Delete!");
            if (r == true) {
                return true;
            } else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div style="float: left; width: 200px">
            <asp:Button runat="server" Visible="false" ID="btnCreate" Text="New Purpose" OnClick="btnCreate_Click" ForeColor="White" BackColor="#0065ca" />
        </div>
        <div style="text-align: initial; font-size: 16px; font-weight: bold; text-align: center; width: 500px" runat="server" id="divHeader">Purpose</div>
    </div>
    <div style="margin-left: 0px; margin-top: 15px" runat="server" id="divCreate">
        <table>
            <tr>
                <td>Purpose Name</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtPurposeCode" MaxLength="50" Width="300" Height="23"/></td>
            </tr>

            <tr>
                <td>Purpose Description</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtPDescription" Width="300" Height="23"></asp:TextBox></td>
            </tr>

            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button Text="Save" runat="server" ID="btnSavePurpose" OnClick="btnSavePurpose_Click" />
                    <asp:Button runat="server" ID="lnkDelete" Text="Delete" Visible="false" OnClick="lnkDelete_Click" OnClientClick="return myFunction()"></asp:Button>
                </td>
            </tr>
        </table>
    </div>

    <div style="margin-left: 0px; margin-top: 15px" runat="server" id="divView">
        <asp:GridView runat="server" ID="gvData" AutoGenerateColumns="false" Width="100%" HeaderStyle-BackColor="White">
            <Columns>
                <asp:TemplateField HeaderText="Purpose Code">
                    <ItemTemplate>
                        <%#Eval("t_prcd") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Purpose Description">
                    <ItemTemplate>
                        <%#Eval("t_desc") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lnkUpdate" Text="Update" CommandArgument='<%#Eval("t_prcd") %>' OnClick="lnkUpdate_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
