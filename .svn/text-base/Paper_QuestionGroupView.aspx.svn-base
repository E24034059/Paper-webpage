<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Paper_QuestionGroupView.aspx.cs"
    Inherits="AuthoringTool_CaseEditor_Paper_Paper_QuestionGroupView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Question Group View</title>

    <script type="text/javascript">	
    function goBack()
    {
        if (document.getElementById("hfQuestionType").value == "4") {
            var varGroupID = document.getElementById("hfGroupID").value;
            var CurrentProType = document.getElementById("hfCurrentProType").value;
            window.location.href = "Paper_IndexListOfVPAnsSet.aspx?Opener=VPAnsSet&CurrentProType=" + CurrentProType + "&GroupID=" + varGroupID + "";
        }
        else {
            var varGroupID = document.getElementById("hfGroupID").value;
            window.location.href = "Paper_QuestionViewNew.aspx?Opener=Paper_QuestionViewNew&GroupID=" + varGroupID + "";
        }
	}
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="body_content" style="width: 99%;" align="center" runat="server">
            <tr id="trFunctionList">
                <td id="tcFunctionList" align="center" class="title">
                    <span id="spanFunctionList">Question Group View </span>
                </td>
            </tr>
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
            <tr>
                <td id="tcQuestionGroupTable" runat="server" align="center" style="width:100%">
                </td>
            </tr>
            <tr style="width: 100%">
                <td align="right">
                    <hr />
                    <input id="btnBack" style="width: 150px; cursor: pointer; height: 30px" onclick="goBack()"
                        type="button" value="<< Back" name="btnBack" class="button_continue">
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfGroupID" runat="server" />
    <asp:HiddenField ID="hfQuestionType" runat="server" />
    <asp:HiddenField ID="hfCurrentProType" runat="server" />
    </form>
</body>
</html>
