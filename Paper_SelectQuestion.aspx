<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.Paper_SelectQuestion" CodeFile="Paper_SelectQuestion.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>Paper_SelectQuestion</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
		function showFinishMsg(strSelectionCount , strTextCount)
		{
		//	alert("You have selected " + strSelectionCount + " of selection questions and " + strTextCount + " of normal questions for this group."  );
			if(parent.parent.frmMain != null)
			{
				parent.parent.frmMain.src = "";
				parent.parent.frmMain.document.location.href = "Paper_MainPage.aspx";
			}
			else if(parent.frmMain != null)
			{
				parent.frmMain.src = "";
				parent.frmMain.document.location.href = "Paper_MainPage.aspx";
			}
			else
			{
				frmMain.src = "";
				frmMain.document.location.href = "Paper_MainPage.aspx";
			}
			//parent.parent.frmMain.document.location.href = "Paper_MainPage.aspx";
			//window.open("Paper_MainPage.aspx","frmMain","","");
		}
		
		
		
		function goNext()
		{
			//location.href = "Paper_OtherQuestion.aspx";
			if(document.getElementById("btnSubmit") != null)
			{
				document.getElementById("btnSubmit").click();
			}
		}

             function goBack()
		{
           
            if (document.getElementById("hiddenOpener").value == "./QuestionGroupTree/QuestionGroupTree")
            {
                window.history.back();
		    }


		    else if (document.getElementById("hiddenPreOpener").value == "SelectPaperMode") {
		        if (document.getElementById("hiddenCourseID").value != "")
		        {
		            location.href = "QuestionGroupTree/QGroupTreeNew.aspx?Opener=Paper_QuestionType";
		        }
		        else 
		        {
		            document.getElementById("btnSubmit").click();
		        }
		    }
		    else
		        location.href = "./QuestionGroupTree/QGroupTree.aspx";
		}
		</script>
	</HEAD>
   
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="body_content" style="WIDTH: 100%;" runat="server" align="center">
				<tr id="trFunctionList" style="DISPLAY: none">
					<td id="tcFunctionList">
						<span id="spanDivisionTitle" style="FONT-WEIGHT: bold; FONT-SIZE: 18px; COLOR: blue">
							Division name:</span>&nbsp; <span id="spanDivisionName" style="FONT-WEIGHT: bold; FONT-SIZE: 18px; COLOR: seagreen"
							runat="server"></span>&nbsp;<br>
						<span id="spanGroupTitle" style="FONT-WEIGHT: bold; FONT-SIZE: 18px; COLOR: blue">Group 
							name:</span>&nbsp; <span id="spanGroupName" style="FONT-WEIGHT: bold; FONT-SIZE: 18px; COLOR: seagreen" runat="server">
						</span>&nbsp;
					</td>
				</tr>
				<tr id="trQuestionTable">
					<td id="tcQuestionTable" runat="server" class="title">
					</td>
				</tr>
				 <tr id="trSimQuestionTable" runat="server">
                    <td id="tcSimQuestionTable" runat="server">
                    </td>
                </tr>
				<tr id="trSubmit" style="DISPLAY: none">
					<td id="tcSubmit" runat="server" align="right">
						<input type="image" id="btnSubmit" name="btnSubmit" runat="server" src="../ILView/r-botton/finish.gif"
							style="CURSOR: hand">
					</td>
				</tr>
				<tr>
					<td style="TEXT-ALIGN: right">
						<input class="button_continue" style="WIDTH: 150px; HEIGHT: 30px" type="button" id="btnPre" name="btnPre" value="<< Back"
							onclick="goBack();"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						<input class="button_continue" style="WIDTH: 150px; HEIGHT: 30px" type="button" id="btnNext" name="btnNext" value="Finish >>"
							onclick="goNext();">
					</td>
				</tr>
			</table>
			<input type="hidden" id="hiddenOpener" name="hiddenOpener" runat="server">
			<input id="hiddenQuestionMode" type="hidden" name="hiddenQuestionMode" runat="server">
			<input id="hiddenPreOpener" type="hidden" name="hiddenPreOpener" value="" runat="server">
            <input id="hiddenCourseID" type="hidden" name="hiddenCourseID" value="" runat="server">
		</form>
	</body>
</HTML>
