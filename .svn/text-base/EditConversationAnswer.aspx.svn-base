<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditConversationAnswer.aspx.cs"
    Inherits="AuthoringTool_CaseEditor_Paper_EditConversationAnswer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Conversation Answer Editor</title>
    <style>
        table
        {
            border-collapse: collapse;
            background: #FFFBD6;
            border: 3px solid #686868;
            font: 1.2em/145% 'Trebuchet MS' ,helvetica,arial,verdana;
            color: #333;
        }
        td, tr
        {
            padding: 5px;
        }
        tbody th, tbody td
        {
            border-bottom: dotted 1px #333;
        }
    </style>

    <script language="javascript">
           //alert('111');
        var avW = window.screen.availWidth;

        var avH = window.screen.availHeight;

        window.resizeBy(50,50);

        function SetUrl(url) {
            document.getElementById('hfFileUrl').value = url;
            document.getElementById('tbEditQuestion').value = url;
        }
        function Open() {
            window.open("/hints/AuthoringTool/MultiMediaDB/Upload/CaseFolder.aspx?Type=Image&EditPositation=QuestionDatabase", "_blank");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="hfFileUrl" runat="server" />
        <asp:HiddenField ID="hfQID" runat="server" />
        <asp:HiddenField ID="hfMode" runat="server" />
        <asp:HiddenField ID="hfDataKind" runat="server" />
        <br />
        <table align="center" height="500px" width="95%" rules="all">
            <tr class="title">
                <td colspan="3">
                    <%=strTitle%>
                </td>
            </tr>
            <tr id="trEditTarget" runat="server">
                <td width="250px">
                    Edit Target :
                </td>
                <td colspan="2">
                    <asp:Label ID="lbRecent" runat="server" Font-Bold="True" ForeColor="Red" Font-Size="Larger"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Answer Content Type:
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlAnswerContentType" runat="server" Font-Size="Larger">
                        <asp:ListItem Value="1">簡短的</asp:ListItem>
                        <asp:ListItem Value="2">複雜的</asp:ListItem>
                        <asp:ListItem Value="3">模糊不清的</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="display: none">
                <td>
                    <%=strKind%>
                </td>
                <td id="trDatakind" runat="server" visible="false">
                    <%=strModifyDataKind %>
                </td>
                <td>
                    <asp:DropDownList ID="ddlQuestionKind" runat="server" AutoPostBack="True">
                        <asp:ListItem>English</asp:ListItem>
                        <asp:ListItem>Multimedia</asp:ListItem>
                        <asp:ListItem>Chinese</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <%=strContent%>
                </td>
                <td colspan="2" align="center">
                    <asp:TextBox ID="tbEditQuestion" runat="server" Width="99%" Height="150px" TextMode="MultiLine"
                        Rows="5" Font-Size="Large"></asp:TextBox><br />
                    <asp:Button ID="btnAni" runat="server" Text="Select a Animation" CssClass="button_continue"
                        OnClick="btnAni_Click" Visible="false" />
                </td>
            </tr>
            <tr id="trOriginalAnswer" runat="server" style="display: none">
                <td>
                    <%=strAnswer%>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbEditAnswer" runat="server" Width="80%"></asp:TextBox><br />
                </td>
            </tr>
            <tr style="display: none">
                <td>
                    <%=strDescription%>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="tbEditDescription" runat="server" Width="80%"></asp:TextBox>
                </td>
            </tr>
            <tr style="display: none">
                <td>
                    <%=strKeyword%>
                </td>
                <td id="tdKeyword" runat="server">
                    <asp:TextBox ID="tbKeyword1" runat="server"></asp:TextBox>&nbsp&nbsp<asp:TextBox
                        ID="tbKeyword2" runat="server"></asp:TextBox>&nbsp&nbsp<asp:TextBox ID="tbKeyword3"
                            runat="server"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="tbKeyword4" runat="server"></asp:TextBox>&nbsp&nbsp<asp:TextBox
                        ID="tbKeyword5" runat="server"></asp:TextBox>&nbsp&nbsp<asp:TextBox ID="tbKeyword6"
                            runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Button ID="btnSubmit" runat="server" Text="OK" CssClass="button_continue"
                        OnClick="btnSubmit_Click" Height="50px"  Width="150px" Font-Size="Larger"/>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button_continue"
                        OnClick="btnCancel_Click" Height="50px" Width="150px" Font-Size="Larger"/>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
