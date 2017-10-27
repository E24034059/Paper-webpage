<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnsProcess.aspx.cs" Inherits="PaperSystem.AnsProcess" %>

<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<html>
<head id="Head1" runat="server">
    <title>Answer Processing</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <script language="javascript">
    function SettingSuccess()
    {
      alert("答案處理功能設定已完成");
      window.close();
    }
    </script>

</head>
<body id="body_content">
    <form id="Form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table id="Table1" style="width: 100%;" cellspacing="1" cellpadding="1" border="1">
                <tr class="title">
                    <td align="center">
                        <font face="新細明體">答案處理功能設定</font>
                    </td>
                </tr>
                <tr class="header1_table_first_row">
                    <td>
                        作答正確
                    </td>
                </tr>
                <tr class="header1_tr_even_row">
                    <td>
                        <asp:RadioButton ID="rbCorrectState1" runat="server" Text="1." Width="24px" AutoPostBack="True"
                            GroupName="RGCorrect1" OnCheckedChanged="rbCorrectState1_CheckedChanged"></asp:RadioButton>
                        <asp:Label ID="lbCorrectState1" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="header1_tr_odd_row">
                    <td>
                        <asp:RadioButton ID="rbCorrectState2" runat="server" Text="2." Width="24px" AutoPostBack="True"
                            GroupName="RGCorrect1" OnCheckedChanged="rbCorrectState2_CheckedChanged"></asp:RadioButton>
                        <asp:Label ID="lbCorrectState2" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="header1_tr_odd_row">
                    <td>
                        <asp:RadioButton ID="rbCorrectState3" runat="server" Text="3." Width="24px" AutoPostBack="True"
                            GroupName="RGCorrect1" OnCheckedChanged="rbCorrectState3_CheckedChanged"></asp:RadioButton>
                        <asp:Label ID="lbCorrectState3" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="header1_table_first_row">
                    <td>
                        作答錯誤
                    </td>
                </tr>
                <tr class="header1_tr_even_row">
                    <td>
                        <asp:RadioButton ID="rbState1" runat="server" Text="1." Width="24px" AutoPostBack="True"
                            GroupName="RG1" OnCheckedChanged="rbState1_CheckedChanged"></asp:RadioButton>
                        <asp:Label ID="lbState1" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="header1_tr_odd_row">
                    <td>
                        <asp:RadioButton ID="rbState2" runat="server" Text="2." Width="24px" AutoPostBack="True"
                            GroupName="RG1" OnCheckedChanged="rbState2_CheckedChanged"></asp:RadioButton>
                        <asp:Label ID="lbState2" runat="server" Text=""></asp:Label>
                        <asp:DropDownList ID="ddlCountState2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountState2_SelectedIndexChanged">
                            <asp:ListItem Selected="True">1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lbState2Description1" runat="server" Text=""></asp:Label>
                        <asp:RadioButton ID="rbCorrectAnsYes2" runat="server" GroupName="RGCorrectAns2" AutoPostBack="True"
                            OnCheckedChanged="rbCorrectAnsYes2_CheckedChanged"></asp:RadioButton>
                        <asp:Label ID="lbState2Description2" runat="server" Text=""></asp:Label>
                        &nbsp;
                        <asp:RadioButton ID="rbCorrectAnsNo2" runat="server" GroupName="RGCorrectAns2" AutoPostBack="True"
                            OnCheckedChanged="rbCorrectAnsNo2_CheckedChanged"></asp:RadioButton>
                        <asp:Label ID="lbState2Description3" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lbState2Description4" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="header1_tr_even_row">
                    <td>
                        <asp:RadioButton ID="rbState3" runat="server" Text="3." Width="24px" AutoPostBack="True"
                            GroupName="RG1" OnCheckedChanged="rbState3_CheckedChanged"></asp:RadioButton>
                        <asp:Label ID="lbState3" runat="server" Text=""></asp:Label>
                        <asp:DropDownList ID="ddlCountState3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountState3_SelectedIndexChanged">
                            <asp:ListItem Selected="True">1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lbState3Description1" runat="server" Text=""></asp:Label>
                        <asp:RadioButton ID="rbCorrectAnsYes3" runat="server" GroupName="RGCorrectAns3" AutoPostBack="True"
                            OnCheckedChanged="rbCorrectAnsYes3_CheckedChanged"></asp:RadioButton>
                        <asp:Label ID="lbState3Description2" runat="server" Text=""></asp:Label>
                        &nbsp;
                        <asp:RadioButton ID="rbCorrectAnsNo3" runat="server" GroupName="RGCorrectAns3" AutoPostBack="True"
                            OnCheckedChanged="rbCorrectAnsNo3_CheckedChanged"></asp:RadioButton>
                        <asp:Label ID="lbState3Description3" runat="server" Text=""></asp:Label>
                        <asp:Label ID="lbState3Description4" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="header1_tr_odd_row">
                    <td>
                        <asp:RadioButton ID="rbState4" runat="server" Text="4." Width="24px" AutoPostBack="True"
                            GroupName="RG1" OnCheckedChanged="rbState4_CheckedChanged"></asp:RadioButton>
                        <asp:Label ID="lbState4" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr class="header1_tr_even_row">
                    <td>
                        <asp:RadioButton ID="rbState5" runat="server" Text="5." Width="24px" AutoPostBack="True"
                            GroupName="RG1" OnCheckedChanged="rbState5_CheckedChanged"></asp:RadioButton>
                        <asp:Label ID="lbState5" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <table width="99%">
        <tr>
            <td align="right">
                <asp:Button ID="btSave" runat="server" Text="確定" OnClick="btSave_Click" CssClass="button_continue">
                </asp:Button>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
