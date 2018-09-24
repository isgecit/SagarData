<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="UploadAttachment.aspx.cs" Inherits="Attachment.UploadAttachment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align:initial;font-size:16px;font-weight:bold">Upload Attachment</div>
    <div style="margin-left:0px;margin-top:15px"><table>
        <tr><td>Attachment Handle</td><td>Index Value</td><td>Purpose</td></tr>
        <tr>
          <%--  <td><asp:DropDownList runat="server" ID="ddlLibrary" Width="150"></asp:DropDownList></td>--%>
            <td><asp:DropDownList runat="server" ID="ddlHandle" Width="150" Height="30" OnSelectedIndexChanged="ddlHandle_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
            <td><asp:DropDownList runat="server" ID="ddlIndex" Width="150" Height="30"></asp:DropDownList></td>
            <td><asp:DropDownList runat="server" ID="ddlPurpose" Width="170" Height="30"></asp:DropDownList></td>
        </tr>
         </table></div>
    <div style="margin-left: 30px; margin-top: 20px">
        <asp:FileUpload CssClass="button" ID="FileUpload" runat="server" AllowMultiple="true" />
        <asp:Button ID="btnUpload" CssClass="button" Text="Upload File" runat="server" OnClick="btnUpload_Click" />
    </div>

</asp:Content>
