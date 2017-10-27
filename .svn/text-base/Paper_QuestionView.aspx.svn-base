<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>

<%@ Page Language="c#" Inherits="PaperSystem.Paper_QuestionView" CodeFile="Paper_QuestionView.aspx.cs" %>

<html>
<head runat="server">
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
				location.href = "./QuestionGroupTree/QGroupTree.aspx";
			}
			
			function goNext()
			{
				location.href = "Paper_QuestionMain.aspx";
			}
			//每個問題的縮放
			function ShowDetail(strQID, strImgID)
            {
               var trQuestionTitle = document.getElementById("trQuestionTitle_" + strQID).style.display;
               if(trQuestionTitle == "none")
               {
                 document.getElementById("trQuestionTitle_" + strQID).style.display = "";
                 document.getElementById("trQuestion_" + strQID).style.display = "";            
                 document.getElementById("trSynQuestionTitle_" + strQID).style.display = "";
                 document.getElementById("trSynQuestion_" + strQID).style.display = "none";
                 document.getElementById("trAnswerTitle_" + strQID).style.display = "";
                 document.getElementById("trAnswer_" + strQID).style.display = "";
                 document.getElementById("trSynAnswerTitle_" + strQID).style.display = "";
                 document.getElementById("trSynAnswer_" + strQID).style.display = "none";
                 document.getElementById("trKeywordTitle_" + strQID).style.display = "";
                 document.getElementById("trKeyword_" + strQID).style.display = "";
                 document.getElementById("trModify_" + strQID).style.display = "";
                 document.getElementById(strImgID).src = "../../../BasicForm/Image/minus.gif";
                 document.getElementById("imgSynQuestion_" + strQID).src = "../../../BasicForm/Image/plus.gif";
                 document.getElementById("imgSynAnswer_" + strQID).src = "../../../BasicForm/Image/plus.gif";
               }
               else
               {
                 document.getElementById("trQuestionTitle_" + strQID).style.display = "none";
                 document.getElementById("trQuestion_" + strQID).style.display = "none";
                 document.getElementById("trSynQuestionTitle_" + strQID).style.display = "none";
                 document.getElementById("trSynQuestion_" + strQID).style.display = "none";
                 document.getElementById("trAnswerTitle_" + strQID).style.display = "none";
                 document.getElementById("trAnswer_" + strQID).style.display = "none";
                 document.getElementById("trSynAnswerTitle_" + strQID).style.display = "none";
                 document.getElementById("trSynAnswer_" + strQID).style.display = "none";
                 document.getElementById("trKeywordTitle_" + strQID).style.display = "none";
                 document.getElementById("trKeyword_" + strQID).style.display = "none";
                 document.getElementById("trModify_" + strQID).style.display = "none";
                 document.getElementById(strImgID).src = "../../../BasicForm/Image/plus.gif";
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
            function PlayFlash(ItemSeq, FileNameNoVice)
            {
               Player = document.getElementById("FlashPlayer" + ItemSeq + FileNameNoVice); 
               Player.Play();
            }
    </script>

</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="body_content" style="width: 100%;" align="center" runat="server">
        <tr id="trFunctionList">
            <td id="tcFunctionList" align="center" class="title">
                <span id="spanFunctionList">Show the questions of
                    <%=strGroupName%>
                </span>
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
        <tr id="trQuestionTable" runat="server">
            <td id="tcQuestionTable" runat="server">
            </td>
        </tr>
        <tr id="trTextQuestionTable" runat="server">
            <td id="tcTextQuestionTable" runat="server">
            </td>
        </tr>
        <tr style="display: none">
            <td style="vertical-align: sub; text-align: right">
                <input id="btnDeleteQuestion" style="display: none; cursor: hand" type="image" name="btnDeleteQuestion"
                    runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right">
                <hr>
                <input id="btnPre" style="width: 80px; height: 30px" onclick="goBack();" type="button"
                    value="<< Back" name="btnPre" class="button_continue">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input id="btnNext" style="width: 80px; height: 30px" onclick="goNext();" type="button"
                    value="Next >>" name="btnNext" class="button_continue">
            </td>
        </tr>
    </table>
    <div style="position: absolute; top: 400px">
        <input id="hiddenOpener" type="hidden" name="hiddenOpener" runat="server">
        <input id="hiddenQuestionMode" type="hidden" name="hiddenQuestionMode" runat="server">
    </div>
    <asp:HiddenField ID="hfSymptoms" runat="server" />
    </form>
</body>
</html>
