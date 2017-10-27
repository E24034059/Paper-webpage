<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="EditRationale.aspx.cs"
    Inherits="AuthoringTool_CaseEditor_Paper_CommonQuestionEdit_Page_EditRationale" %>

<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Rationale</title>

    <script type="text/javascript">
			_editor_url = "/Hints/AuthoringTool/CaseEditor/EditHtml/";
    </script>

    <script type="text/javascript" src="../../../EditHtml/editor.js">
    </script>

    <script language="javascript">
     function EditKeywordRationale()
     {
        var strCaseID = document.getElementById("hfCaseID").value;
        var strQIDandASeq = document.getElementById("hfQIDandASeq").value;
        window.location.href = "../../../../../Learning/Question/qandaReason.aspx?CaseID=" + strCaseID + "&QIDandASeq=" + strQIDandASeq + "&EditMode=Authoring";
     }
    </script>

</head>
<body id="body_content">
    <div align="center">
        <form id="form1" method="post" runat="server">
        <p align="left">
            請填寫Rationale</p>
        <asp:TextBox ID="txtData" Style="display: none" runat="server"></asp:TextBox>
        <asp:HiddenField ID="hfCaseID" runat="server" />
        <asp:HiddenField ID="hfQIDandASeq" runat="server" />
        </form>
        <textarea style="width: 100%; height: 70%" name="txtEdit"></textarea></div>
    <hr />
    <div style="text-align: right;">
        <input id="btEditRationale" type="button" value="Edit Keyword Rationale" class="button_continue"
            onclick="EditKeywordRationale()" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <input onclick="EditData()" type="button" value="submit" id="btn_submit" class="button_continue" />
        &nbsp;&nbsp;
    </div>

    <script language="javascript">
			editor_generate('txtEdit');

			function EditData()	{
				document.all("txtData").value = document.all("txtEdit").value;
				document.form1.submit();
			}
			
			document.all("txtEdit").value = document.all("txtData").value;
    </script>

</body>
</html>
