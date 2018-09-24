<%@ Page Title="" Language="C#" MasterPageFile="~/mstAttachment.Master" AutoEventWireup="true" CodeBehind="Attachment.aspx.cs" Inherits="Attachment.Attachment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" rel="stylesheet">
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
    <div style="text-align: initial; font-size: 16px; font-weight: bold; text-align: center; font-size: 20px">Attachments</div>
    <%--<div style="margin-left: 70px; width: 200px; margin-bottom: 20px">
        <asp:Button runat="server" ID="btnCreate" Text="Upload Attachment" OnClick="btnCreate_Click" ForeColor="White" BackColor="#0065ca" />
    </div>--%>
    <asp:Label ID="err" runat="server"></asp:Label>
    <div style="margin-left: 70px; margin-top: 20px" runat="server" id="divUploadAttachment">
        <asp:FileUpload CssClass="button" ID="FileUpload" runat="server" AllowMultiple="true" />
        <asp:Button ID="btnUpload" CssClass="button" Text="Upload Attachment" runat="server" OnClick="btnUpload_Click" BackColor="#53a9ff" ForeColor="White" />
    </div>
    <div style="margin-top: 20px; font-size: 12px; color: #646363" runat="server" id="divViewAttachment">
        <asp:GridView runat="server" HorizontalAlign="Center" ID="gvAttachment" AutoGenerateColumns="false" Width="90%"
             HeaderStyle-BackColor="White" HeaderStyle-Height="40"  OnPageIndexChanging="gvAttachment_PageIndexChanging" AllowPaging="true" PageSize="50"> <%--OnRowDataBound="gvAttachment_RowDataBound" --%>
            <Columns>
                <asp:TemplateField HeaderText="File Name">
                    <ItemTemplate>
                        <%#Eval("t_fnam") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Upload Date" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="120">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("t_aton")).ToString("dd-MM-yyyy") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--<asp:TemplateField HeaderText="Attachment Handle">
                    <ItemTemplate>
                        <%#Eval("t_hndl") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Index Value">
                    <ItemTemplate>
                        <%#Eval("t_indx") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Purpose">
                    <ItemTemplate>
                        <%#Eval("t_desc") %>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Download" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lnkDownload" CommandArgument='<%#Eval("t_dcid") +"@"+ Eval("t_fnam") +"@"+ Eval("t_hndl")%>' OnClick="lnkDownload_Click"><i class="fa fa-download" aria-hidden="true" style="color:#4371f1;font-size:12px"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" Visible='<%# Request.QueryString["ed"] == "y"?true:false%>' ID="lnkDelete" Text="Delete" CommandArgument='<%#Eval("t_dcid") %>' OnClick="lnkDelete_Click" OnClientClick="return myFunction()"><i class="fa fa-times-circle-o"  aria-hidden="true"  style="color:#ff0000;font-size:12px"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="e-mail" ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lnkSend" Text="Send" CommandArgument='<%#Eval("t_dcid") +"&"+ Eval("t_fnam") +"&"+ Eval("t_hndl")%>' OnClick="lnkSend_Click"><i class="fa fa-share"  aria-hidden="true"  style="color:#4371f1;font-size:12px"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>

    <div>
        <asp:Label runat="server" ID="lblMail"></asp:Label>
        <ajaxToolkit:ModalPopupExtender ID="mpeMail" Drag="true" runat="server" TargetControlID="lblMail" PopupControlID="pnlMail" CancelControlID="btncloseCancel"></ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlMail" runat="server" Width="40%" Height="300" BorderStyle="Solid" BorderWidth="1" BorderColor="Black" Style="display: none; background: #fff; margin-left: 50px">
            <asp:Button ID="btncloseCancel" runat="server" CssClass="mfp-close" Text="x" Style="width: 24px; float: right; margin-bottom: 5px; font-size: 14px;" />
            <div style="margin-top: 50px; background-color: #fff; margin-left: 20px">
                <asp:HiddenField runat="server" ID="hdfFile" />
                Mail Send To:
                    <asp:TextBox runat="server" ID="txtTo" />
                <asp:Button Text="Send" runat="server" ID="btnSendMail" OnClick="btnSendMail_Click" />
            </div>
        </asp:Panel>
    </div>

    <div runat="server" id="divNoRecord" style="text-align: center; font-size: 20px; margin-top: 50px; color: #b44c4c">No Record Found.</div>
</asp:Content>
