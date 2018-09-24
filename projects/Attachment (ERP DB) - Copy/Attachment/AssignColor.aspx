<%@ Page Title="" Language="C#" MasterPageFile="~/mstInner.Master" AutoEventWireup="true" CodeBehind="AssignColor.aspx.cs" Inherits="Attachment.AssignColor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">



    <link href="ColorPiker/bootstrap-colorpicker.css" rel="stylesheet" />
    <link href="ColorPiker/bootstrap-colorpicker.min.css" rel="stylesheet" />
    <link href="ColorPiker/bootstrap-colorpicker-plus.min.css" rel="stylesheet" />
    <link href="ColorPiker/bootstrap-colorpicker-plus.css" rel="stylesheet" />


    <script src="ColorPiker/bootstrap-colorpicker.js"></script>
    <script src="ColorPiker/bootstrap-colorpicker.min.js"></script>
    <script src="ColorPiker/bootstrap-colorpicker-plus.js"></script>
    <script src="ColorPiker/bootstrap-colorpicker-plus.min.js"></script>

    <script>
        $(function () {
            //var demo3 = $('.colorpickerplus-embed .colorpickerplus-container');
            //demo3.colorpickerembed();
            //demo3.on('changeColor', function (e) {
            //    if (e.color == null)
            //        $('#demo3').val('transparent').css('background-color', '#fff');//tranparent
            //    else
            //        $('#demo3').val(e.color).css('background-color', e.color);
            //});

            var demo2 = $('#demo2');
            demo2.colorpicker();
            demo2.on('changeColor', function (e) {
                if (e.color == null) {
                    $('#ContentPlaceHolder1_txtColor').val('transparent').css('background-color', '#fff');//tranparent
                    $('.color-fill-icon', $(this)).addClass('colorpicker-color');
                }
                else {
                    $('#ContentPlaceHolder1_txtColor').val(e.color).css('background-color', e.color);
                    $("#ContentPlaceHolder1_hdfColor").val(e.color);
                    $('.color-fill-icon', $(this)).removeClass('colorpicker-color');
                    $('.color-fill-icon', $(this)).css('background-color', e.color);
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        User:
        <asp:TextBox runat="server" ID="txtUser" placeholder="Type Name or ID"></asp:TextBox>

        <ajaxToolkit:AutoCompleteExtender ServiceMethod="GetUSer" MinimumPrefixLength="3"
            CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" TargetControlID="txtUser"
            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false" CompletionListElementID="divUser">
        </ajaxToolkit:AutoCompleteExtender>

        <div id="divUser" style="overflow-y: scroll; overflow-x: hidden; max-height: 400px;"></div>

        Color: 
        <asp:TextBox runat="server" ID="txtColor" Style="color: #fff; background-color: cornsilk" ReadOnly="true"></asp:TextBox>
        <asp:HiddenField runat="server" ID="hdfColor" />
        <button type="button" class="btn btn-default" id="demo2" style="background-color: darkgray; width: 30px">..</button>

        <asp:Button runat="server" ID="btnSaveColor" OnClick="btnSaveColor_Click" Text="Save" Style="margin-left: 40px" />
    </div>
    <div style="margin-left: 380px">
    </div>

    <div style="margin-top: 40px; font-size: 12px">
        <asp:GridView runat="server" ID="gvdata" AutoGenerateColumns="false" Width="80%" OnRowDataBound="gvdata_RowDataBound" HeaderStyle-BackColor="White" HeaderStyle-Height="30">
            <Columns>
                <asp:TemplateField HeaderText="User Name">
                    <ItemTemplate>
                        <%#Eval("EmployeeNAme") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Color" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:HiddenField Value='<%#Eval("ColorId") %>' runat="server" ID="hdfColor" />
                        <asp:TextBox runat="server" Width="30" ID="txtColor"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lnkUpdateColor" CommandArgument='<%#Eval("EmployeeNAme") +"&"+Eval("USerId")+"&"+Eval("ColorId")%>'  OnClick="lnkUpdateColor_Click" Text="Edit"/>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" ID="lnkDeleteColor" CommandArgument='<%#Eval("EmployeeNAme") +"&"+Eval("USerId")+"&"+Eval("ColorId")%>'  OnClick="lnkDeleteColor_Click" Text="Delete"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
