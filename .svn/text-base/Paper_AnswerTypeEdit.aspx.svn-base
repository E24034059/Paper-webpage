<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Paper_AnswerTypeEdit.aspx.cs"
    Inherits="AuthoringTool_CaseEditor_Paper_Paper_AnswerTypeEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit the answer type</title>

    <script type="text/javascript">	
    function goBack()
    {
        var varGroupID = document.getElementById("hfGroupID").value;
        var varCareer = document.getElementById("hfCareer").value;
		window.location.href = "Paper_QuestionViewNew.aspx?Opener=Paper_QuestionViewNew&GroupID=" + varGroupID + "&Career=" + varCareer;
	}
	function SaveAnswerTypeName()
	{
	  var varAnswerTypeNum = "";
	  varAnswerTypeNum = document.getElementById("hfAnswerTypeNum").value;
	  var bFlag = true;
	  var varAnswerTypeList = "";
	  var varHint = "";
	  for(var i = 1; i <= varAnswerTypeNum; i++ )
	  {
	    if(document.getElementById("tbEditAnswerTypeName_" + i).value == "")
	    {
	      bFlag = false;
	      varHint += "答案型態 " + i + "\n";
	    }
	    else
	    {
	      varAnswerTypeList += document.getElementById("tbEditAnswerTypeName_" + i).value + "|";
	    }
	  }
	  if(bFlag)
	  {
	     var varGroupID = document.getElementById("hfGroupID").value;
	     var varGroupSerialNum = document.getElementById("hfGroupSerialNum").value;
	     AuthoringTool_CaseEditor_Paper_Paper_AnswerTypeEdit.SaveAnswerType(varGroupSerialNum, varAnswerTypeList);
	     var varGroupID = document.getElementById("hfGroupID").value;
	     var varCareer = document.getElementById("hfCareer").value;
		 window.location.href = "Paper_QuestionViewNew.aspx?Opener=Paper_QuestionViewNew&GroupID=" + varGroupID + "&Career=" + varCareer;
	  } 
	  else
	  {
	    varHint += "欄位為空，煩請填寫！";
	    alert(varHint)
	  }
	}
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table id="body_content" style="width: 99%;" align="center" runat="server">
                    <tr id="trFunctionList">
                        <td id="tcFunctionList" align="center" class="title">
                            <span id="spanFunctionList">Edit the answer type</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr />
                            請設定此問題分類的答案型態：
                            <asp:Label ID="lbQuestionClassify" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            <hr />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>
                            請選擇答案型態的數量：&nbsp;
                            <asp:DropDownList ID="ddlAnswerTypeNum" runat="server" AutoPostBack="True" Width="40px"
                                OnSelectedIndexChanged="ddlAnswerTypeNum_SelectedIndexChanged">
                                <asp:ListItem Selected="True">0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                            </asp:DropDownList>
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btAddAnswerType" runat="server" Text="Add a new answer type" Style="cursor: hand"
                                CssClass="button_continue" Width="250px" OnClick="btAddAnswerType_Click" />
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="pSettingAnswerTypeName" runat="server" HorizontalAlign="Center" Width="100%">
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr style="width: 100%">
                        <td align="right">
                            <hr />
                            <input id="btnSave" style="width: 150px; cursor: hand; height: 30px" onclick="SaveAnswerTypeName()"
                                type="button" value="OK" name="btnBack" class="button_continue">
                            &nbsp;&nbsp;
                            <input id="btnBack" style="width: 150px; cursor: hand; height: 30px" onclick="goBack()"
                                type="button" value="Cancel" name="btnBack" class="button_continue">
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfGroupID" runat="server" />
                <asp:HiddenField ID="hfGroupSerialNum" runat="server" />
                <asp:HiddenField ID="hfAnswerTypeNum" runat="server" />
                <asp:HiddenField ID="hfCareer" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
