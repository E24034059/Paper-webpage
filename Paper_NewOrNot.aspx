<%@ Reference Page="~/AuthoringTool/BasicForm/BasicForm.aspx" %>
<%@ Page language="c#" Inherits="PaperSystem.Paper_NewOrNot" CodeFile="Paper_NewOrNot.aspx.cs" %>

<HTML>
	<HEAD runat="server">
		<title>Paper_NewOrNot</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
			
			function goBack()
			{
				/*
				var Back = "";
				if(document.getElementById("hiddenOpener") != null)
				{
					Back = document.getElementById("hiddenOpener").value + ".aspx";
				}
				location.href=Back;
				*/
				//location.href = "./QuestionGroupTree/QuestionGroupTree.aspx"; // modified by dolphin @ 2006-07-29, new Question Group Tree
				location.href = "./QuestionGroupTree/QGroupTree.aspx";
			}
			
			function goNext()
			{
				var strPresentType = "";
				if(document.getElementById("hiddenPresentType") != null)
				{
					strPresentType = document.getElementById("hiddenPresentType").value;
				}
				
				var strQuestionMode = "";
				if(document.getElementById("hiddenQuestionMode") != null)
				{
					strQuestionMode = document.getElementById("hiddenQuestionMode").value;
				}
				
				var strEditMode = "";
				if(document.getElementById("hiddenEditMode") != null)
				{
					strEditMode = document.getElementById("hiddenEditMode").value;
				}
				
				if(strPresentType == "Edit")
				{
					if(strEditMode == "Manual")
					{
						if(document.getElementById("rb1") != null && document.getElementById("rb2") != null)
						{
							if(document.getElementById("rb1").checked == true)
							{
								location.href = "Paper_SelectQuestion.aspx?Opener=Paper_NewOrNot";
							}
							else if(document.getElementById("rb2").checked == true)
							{
								location.href = "./CommonQuestionEdit/Page/ShowQuestion.aspx?Opener=Paper_NewOrNot";
							}
						}
					}
					else
					{
						if(document.getElementById("rb1") != null && document.getElementById("rb2") != null)
						{
							if(document.getElementById("rb1").checked == true)
							{
								location.href = "Paper_RandomSelect.aspx?Opener=Paper_NewOrNot";
							}
							else if(document.getElementById("rb2").checked == true)
							{
								location.href = "./CommonQuestionEdit/Page/ShowQuestion.aspx?Opener=Paper_NewOrNot";
							}
						}
					}
				}
				else
				{
					if(document.getElementById("rb1") != null && document.getElementById("rb2") != null)
					{
						if(document.getElementById("rb1").checked == true)
						{
							location.href = "Paper_RandomSelect.aspx?Opener=Paper_NewOrNot";
						}
						else if(document.getElementById("rb2").checked == true)
						{
							location.href = "./CommonQuestionEdit/Page/ShowQuestion.aspx?Opener=Paper_NewOrNot";
						}
					}
				}
			}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
				<table style="WIDTH: 400px" cellPadding="10" align="center" id="body_content">
					<tr>
						<td class="title">Select a function:</td>
					</tr>
					<tr align="left">
						<td class="subtitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="rb1" type="radio" CHECKED value="rb1" name="rb" runat="server">Select 
							questions
						</td>
					</tr>
					<tr align="left">
						<td class="subtitle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="rb2" type="radio" value="rb2" name="rb" runat="server">Edit 
							questions
						</td>
					</tr>
					<tr align="right">
						<td>
						    <input id="btnPre" style="WIDTH: 150px; HEIGHT: 30px" onclick="goBack();" type="button" class="button_continue"
								value="<< Back" name="btnPre"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<input id="btnNext" style="WIDTH: 150px; HEIGHT: 30px" onclick="goNext();" type="button" class="button_continue"
								value="Next >>" name="btnNext">
						</td>
					</tr>
				</table>
				<input id="hiddenOpener" type="hidden" name="hiddenOpener" runat="server"> <input id="hiddenEditMode" type="hidden" name="hiddenEditMode" runat="server">
				<input id="hiddenQuestionMode" type="hidden" name="hiddenQuestionMode" runat="server">
				<input id="hiddenPresentType" type="hidden" name="hiddenPresentType" runat="server">
		</form>
	</body>
</HTML>
