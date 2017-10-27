<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchQuestionResult.aspx.cs"
    Inherits="AuthoringTool_CaseEditor_Paper_QuestionGroupTree_SearchQuestionResult"
    EnableEventValidation="false" %>

<%@ Reference Page="~/basicform/basicform05.aspx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search Question Result</title>
    <style>
        .span_keyword
        {
            background-color: #bbffbb;
        }
    </style>

    <script language="javascript">
     function CloseWindow()
     {
       window.close();
       //window.opener.location.reload();
     }
     function EditQuestion(cQID, cAID, cGroupID, cQuestionType)
     {
        if(cQuestionType == "1")
            window.opener.parent.location.href = "../CommonQuestionEdit/Page/ShowQuestion.aspx?QID=" + cQID + "&GroupID=" + cGroupID + "";
        else if(cQuestionType == "2")
            window.opener.parent.location.href = "../Paper_TextQuestionEditorNew.aspx?QID=" + cQID + "&AID=" + cAID + "&GroupID=" + cGroupID + "";
        else if (cQuestionType == "4")
            window.opener.parent.location.href = "../Paper_ConversationQuestionEditor.aspx?OpenGroupID=" + document.getElementById("hfOpenGroupID").value + "&QID=" + cQID + "&AnswerType=1&GroupID=" + cGroupID + "&Career=" + document.getElementById("hfCareer").value + "&bModify=True&FromSearch=True";
        else if(cQuestionType == "5")
            window.opener.parent.location.href = "../Paper_EmulationQuestion.aspx?QID=" + cQID + "&GroupID=" + cGroupID + "";
       
        window.close();     
     }
    </script>

</head>
<body id="body_content">
    <form id="form1" runat="server">
    <div>
        <table width="100%" class='title'>
            <tr>
                <td align="center">
                    <asp:Label ID="lbTitle" runat="server" Text="Search questions result" Font-Bold="True"
                        Font-Size="Medium" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
        </table>
        <hr />
        <table width="100%">
            <tr>
                <td>
                    <asp:GridView ID="gvSearchResult" runat="server" CellPadding="3" AllowPaging="True"
                        AutoGenerateColumns="False" Width="100%" BackColor="#DEBA84" BorderColor="#DEBA84"
                        BorderStyle="None" BorderWidth="1px" CellSpacing="2" OnPageIndexChanging="gvSearchResult_PageIndexChanging"
                        PageSize="10" >
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <Columns>
                            <asp:TemplateField HeaderText="題庫群組名稱">
                                <HeaderStyle Width="19%" />
                                <ItemStyle HorizontalAlign="left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbGroupName" runat="server" Text='<%# Bind("cQuestionGroupName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="問題內容">
                                <HeaderStyle Width="33%" />
                                <ItemStyle HorizontalAlign="left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbQuestion" runat="server" Text='<%# Bind("cQuestion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
<%--                            <asp:TemplateField HeaderText="答案內容" >
                                <HeaderStyle Width="30%" />
                                <ItemStyle HorizontalAlign="left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbAnswer" runat="server" Text='<%# Bind("cAnswer") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Keyword">
                                <HeaderStyle Width="20%" />
                                <ItemStyle HorizontalAlign="left" />
                                <ItemTemplate>
                                    <asp:Label ID="lbKeyword" runat="server" Text='<%# Bind("cKeyword") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="問題型態">
                                <HeaderStyle Width="15%" />
                                <ItemStyle HorizontalAlign="center" />
                                <ItemTemplate>
                                    <asp:Label ID="lbQuestionType" runat="server" Text='<%# Bind("cQuestionType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle Width="13%" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Button ID="btEdit" CssClass="button_continue" runat="server" CausesValidation="False"
                                        Text="編輯" CommandArgument='<%# Bind("cQID") %>' OnClick="btEdit_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <hr />
                    <asp:Button ID="btClose" runat="server" Text="關閉視窗" OnClientClick="CloseWindow()"
                        CssClass="button_continue" />
                </td>
            </tr>
        </table>
    </div>
            <asp:HiddenField ID="hfOpenGroupID" runat="server" />
        <asp:HiddenField ID="hfCareer" runat="server" />
    </form>
</body>
</html>
