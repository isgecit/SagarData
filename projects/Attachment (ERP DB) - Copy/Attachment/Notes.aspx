<%@ Page Title="" Language="C#" validateRequest="false" MasterPageFile="~/mstAttachment.Master" AutoEventWireup="true" CodeBehind="Notes.aspx.cs" Inherits="Attachment.Notes" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
       
    </style>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="scripts/bootstrap-datetimepicker.js"></script>
    <script src="scripts/bootstrap-datetimepicker.min.js"></script>
    <link href="css/bootstrap-datetimepicker.css" rel="stylesheet" />
    <link href="css/bootstrap-datetimepicker.min.css" rel="stylesheet" />


    <script>
        //$(function () {
        //    $('#ContentPlaceHolder1_txtDateTime').datetimepicker(
        //        {
        //            interval: 15
        //        });

        //    var currentDate = new Date();
        //    $("#ContentPlaceHolder1_txtDateTime").datetimepicker("setDate", currentDate)
        //});

        $(function () {
            var date = new Date();
            date.setDate(date.getDate());
            $("#ContentPlaceHolder1_txtDate").datetimepicker({
                startDate: date,
                format: "dd-mm-yyyy",
                autoclose: true,
                linkFormat: 'yyyy-mm-dd',
                todayHighlight: 1,
                startView: 2,
                minView: 2,
                forceParse: 0,
                daysOfWeekDisabled: "0",
                //daysOfWeekHighlighted: "6",
                beforeShowDay: function (date) {
                    // Always available
                    return [true, 'available', null];
                }
            });
            $("#ContentPlaceHolder1_txtTime").datetimepicker({
                format: 'H:ii',
                linkFormat: 'hh:ii',
                showMeridian: true,
                autoclose: true,
                todayHighlight: 0,
                startView: 1,
                minView: 0,
                maxView: 0,
                minuteStep: 1,
                forceParse: 0,
            });
        });

     

    </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="font-size: 22px; font-weight: bold; margin-top: 20px">
        <div style="text-align: center; font-size: 14px">Notes for <strong runat="server" id="spIndex"></strong></div>
        <div style="font-size: 12px; margin-left: 340px;">
            <asp:Button runat="server" ID="btnNewNotes" OnClick="btnNewNotes_Click" Text="New Notes" BackColor="" />
        </div>
    </div>
    <div style="margin-top: 10px; margin-left: 340px;">
        <div style="background-color: ; min-height: 510px; width: 700px; border: 1px solid; overflow: hidden;">
            <table style="margin-top: 10px">
                <tr>
                    <td style="font-weight: bold">Title: &nbsp &nbsp &nbsp &nbsp &nbsp</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTitle" Width="500"></asp:TextBox></td>
                </tr>
                <tr style="height: 10px"></tr>
                <tr>
                    <td style="text-align: start; font-weight: bold">Desription</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine" Height="300" Width="500" />
                    </td>
                </tr>
                <tr style="height: 10px"></tr>
                  <tr>
                    <td style="font-weight: bold">Send mail to:&nbsp&nbsp&nbsp</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMailTo" Width="500" />
                        &nbsp &nbsp
                        </td>
                </tr>

                 <tr style="height: 10px"></tr>
        
                <tr>
                    <td style="font-weight: bold">Reminder:</td>
                    <td>E-mail Id:&nbsp<asp:TextBox ID="txtMailIdReminder" runat="server" Width="430" />
                    </td>
                </tr>
                <tr style="height: 10px"></tr>
                <tr>
                    <td></td>
                    <td>Date :&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:TextBox runat="server" ID="txtDate" />
                        &nbsp &nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp<asp:TextBox runat="server" ID="txtTime" Text="9:00" ReadOnly="true" Enabled="false" Visible="false" />
                    </td>
                </tr>
                <tr style="height: 20px"></tr>

              
                <tr>
                    <td></td>
                    <td>
                        <asp:Button runat="server" ID="btnSaveNotes" OnClick="btnSaveNotes_Click" Text="Submit" Width="100" />
                        <asp:Button runat="server" ID="btnDeleteNotes" OnClick="btnDeleteNotes_Click" Text="Delete" Visible="false"/>
                        <asp:Button runat="server" ID="btnAttachment" OnClick="btnAttachment_Click" Text="Attachment" /></td>
                </tr>
                <tr style="height: 10px"></tr>
                
            </table>
        </div>

        <div style="background-color: #cccaca; width: 800px; min-height: 100px; margin-top: 30px; font-weight: bold; overflow: hidden; font-size: 12px">
            <div style="float: left; width: 50px">Notes</div>
            <div style="float: right; width: 750px;">
                <asp:Repeater runat="server" ID="rptNotes" OnItemDataBound="rptNotes_ItemDataBound">
                    <ItemTemplate>
                        <table>
                            <tr id="row" runat="server">
                                <td>
                                    <asp:HiddenField Value='<%#Eval("ColorId") %>' runat="server" ID="hdfUserID" />
                                </td>
                                <td style="width: 400px">
                                    <asp:LinkButton Text='<%#Eval("Description").ToString().Length>80 ? Eval("Description").ToString().Substring(0,80):Eval("Title").ToString() %>' runat="server" ID="lnkUpdate" CommandArgument='<%#Eval("UserId") +"&"+ Eval("NotesId") %>' OnClick="lnkUpdate_Click"></asp:LinkButton></td>
                                <td>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<%# Convert.ToDateTime(Eval("Created_Date")).ToString("dd-MM-yyyy HH:mm ")%></td>
                                <td>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<%#Eval("EmployeeName") %></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <asp:HiddenField runat="server" ID="hdfNoteId" />
         <asp:HiddenField runat="server" ID="hdfNewNoteId" />
        <asp:HiddenField runat="server" ID="hdfUser" />

    </div>
</asp:Content>
