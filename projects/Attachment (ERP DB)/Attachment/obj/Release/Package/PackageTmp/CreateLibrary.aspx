<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="CreateLibrary.aspx.cs" Inherits="Attachment.CreateLibrary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table tr{
            margin-top:20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align:initial;font-size:16px;font-weight:bold" runat="server" id="divHeader">Create Library</div>
    <div style="margin-left:0px;margin-top:15px">
        <table>
            <tr>
                <td>Library Code</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtLibraryCode" MaxLength="15" Width="300" Height="23"/></td>
            </tr>

             <tr>
                <td>Library Description</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtLibDesc"  Width="300" Height="23"/></td>
            </tr>

             <tr>
                <td>Server Name/IP</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtServerName" MaxLength="50" Width="300" Height="23"/></td>
            </tr>

             <tr>
                <td>Path</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtPath" MaxLength="50" Width="300" Height="23"/></td>
            </tr>

             <tr>
                <td>Is Active</td>
                <td>:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlIsActive" Width="300" Height="24"><asp:ListItem Value="Y">Y</asp:ListItem><asp:ListItem Value="N">N</asp:ListItem></asp:DropDownList></td>
            </tr>
             <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button Text="Save"  runat="server" ID="btnSaveLibrary" OnClick="btnSaveLibrary_Click"/>
                    <asp:Button Text="Delete"  runat="server" ID="btnDelete" OnClick="btnDelete_Click" Visible="false"/></td>
            </tr>
        </table>
    </div>
</asp:Content>
