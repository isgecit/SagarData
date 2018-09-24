<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="ViewAttachment.aspx.cs" Inherits="Attachment.ViewAttachment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <div><div style="float: left; width: 200px">
     <asp:Button runat="server" ID="btnCreate" Text="Upload Attachment" OnClick="btnCreate_Click" ForeColor="White" BackColor="#0065ca" /></div>
    <div style="text-align:initial;font-size:16px;font-weight:bold;text-align:center;width:500px">Attachment Details</div></div>

     <div style="margin-left:0px;margin-top:15px"><table>
        <tr><td>Attachment Handle</td><td>Index Value</td><td></td></tr>
        <tr>
          <%--  <td><asp:DropDownList runat="server" ID="ddlLibrary" Width="150"></asp:DropDownList></td>--%>
            <td><asp:DropDownList runat="server" ID="ddlHandle" Width="150" Height="30" OnSelectedIndexChanged="ddlHandle_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
            <td><asp:DropDownList runat="server" ID="ddlIndex" Width="150" Height="30"></asp:DropDownList></td>
            <td><asp:Button runat="server" ID="btnSearch"  Text="Display" OnClick="btnSearch_Click" Width="130" Height="30"/></td>
        </tr>
         </table></div>
    <div style="margin-top:20px">
        <asp:GridView runat="server" ID="gvAttachment" AutoGenerateColumns="false" Width="100%" HeaderStyle-BackColor="White">
            <Columns>
                  <asp:TemplateField HeaderText="Attachment Handle">
                    <ItemTemplate>
                        <%#Eval("t_hndl") %>
                    </ItemTemplate>
                 </asp:TemplateField>

                  <asp:TemplateField HeaderText="Index Value">
                    <ItemTemplate>
                        <%#Eval("t_indx") %>
                    </ItemTemplate>
                  </asp:TemplateField>

                <asp:TemplateField HeaderText="File">
                    <ItemTemplate>
                        <%#Eval("t_fnam") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Purpose">
                    <ItemTemplate>
                        <%#Eval("t_desc") %>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Attached On">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("t_aton")).ToString("dd-MM-yyyy") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                    <asp:LinkButton runat="server" Text="Download" ID="lnkDownload" CommandArgument='<%#Eval("t_dcid") +"&"+ Eval("t_fnam") +"&"+ Eval("t_indx")%>' OnClick="lnkDownload_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                  <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                    <asp:LinkButton runat="server" ID="lnkDelete" Text="Delete" CommandArgument='<%#Eval("t_dcid") %>' OnClick="lnkDelete_Click"  OnClientClick="return myFunction()"></asp:LinkButton>
                    </ItemTemplate>
                  </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
