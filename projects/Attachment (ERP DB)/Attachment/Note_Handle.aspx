<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="Note_Handle.aspx.cs" Inherits="Attachment.Note_Handle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
   
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

        //function AddComments() {
        //    $("#divComments").show();
        //}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align: initial; font-size: 16px; font-weight: bold" runat="server" id="divHeader">Notes Handle</div>

    <div style="margin-left: 0px; margin-top: 15px">
        <table>
            <tr>
                <td>Notes Handle</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtNotesHandle" MaxLength="50" /></td>
            </tr>

            <tr>
                <td>Database Id</td>
                <td>:</td>
                <td>
                     <asp:TextBox runat="server" ID="txtDBID" MaxLength="50" /></td>
              <%--      <asp:DropDownList runat="server" ID="ddlDBID" Width="170"></asp:DropDownList></td>--%>
            </tr>

            <tr>
                <td>Table Name</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtTableName" /></td>
            </tr>
            <tr>
                <td>Table Description</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtTableDesc" /></td>
            </tr>
            <tr>
                <td>Acess Index</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtAcessIndex" MaxLength="50" /></td>
            </tr>

            <tr>
                <td>Remarks</td>
                <td>:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtRemarks" MaxLength="50" /></td>
            </tr>

            <tr>
                <td></td>
                <td></td>
                <td>
                    <asp:Button Text="Save" runat="server" ID="btnSaveHandle" OnClick="btnSaveHandle_Click" />
                    <asp:Button runat="server" ID="lnkDelete" Text="Delete" OnClick="lnkDelete_Click" Visible="false" OnClientClick="return myFunction() "></asp:Button>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
