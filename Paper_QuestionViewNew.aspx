<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>

<%@ Page Language="c#" Inherits="PaperSystem.Paper_QuestionViewNew" CodeFile="Paper_QuestionViewNew.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<html>
<head id="Head1" runat="server">
    <style>
        .span_keyword
        {
            background-color: #bbffbb;
        }
    </style>
    <title>Paper_QuestionView</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <script src="../../../Scripts/jquery-1.11.2.min.js"></script>
    <script language="javascript">
			function ShowbtnDelete()
			{
				//CheckBox被選取後顯示刪除按鈕
				if(document.getElementById("btnDeleteQuestion") != null)
				{
					document.getElementById("btnDeleteQuestion").style.display = "";
				}
			}
			
			function goBack()
			{
			    // modified by dolphin @ 2006-07-29, new Question Group Tree
				//location.href = "./QuestionGroupTree/QuestionGroupTree.aspx";
				//location.href = "./QuestionGroupTree/QGroupTree.aspx";
				location.href = "./QuestionGroupTree/QGroupTreeNew.aspx?Career=";
			}
			
			function goNext()
			{
				//location.href = "Paper_QuestionMain.aspx";
			    location.href = "Paper_QuestionMainNew.aspx?Career=";
			}
			//每個問題的縮放
			function ShowDetail(strQID, strImgID, strAnswerCount)
            {
               var trQuestionTitle = document.getElementById("trQuestionTitle_" + strQID).style.display;
               
               if(trQuestionTitle == "none")
               {
                 document.getElementById("trQuestionTitle_" + strQID).style.display = "";
                 //document.getElementById("trQuestion_" + strQID).style.display = "";            
                
                 document.getElementById("trKeywordTitle_" + strQID).style.display = "";
                 //document.getElementById("trKeyword_" + strQID).style.display = "";
                 document.getElementById("trModify_" + strQID).style.display = "";
                 document.getElementById(strImgID).src = "../../../BasicForm/Image/minus.gif";
                 //document.getElementById("imgSynQuestion_" + strQID).src = "../../../BasicForm/Image/plus.gif"; 
                 
                 var varHaveSynQuestion = Paper_QuestionViewNew.CheckSynQuestion(strQID).value;
                 if(varHaveSynQuestion)
                 {
                   document.getElementById("trSynQuestionTitle_" + strQID).style.display = "";
                   //document.getElementById("trSynQuestion_" + strQID).style.display = "none";
                 }
                              
                 for(var iACount = 1; iACount <= parseInt(strAnswerCount); iACount++ )
                 {
                   document.getElementById("trAnswerTitle_" + strQID + "_" + iACount).style.display = "";
                   //document.getElementById("trAnswer_" + strQID + "_" + iACount).style.display = "";
                   var varHaveSynAnswer = Paper_QuestionViewNew.CheckSynAnswer(strQID, iACount).value;
                   if(varHaveSynAnswer)
                   {
                    document.getElementById("trSynAnswerTitle_" + strQID + "_" + iACount).style.display = "";
                    //document.getElementById("trSynAnswer_" + strQID + "_" + iACount).style.display = "none";          
                    //document.getElementById("imgSynAnswer_" + strQID+ "_" + iACount).src = "../../../BasicForm/Image/plus.gif";
                   }
                 }
                 
               }
               else
               {
                 document.getElementById("trQuestionTitle_" + strQID).style.display = "none";
                 //document.getElementById("trQuestion_" + strQID).style.display = "none";
                 document.getElementById("trSynQuestionTitle_" + strQID).style.display = "none";
                 //document.getElementById("trSynQuestion_" + strQID).style.display = "none";
                 
                 document.getElementById("trKeywordTitle_" + strQID).style.display = "none";
                 //document.getElementById("trKeyword_" + strQID).style.display = "none";
                 document.getElementById("trModify_" + strQID).style.display = "none";
                 document.getElementById(strImgID).src = "../../../BasicForm/Image/plus.gif";
                 
                 for(var iACount = 1; iACount <= parseInt(strAnswerCount); iACount++ )
                 {
                   document.getElementById("trAnswerTitle_" + strQID + "_" + iACount).style.display = "none";
                   //document.getElementById("trAnswer_" + strQID + "_" + iACount).style.display = "none";
                   document.getElementById("trSynAnswerTitle_" + strQID + "_" + iACount).style.display = "none";
                   //document.getElementById("trSynAnswer_" + strQID + "_" + iACount).style.display = "none";
                 }
               }
            }
            //同義問題或答案的縮放
            function ShowSynDetail(strTR, strImgID)
            {
               if(document.getElementById(strTR).style.display == "none")
               {
                 document.getElementById(strTR).style.display = "";
                 document.getElementById(strImgID).src = "../../../BasicForm/Image/minus.gif";
               }
               else
               {
                 document.getElementById(strTR).style.display = "none";
                 document.getElementById(strImgID).src = "../../../BasicForm/Image/plus.gif";
               }
            }
            //每個選擇的縮放
			function ShowSelectionQuestionDetail(strQID, strImgID, strAnswerCount)
            {
               var trModify = document.getElementById("trModify_" + strQID).style.display;
               
               if(trModify == "none")
               {
                 document.getElementById("trModify_" + strQID).style.display = "";
                 document.getElementById(strImgID).src = "../../../BasicForm/Image/minus.gif";
                              
                 for(var iACount = 1; iACount <= parseInt(strAnswerCount); iACount++ )
                 {
                   document.getElementById("trAnswerTitle_" + strQID + "_" + iACount).style.display = "";
                 }
                 
               }
               else
               {      
                 document.getElementById("trModify_" + strQID).style.display = "none";
                 document.getElementById(strImgID).src = "../../../BasicForm/Image/plus.gif";
                 
                 for(var iACount = 1; iACount <= parseInt(strAnswerCount); iACount++ )
                 {
                   document.getElementById("trAnswerTitle_" + strQID + "_" + iACount).style.display = "none";
                 }
               }
            }
            //每個圖形題的縮放
            function ShowSimuQuestionDetail(strQID, strImgID, strAnswerCount) {
               var trModify = document.getElementById("trModify_" + strQID).style.display;

               if (trModify == "none") {
                   document.getElementById("trModify_" + strQID).style.display = "";
                   document.getElementById("trimg_" + strQID).style.display = "";
                   document.getElementById(strImgID).src = "../../../BasicForm/Image/minus.gif";

                   for (var iACount = 1; iACount <= parseInt(strAnswerCount); iACount++) {
                       document.getElementById("trAns_" + strQID + "_" + iACount).style.display = "";
                       document.getElementById("trAnsOrder_" + strQID + "_" + iACount).style.display = "";
                   }

               }
               else {
                   document.getElementById("trModify_" + strQID).style.display = "none";
                   document.getElementById("trimg_" + strQID).style.display = "none";
                   document.getElementById(strImgID).src = "../../../BasicForm/Image/plus.gif";

                   for (var iACount = 1; iACount <= parseInt(strAnswerCount); iACount++) {
                       document.getElementById("trAns_" + strQID + "_" + iACount).style.display = "none";
                       document.getElementById("trAnsOrder_" + strQID + "_" + iACount).style.display = "none";
                   }
               }
            }
            //每個對話題的縮放
			function ShowConversationDetail(strQID, strImgID, strAnswerTypeCount)
            {
               var trConversationTitle = document.getElementById("trConversationTitle_" + strQID).style.display;
               
               if(trConversationTitle == "none")
               {
                 document.getElementById("trConversationTitle_" + strQID).style.display = "";       
                
                 //document.getElementById("trKeywordTitle_" + strQID).style.display = "";
                 document.getElementById("trModify_" + strQID).style.display = "";
                 document.getElementById(strImgID).src = "../../../BasicForm/Image/minus.gif";
                 
                 var varHaveSynQuestion = Paper_QuestionViewNew.CheckSynQuestion(strQID).value;
                 if(varHaveSynQuestion)
                 {
                   document.getElementById("trSynQuestionTitle_" + strQID).style.display = "";
                 }
                              
                 for(var iACount = 1; iACount <= parseInt(strAnswerTypeCount); iACount++ )
                 {
                   document.getElementById("trAnswerTypeTitle_" + strQID + "_" + iACount).style.display = "";
                   var varHaveSynAnswer = Paper_QuestionViewNew.CheckSynAnswer(strQID, iACount).value;
                   if(varHaveSynAnswer)
                   {
                    document.getElementById("trSynAnswerTitle_" + strQID + "_" + iACount).style.display = "";
                   }
                 }
                 
               }
               else
               {
                 document.getElementById("trConversationTitle_" + strQID).style.display = "none";
                // document.getElementById("trSynQuestionTitle_" + strQID).style.display = "none";
                 
                 //document.getElementById("trKeywordTitle_" + strQID).style.display = "none";
                 document.getElementById("trModify_" + strQID).style.display = "none";
                 document.getElementById(strImgID).src = "../../../BasicForm/Image/plus.gif";
                 
                 for(var iACount = 1; iACount <= parseInt(strAnswerTypeCount); iACount++ )
                 {
                   document.getElementById("trAnswerTypeTitle_" + strQID + "_" + iACount).style.display = "none";
                   //document.getElementById("trSynAnswerTitle_" + strQID + "_" + iACount).style.display = "none";
                 }
               }
            }
            function PlayFlash(ItemSeq, FileNameNoVice)
            {
               Player = document.getElementById("FlashPlayer" + ItemSeq + FileNameNoVice); 
               Player.Play();
            }
            
            function setAnswerID(strQID)
			{
			   var selectedRB = "";
               var rbAnswer = document.getElementsByName("rbAnswerGroup_" + strQID)
               var rbAnswerCount = rbAnswer.length;
               for(var i = 0; i < rbAnswerCount; i++)
               {
                 var currentElement = rbAnswer[i];
                 if (currentElement.checked) 
                 {
                   selectedRB = currentElement.value;
                   document.getElementById("hfAnswerID").value = selectedRB.split('|')[2];
                   break;
                 }  
               }
                //document.getElementById("btModify").click();
			}
			function setAnswerTypeID(strQID)
			{
			   var selectedRB = "";
               var rbAnswerType = document.getElementsByName("rbAnswerTypeGroup_" + strQID);
                  alert(rbAnswerType)
               var rbAnswerTypeCount = rbAnswerType.length;
               for(var i = 0; i < rbAnswerTypeCount; i++)
               {
                 var currentElement = rbAnswerType[i];
                 if (currentElement.checked) 
                 {
                   selectedRB = currentElement.value;
                   document.getElementById("hfAnswerTypeID").value = selectedRB.split('|')[2];
                   break;
                 }  
               }
            
			}

			function LatchPreviousPage() {
			    //alert("hi ben");
			    //set current URL to a session to allow next page to redirect back. 
			    $.ajax({
			        url: "Paper_MainPage.aspx/LatchPreviousPage",
			        data: '{PreviousPageURL: "' + window.location.href + '" }',
			        dataType: "json",
			        type: "POST",
			        contentType: "application/json; charset=utf-8",
			        async: false,
			        success: function (result) {


			        }
			    });


			}
			
    </script>

</head>

  
<body>
    <form id="Form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table id="body_content" style="width: 99%;" align="center" runat="server">
                <tr id="trFunctionList">
                    <td id="tcFunctionList" align="center" class="title">
                        <span id="spanFunctionList">Show the questions of <u>
                            <%=strGroupName%></u> </span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbSymptoms" runat="server" Text="Symptoms :  &nbsp;"></asp:Label>
                        <asp:DropDownList ID="ddlSymptoms" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSymptoms_SelectedIndexChanged">
                        </asp:DropDownList>
                        <hr />
                    </td>
                </tr>
                <tr id="trConversationQuestionTable" runat="server">
                    <td id="tcConversationQuestionTable" runat="server">
                    </td>
                </tr>
                <tr id="trQuestionTable" runat="server">
                    <td id="tcQuestionTable" runat="server">
                    </td>
                </tr>
                <tr id="trQuestionWithReasonsTable" runat="server">
                    <td id="tcQuestionWithReasonsTable" runat="server">
                    </td>
                </tr>
                <tr id="trTextQuestionTable" runat="server">
                    <td id="tcTextQuestionTable" runat="server">
                    </td>
                </tr>
                <tr id="trSimQuestionTable" runat="server">
                    <td id="tcSimQuestionTable" runat="server">
                    </td>
                </tr>
                <tr id="trSituationTable" runat="server">
                    <td id="tcSituationTable" runat="server">
                    </td>
                </tr>
                
                <tr style="display: none">
                    <td style="vertical-align: sub; text-align: right">
                        <input id="btnDeleteQuestion" style="display: none; cursor: hand" type="image" name="btnDeleteQuestion"
                            runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <hr />
                        <asp:Button ID="btEditAnswerType" runat="server" Text="Edit answer type" CssClass="button_continue"
                            Height="30px" OnClick="btEditAnswerType_Click" Visible="false" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btAddQuestion" runat="server" Text="Add a new question" CssClass="button_continue"
                            Height="30px"  OnClientClick="LatchPreviousPage()" OnClick="btAddQuestion_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <input id="btnPre" style="width: 80px; height: 30px" onclick="goBack();" type="button"
                            value="<< Back" name="btnPre" class="button_continue">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <input id="btnNext" style="width: 80px; height: 30px" onclick="goNext();" type="button"
                            value="Finish>>" name="btnNext" class="button_continue" runat="server" />
                        <input id="btModify" type="button" value="" class="button_continue" runat="server"
                            style="display: none" />
                    </td>
                </tr>
            </table>
            <div style="position: absolute; top: 400px">
                <input id="hiddenOpener" type="hidden" name="hiddenOpener" runat="server" />
                <input id="hiddenQuestionMode" type="hidden" name="hiddenQuestionMode" runat="server" />
            </div>
            <asp:HiddenField ID="hfSymptoms" runat="server" />
            <asp:HiddenField ID="hfQuestionID" runat="server" />
            <asp:HiddenField ID="hfAnswerID" runat="server" />
            <asp:HiddenField ID="hfAnswerCount" runat="server" />
            <asp:HiddenField ID="hfAnswerTypeID" runat="server" />
            <asp:HiddenField ID="hfAnswerTypeCount" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
