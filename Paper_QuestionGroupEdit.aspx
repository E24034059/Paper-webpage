<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Paper_QuestionGroupEdit.aspx.cs"
    Inherits="AuthoringTool_CaseEditor_Paper_Paper_QuestionGroupEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<style type="text/css">
    input.bigcheck
        {
            height: 25px;
            width: 25px;
            cursor: pointer;
        }
</style>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Question Group Edit</title>

    <script type="text/javascript">	
    function goBack()
    {
	    var varGroupID = document.getElementById("hfGroupID").value;
	    var varQID = document.getElementById("hfQID").value;
	    var CurrentProType = document.getElementById("hfCurrentProType").value;
	    window.location.href = "Paper_QuestionGroupView.aspx?Opener=Paper_QuestionViewNew&GroupID=" + varGroupID + "&QID=" + varQID + "&CurrentProType=" + CurrentProType;
	}
	//每個選擇的縮放
	function ShowSelectionQuestionDetail(strQID, strImgID, strAnswerCount)
    { 
       //取得縮放圖示為展開或縮起
       var ImgLogo = document.getElementById(strImgID).src;
       var varImgLogoPosition = ImgLogo.indexOf("Image/");
       var varImgLogoState = ImgLogo.substr(varImgLogoPosition,16);
       
       if(varImgLogoState == "Image/plus.gif")
       {
         document.getElementById(strImgID).src = "../../../BasicForm/Image/minus.gif";
                      
         for(var iACount = 1; iACount <= parseInt(strAnswerCount); iACount++ )
         {
           document.getElementById("trAnswerTitle_" + strQID + "_" + iACount).style.display = "";
         }
         
       }
       else
       {      
         document.getElementById(strImgID).src = "../../../BasicForm/Image/plus.gif";
         
         for(var iACount = 1; iACount <= parseInt(strAnswerCount); iACount++ )
         {
           document.getElementById("trAnswerTitle_" + strQID + "_" + iACount).style.display = "none";
         }
       }
   }
   //每個圖形題的縮放
   function ShowSimuQuestionDetail(strQID, strImgID, strAnswerCount) {
       //var trModify = document.getElementById("trModify_" + strQID).style.display;
       var trModify = document.getElementById("trimg_" + strQID).style.display;
       if (trModify == "none") {
//           document.getElementById("trModify_" + strQID).style.display = "";
           document.getElementById("trimg_" + strQID).style.display = "";
           document.getElementById(strImgID).src = "../../../BasicForm/Image/minus.gif";

           for (var iACount = 1; iACount <= parseInt(strAnswerCount); iACount++) {
               document.getElementById("trAns_" + strQID + "_" + iACount).style.display = "";
               document.getElementById("trAnsOrder_" + strQID + "_" + iACount).style.display = "";
           }

       }
       else {
//           document.getElementById("trModify_" + strQID).style.display = "none";
           document.getElementById("trimg_" + strQID).style.display = "none";
           document.getElementById(strImgID).src = "../../../BasicForm/Image/plus.gif";

           for (var iACount = 1; iACount <= parseInt(strAnswerCount); iACount++) {
               document.getElementById("trAns_" + strQID + "_" + iACount).style.display = "none";
               document.getElementById("trAnsOrder_" + strQID + "_" + iACount).style.display = "none";
           }
       }
   }
   //每個對話題的縮放
   function ShowConversationDetail(strQID, strImgID)
   {
       var trConversationTitle = document.getElementById("trKeywordTitle_" + strQID).style.display;
       if (trConversationTitle == "none") {
           //document.getElementById("trConversationTitle_" + strQID).style.display = "";
           document.getElementById("trKeywordTitle_" + strQID).style.display = "";
           document.getElementById(strImgID).src = "../../../BasicForm/Image/minus.gif";
       }
       else {
           //document.getElementById("trConversationTitle_" + strQID).style.display = "none";
           document.getElementById("trKeywordTitle_" + strQID).style.display = "none";
           document.getElementById(strImgID).src = "../../../BasicForm/Image/plus.gif";
       }
   }
	//每個問題的縮放
	function ShowDetail(strQID, strImgID, strAnswerCount)
    {
      //取得縮放圖示為展開或縮起
       var ImgLogo = document.getElementById(strImgID).src;
       var varImgLogoPosition = ImgLogo.indexOf("Image/");
       var varImgLogoState = ImgLogo.substr(varImgLogoPosition,16);
      
       if(varImgLogoState == "Image/plus.gif")
       {         
         document.getElementById("trKeywordTitle_" + strQID).style.display = "";
         document.getElementById(strImgID).src = "../../../BasicForm/Image/minus.gif";
         var varHaveSynQuestion = AuthoringTool_CaseEditor_Paper_Paper_QuestionGroupEdit.CheckSynQuestion(strQID).value;
         if(varHaveSynQuestion)
         {
           document.getElementById("trSynQuestionTitle_" + strQID).style.display = "";
         }
                      
         for(var iACount = 1; iACount <= parseInt(strAnswerCount); iACount++ )
         {
           document.getElementById("trAnswerTitle_" + strQID + "_" + iACount).style.display = "";
           var varHaveSynAnswer = AuthoringTool_CaseEditor_Paper_Paper_QuestionGroupEdit.CheckSynAnswer(strQID, iACount).value;
           if(varHaveSynAnswer)
           {
            document.getElementById("trSynAnswerTitle_" + strQID + "_" + iACount).style.display = "";
           }
         }
         
       }
       else
       {
         document.getElementById("trSynQuestionTitle_" + strQID).style.display = "none";
         
         document.getElementById("trKeywordTitle_" + strQID).style.display = "none";
         document.getElementById(strImgID).src = "../../../BasicForm/Image/plus.gif";
         
         for(var iACount = 1; iACount <= parseInt(strAnswerCount); iACount++ )
         {
           document.getElementById("trAnswerTitle_" + strQID + "_" + iACount).style.display = "none";
           document.getElementById("trSynAnswerTitle_" + strQID + "_" + iACount).style.display = "none";
         }
       }
    }
    function SaveQuestionGroup(strQID, strAssignedQID, strSelectionID)
    {
        var varCheckBoxState = document.getElementById("cbQuestionGroup" + strQID).checked
        
        var IsConversation = document.getElementById("hfCurrentProType").value;
        if (IsConversation != "")
            AuthoringTool_CaseEditor_Paper_Paper_QuestionGroupEdit.SaveConversationRelatedQuestion(strQID, strAssignedQID, strSelectionID, varCheckBoxState, document.getElementById('hfIsOriTopic').value, document.getElementById('hfChangedGroupID').value);
        else
            AuthoringTool_CaseEditor_Paper_Paper_QuestionGroupEdit.SaveSelectionRelatedQuestion(strQID, strAssignedQID, strSelectionID, varCheckBoxState);
    }

    function ClosePage() {
        //window.opener.location.reload(true);
        window.opener.location = window.opener.location.href;
        window.close();
    }

    function GoToNewTopicEdit(NewTopic, CaseID, CareerID) {
        window.open('EditTestPaper_VPAns.aspx?GroupID=' + NewTopic + '&Career=' + CareerID + '&CaseID=' + CaseID + '&NewTopic=Yes', 'NewTopic', 'fullscreen=yes, scrollbars=yes, resizable=yes');
        setTimeout(function () { ClosePage(); }, 500);
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <table id="body_content" style="width: 99%; border-collapse: collapse;" align="center" runat="server">
            <tr id="trFunctionList">
                <td id="tcFunctionList" align="center" class="title" colspan="2">
                    <span id="spanFunctionList">Question Group Edit </span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                    <asp:Label ID="lbTitle" runat="server" Text="Step1. 請設定此選項的相關問題：" Font-Size="Larger"></asp:Label>
                    <asp:Label ID="lbSelectionItem" runat="server" Font-Bold="True" ForeColor="Red" Font-Size="Larger"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lbCurrentTopic" runat="server" Text="目前的故事主題：" Visible="False" Font-Size="Larger"></asp:Label>
                    <asp:DropDownList ID="ddlChangeTopic" runat="server" Visible="False" Font-Size="Larger" OnSelectedIndexChanged="ddlChangeTopic_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    <hr />
                </td>
            </tr>
            <tr id="trQuestionTable" runat="server">
                <td id="tcQuestionTable" runat="server" colspan="2">
                </td>
            </tr>
            <tr id="trTextQuestionTable" runat="server">
                <td id="tcTextQuestionTable" runat="server" colspan="2">
                </td>
            </tr>
            <tr id="trConversationQuestionTable" runat="server">
                <td id="tcConversationQuestionTable" runat="server" colspan="2">
                </td>
            </tr>
            <tr id="trSimQuestionTable" runat="server">
                    <td id="tcSimQuestionTable" runat="server" colspan="2">
                    </td>
                </tr>
            <tr style="width: 100%;">
               <td colspan="2" style="line-height: 30px">
                 <hr />
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                     <ContentTemplate>
                  <asp:Label ID="LbDisplayMotionTitle" runat="server" Text="Step2. 請設定此題組的產生時機：" Font-Size="Larger"></asp:Label><br/>
                  <asp:RadioButton ID="rbDisplayBoth"  runat="server" Text="無論作答正確或錯誤皆產生題組，並" Font-Size="Larger" GroupName="DisplayMotion" Checked="true" AutoPostBack="true" OnCheckedChanged="rbDisplayBoth_CheckedChanged" />&nbsp;&nbsp;
                  <asp:RadioButton ID="rbGiveWarningYes"  runat="server" Text="給予" Font-Size="Larger" Checked="true" GroupName="GiveWarning" />&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:RadioButton ID="rbGiveWarningNo"  runat="server" Text="不給予" Font-Size="Larger" GroupName="GiveWarning" />&nbsp;&nbsp;
                  <asp:Label ID="LbDisplayBoth" runat="server" Text="錯誤警告。" Font-Size="Larger"></asp:Label><br/>
                  <asp:RadioButton ID="rbDisplayWhenTrue"  runat="server" Text="僅作答正確才產生題組。" Font-Size="Larger" GroupName="DisplayMotion" AutoPostBack="true" OnCheckedChanged="rbDisplayWhenTrue_CheckedChanged" />
                     </ContentTemplate>
                 </asp:UpdatePanel>
               </td>
            </tr>
            <tr style="width: 100%;">
                <td style="text-align: left; width: 70%; vertical-align: top; padding-right: 0px; margin-right: 0px;">
                    <hr />
                    <asp:Label ID="lbIsReturnTitle" runat="server" Text="Step3. 請設定此題組完成後：" Font-Size="Larger"></asp:Label>
                    <asp:RadioButton ID="rbReturn"  runat="server" Text="返回原始考卷" Font-Size="Larger" GroupName="IsReturn" Checked="true" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbGoToNew" runat="server" Text="不返回繼續作答" Font-Size="Larger" GroupName="IsReturn" />
                </td>
                <td style="text-align: right; width: 30%; vertical-align: top; padding-left: 0px; margin-left: 0px;">
                    <hr />
                    <input id="btnBack" style="width: 150px; cursor: pointer; height: 30px" onclick="goBack()"
                        type="button" value="<< Back" name="btnBack" class="button_continue">
                    &nbsp;&nbsp;    
                     <asp:Button id="btnSave" runat="server" style="width: 150px; height: 30px; cursor: pointer;"
                        Text="Submit" name="btnBack" class="button_continue" OnClick="btnSave_Click"> </asp:Button>   
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hfGroupID" runat="server" />
    <asp:HiddenField ID="hfQID" runat="server" />
    <asp:HiddenField ID="hfAnswerCount" runat="server" />
    <asp:HiddenField ID="hfCurrentProType" runat="server" Value="" />
    <asp:HiddenField ID="hfIsOriTopic" runat="server" Value="1" />
    <asp:HiddenField ID="hfChangedGroupID" runat="server" />
    </form>
</body>
</html>
